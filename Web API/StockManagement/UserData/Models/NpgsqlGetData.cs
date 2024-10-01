using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace UserData.Models
{
    public class NpgsqlGetData
    {
        NpgsqlConnection sq = new NpgsqlConnection(Startup.connString);
        NpgsqlCommand command = null;
        public static void AddParams(List<NpgsqlParameter> collection, NpgsqlDbType paramType, ParameterDirection direction, string paramName, object value)
        {
            NpgsqlParameter param = new NpgsqlParameter(paramName, paramType);
            param.Value = value;
            param.Direction = direction;
            collection.Add(param);
        }

        public Result SelectData(string functionName, List<NpgsqlParameter> param)
        {
            Result res = new Result();
            DataTable dt = null;
            NpgsqlTransaction transaction = null;
            //List<DataTable> dtRtn = new List<DataTable>();
            try
            {
                sq.Open();
                transaction = sq.BeginTransaction();
                command = new NpgsqlCommand();
                command.Connection = sq;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = functionName;
                command.Transaction = transaction;

                if (param != null)
                {
                    foreach (object item in param)
                    {
                        command.Parameters.Add(item);
                    }
                }

                string data = command.ExecuteScalar().ToString();
                res.isSuccess = true;
                //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                //adapter.Fill(dt);
                dt = GetDataFromCursor(data);
                transaction.Commit();
                res.Data = JsonConvert.SerializeObject(dt);
            }
            catch(Exception ex)
            {
                res.Message = ex.Message;
                res.Exception = ex;
            }
            finally
            {
                if(sq != null)
                    sq.Close();
            }
            return res;
        }

        public Result Get_CUD_Data(string functionName, List<NpgsqlParameter> param)
        {
            Result res = new Result();
            DataTable dt = null;
            NpgsqlTransaction transaction = null;
            //List<DataTable> dtRtn = new List<DataTable>();
            try
            {
                sq.Open();
                transaction = sq.BeginTransaction();
                command = new NpgsqlCommand();
                command.Connection = sq;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = functionName;
                command.Transaction = transaction;

                if (param != null)
                {
                    foreach (object item in param)
                    {
                        command.Parameters.Add(item);
                    }
                }

                string data = command.ExecuteScalar().ToString();
                res.isSuccess = true;
                //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                //adapter.Fill(dt);
                dt = GetDataFromCursor(data);
                transaction.Commit();
                res.Message = dt.Rows[0][1].ToString();
                res.Data = JsonConvert.SerializeObject(dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Exception = ex;
            }
            finally
            {
                if (sq != null)
                    sq.Close();
            }
            return res;
        }
        public DataTable GetData_DT(string functionName, List<NpgsqlParameter> param)
        {
            DataTable dt = new DataTable();
            NpgsqlTransaction transaction = null;
            //List<DataTable> dtRtn = new List<DataTable>();
            try
            {
                sq.Open();
                transaction = sq.BeginTransaction();
                command = new NpgsqlCommand();
                command.Connection = sq;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = functionName;
                command.Transaction = transaction;

                if (param != null)
                {
                    foreach (object item in param)
                    {
                        command.Parameters.Add(item);
                    }
                }

                string data = command.ExecuteScalar().ToString();
                dt = GetDataFromCursor(data);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //dt.Rows[0][0] = ex.Message.ToString();
            }
            finally
            {
                if (sq != null)
                    sq.Close();
            }
            return dt;
        }
        public DataTable GetDataFromCursor(string cursorName)
        {
            DataTable dt = new DataTable();

            command = new NpgsqlCommand();
            command.Connection = sq;
            command.CommandText = "FETCH ALL IN " + "\"" + cursorName + "\"";
            command.CommandType = CommandType.Text;

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
    }
}