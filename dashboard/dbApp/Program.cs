﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace mysqlefcore
{
    public class tempCData
    {
        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public double temperatureC { get; set; }
        public string sensorName { get; set; }
    }

    public class TempSensorDbContext : DbContext
    {
        public DbSet<tempCData> tempCData { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("");
        }
    }
    public class ExampleTempOutput
    {
        public static void Main(string[] args)
        {
            using (var db = new TempSensorDbContext())
            {
                var count = db.tempCData.Count();
                Console.WriteLine($"There are {count} records in the database.");
                var records = db.tempCData.OrderByDescending(r => r.timestamp).Take(10);
                foreach (var record in records)
                {
                    Console.WriteLine($"{record.timestamp} - {record.temperatureC}C - {record.sensorName}");
                }
            }
        }
    }

}


