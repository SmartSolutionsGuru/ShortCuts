using System.Collections.Generic;
using System.Threading.Tasks;
using ShortCuts.DAL.Models;

namespace ShortCuts.DAL.Managers.Product
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductTypeModel>> GetProductTypesAsync();
        Task<IEnumerable<ProductModel>> LoadAppropriateSuitAsync(int? productTypeId);
        Task<IEnumerable<ShortCutModel>> LoadProductShortCutsAsync(int? productId);

        /// <summary>
        /// Add new ShortCut into ShortCut Db
        /// </summary>
        /// <param name="shortCutModel"></param>
        /// <returns></returns>
        Task<bool> AddShortCutAsync(Models.ShortCutModel shortCutModel);

        Task<bool> UpdateShortCutAsync(Models.ShortCutModel shortCutModel);

        Task<bool> RemoveShortCutAsync(int Id);
        Task<int> GetLastIdAsync();
    }
}
