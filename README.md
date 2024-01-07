# barry

Contains:
- C# A Entity Framework wrapper for MySQL `dashboard/dbApp`
- Some powershell 7 functions/scripts for interacting with the MySQL C# wrapper `dashboard/runtime`
- A Blazor Plotly app for displaying Dashboards `dashboard/dashboard`


## Powershell 7 REPL experience with dashboard/dbApp
build to produce dbApp.dll and CD into the build output DIR
```powershell
import-module .\dbApp.dll
# The db string can come from anywhere you like, a local powershell vault, docker secrets (see the dashboard/runtime/helpers.psm1)
$dbstring = 'server=;database=tempdatadb;user=;password='
$x=[mysqlefcore.TempSensorDbContext]::Create($dbstring)
$x.GetAllRecordsInMonth().Result
id timestamp           temperatureC sensorName
-- ---------           ------------ ----------
15 06/01/2024 18:24:19        19.89 roomOfPain
16 06/01/2024 18:24:20        23.75 roomOfHorror
17 06/01/2024 18:24:20        11.25 roomOfEggs
```

`dashboard/runtime/add_records.ps1` is a powershell script which can be used to add new records to the DB.  
I schedule invoking this docker-compose file `dashboard/docker-compose.yml` with cron, but you do you.

## Plotly Dashboard
`dashboard/dashboard-docker-compose.yml` is how I am building and running the dashboards.  
This will rely on an `appsettings.json` file existing in `dashboard/dashboard` to connect to the MySQL DB, it should contain the keys: `{"ConnectionStrings" {"TempSensorDb": ""}`.  

I have dashboards that look like the below: 

![image](https://github.com/another-salad/barry/assets/48966874/3650c9f2-9aba-4d43-b1c4-1d233b4c4558)
