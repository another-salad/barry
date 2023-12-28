
function Read-DockerSecrets {
    [CmdletBinding()]
    $secretsWithValues = @{}
    foreach ($secret in Get-ChildItem /run/secrets/) {
        $secretsWithValues[$secret.Name] = (Get-Content -Raw $secret).Trim()
    }
    $secretsWithValues
}

# This is a specific for my setup, may not be fit for use more broadly.
# I have an internal webpage which has links to all of my available temp sensors (on the local network).
# This will expect the below workflow:
# First request to the 'sensorHost' will return:
# {"sensorName": "http://local.sensor.address", "sensorName2": "http://local.sensor2.address"}
# We will then iterate through each of these sensors, and expect a json response like the below from each:
# {"loc": "SomePlace", "temp": 12.75} (Assumes Celsius, yes this could be nicer but here we are, past me did bad things.)
function Get-TempData {
    [CmdletBinding()]
    param (
        [string]$sensorHost
    )
    $headers = @{"Content-Type" = "application/JSON; charset=UTF-8"}
    $availableSensors = Invoke-RestMethod -Uri $sensorHost -headers $headers
    $tempData = @{}
    foreach ($sensor in $availableSensors.PSObject.Properties) {
        $tempData[$sensor.Name] = (Invoke-RestMethod -Uri $sensor.value -headers $headers).temp
    }
    $tempData
}

function Create-DBConnectionString {
    [CmdletBinding()]
    param (
        [string]$dbHost,
        [string]$dbName
    )
    # Secret names are set in the docker-compose.yml file.
    $dbSecrets = Read-DockerSecrets
    $dbConnectionString = "server=$($dbHost);database=$($dbName);user=$($dbSecrets.dbUser);password=$($dbSecrets.dbPassword);"
    $dbConnectionString
}