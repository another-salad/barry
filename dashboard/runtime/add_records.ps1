param(
    [string]$sensorHost,
    [string]$dbHost,
    [string]$dbName
)

Import-Module ./helpers.psm1
# Expects the build artifacts from dbApp to be in the same DIR
Import-Module ./dbApp.dll


$dbConnectionString = Create-DBConnectionString $dbHost $dbName
$dbContext=[mysqlefcore.SensorDBContext]::Create($dbConnectionString)
$SensorController=[mysqlefcore.TempSensorController]::New($dbContext)
$allSensors = Get-TempData $sensorHost
foreach ($sensor in $allSensors.Keys) {
    $SensorController.AddTempRecord($allSensors.$sensor, $sensor)
}
