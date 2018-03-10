# ECB_API

1. Download the project file and unzip it.

2. Using Visual Studio to open the project.

3. Open Package Manager Console (Click Tools -> NuGet Package Manager -> Package Manager Console)

4. Enter 2 commands "add-migration Initial" and "update-database" in Package Manager Console.
      (These commands create a local database and dump the data from ECB into the local database automatically)

5. Run the project from Visual Studio.(Make sure using Microsoft Edge because Google Chrome may have some style format issues and Internet Explorer may ask you to download the json file)

6. For testing: add "/rates/lastest" to the URL to get the latest record. Add "/rates/YYYY-mm-dd" to get the record based on specific date.Add "/rates/analyze" to get the minmum,maxmum and average value. All sorted by Currency.
