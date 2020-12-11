using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShortCut.Util.BooleanUtils;
using ShortCut.Util.DateAndTimeUtils;
using ShortCut.Util.DictionaryUtils;
using ShortCut.Util.NumericUtils;
using ShortCuts.DAL.Managers.Base;
using ShortCuts.DAL.Models;
using ShortCuts.DAL.Repo;

namespace ShortCuts.DAL.Managers.Product
{
    public class ProductManager : BaseManager, IProductManager
    {
        #region Private Memebers
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public ProductManager()
        {
            _repository = new Repository();
        }
        #endregion

        #region GET
        public async Task<IEnumerable<ProductTypeModel>> GetProductTypesAsync()
        {
            var resultProducts = new List<ProductTypeModel>();
            var query = @"SELECT Id,ShortName,FullName,IsActive,IsDeleted,CreatedAt,CreatedBy,UpdatedAt,Image,ImageColor FROM ProductType WHERE IsActive = 1 AND IsDeleted = 0";
            var values = await _repository.QueryAsync(query: query);
            if (values != null)
            {
                foreach (var value in values)
                {
                    ProductTypeModel productType = new ProductTypeModel()
                    {
                        Id = value?.GetValueFromDictonary("Id")?.ToString()?.ToNullableInt() ?? 0,
                        ShortName = value?.GetValueFromDictonary("ShortName")?.ToString(),
                        FullName = value?.GetValueFromDictonary("FullName")?.ToString(),
                        IsActive = value?.GetValueFromDictonary("IsActive")?.ToString()?.ToNullableBoolean(),
                        IsDeleted = value?.GetValueFromDictonary("IsDeleted")?.ToString()?.ToNullableBoolean(),
                        CreatedAt = value?.GetValueFromDictonary("CreatedAt")?.ToString()?.ToNullableDateTime(),
                        CreatedBy = value?.GetValueFromDictonary("CreatedBy")?.ToString(),
                        UpdatedAt = value?.GetValueFromDictonary("UpdatedAt")?.ToString()?.ToNullableDateTime(),
                        UpdatedBy = value?.GetValueFromDictonary("UpdatedBy")?.ToString(),
                        Image = value?.GetValueFromDictonary("Image")?.ToString(),
                        ImageColor = value?.GetValueFromDictonary("ImageColor")?.ToString()

                    };
                    resultProducts.Add(productType);
                }
            }

            return resultProducts;
        }

        public async Task<IEnumerable<ProductModel>> LoadAppropriateSuitAsync(int? id)
        {
            var Products = new List<ProductModel>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@v_ProductTypeId"] = id;
            string query = @"SELECT * FROM Product WHERE ProductTypeId = @v_ProductTypeId AND IsActive = 1 AND IsDeleted = 0";
            var values = await _repository.QueryAsync(query: query, parameters: parameters);
            if (values != null)
            {
                foreach (var value in values)
                {
                    ProductModel product = new ProductModel()
                    {
                        Id = value?.GetValueFromDictonary("Id")?.ToString()?.ToNullableInt() ?? 0,
                        ProductTypeId = value?.GetValueFromDictonary("ProductTypeId")?.ToString()?.ToNullableInt() ?? 0,
                        ShortName = value?.GetValueFromDictonary("ShortName")?.ToString(),
                        FullName = value?.GetValueFromDictonary("FullName")?.ToString(),
                        IsDepricated = value?.GetValueFromDictonary("IsDepricated")?.ToString()?.ToNullableBoolean(),
                        IsActive = value?.GetValueFromDictonary("IsActive")?.ToString()?.ToNullableBoolean(),
                        IsDeleted = value?.GetValueFromDictonary("IsDeleted")?.ToString()?.ToNullableBoolean(),
                        CreatedAt = value?.GetValueFromDictonary("CreatedAt")?.ToString()?.ToNullableDateTime(),
                        CreatedBy = value?.GetValueFromDictonary("CreatedBy")?.ToString(),
                        UpdatedAt = value?.GetValueFromDictonary("UpdatedAt")?.ToString()?.ToNullableDateTime(),
                        UpdatedBy = value?.GetValueFromDictonary("UpdatedBy")?.ToString(),
                        Image = value?.GetValueFromDictonary("Image")?.ToString(),
                        ImageColor = value?.GetValueFromDictonary("ImageColor")?.ToString()
                    };
                    Products.Add(product);
                }
            }
            return Products;
        }

