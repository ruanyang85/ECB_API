# ECB_API

1. Download the project file and unzip it.

2. Using Visual Studio to open the project.

3. Open Package Manager Console (Click Tools -> NuGet Package Manager -> Package Manager Console) and you will see a warning to restore the NuGet Package.Click the "restore" button to restore the NuGet Package. (This package is used for database)

4. I have already attached an empty database in App_Data folder. If you want to test, you can open query for database and check whether it is empty or not. 

5. Run the project from Visual Studio and at same time it will dump the data from ECB into the local database. Every time run the code, the database will update based on the latest rate from ECB. (Make sure using Microsoft Edge because Google Chrome may have some style format issues and Internet Explorer may ask you to download the json file)

6. For testing: add "/rates/lastest" to the URL to get the latest record. Add "/rates/YYYY-mm-dd" to get the record based on specific date.Add "/rates/analyze" to get the minmum,maxmum and average value. All sorted by Currency.


I have tried 4 times and all work fine.If you face any issues please let me know anytime.
