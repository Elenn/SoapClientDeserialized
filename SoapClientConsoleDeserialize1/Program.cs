using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;

namespace SoapClientConsoleDeserialize1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Адрес SOAP-эндпоинта
            string url = "https://apps.learnwebservices.com/services/hello";

            // SOAP-запрос (метод SayHello)
            string soapXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <soap:Body>
                        <HelloRequest xmlns=""http://learnwebservices.com/services/hello"">
                            <Name>John Doe</Name>
                        </HelloRequest>
                    </soap:Body>
                </soap:Envelope>";

            string innerNode = "HelloResponse";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("SOAPAction", ""); // Для этого сервиса — пустая строка

            var content = new StringContent(soapXml, Encoding.UTF8, "text/xml");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Запрос успешно выполнен.");
                    Console.WriteLine("Ответ сервера (XML):");
                    Console.WriteLine(responseBody);

                    // Попытка десериализовать ответ через SoapDeserializer
                    try
                    {
                        var helloResponse = SoapDeserializer.DeserializeSoapResponse(responseBody, innerNode);
                        if (helloResponse != null && !string.IsNullOrEmpty(helloResponse.Message))
                        {
                            Console.WriteLine($"\n👉 Приветствие (deserialized): {helloResponse.Message}");
                        } 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n⚠️ Десериализация не удалась: {ex.Message}"); 
                        Console.WriteLine("❌ Не удалось найти элемент Message в ответе."); 
                    }
                }
                else
                {
                    Console.WriteLine($"❌ Ошибка: {response.StatusCode}");
                    Console.WriteLine(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Исключение: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    } 
}