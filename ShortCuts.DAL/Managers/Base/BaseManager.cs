using System.Data.SQLite;
using System.Threading.Tasks;

namespace ShortCuts.DAL.Managers.Base
{
    public class BaseManager : IBaseManager
    {
        #region Constructor
        public BaseManager()
        {
            OnActivated();
        }
        #endregion

        public async Task<bool> CreateConnection(string connectionString)
        {
            bool retVal = false;
            await Task.Run(() =>
            {

                //// connectionString = "Data Source=:memory:";
                //connectionString = @"Data Source=C:\Users\sbutt\source\repos\ShortCuts\ShortCuts.UI\Assets\ShortCuts.sqlite;";
                //if (!string.IsNullOrEmpty(connectionString))
                //{
                //    using (var con = new SQLiteConnection(connectionString))
                //    {

                //        await con?.OpenAsync();
                //        retVal = true;
                //    }
                //}
                //else
                //{
                //    //connectionString = ConnectionInfo.Instance.ConnectionString;
                //}
            });
            return retVal;
        }

        public void OnActivated()
        {
            //await CreateConnection(string.Empty);
        }
    }
}
