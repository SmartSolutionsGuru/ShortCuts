using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortCuts.DAL.Managers.Base
{
    public interface IBaseManager
    {
        /// <summary>
        /// If Manager Implement Interface 
        /// Always Create Connection With DataBase
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        Task<bool> CreateConnection(string connectionString);
         void OnActivated();
    }
}
