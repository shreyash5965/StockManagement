using UserData.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections;
using Microsoft.AspNetCore.Http;

namespace UserData.Services
{
    public interface IJWTAuthentication
    {
        public Result validateUser(Login obj);
        public Hashtable DecryptToken();
    }

    public class ValidateUser : IJWTAuthentication
    {
        NpgsqlGetData npgsqlGetData = new NpgsqlGetData();
        public readonly IConfiguration configuration;
        private IHttpContextAccessor httpContextAccessor;

        public ValidateUser(IConfiguration _configuration, IHttpContextAccessor _httpContextAccessor)
        {
            configuration = _configuration;
            httpContextAccessor = _httpContextAccessor;
        }

        public Result validateUser(Login obj)
        {
            Result res = new Result();

            var param = new List<NpgsqlParameter>();
            DataTable dt = null;
            try
            {
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Refcursor, ParameterDirection.InputOutput, "ref1", "refcursor");
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Integer, ParameterDirection.Input, "p_chk", 4);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_username", obj.strusername);
                NpgsqlGetData.AddParams(param, NpgsqlTypes.NpgsqlDbType.Text, ParameterDirection.Input, "p_password", obj.strpassword);

                dt = npgsqlGetData.GetData_DT("fn_usermaster_crud", param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string token = GenerateToken(dt);
                    
                    if (token != String.Empty)
                    {
                        dt.Columns.Add("token", typeof(System.String));
                        dt.Rows[0]["token"] = token;
                        res.isSuccess = true;
                        res.Data = JsonConvert.SerializeObject(dt);
                    }
                    else
                    {
                        res.Message = "Error while generating token";
                    }
                }
                else
                {
                    res.Data = "3";
                    res.Message = "Username or password is incorrect";
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Exception = ex;
            }

            return res;
        }

        public Hashtable DecryptToken()
        {
            string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            Hashtable dt = new Hashtable();
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                foreach(var item in jwtSecurityToken.Claims)
                {
                    dt.Add(item.Type, item.Value);
                }
            }
            catch (Exception ex)
            {
                token = ex.Message;
            }

            return dt;
        }

        private string GenerateToken(DataTable dt)
        {
            string token = String.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(Startup.key);
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, dt.Rows[0]["intUserID"].ToString()),
                    new Claim(ClaimTypes.GivenName, dt.Rows[0]["strUserName"].ToString()),
                    new Claim(ClaimTypes.Email, dt.Rows[0]["strEmailID"].ToString()),
                    new Claim(ClaimTypes.MobilePhone, dt.Rows[0]["strContactNo"].ToString())
                    }),
                    //Expires = DateTime.Now.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            }
            catch (Exception ex) {
                token = ex.Message;
            }

            return token;
        }
    }
}
