using UserData.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace UserData.Services
{
    public interface IUserMaster
    {
        public Result GetData(int? intUserID);
        public Result Insert_Update_Data(UserModel obj);
        public Result Delete_Data(UserModel obj);
        public void SendEmail(string toemail, string subject, string body);
    }

    public class UserMaster : IUserMaster
    {
        NpgsqlGetData npgsqlGetData = new NpgsqlGetData();
        private string _host;
        private string _from;
        private string _alias;
        private string _port;
        private string _password;
        public UserMaster(IConfiguration iConfiguration)
        {
            var smtpSection = iConfiguration.GetSection("MailSettings");
            if (smtpSection != null)
            {
                _host = smtpSection.GetSection("Host").Value;
                _from = smtpSection.GetSection("Mail").Value;
                _alias = smtpSection.GetSection("DisplayName").Value;
                _port = smtpSection.GetSection("Port").Value;
                _password = smtpSection.GetSection("Password").Value;
            }
        }
        public Result GetData(int? intUserID)
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
        public Result Insert_Update_Data(UserModel obj)
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");

                if (obj.intuserid != null && obj.intuserid != 0)
                {
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 2);
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_userid", obj.intuserid);
                }
                else
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 1);

                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_username", obj.strusername);

                if (obj.strpassword != "" && obj.strpassword != null)
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_password", obj.strpassword);

                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_firstname", obj.strfirstname);

                if (obj.strmiddlename != "" && obj.strmiddlename != null)
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_middlename", obj.strmiddlename);

                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_lastname", obj.strlastname);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_emailid", obj.stremailid);

                if (obj.dtdob != "" && obj.dtdob != null)
                    NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_dob", Convert.ToDateTime(obj.dtdob).ToString());

                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_contactno", obj.strcontactno);

                res = npgsqlGetData.Get_CUD_Data("fn_usermaster_crud", param);

                if (res.isSuccess)
                {
                    if (res.Data == null)
                    {
                        res.Message = "Error while getting data";
                    }
                    else if (res.Data == "1")
                    {
                        if(obj.intuserid == 0)
                            SendEmail(obj.stremailid, "Account Created", "Hello " + obj.strfirstname + " " + obj.strlastname + ", <br/><br/> Your account has been created <br/><br/> Thank you for connecting with us");
                        else
                            SendEmail(obj.stremailid, "Account Data Updated", "Hello " + obj.strfirstname + " " + obj.strlastname + ", <br/><br/> Your account data has been updated <br/><br/> Thank you for connecting with us");
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
        public Result Delete_Data(UserModel obj)
        {
            Result res = new Result();
            var param = new List<NpgsqlParameter>();

            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 3);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_userid", obj.intuserid);

                res = npgsqlGetData.SelectData("fn_usermaster_crud", param);

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
        public void SendEmail(string toemail, string subject, string body)
        {
            try
            {
                //using (MailMessage mm = new MailMessage(_from, toemail))
                //{
                //    mm.Subject = subject;
                //    mm.Body = body;
                //    mm.IsBodyHtml = true;
                //    SmtpClient smtp = new SmtpClient();
                //    smtp.Host = _host;
                //    smtp.EnableSsl = true;
                //    smtp.UseDefaultCredentials = false;
                //    smtp.Port = Convert.ToInt32(_port);
                //    smtp.Send(mm);
                //}

                MailMessage mail = new MailMessage();
                mail.To.Add(toemail);
                mail.From = new MailAddress(_alias + "<" + _from + ">");
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                
                SmtpClient smtp = new SmtpClient();
                smtp.Port = Convert.ToInt32(_port);
                smtp.UseDefaultCredentials = false;
                smtp.Host = _host;
                smtp.TargetName = "Shreyash Banawala";
                smtp.Credentials = new NetworkCredential(_from,_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(mail);
            
            }
            catch (Exception ex)
            {

            }
        }
    }
}
