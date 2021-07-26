## Tool for working with large amount of the address book contacts

### Upload CSV file with contacts to database
  
1. Place the csv files in the folder **files**.  
2. List the file names in the file **files\input_files.txt**.  
3. Make appropriate settings in the file **appsettings.json**:   
   - Insert your API key in the section **api_keys**;   
   - Specify the contact mapping in the section **csv_address_mapping**. You can find available for uploading contact fields in the section **contact_fields**;   
   - Make the chunk settings in the section **chank_setting**;   
4. Run the batch file **CsvFileUploading.bat** (or the exe file **CsvFileUploading.exe** directly);   
  
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
 3. Run the batch file **AbConactsRemoveFromDb.bat** (or the command **CsvFileUploading.exe remove** directly).
