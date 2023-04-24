using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
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

            _db.CreateTableAsync<PulseCommonInformationRecord>().GetAwaiter().GetResult();
            _db.CreateTableAsync<BaevskyIndicatorsRecord>().GetAwaiter().GetResult();
            _db.CreateTableAsync<PulseStatistic>().GetAwaiter().GetResult();

            _db.CreateTableAsync<SaturationCommonInformationRecord>().GetAwaiter().GetResult();
            _db.CreateTableAsync<SaturationStatistic>().GetAwaiter().GetResult();
        }

        public Task ClearPulseStatisticTable()
        {
            return Task.WhenAll(_db.DeleteAllAsync<PulseCommonInformationRecord>(), _db.DeleteAllAsync<BaevskyIndicatorsRecord>(), _db.DeleteAllAsync<PulseStatistic>());
        }

        public Task ClearSaturationStatisticTable()
        {
            return Task.WhenAll(_db.DeleteAllAsync<SaturationCommonInformationRecord>(), _db.DeleteAllAsync<SaturationStatistic>());
        }

        #region PulseCommonInformationRecord
        public Task<int> SavePulseCommonInformationRecordAsync(PulseCommonInformationRecord pulseCommonInformationRecord)
        {
            return pulseCommonInformationRecord.ID != 0 ? _db.UpdateAsync(pulseCommonInformationRecord) : _db.InsertAsync(pulseCommonInformationRecord);
        }

        public Task<int> DeletePulseCommonInformationRecordAsync(PulseCommonInformationRecord pulseCommonInformationRecord)
        {
            return _db.DeleteAsync(pulseCommonInformationRecord);
        }
        #endregion

        #region BaevskyIndicatorsRecord
        public Task<int> SaveBaevskyIndicatorsRecordAsync(BaevskyIndicatorsRecord baevskyIndicators)
        {
            return baevskyIndicators.ID != 0 ? _db.UpdateAsync(baevskyIndicators) : _db.InsertAsync(baevskyIndicators);
        }

        public Task<int> DeleteBaevskyIndicatorsRecordAsync(BaevskyIndicatorsRecord baevskyIndicators)
        {
            return _db.DeleteAsync(baevskyIndicators);
        }
        #endregion

        #region SaturationCommonInformationRecord
        public Task<int> SaveSaturationCommonInformationRecordAsync(SaturationCommonInformationRecord saturationCommonInformationRecord)
        {
            return saturationCommonInformationRecord.ID != 0 ? _db.UpdateAsync(saturationCommonInformationRecord) : _db.InsertAsync(saturationCommonInformationRecord);
        }

        public Task<int> DeleteSaturationCommonInformationRecordAsync(SaturationCommonInformationRecord saturationCommonInformationRecord)
        {
            return _db.DeleteAsync(saturationCommonInformationRecord);
        }
        #endregion

        #region PulseRecord
        public Task<List<PulseStatistic>> GetPulseStatisticsAsync()
        {
            return _db.GetAllWithChildrenAsync<PulseStatistic>();
        }

        public Task<PulseStatistic> GetPulseStatisticAsync(int id)
        {
            return _db.Table<PulseStatistic>().Where(el => el.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertPulseStatisticAsync(PulseStatistic pulseStatistic)
        {
            return _db.InsertAsync(pulseStatistic);
        }

        public Task UpdatePulseStatisticAsync(PulseStatistic pulseStatistic)
        {
            return _db.UpdateWithChildrenAsync(pulseStatistic);
        }

        public Task<int> DeletePulseStatisticAsync(PulseStatistic pulseStatistic)
        {
            return _db.DeleteAsync(pulseStatistic);
        }
        #endregion

        #region SaturationStatistic
        public Task<List<SaturationStatistic>> GetSaturationStatisticsAsync()
        {
            return _db.GetAllWithChildrenAsync<SaturationStatistic>();
        }

        public Task<SaturationStatistic> GetSaturationStatisticAsync(int id)
        {
            return _db.Table<SaturationStatistic>().Where(el => el.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertSaturationStatisticsync(SaturationStatistic saturationStatistic)
        {
            return _db.InsertAsync(saturationStatistic);
        }

        public Task UpdateSaturationStatisticAsync(SaturationStatistic saturationStatistic)
        {
            return _db.UpdateWithChildrenAsync(saturationStatistic);
        }

        public Task<int> DeleteSaturationStatisticAsync(SaturationStatistic saturationStatistic)
        {
            return _db.DeleteAsync(saturationStatistic);
        }
        #endregion
    }
}