        public async Task<IEnumerable<ShortCutModel>> LoadProductShortCutsAsync(int? productId)
        {
            List<ShortCutModel> ProductShortCut = new List<ShortCutModel>();
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["@v_productId"] = productId;
                //string query = @"SELECT * FROM ShortCut";
                //var values = await _repository.QueryAsync(query: query);
                string query = @"SELECT * FROM Shortcut WHERE IsActive = 1 AND IsDeleted = 0 AND ProductId = @v_productId";
                var values = await _repository.QueryAsync(query: query, parameters: parameters);
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        ShortCutModel shortcut = new ShortCutModel()
                        {
                            Id = value?.GetValueFromDictonary("Id")?.ToString()?.ToNullableInt() ?? 0,
                            ProductTypeId = value?.GetValueFromDictonary("ProductTypeId")?.ToString()?.ToNullableInt() ?? 0,
                            ProductId = value?.GetValueFromDictonary("ProductId")?.ToString()?.ToNullableInt() ?? 0,
                            MacKey = value?.GetValueFromDictonary("MacKey")?.ToString()?.Trim(),
                            WinKey = value?.GetValueFromDictonary("WinKey")?.ToString()?.Trim(),
                            Description = value?.GetValueFromDictonary("Description")?.ToString()?.Trim(),
                            IsActive = value?.GetValueFromDictonary("IsActive")?.ToString()?.ToNullableBoolean(),
                            IsDeleted = value?.GetValueFromDictonary("IsDeleted")?.ToString()?.ToNullableBoolean(),
                            CreatedAt = value?.GetValueFromDictonary("CreatedAt")?.ToString()?.ToNullableDateTime(),
                            CreatedBy = value?.GetValueFromDictonary("CreatedBy")?.ToString(),
                            UpdatedAt = value?.GetValueFromDictonary("UpdatedAt")?.ToString()?.ToNullableDateTime(),
                            UpdatedBy = value?.GetValueFromDictonary("UpdatedBy")?.ToString(),
                            IsDepricated = value?.GetValueFromDictonary("IsDeprecated")?.ToString()?.ToNullableBoolean() ?? false
                        };
                        ProductShortCut.Add(shortcut);
                    }
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }

            return ProductShortCut;
        }

        #endregion

        #region Add
        public async Task<bool> AddShortCutAsync(ShortCutModel shortCutModel)
        {
            bool retVal = false;
            try
            {
                int Id = await GetLastIdAsync();
                if (Id > 0)
                {
                    if (shortCutModel != null)
                    {
                        Dictionary<string, object> parameters = new Dictionary<string, object>();
                        parameters["@v_Id"] = Id + 1;//shortCutModel.Id ?? 0;
                        parameters["@v_ProductTypeId"] = shortCutModel.ProductTypeId;
                        parameters["@v_PrductId"] = shortCutModel.ProductId;
                        parameters["@v_WinKey"] = shortCutModel.WinKey ?? string.Empty;
                        parameters["@v_MacKey"] = shortCutModel.MacKey ?? string.Empty;
                        parameters["@v_Description"] = shortCutModel.Description ?? string.Empty;
                        parameters["@v_IsActive"] = 1;
                        parameters["@v_IsDeleted"] = 0;
                        parameters["@v_CreatedAt"] = DateTime.Now;
                        parameters["@v_CreateBy"] = "Shabab Butt";
                        var query = @"INSERT INTO  Shortcut(Id,ProductTypeId,ProductId,WinKey,MacKey,Description,IsActive,IsDeleted,CreatedAt,CreatedBy)
                                                VALUES(@v_Id,@v_ProductTypeId,@v_PrductId,@v_WinKey,@v_MacKey,@v_Description,@v_IsActive,@v_IsDeleted,datetime(),@v_CreateBy)";

                        var result = await _repository.NonQueryAsync(query: query, parameters: parameters);
                        retVal = result > 0 ? true : false;
                    }
                }

            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return retVal;
        }
        #endregion

        #region Delete

        public async Task<bool> RemoveShortCutAsync(int Id)
        {
            var retVal = false;
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["@v_Id"] = Id;
                var query = string.Empty;
                query = @"UPDATE ShortCut SET IsActive = 0 WHERE Id = @v_Id";
                var result = await _repository.NonQueryAsync(query: query, parameters: parameters);
                retVal = result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return retVal;
        }
        #endregion

        #region Update
        public async Task<bool> UpdateShortCutAsync(ShortCutModel shortCutModel)
        {
            var retVal = false;
            try
            {

                if (shortCutModel != null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters["@v_Id"] = shortCutModel.Id;
                    parameters["@v_ProductTypeId"] = shortCutModel.ProductTypeId;
                    parameters["@v_PrductId"] = shortCutModel.ProductId;
                    parameters["@v_WinKey"] = shortCutModel.WinKey ?? string.Empty;
                    parameters["@v_MacKey"] = shortCutModel.MacKey ?? string.Empty;
                    parameters["@v_Description"] = shortCutModel.Description ?? string.Empty;
                    parameters["@v_IsActive"] = 1;
                    parameters["@v_IsDeleted"] = 0;
                    parameters["@v_CreatedAt"] = shortCutModel.CreatedAt;
                    parameters["@v_CreateBy"] = "Shabab Butt";
                    parameters["@v_UpdatedAt"] = DateTime.Now;
                    var query = @"UPDATE Shortcut SET ProductTypeId = @v_ProductTypeId
                                                      ,ProductId = @v_PrductId
                                                      ,WinKey = @v_WinKey
                                                      ,MacKey = @v_MacKey
                                                      ,Description = @v_Description
                                                      ,IsActive = @v_IsActive
                                                      ,IsDeleted = @v_IsDeleted
                                                      ,CreatedAt = @v_CreatedAt
                                                      ,CreatedBy = @v_CreateBy
                                                      ,UpdatedAt = @v_UpdatedAt
                                                                 WHERE Id = @v_Id";

                    var result = await _repository.NonQueryAsync(query: query, parameters: parameters);
                    retVal = result > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return retVal;
        }

        #endregion

        #region Helper Function
        public async Task<int> GetLastIdAsync()
        {
            int lastId = 0;
            try
            {
                var query = @"Select Id from ShortCut ORDER by 1 DESC LIMIT 1";
                var result = await _repository.QueryAsync(query: query);
                lastId = result.FirstOrDefault().GetValueFromDictonary("Id").ToString()?.ToNullableInt() ?? 0;
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
            return lastId;
        }
        #endregion
    }
}
