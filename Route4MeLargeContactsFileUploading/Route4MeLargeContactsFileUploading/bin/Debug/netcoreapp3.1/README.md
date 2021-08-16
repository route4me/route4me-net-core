## Tool for working with large amount of the address book contacts

### Validate the contacts from large CSV files

Before uploading to the database, the contact should be validated because one contact failure causes omitting of entire contacts chunk - which can contain 200, 500, 1000 contacts.

The validator tested simultaneously for two files containing 30 000 contacts and 60 000 contacts - the process finished almost instantly.

Users can write messages on the console or get a list of the error messages and analyze it programmatically.

1. Place the CSV files in the folder **files**.  
2. List the file names in the file **files\input_files.txt**.  
3. Make appropriate settings in the file **appsettings.json**:   
   - Insert your API key in the section **api_keys**;   
   - Specify the contact mapping in the section **csv_address_mapping**. You can find available for uploading contact fields in the section **contact_fields**;   
   - Make the chunk settings in the section **chank_setting**; 
4. Run the batch file **CsvFileValidate.bat** (or the EXE file **Route4MeLargeContactsFileUploading.exe validate** directly);  

### Geocode the contacts from the large CSV files

User can geocode the addresses contained in the large CSV file.

1. Place the CSV files in the folder **files**.  
2. List the file names in the file **files\input_files.txt**.  
3. Make appropriate settings in the file **appsettings.json**:   
   - Insert your API key in the section **api_keys**;   
   - Specify the contact mapping in the section **csv_address_mapping**. You can find available for uploading contact fields in the section **contact_fields**;   
   - Make the chunk settings in the section **chank_setting**; 
4. To geocode all the addresses in the CSV file run the batch file **CsvFileGeocode.bat** (or the EXE file **Route4MeLargeContactsFileUploading.exe geocode all** directly); 
5. To geocode only the addresses with empty lattitude and longitude fields run the batch file **CsvFileGeocodeOnlyEmpty.bat** (or the EXE file **Route4MeLargeContactsFileUploading.exe geocode only_empty** directly); 

### Upload the contacts from large CSV files
  
1. Place the csv files in the folder **files**.  
2. List the file names in the file **files\input_files.txt**.  
3. Make appropriate settings in the file **appsettings.json**:   
   - Insert your API key in the section **api_keys**;   
   - Specify the contact mapping in the section **csv_address_mapping**. You can find available for uploading contact fields in the section **contact_fields**;   
   - Make the chunk settings in the section **chank_setting**;   
4. Run the batch file **CsvFileUploade.bat** (or the exe file **Route4MeLargeContactsFileUploading.exe** directly);   
  
The uploading process is made based on the API 5 endpoint: <br>  
```https://wh.route4me.com/modules/api/v5.0/address-book``` 
<br> and has some features:  
   - Mandatory fields: Address1, AddressAlias, AddressZip, CachedLat, CachedLng;    
   - Time windows should be specified in the form HH:mm;
   - Service time - in minutes;
   - In case of chunk failure, you get only the start and end addresses of the chunk.   

### Bulk Remove the contacts from the database

 1. Insert your API key in the section **api_keys**;  
 2. Make settings in the file **files\remove_contacts.json**:   
   - Assign to the key **query** string value for removing all the contacts containing the specified text. If the value is specified as "", the option will be disabled;   
   - Assign an array of the contact IDs to the key **address_ids" to remove them. If the value is specified as an empty array, the option will be disabled;
   - Assign appropriate values to the keys: offset and limit to specify the limit of the queried contacts to remove. If these parameters are not specified, all the contacts found by the query will be removed.   
 3. Run the batch file **AbConactsRemoveFromDb.bat** (or the command **Route4MeLargeContactsFileUploading.exe remove** directly).  
 4. You can run CMD command in alternative format:
 Route4MeLargeContactsFileUploading.exe --API_KEY XXXXXX --CSV file_name.csv --COLUMN_NAME col_uid
 
 