using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace mysqlefcore {
    public class tempCData {
        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public double temperatureC { get; set; }
        public string? sensorName { get; set; }
    }

    public class TempSensorDbContext(string DBConnectionString) : DbContext {

        public DbSet<tempCData> tempCData { get; set; }

        private readonly int CurrentYear = DateTime.Now.Year;

        private readonly int CurrentMonth = DateTime.Now.Month;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL(DBConnectionString);
        }

        public void AddTempRecord(double tempC, string sensorName) {
            var record = new tempCData {
                temperatureC = tempC,
                sensorName = sensorName
            };
            tempCData.Add(record);
            SaveChanges();
        }

        public async Task<List<tempCData>> GetAllRecords() {
            var records = await tempCData.ToListAsync();
            return records;
        }

        public async Task<List<tempCData>> GetRecordsBySensorName(string sensorName) {
            var records = await tempCData.Where(x => x.sensorName == sensorName).ToListAsync();
            return records;
        }

        private static int ReturnValue(int value, int defaultValue) {
            if (value == -1) {
                return defaultValue;
            }
            return value;
        }

        private int GetYear(int year) {
            return TempSensorDbContext.ReturnValue(year, CurrentYear);
        }

        private int GetMonth(int month) {
            return TempSensorDbContext.ReturnValue(month, CurrentMonth);
        }

        public async Task<List<tempCData>> GetAllRecordsInMonth(int month = -1, int year = -1) {
            var records = await tempCData.Where(x => x.timestamp.Month == GetMonth(month) && x.timestamp.Year == GetYear(year)).ToListAsync();
            return records;
        }

        public async Task<List<tempCData>> GetAllRecordsInYear(int year = -1) {
            var records = await tempCData.Where(x => x.timestamp.Year == GetYear(year)).ToListAsync();
            return records;
        }

    }

}


