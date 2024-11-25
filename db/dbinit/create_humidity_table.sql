use tempdatadb;

CREATE TABLE humidityData (
    id INT NOT NULL AUTO_INCREMENT,
    humidity FLOAT NOT NULL,
    sensorName VARCHAR(255) NOT NULL,
    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
);
