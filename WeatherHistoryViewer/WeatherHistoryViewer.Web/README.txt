WeatherHistoryViewer
The database is created automatically on first start, and seeded with data from the .csv file. That happens from program.cs.
To enable SQL logging put the following in the log section of appsettings.json:
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"

