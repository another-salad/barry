using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace mysqlefcore {
    public class tempCData {
        public int id { get; set; }
        public DateTime timestamp { get; }
        public double temperatureC { get; set; }
        public string? sensorName { get; set; }
    }

    public class TempSensorDbContext(string DBConnectionString) : DbContext {

        public DbSet<tempCData> tempCData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL(DBConnectionString);
        }

        public void AddTempRecord(double tempC, string sensorName) {
            var record = new tempCData {
                temperatureC = tempC,
                sensorName = sensorName
            };
            using var db = new TempSensorDbContext(DBConnectionString);
            db.tempCData.Add(record);
            db.SaveChanges();
        }

    }

}


