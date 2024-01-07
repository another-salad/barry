namespace dashboard.Data;

public class TemperatureSensorDataService {
    private readonly mysqlefcore.TempSensorDbContext _context;

    public TemperatureSensorDataService(mysqlefcore.TempSensorDbContext context) {
        _context = context;
    }

    public async Task<List<mysqlefcore.tempCData>> GetAllRecordsInMonthAsync()
    {
        return await _context.GetAllRecordsInMonth();
    }
}
