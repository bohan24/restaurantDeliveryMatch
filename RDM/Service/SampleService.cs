using Dapper;

namespace RDM.Service
{
    public class SampleService
    {

        private Database _dataBase;
        public SampleService(IServiceProvider service)
        {
            _dataBase = service.GetService<Database>();
        }
        public async Task<dynamic> GetDataSample()
        {
            string sql = @"";
            DynamicParameters parameters = new DynamicParameters();
            var result = await _dataBase.MysqlQueryAsync(sql, parameters);
            return result;
        }
    }
}
