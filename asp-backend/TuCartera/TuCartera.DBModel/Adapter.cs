using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TuCartera.DBModel.Contexts;
using TuCartera.DBModel.Contexts.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace TuCartera.DBModel
{
    public interface IAdapter
    {
        #region Users

        SpUserGetLoginResult GetLogin(int userId);
        int PostLogin(string email, string password);
        int Register(string name, string email, string password);

        #endregion

        #region Portfolios



        #endregion

        #region Transactions



        #endregion

        #region Tickers



        #endregion

        #region Currencies



        #endregion

        #region Transation types



        #endregion
    }

    public class Adapter: IAdapter
    {
        #region Initialization

        private TuCarteraContext _context;

        public Adapter(string connection)
        {
            _context = new TuCarteraContext(connection);
        }

        #endregion

        #region Users

        public SpUserGetLoginResult GetLogin(int userId)
        {
            try
            {
                SqlParameter idParam = new SqlParameter("@user_id", userId);
                string sqlQuery = "EXECUTE [dbo].[spUserGetLogin] @user_id";
                var user = _context.SpUserGetLogin.FromSqlRaw(sqlQuery, idParam)
                                   .ToListAsync().GetAwaiter().GetResult().FirstOrDefault();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public int PostLogin(string email, string password)
        {
            try
            {
                SqlParameter emailParam = new SqlParameter("@user_email", email);
                SqlParameter passwordParam = new SqlParameter("@user_pass", password);
                string sqlQuery = "EXECUTE [dbo].[spUserPostLogin] @user_email,@user_pass";
                var userId = _context.SpUserPostLogin.FromSqlRaw(sqlQuery, emailParam, passwordParam)
                                     .ToListAsync().GetAwaiter().GetResult().FirstOrDefault().user_id;
                return userId;
            }
            catch
            {
                return -1;
            }
        }

        public int Register(string name, string email, string password)
        {
            try
            {
                SqlParameter nameParam = new SqlParameter("@user_name", name);
                SqlParameter emailParam = new SqlParameter("@user_email", email);
                SqlParameter passwordParam = new SqlParameter("@user_pass", password);
                string sqlQuery = "EXECUTE [dbo].[spUserRegister] @user_name,@user_email,@user_pass";
                var userId = _context.SpUserRegister.FromSqlRaw(sqlQuery, nameParam, emailParam, passwordParam)
                                     .ToListAsync().GetAwaiter().GetResult().FirstOrDefault().user_id;
                return userId;
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region Portfolios



        #endregion

        #region Transactions



        #endregion

        #region Tickers



        #endregion

        #region Currencies



        #endregion

        #region Transation types



        #endregion
    }
}
