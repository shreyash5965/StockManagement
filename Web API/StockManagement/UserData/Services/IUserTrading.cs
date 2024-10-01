using UserData.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Newtonsoft.Json;

namespace UserData.Services
{
    public interface IUserTrading
    {
        public Result GetTradingHistory();
        public Result GetUserTraingDetails();
        public Result GetDashboardData();
        public Result Insert_Update_Data(UserStockDetails obj);
        public Result GetTradingHistory(TradingHistoryFilter obj);
    }

    public class UserTrading : IUserTrading
    {
        private readonly IJWTAuthentication jWTAuthentication;
        private IUserMaster userMaster;

        public UserTrading(IJWTAuthentication _jWTAuthentication, IUserMaster _userMaster)
        {
            jWTAuthentication = _jWTAuthentication;
            userMaster = _userMaster;
        }

        NpgsqlGetData npgsqlGetData = new NpgsqlGetData();
        public Result GetTradingHistory()
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 0);

                res = npgsqlGetData.SelectData("fn_usermaster_crud", param);

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
        public Result GetUserTraingDetails()
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 0);

                res = npgsqlGetData.SelectData("sp_user_stock_detail_crud", param);

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
        public Result GetTradingHistory(TradingHistoryFilter obj)
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();
            Hashtable tokenData = jWTAuthentication.DecryptToken();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 2);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intuserid", Convert.ToInt32(tokenData["name"]));

                if (obj.intstockids != null && obj.intstockids != "")
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_stockids", obj.intstockids);

                if (obj.strtradetype != null && obj.strtradetype != "")
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_strtradetype", obj.strtradetype);

                if (obj.strtrading != null && obj.strtrading != "")
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_strtrading", obj.strtrading);

                if (obj.fromdate != null && obj.fromdate != "")
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_fromdate", obj.fromdate);

                if (obj.todate != null && obj.todate != "")
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_todate", obj.todate);

                res = npgsqlGetData.SelectData("sp_user_stock_detail_crud", param);

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

        public Result GetDashboardData()
        {
            Result res = new Result();

            try
            {
                Hashtable tokenData = jWTAuthentication.DecryptToken();

                Dashboard obj = new Dashboard();

                obj.investment_data = GetBasicInfo(Convert.ToInt32(tokenData["name"]));
                obj.profit_loss_data = GetBarChart(Convert.ToInt32(tokenData["name"]));
                obj.history_data = GetDashboardHistoryData(Convert.ToInt32(tokenData["name"]));

                //if (obj.investment_data.Rows.Count > 0 && obj.profit_loss_data.Rows.Count > 0 && obj.history_data.Rows.Count > 0)
                //{
                    res.isSuccess = true;
                    res.Data = JsonConvert.SerializeObject(obj);
                //}
            }
            catch(Exception ex)
            {
                res.Message = ex.Message;
            }

            return res;
        }

        private DataTable GetBasicInfo(int intUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                var param = new List<NpgsqlParameter>();
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 0);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intuserid", intUserID);

                dt = npgsqlGetData.GetData_DT("sp_dashboard_data", param);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        private DataTable GetBarChart(int intUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                var param = new List<NpgsqlParameter>();
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 1);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intuserid", intUserID);

                dt = npgsqlGetData.GetData_DT("sp_dashboard_data", param);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        private DataTable GetDashboardHistoryData(int intUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                var param = new List<NpgsqlParameter>();
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 2);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intuserid", intUserID);

                dt = npgsqlGetData.GetData_DT("sp_dashboard_data", param);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public Result Insert_Update_Data(UserStockDetails obj)
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();
            Hashtable tokenData = jWTAuthentication.DecryptToken();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 1);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intuserid", Convert.ToInt32(tokenData["name"]));
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intstockid", obj.intstockid);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_intquantity", obj.intquantity);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_intstockprice", obj.intstockprice);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_strtradetype", obj.strtradetype);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_strtrading", obj.strtrading);

                res = npgsqlGetData.Get_CUD_Data("sp_user_stock_detail_crud", param);

                if (res.isSuccess)
                {
                    if (res.Data == null)
                    {
                        res.Message = "Error while getting data";
                    }
                    else if (res.Data == "1")
                    {
                        string body = "Hello " + tokenData["given_name"].ToString() + 
                                        "<br /><h2>Today's Trading Detail:<h2><br />" +
                                            "<table border='1'>" +
                                                "<thead style='background-color:#0783db;color:white;font-weight: bold'>" +
                                                    "<tr>" +
                                                        "<th>Buy/Sell</th>" +
                                                        "<th>StockName</th>" +
                                                        "<th>ShortName</th>" +
                                                        "<th>Trade Type</th>" +
                                                        "<th>Quantity</th>" +
                                                        "<th>Stock Price</th>" +
                                                        "<th>Total Amount</th>" +
                                                    "</tr>" +
                                                "</thead>" +
                                                "</tbody>" +
                                                    "<tr>" +
                                                        "<td>" + obj.strtrading + "</td>" +
                                                        "<td>" + obj.strstockname + "</td>" +
                                                        "<td>" + obj.strshortname + "</td>" +
                                                        "<td>" + obj.strtradetype + "</td>" +
                                                        "<td>" + obj.intquantity + "</td>" +
                                                        "<td>" + obj.intstockprice + "</td>" +
                                                        "<td>" + (obj.intquantity * Convert.ToDecimal(obj.intstockprice)) + "</td>" +
                                                    "</tr>" +
                                                "</tbody>" +
                                            "</table>";
                        userMaster.SendEmail(tokenData["email"].ToString(), "Trading successfully", body);
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
