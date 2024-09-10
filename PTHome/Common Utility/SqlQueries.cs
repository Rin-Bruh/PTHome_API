namespace PTHome.Common_Utility
{
    public class SqlQueries
    {
        public static IConfiguration _sqlQueryConfiguration = new ConfigurationBuilder()
            .AddXmlFile("SqlQueries.xml", true, true)
            .Build();
        public static string AddHouse { get { return _sqlQueryConfiguration["AddHouse"]; } }
        public static string UpdateAllHouseById { get { return _sqlQueryConfiguration["UpdateAllHouseById"]; } }
        public static string ReadAllHouse { get { return _sqlQueryConfiguration["ReadAllHouse"]; } }
        public static string DeleteHouseById { get { return _sqlQueryConfiguration["DeleteHouseById"]; } }
        public static string ReadHouseById { get { return _sqlQueryConfiguration["ReadHouseById"]; } }
        public static string AddUser { get { return _sqlQueryConfiguration["AddUser"]; } }
        public static string ReadAllUser { get { return _sqlQueryConfiguration["ReadAllUser"]; } }
        public static string UpdateAllUserById { get { return _sqlQueryConfiguration["UpdateAllUserById"]; } }
        public static string DeleteUserById { get { return _sqlQueryConfiguration["DeleteUserById"]; } }
        public static string ReadUserById { get { return _sqlQueryConfiguration["ReadUserById"]; } }
        public static string AddContract { get { return _sqlQueryConfiguration["AddContract"]; } }
        public static string ReadAllContract { get { return _sqlQueryConfiguration["ReadAllContract"]; } }
        public static string AddContractHistory { get { return _sqlQueryConfiguration["AddContractHistory"]; } }
        public static string ReadAllContractHistory { get { return _sqlQueryConfiguration["ReadAllContractHistory"]; } }
    }
}
