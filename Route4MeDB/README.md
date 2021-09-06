# Route4MeDbLibrary - Route4Me Database Tool


### Solution Route4MeDb - Description  

The solution Route4MeDb is done on the base of Route4me c# SDK and EFC (Entity Framework Core). It's used for creating/consuming Route4me data in some relational DBMS.

Currently are supported the DBMS:  
- MS SQL;
- SQL Express;
- PostgreSQL;
- MySql;
- SQLite;
- LocalDB;
- InMemory DB.

You can create DB for Route4Me data in one DB engine and easily migrate it to another DB engine.

The solution contains the projects:

- **ApplicationCore**: Route4me database entities (AddressBookContact, Geocoding, Order, Address, Route), specifications, interfaces;  
- **Infrastructure**: entity builders, repositories, identity;
- **DatabaseMirgation**: a utility for creating database structure by selecting DB provider;
- **UnitTests**: the unit tests for the project Route4MeDbLibrary;
- **IntegrationTests** - the integration tests for the project Route4MeDbLibrary; 
- **FunctionalTests** - the functional tests for the project Route4MeDbLibrary; 
- **Route4MeDbLibrary** - the output compiled library. With later uploading on the NuGet central repository of the packages;

### Usage Examples 
The typical workflow of the library usage is:  
- Create .net core (version >= 3.1) project; 
- install NuGet package **Route4MeDbLibrary** (for more complex project install also package **Route4MeSDKLibrary**);
- Create file **appSettings.json** in the project core folder with content:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Route4MeDB;Trusted_Connection=True;",
    "SqlExpressConnection": "Server=.\\SQLEXPRESS;Database=Route4MeDB;Trusted_Connection=True;MultipleActiveResultSets=true",
    "MsSqlConnection": "Server=ServerAddress;Database=Route4MeDB;Integrated Security=True;Asynchronous Processing=True;",
    "SQLiteConnection": "Filename=c:\\SqLite_DB\\Route4MeDB.db",
    "PostgreSqlConnection": "Host=my_host;Database=Route4MeDB;Username=docker;Password=;Pooling=true;",
    "MySqlConnection": "server=localhost;port=3306;database=Route4MeDB;uid=root;password=***"
  }
}
``` 
Of course, appropriate DB providers must be installed.  
The DB provider **InMemory** does not require installation.  
The DB provider **SQLite** does not require installation - only the path to a SQLite file.  

Set the properties of the file **appSettings.json** as:  
- Build Action: Content  
- Copy to Output Directory: Copy if Newer  

You can find the class **RunExamples** in the provided example project **Route4MeDbExample**, where is demonstrated how to:  
- Create DB;  
- Create address book contact;  
- Copy the content of a JSON response file (with retrieved/created/updated route) to the DB;  
- Create an optimization (using Route4MeSDKLibrary) and save it to DB;  
- Export order entity from DB to Route4Me database;  

You can do the same things for other entities: address book contact, order, route, optimization, address, address note.

