param(
    [string]$sensorHost,
    [string]$dbHost,
    [string]$dbName
)

Import-Module ./helpers.psm1
# Expects the build artifacts from dbApp to be in the same DIR
Import-Module ./dbApp.dll


$dbConnectionString = Create-DBConnectionString $dbHost $dbName
$db=[mysqlefcore.TempSensorDbContext]::new($dbConnectionString)
$allSensors = Get-TempData $sensorHost
foreach ($sensor in $allSensors.Keys) {
    $db.AddTempRecord($allSensors.$sensor, $sensor)
}
