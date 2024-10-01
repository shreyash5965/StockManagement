using UserData.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;

namespace UserData.Services
{
    public interface IStockMaster
    {
        public Result GetData(int? intUserID);
        public Result Insert_Update_Data(StockMasterModel obj);
        public Result Delete_Data(StockMasterModel obj);
    }

    public class StockMaster : IStockMaster
    {
        NpgsqlGetData npgsqlGetData = new NpgsqlGetData();

        public Result GetData(int? intUserID)
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 0);

                res = npgsqlGetData.SelectData("sp_stock_master_crud", param);

                if (res.isSuccess)
                {
                    if (res.Data == null)
                    {
                        res.Message = "Error while getting data";
                    }
                }
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                res.Message = ex.Message;
            }
            return res;
        }
        public Result Insert_Update_Data(StockMasterModel obj)
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");

                if (obj.intstockid != null && obj.intstockid != 0)
                {
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 2);
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intstockid", obj.intstockid);
                }
                else
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 1);

                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_strstockname", obj.strstockname);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_strshortname", obj.strshortname);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_strdescription", obj.strdescription);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_intlowestprice", obj.intlowestprice);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_inthighestprice", obj.inthighestprice);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Boolean, ParameterDirection.Input, "p_islisted", obj.islisted);

                res = npgsqlGetData.Get_CUD_Data("sp_stock_master_crud", param);

                if (res.isSuccess)
                {
                    if (res.Data == null)
                    {
                        res.Message = "Error while getting data";
                    }
                }
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                res.Message = ex.Message;
            }
            return res;
        }
        public Result Delete_Data(StockMasterModel obj)
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 3);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intstockid", obj.intstockid);

                res = npgsqlGetData.SelectData("sp_stock_master_crud", param);

                if (res.isSuccess)
                {
                    if (res.Data == null)
                    {
                        res.Message = "Error while getting data";
                    }
                    else
                    {
                        res.Message = "Data deleted successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
