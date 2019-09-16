@ECHO OFF

curl -F files[]="@r4me_scripts.sql" -k "https://www.rebasedata.com/api/v1/convert?outputFormat=mysql&errorResponse=zip" -o output.zip

timeout /t 30