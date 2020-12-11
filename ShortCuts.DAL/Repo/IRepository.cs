using ShortCuts.DAL.DatabaseHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortCuts.DAL.Repo
{
    public interface IRepository
    {
        void CreateConnection(ConnectionInfo connection);
        /// <summary>
        /// A simple function get the Query and return result
        /// </summary>
        /// <param name="query"> Query in form of string</param>
        /// <param name="parameters">parameters if there is</param>
        /// <returns>Returnns the resultset in form of Dictonery</returns>
        Task<List<Dictionary<string, object>>> QueryAsync(string query,Dictionary<string,object>parameters = null);
        /// <summary>
        ///  Function For CRUD Opreations
        ///  Like Addintion Updateion And Deletions
        /// </summary>
        /// <param name="query">Query for Opreations</param>
        /// <param name="parameters">Parameters For Inserting Or Deletion Or Updating Values</param>
        /// <param name="outParameters"></param>
        /// <returns></returns>
        Task<int> NonQueryAsync(string query = null, Dictionary<string, object> parameters = null, Dictionary<string, object> outParameters = null);
    }
}
