using UserData.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;

namespace UserData.Services
{
    public interface ICommonService
    {
        public Result GetStockList();
    }

    public class CommonService : ICommonService
    {
        NpgsqlGetData npgsqlGetData = new NpgsqlGetData();

        public Result GetStockList()
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 0);

                res = npgsqlGetData.SelectData("sp_get_common_data", param);

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
    }
}
