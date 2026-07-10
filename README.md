# SoapClientDeserialized

*********************************************************************
soap c# how to get .xsd
*********************************************************************
1. wsdl: беру часть между <wsdl:types> и сохраняю с расширением .xsd

https://apps.learnwebservices.com/services/hello?WSDL

--------------------------------------------------
2. Добавляю строчку - <?xml version="1.0" encoding="UTF-8"?>

servicesHello.xsd

```
<?xml version="1.0" encoding="UTF-8"?>
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns="http://learnwebservices.com/services/hello" attributeFormDefault="unqualified" elementFormDefault="qualified" targetNamespace="http://learnwebservices.com/services/hello" version="1.0">
	<xs:complexType name="helloRequest">
		<xs:sequence>
			<xs:element name="Name" type="xs:string"/>
		</xs:sequence>
	</xs:complexType>
	<xs:complexType name="helloResponse">
		<xs:sequence>
			<xs:element name="Message" type="xs:string"/>
		</xs:sequence>
	</xs:complexType>
	<xs:element name="HelloRequest" nillable="true" type="helloRequest"/>
	<xs:element name="HelloResponse" nillable="true" type="helloResponse"/>
</xs:schema>
```
-----------------------------------------------------------
3. 
Создаю классы из servicesHello.xsd
в XmlRootAttribute есть название атрибута "HelloResponse"

в start menu впечатать Developer Command Prompt

cd C:\web\soap\c#\SoapClientConsoleDeserialize1 

xsd.exe servicesHello.xsd /classes /language:CS

Microsoft (R) Xml Schemas/DataTypes support utility
[Microsoft (R) .NET Framework, Version 4.8.3928.0]
Copyright (C) Microsoft Corporation. All rights reserved.
Writing file 'C:\web\soap\c#\SoapClientConsoleDeserialize1\servicesHello.cs'

C:\web\soap\c#\SoapClientConsoleDeserialize1\servicesHello.cs
 