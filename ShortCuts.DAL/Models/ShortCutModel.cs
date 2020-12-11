using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortCuts.DAL.Models
{
    public class ShortCutModel : BaseModel
    {
        #region Properties
        public int ProductTypeId { get; set; }
        public int ProductId { get; set; }
        public string WinKey { get; set; }
        public string MacKey { get; set; }
        public bool IsDepricated { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
