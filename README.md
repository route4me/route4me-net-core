# Route4Me Projects Done In .Net Core Framework

The NET Core framework was done by Microsoft, in an effort to include cross-platform support for .NET, including Linux and macOS. We have a separate c# SDK project done in .net 4.6, which is aimed for the Windows platform. In order to use the full strength of the open-source .net core framework, we created c# SDK (.net core) in this repo. Also, here are other projects done on the basis .net core framework.

Currently, we use .net core 3.1. Microsoft already released the stable .net 5 version, which offers everything you expect from the .Net core while making it suitable for Mobile and IoT platform development as well as all the tools for windows desktop and web development. In the future, we intend to merge our two c# SDK projects in one on the basis .net 5.

## .Net Core Projects List

In this repository you can find the c# SDK (.net core 3.1) and some other projects done on the base of c# SDK (.net core 3.1):

### Route4me c# SDK (.net core)   

This SDK is cross-platform. All the used libraries are .net core compatible.  

Find the project [here](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk)  

Read about the project [here](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/README.md).

### Usage Example of the Route4Me C# SDK (.net core)

This sample project demonstrates how to consume the library Route4MeSDKLibrary from the project Route4me c# SDK (.net core). 

Find the project [here](https://github.com/route4me/route4me-net-core/tree/master/TestRoute4MeSharpSDKCore)  

Read about the project [here](https://github.com/route4me/route4me-net-core/blob/master/TestRoute4MeSharpSDKCore/README.md).  

### Route4MeDB

The project Route4MeDb is done on the base of Route4me c# SDK and EFC (Entity Framework Core). It is used for creating/consuming Route4me data in some relational DBMS.

Corrently are supported the DBMS:  
- MS SQL;
- SQL Express;
- PostgreSQL;
- MySql;
- SQLite;
- LocalDB;
- InMemory DB.

You can create DB for Route4Me data in one DB engine and then easily migrate it to another DB engine.

Find the project [here](https://github.com/route4me/route4me-net-core/tree/master/Route4MeDB)  

Read about the project [here](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/README.md).

### Usage Example of the Route4MeDbLibrary package

This sample project demonstrates how to consume the library Route4MeDbLibrary from the project Route4MeDB.

Find the project [here](https://github.com/route4me/route4me-net-core/tree/master/Route4MeDbExample)  

Read about the project [here](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDbExample/Route4MeDbExample/README.md).  

### Tool for working with large amount of the address book contacts

The project Route4MeLargeContactsFileUploading is done for consuming large CSV files with address book contacts.

With this tool you can:
- Validate the contacts from large CSV files;
- Geocode the contacts from the large CSV files;
- Upload the contacts from large CSV files;
- Bulk Remove the contacts from the database.

The tool is done as a console application - thus, you can insert it in the system schedule for uploading/removing the scheduled contacts automatically.

Find the project [here](https://github.com/route4me/route4me-net-core/tree/master/Route4MeLargeContactsFileUploading)  

Read about the project [here](https://github.com/route4me/route4me-net-core/blob/master/Route4MeLargeContactsFileUploading/Route4MeLargeContactsFileUploading/README.md).  
