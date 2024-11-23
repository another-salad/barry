using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mysqlefcore {

    public class tempCData {
        public int id { get; private set; }
        public DateTime timestamp { get; set; }
        public double temperatureC { get; set; }
        public string? sensorName { get; set; }
    }

    // Cased correctly, I am not going to deal with a db table rename for above....
    public class HumidityData {
        public int Id { get; private set; }
        public DateTime Timestamp { get; set; }
        public double Humidity { get; set; }
        public string? SensorName { get; set; }
    }

    public class SensorDBContext : DbContext {

        public SensorDBContext(DbContextOptions<SensorDBContext> options) : base(options) {
            Database.EnsureCreated();
            tempCData = Set<tempCData>();
        }

        private string? _DBConnectionString { get; set; }

        public DbSet<tempCData> tempCData { get; set; }

        public static SensorDBContext Create(string connectionString) {
            return new SensorDBContext(
                new DbContextOptionsBuilder<SensorDBContext>().UseMySQL(connectionString).Options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured && _DBConnectionString != null) {
                optionsBuilder.UseMySQL(_DBConnectionString);
            }
        }

    }

    public class SensorControllerBase(SensorDBContext dbContext) {

        protected readonly int CurrentYear = DateTime.Now.Year;

        protected readonly int CurrentMonth = DateTime.Now.Month;

        protected SensorDBContext _dbContext = dbContext;

        private static int ReturnValue(int value, int defaultValue) {
            if (value == -1) {
                return defaultValue;
            }
            return value;
        }

        internal int GetYear(int year) {
            return ReturnValue(year, CurrentYear);
        }

        internal int GetMonth(int month) {
            return ReturnValue(month, CurrentMonth);
        }

        public async Task<List<T>> GetAllRecords<T>() where T : class {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetRecordsBySensorName<T>(string sensorName) where T : class {
            return await _dbContext.Set<T>().Where(x => EF.Property<string>(x, "sensorName") == sensorName).ToListAsync();
        }

        public async Task<List<T>> GetAllRecordsInMonth<T>(int month = -1, int year = -1) where T : class {
            return await _dbContext.Set<T>()
                .Where(x => EF.Property<DateTime>(x, "timestamp").Month == GetMonth(month) && EF.Property<DateTime>(x, "timestamp").Year == GetYear(year))
                .ToListAsync();
        }

        public async Task<List<T>> GetAllRecordsInYear<T>(int year = -1) where T : class {
            return await _dbContext.Set<T>().Where(x => EF.Property<DateTime>(x, "timestamp").Year == GetYear(year)).ToListAsync();
        }

        public async Task<List<T>> GetRecordsLastXDays<T>(int days = -7) where T : class {
            // Defaults to the last 7 days
            return await _dbContext.Set<T>().Where(x => EF.Property<DateTime>(x, "timestamp") > DateTime.Now.AddDays(days)).ToListAsync();
        }

        public async Task<List<T>> GetRecordsInRange<T>(DateTime start, DateTime end) where T : class {
            end = end.AddDays(1);  // Add a day to the end date to include it in the range
            return await _dbContext.Set<T>()
                .Where(x => EF.Property<DateTime>(x, "timestamp") >= start && EF.Property<DateTime>(x, "timestamp") <= end)
                .ToListAsync();
        }

    }

    public class TempSensorController : SensorControllerBase {

        public TempSensorController(SensorDBContext dbContext) : base(dbContext){
            _dbContext = dbContext;
        }
        public void AddTempRecord(double tempC, string sensorName) {
            var record = new tempCData {
                temperatureC = tempC,
                sensorName = sensorName,
                timestamp = DateTime.Now
            };
            _dbContext.Add(record);
            _dbContext.SaveChanges();
        }

    }

}
