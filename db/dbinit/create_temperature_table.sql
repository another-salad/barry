use tempdatadb;

CREATE TABLE tempCData (
    id INT NOT NULL AUTO_INCREMENT,
    temperatureC FLOAT NOT NULL,
    sensorName VARCHAR(255) NOT NULL,
    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
);