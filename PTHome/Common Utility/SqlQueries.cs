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
    }
}
