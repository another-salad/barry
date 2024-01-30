namespace dashboard.Data;

public class TemperatureSensorDataService {
    private readonly mysqlefcore.TempSensorDbContext _context;

    public TemperatureSensorDataService(mysqlefcore.TempSensorDbContext context) {
        _context = context;
    }

    public async Task<List<mysqlefcore.tempCData>> GetAllRecordsInMonthAsync() {
        return await _context.GetAllRecordsInMonth();
    }

    // Expensive (Current year only)
    public async Task<List<mysqlefcore.tempCData>> GetAllRecordsInYearAsync() {
        return await _context.GetAllRecordsInYear();
    }

    public async Task<List<mysqlefcore.tempCData>> GetLastSevenDaysAsync() {
        // Defaults to last 7 days
        return await _context.GetRecordsLastXDays();
    }

    public async Task<List<mysqlefcore.tempCData>> GetRecordsInRangeAsync(DateTime start, DateTime end) {
        return await _context.GetRecordsInRange(start, end);
    }

}
