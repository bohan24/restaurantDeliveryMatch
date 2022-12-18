namespace RDM.Service.Util
{
    public class Appsettings
    {
        private readonly IConfiguration _config;
        public Appsettings(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 取得Wallet mysql連線字串
        /// </summary>
        /// <returns></returns>
        public string GetMysqlConString()
        {
            var mysqlConString = _config["ConnectionStrings:wallet"];//_config.GetValue<string>("MySQL");
            return mysqlConString;
        }

        public string GetApiUrl()
        {
            var gameApi = _config.GetSection("ApiUrl").Value;
            return gameApi;
        }

        /// <summary>
        /// Wallet API Domain
        /// </summary>
        /// <returns></returns>
        public string GetWalletApiUrl()
        {
            var walletApi = _config["WalletApi:domain"]; 
            return walletApi;
        }

        /// <summary>
        /// 手機發送驗證碼
        /// </summary>
        /// <returns></returns>
        public string GetGCApiUrl()
        {
            var vertifyByPhone = _config["GCApi:gcsw_vertifyByPhone"];
            return vertifyByPhone;
        }
    }
}
