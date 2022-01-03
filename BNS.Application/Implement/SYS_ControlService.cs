using BNS.Application.Interface;
using BNS.Utilities;
using BNS.Utilities.Constant;
using BNS.Utilities.Interface;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using Microsoft.Extensions.Hosting;
using System;

namespace BNS.Application.Implement
{
    public class SYS_ControlService : ISYS_ControlService
    {
        private readonly ICacheData _cache;
        private readonly IHostingEnvironment _hostingEnvironment;
        public SYS_ControlService(ICacheData cache, IHostingEnvironment hostingEnvironment)
        {
            _cache = cache;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ApiResult<SYS_FieldControlResponseModel>> GetFieldControl(SYS_ControlModel model)
        {
            var result = new ApiResult<SYS_FieldControlResponseModel>();
            string key = Ultility.GetCacheKey(EDataType.FieldInfo, model.ShopIndex, null);
            var dir = _hostingEnvironment.ContentRootPath;
            var data = _cache.GetToCache(key);
            if (data != null)
            {
                var dataItem = (List<SYS_FieldControlResponseModel>)data;
                var resultData = dataItem.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
                result.data = resultData;
                return result;
            }
            var value = new List<SYS_FieldControlResponseModel>();

            string folderPath = string.Empty;
            var dirFieldInfo = dir + Constants.__PATH_FIELD_INFO + ".json";
            var dirFieldInfoCus = dir + Constants.__PATH_FIELD_INFO_CUS + "_" + model.ShopIndex + ".json";
            if (File.Exists(dirFieldInfoCus))
                folderPath = dirFieldInfoCus;
            else
            {
                File.Copy(dirFieldInfo, dirFieldInfoCus);
                folderPath = dirFieldInfoCus;
            }
            using (StreamReader r = new StreamReader(folderPath))
            {
                string json = r.ReadToEnd();
                value = JsonConvert.DeserializeObject<List<SYS_FieldControlResponseModel>>(json);
            }
            _cache.AddToCache(key, value);
            var field = value.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
            if (field == null)
            {
                var valueDefault = new List<SYS_FieldControlResponseModel>();
                using (StreamReader r = new StreamReader(dirFieldInfo))
                {
                    string json = r.ReadToEnd();
                    valueDefault = JsonConvert.DeserializeObject<List<SYS_FieldControlResponseModel>>(json);
                }
                field = valueDefault.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
                if (result != null)
                {
                    value.Add(field);
                    string json = JsonConvert.SerializeObject(value);
                    File.WriteAllText(folderPath, json);
                    _cache.AddToCache(key, value);
                }
            }
            result.data = field;
            return result;
        }
        private List<SYS_ColumnControlResponseModel> GetColumnControlData(Guid shopIndex)
        {
            string key = Ultility.GetCacheKey(EDataType.ColumnInfo, shopIndex, null);
            var dir = _hostingEnvironment.ContentRootPath;
            var data = _cache.GetToCache(key);
            if (data != null)
            {
                var dataItem = (List<SYS_ColumnControlResponseModel>)data;
                return dataItem;
            }
            var value = new List<SYS_ColumnControlResponseModel>();

            string folderPath = string.Empty;
            var dirFieldInfo = dir + Constants.__PATH_COLUMN_INFO + ".json";
            var dirFieldInfoCus = dir + Constants.__PATH_COLUMN_INFO_CUS + "_" + shopIndex + ".json";
            if (File.Exists(dirFieldInfoCus))
                folderPath = dirFieldInfoCus;
            else
            {
                File.Copy(dirFieldInfo, dirFieldInfoCus);
                folderPath = dirFieldInfoCus;
            }
            using (StreamReader r = new StreamReader(folderPath))
            {
                string json = r.ReadToEnd();
                value = JsonConvert.DeserializeObject<List<SYS_ColumnControlResponseModel>>(json);
            }
            _cache.AddToCache(key, value);
            return value;
        }

        private SYS_ColumnControlResponseModel GetColumnControlData(SYS_ControlModel model)
        {
            string key = Ultility.GetCacheKey(EDataType.ColumnInfo, model.ShopIndex, null);
            var dir = _hostingEnvironment.ContentRootPath;
            var data = _cache.GetToCache(key);
            if (data != null)
            {
                var dataItem = (List<SYS_ColumnControlResponseModel>)data;
                var resultData = dataItem.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
                if(resultData !=null)
                    return resultData;
            }
            var value = new List<SYS_ColumnControlResponseModel>();

            string folderPath = string.Empty;
            var dirFieldInfo = dir + Constants.__PATH_COLUMN_INFO + ".json";
            var dirFieldInfoCus = dir + Constants.__PATH_COLUMN_INFO_CUS + "_" + model.ShopIndex + ".json";
            if (File.Exists(dirFieldInfoCus))
                folderPath = dirFieldInfoCus;
            else
            {
                File.Copy(dirFieldInfo, dirFieldInfoCus);
                folderPath = dirFieldInfoCus;
            }
            using (StreamReader r = new StreamReader(folderPath))
            {
                string json = r.ReadToEnd();
                value = JsonConvert.DeserializeObject<List<SYS_ColumnControlResponseModel>>(json);
            }
            _cache.AddToCache(key, value);
            var result = value.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
            if (result == null)
            {
                var valueDefault = new List<SYS_ColumnControlResponseModel>();
                using (StreamReader r = new StreamReader(dirFieldInfo))
                {
                    string json = r.ReadToEnd();
                    valueDefault = JsonConvert.DeserializeObject<List<SYS_ColumnControlResponseModel>>(json);
                }
                result = valueDefault.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
                if (result != null)
                {
                    value.Add(result);
                    string json = JsonConvert.SerializeObject(value);
                    File.WriteAllText(folderPath, json);
                    _cache.AddToCache(key, value);
                }
            }
            return result;
        }
        public async Task<ApiResult<SYS_ColumnControlResponseModel>> GetColumnControl(SYS_ControlModel model)
        {
            var result = new ApiResult<SYS_ColumnControlResponseModel>();
            result.data = GetColumnControlData(model);
            return result;
        }
        public async Task<ApiResult<List<SYS_ColumnControlResponseModel>>> GetColumnControl(Guid shopIndex)
        {
            var result = new ApiResult<List<SYS_ColumnControlResponseModel>>();
            result.data = GetColumnControlData(shopIndex);
            return result;
        }
        public async Task<ApiResult<SYS_FieldControlResponseModel>> SaveFieldControl(SYS_FieldModel model)
        {
            var result = new ApiResult<SYS_FieldControlResponseModel>();
            string key = Ultility.GetCacheKey(EDataType.FieldInfo, model.ShopIndex, null);
            var dir = _hostingEnvironment.ContentRootPath;

            var value = new List<SYS_FieldControlResponseModel>();

            string folderPath = string.Empty;
            var dirFieldInfo = dir + Constants.__PATH_FIELD_INFO + ".json";
            var dirFieldInfoCus = dir + Constants.__PATH_FIELD_INFO_CUS + "_" + model.ShopIndex + ".json";
            if (File.Exists(dirFieldInfoCus))
                folderPath = dirFieldInfoCus;
            else
            {
                File.Copy(dirFieldInfo, dirFieldInfoCus);
                folderPath = dirFieldInfoCus;
            }

            var data = _cache.GetToCache(key);
            if (data != null)
            {
                value = (List<SYS_FieldControlResponseModel>)data;
            }
            else
            {
                using (StreamReader r = new StreamReader(folderPath))
                {
                    string json = r.ReadToEnd();
                    value = JsonConvert.DeserializeObject<List<SYS_FieldControlResponseModel>>(json);
                }
                _cache.AddToCache(key, value);
            }

            var fieldControl = value.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
            if (fieldControl != null)
            {
                if (!model.Hidden)
                {
                    var field = fieldControl.field.Where(s => s.Column.ToLower() == model.FieldName.ToLower()).FirstOrDefault();
                    if (field != null)
                        fieldControl.field.Remove(field);
                }
                else
                {
                    if (!fieldControl.field.Any(s => s.Column.ToLower() == model.FieldName.ToLower()))
                        fieldControl.field.Add(new FieldControl { Column = model.FieldName });
                }

                string json = JsonConvert.SerializeObject(value);
                File.WriteAllText(folderPath, json);
                _cache.AddToCache(key, value);
            }
            await SaveColumnControl(new SYS_FieldModel
            {
                ActionName = model.ActionName,
                ShopIndex = model.ShopIndex,
                Hidden = model.Hidden,
                FieldName = model.FieldName
            });
            //var columnControl = GetColumnControlData(new SYS_ControlModel
            //{
            //    ActionName = model.ActionName,
            //    ShopIndex = model.ShopIndex,
            //});
            //if (columnControl != null)
            //{

            //}

            result.data = fieldControl;
            return result;
        }

        public async Task<ApiResult<SYS_ColumnControlResponseModel>> SaveColumnControl(SYS_FieldModel model)
        {
            var result = new ApiResult<SYS_ColumnControlResponseModel>();
            string key = Ultility.GetCacheKey(EDataType.ColumnInfo, model.ShopIndex, null);
            var dir = _hostingEnvironment.ContentRootPath;

            var value = new List<SYS_ColumnControlResponseModel>();

            string folderPath = string.Empty;
            var dirFieldInfo = dir + Constants.__PATH_COLUMN_INFO + ".json";
            var dirFieldInfoCus = dir + Constants.__PATH_COLUMN_INFO_CUS + "_" + model.ShopIndex + ".json";
            if (File.Exists(dirFieldInfoCus))
                folderPath = dirFieldInfoCus;
            else
            {
                File.Copy(dirFieldInfo, dirFieldInfoCus);
                folderPath = dirFieldInfoCus;
            }

            var data = _cache.GetToCache(key);
            if (data != null)
            {
                value = (List<SYS_ColumnControlResponseModel>)data;
            }
            else
            {
                using (StreamReader r = new StreamReader(folderPath))
                {
                    string json = r.ReadToEnd();
                    value = JsonConvert.DeserializeObject<List<SYS_ColumnControlResponseModel>>(json);
                }
                _cache.AddToCache(key, value);
            }

            var fieldControl = value.Where(s => s.action == model.ActionName.ToLower()).FirstOrDefault();
            if (fieldControl != null)
            {
                if (!model.Hidden)
                {
                    var field = fieldControl.field.Where(s => s.Column.ToLower() == model.FieldName.ToLower()).FirstOrDefault();
                    if (field != null)
                        fieldControl.field.Remove(field);
                }
                else
                {
                    if (!fieldControl.field.Any(s => s.Column.ToLower() == model.FieldName.ToLower()))
                        fieldControl.field.Add(new ColumnControl { Column = model.FieldName });
                }

                string json = JsonConvert.SerializeObject(value);
                File.WriteAllText(folderPath, json);
                _cache.AddToCache(key, value);
            }

            //var columnControl = GetColumnControlData(new SYS_ControlModel
            //{
            //    ActionName = model.ActionName,
            //    ShopIndex = model.ShopIndex,
            //});
            //if (columnControl != null)
            //{

            //}

            return result;
        }
    }
}
