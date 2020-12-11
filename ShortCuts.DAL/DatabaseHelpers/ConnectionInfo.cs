namespace ShortCuts.DAL.DatabaseHelpers
{
    /// <summary>
    /// A class that will help to Create Connection String
    /// </summary>
    public class ConnectionInfo
    {

        #region Siglten Instance
        
        private static ConnectionInfo m_Instance;
        /// <summary>
        /// Creating Property  for siglten Instance
        /// </summary>
        public static ConnectionInfo Instance => m_Instance = m_Instance ?? new ConnectionInfo();
        #endregion

        #region Constructor
        public ConnectionInfo() { }
        #endregion

        #region Properties

        /// <summary>
        /// Conection string or IP Address for DB
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Name of the DB
        /// </summary>
        public string Database { get; set; }
        public string InitialCatalog { get; set; }

        /// <summary>
        /// Ip address for Remote connection to DB
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Port Number for Remote connection to DB
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// UserName for DB
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///  Password for DB
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Path of DB
        /// </summary>
        public string Path { get; set; }

        public string Name { get; set; }
        public int MachineId { get; set; }

        /// <summary>
        /// Databae Type for Application Like SQLite,SQL Server
        /// </summary>
        public DBTypes Type { get; set; }
        #endregion
    }

    #region Enum
    /// <summary>
    /// Enum for Diffrent Type Of Database 
    /// Like SQLite SQLServer etc..
    /// </summary>
    public enum DBTypes
    {
        SQLite = 1,
        SQLServer = 2,
        MySQL = 3,
        Oracle =4,
    }
    #endregion
}
