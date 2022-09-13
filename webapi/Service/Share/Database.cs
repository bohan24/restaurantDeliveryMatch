using Dapper;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data;
using System.Text;

namespace webapi.Service
{
    public class Database
    {
        private int defaultTimeout = 300;//預設timeout時間

        IConfiguration _config;

        public Database(IConfiguration config, IServiceProvider service)
        {
            this._config = config;
        }
        public class PageInfo
        {
            /// <summary>
            /// 每頁行數 預設10
            /// </summary>
            public int pageSize { get; set; } = 10;

            /// <summary>
            /// 預設第一頁
            /// </summary>
            public int pageIndex { get; set; } = 1;

            /// <summary>
            /// 總筆數
            /// </summary>
            public int count { get; set; } = 0;

            /// <summary>
            /// 總頁數
            /// </summary>
            public int pageCount
            {
                get
                {
                    if (count > 0)
                    {
                        return count % this.pageSize == 0 ? count / this.pageSize : count / this.pageSize + 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        private string GetDefaultConnectionString(string databasename)
        {
            string defaultConnectionString = "";
            return defaultConnectionString;
        }

        /// <summary>
        /// Mysql異步執行 可做新修刪
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>bool</returns>
        public async Task<bool> MysqlExecuteAsync(string sql, DynamicParameters dynamics, string ConStr = "")
        {

            #region example : 新增範例
            //string sql =   @"
            //                INSERT INTO Card 
            //                (
            //                    [Name]
            //                   ,[Description]
            //                   ,[Attack]
            //                   ,[Health]
            //                   ,[Cost]
            //                ) 
            //                VALUES 
            //                (
            //                    @Name
            //                   ,@Description
            //                   ,@Attack
            //                   ,@Health
            //                   ,@Cost
            //                );

            //                SELECT @@IDENTITY;
            //            ";
            #endregion
            #region example : 修改範例
            //string sql =@"
            //        UPDATE Card
            //        SET 
            //             [Name] = @Name
            //            ,[Description] = @Description
            //            ,[Attack] = @Attack
            //            ,[Health] = @Health
            //            ,[Cost] = @Cost
            //        WHERE 
            //            Id = @id
            //        ";
            //var parameters = new DynamicParameters(parameter);
            //parameters.Add("Id", id, System.Data.DbType.Int32);
            #endregion
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();

                    var result = await conn.ExecuteAsync(sql, dynamics);
                    conn.Close();
                    return result > 0;
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// Mysql同步執行 可做新修刪
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>bool</returns>
        public bool MysqlExecute(string sql, DynamicParameters dynamics, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.Execute(sql, dynamics);
                conn.Close();
                return result > 0;
            }
        }

        /// <summary>
        /// Mysql異步執行 可做新修刪 多筆
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<int> MysqlExecuteMultipleAsync(string sql, List<DynamicParameters> list, string ConStr = "")
        {

            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(sql, list);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql同步執行 可做新修刪 多筆
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public int MysqlExecuteMultiple(string sql, List<DynamicParameters> list, string ConStr = "")
        {

            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.Execute(sql, list);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql單筆異步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>IEnumerable<dynamic></returns>
        public async Task<dynamic> MysqlQueryFirstOrDefaultAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql單筆同步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>IEnumerable<dynamic></returns>
        public dynamic MysqlQueryFirstOrDefault(string sql, DynamicParameters parameters, string ConStr = "")
        {
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// Mysql異步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>IEnumerable<dynamic></returns>
        public async Task<IEnumerable<dynamic>> MysqlQueryAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            #region example :
            // string sql = @"select * from User where id = ?"
            // parameters.Add("id", id);
            #endregion
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                try
                {
                    conn.Open();
                    var result = await conn.QueryAsync(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result.AsEnumerable();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    return null;
                }
            }
        }


        /// <summary>
        /// Mysql同步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>IEnumerable<dynamic></returns>
        public IEnumerable<dynamic> MysqlQuery(string sql, DynamicParameters parameters, string ConStr = "")
        {
            #region example :
            // string sql = @"select * from User where id = ?"
            // parameters.Add("id", id);
            #endregion
            using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
            {
                conn.Open();
                var result = conn.Query(sql, parameters, commandTimeout: defaultTimeout);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        ///  Mysql 單筆異步查詢: return a single object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public async Task<dynamic> MysqlQuerySingleAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();
                    var result = await conn.QuerySingleAsync<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        ///  Mysql 單筆同步查詢: return a single object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public dynamic MysqlQuerySingle(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new MySqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();
                    var result = conn.QuerySingle<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        ///  PostgreSQL 單筆同步查詢: return a single object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="ConStr"></param>
        /// <returns></returns>
        public dynamic PostgreQuerySingle(string sql, DynamicParameters parameters, string ConStr = "")
        {
            try
            {
                using (var conn = new NpgsqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();
                    var result = conn.QuerySingle<dynamic>(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 異步查詢
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="parameters">動態參數設置</param>
        /// <returns>IEnumerable<dynamic></returns>
        public async Task<IEnumerable<dynamic>> PostgreQueryAsync(string sql, DynamicParameters parameters, string ConStr = "")
        {
            #region example :
            // string sql = @"select * from User where id = ?"
            // parameters.Add("id", id);
            #endregion
            using (var conn = new NpgsqlConnection(GetDefaultConnectionString(ConStr)))
            {
                try
                {
                    conn.Open();
                    var result = await conn.QueryAsync(sql, parameters, commandTimeout: defaultTimeout);
                    conn.Close();
                    return result.AsEnumerable();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    return null;
                }
            }
        }

        /// <summary>
        /// 異步執行 可做新修刪
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>bool</returns>
        public async Task<int> PostgreExecuteAsync(string sql, DynamicParameters dynamics, string ConStr = "")
        {

            #region example : 新增範例
            //string sql =   @"
            //                INSERT INTO Card 
            //                (
            //                    [Name]
            //                   ,[Description]
            //                   ,[Attack]
            //                   ,[Health]
            //                   ,[Cost]
            //                ) 
            //                VALUES 
            //                (
            //                    @Name
            //                   ,@Description
            //                   ,@Attack
            //                   ,@Health
            //                   ,@Cost
            //                );

            //                SELECT @@IDENTITY;
            //            ";
            #endregion
            #region example : 修改範例
            //string sql =@"
            //        UPDATE Card
            //        SET 
            //             [Name] = @Name
            //            ,[Description] = @Description
            //            ,[Attack] = @Attack
            //            ,[Health] = @Health
            //            ,[Cost] = @Cost
            //        WHERE 
            //            Id = @id
            //        ";
            //var parameters = new DynamicParameters(parameter);
            //parameters.Add("Id", id, System.Data.DbType.Int32);
            #endregion
            try
            {
                using (var conn = new NpgsqlConnection(GetDefaultConnectionString(ConStr)))
                {
                    conn.Open();

                    var result = await conn.ExecuteAsync(sql, dynamics);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }


    }

}
