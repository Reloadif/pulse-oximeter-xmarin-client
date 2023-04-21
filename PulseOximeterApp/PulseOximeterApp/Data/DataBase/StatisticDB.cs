using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PulseOximeterApp.Data.DataBase
{
    public class StatisticDB
    {
        private readonly SQLiteAsyncConnection _db;

        public int PulseRecordCount => _db.Table<PulseStatistic>().CountAsync().GetAwaiter().GetResult();
        public int SaturationRecordCount => _db.Table<SaturationStatistic>().CountAsync().GetAwaiter().GetResult();

        public StatisticDB(string connectionString)
        {
            _db = new SQLiteAsyncConnection(connectionString);

            _db.CreateTableAsync<PulseStatistic>().GetAwaiter().GetResult();
            _db.CreateTableAsync<SaturationStatistic>().GetAwaiter().GetResult();
        }

        public Task<int> ClearPulseStatisticTable()
        {
            return _db.DeleteAllAsync<PulseStatistic>();
        }

        public Task<int> ClearSaturationStatisticTable()
        {
            return _db.DeleteAllAsync<SaturationStatistic>();
        }

        #region PulseRecord
        public Task<List<PulseStatistic>> GetPulseStatisticsAsync()
        {
            return _db.Table<PulseStatistic>().ToListAsync();
        }

        public Task<PulseStatistic> GetPulseStatisticAsync(int id)
        {
            return _db.Table<PulseStatistic>().Where(el => el.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SavePulseStatisticAsync(PulseStatistic pulseStatistic)
        {
            return pulseStatistic.ID != 0 ? _db.UpdateAsync(pulseStatistic) : _db.InsertAsync(pulseStatistic);
        }

        public Task<int> DeletePulseStatisticAsync(PulseStatistic pulseStatistic)
        {
            return _db.DeleteAsync(pulseStatistic);
        }
        #endregion

        #region SaturationStatistic
        public Task<List<SaturationStatistic>> GetSaturationStatisticsAsync()
        {
            return _db.Table<SaturationStatistic>().ToListAsync();
        }

        public Task<SaturationStatistic> GetSaturationStatisticAsync(int id)
        {
            return _db.Table<SaturationStatistic>().Where(el => el.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveSaturationStatisticAsync(SaturationStatistic saturationStatistic)
        {
            return saturationStatistic.ID != 0 ? _db.UpdateAsync(saturationStatistic) : _db.InsertAsync(saturationStatistic);
        }

        public Task<int> DeleteSaturationStatisticAsync(SaturationStatistic saturationStatistic)
        {
            return _db.DeleteAsync(saturationStatistic);
        }
        #endregion
    }
}
