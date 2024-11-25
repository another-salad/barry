using System.Collections.Generic;
using mysqlefcore;

namespace dashboard.Data {
    public class SensorDataService<TController, TData> where TController : SensorControllerBase where TData : class {
        private readonly TController _controller;

        public SensorDataService(TController controller) {
            _controller = controller;
        }

        public async Task<List<TData>> GetAllRecordsInMonthAsync() {
            return await _controller.GetAllRecordsInMonth<TData>();
        }

        public async Task<List<TData>> GetAllRecordsInYearAsync() {
            return await _controller.GetAllRecordsInYear<TData>();
        }

        public async Task<List<TData>> GetLastSevenDaysAsync() {
            return await _controller.GetRecordsLastXDays<TData>();
        }

        public async Task<List<TData>> GetRecordsInRangeAsync(DateTime start, DateTime end) {
            return await _controller.GetRecordsInRange<TData>(start, end);
        }
    }
}
