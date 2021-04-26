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

        List<SpTransactionItemResult> TransactionList(int userId);
        SpTransactionItemResult TransactionItem(int transactionId);

        int TransactionAdd(int shares, float price, DateTime date, string comment, int userId, int transactionTypeId, int currencyId, int tickerId);
        int TransactionEdit(int transactionId, int shares, float price, DateTime date, string comment, int transactionTypeId, int currencyId, int tickerId);
        int TransactionDelete(int transactionId);

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

        public List<SpTransactionItemResult> TransactionList(int userId)
        {
            try
            {
                SqlParameter userParam = new SqlParameter("@user_id", userId);
                string sqlQuery = "EXECUTE [dbo].[spTransactionList] @user_id";
                var transactions = _context.SpTransactionList.FromSqlRaw(sqlQuery, userParam)
                                     .ToListAsync().GetAwaiter().GetResult();
                return transactions;
            }
            catch
            {
                return null;
            }
        }

        public SpTransactionItemResult TransactionItem(int transactionId)
        {
            try
            {
                SqlParameter transactionParam = new SqlParameter("@transaction_id", transactionId);
                string sqlQuery = "EXECUTE [dbo].[spTransactionItem] @transaction_id";
                var transaction = _context.SpTransactionItem.FromSqlRaw(sqlQuery, transactionParam)
                                     .ToListAsync().GetAwaiter().GetResult().FirstOrDefault();
                return transaction;
            }
            catch
            {
                return null;
            }
        }

        public int TransactionAdd(int shares, float price, DateTime date, string comment, int userId, int transactionTypeId, int currencyId, int tickerId)
        {
            try
            {
                SqlParameter sharesParam = new SqlParameter("@shares", shares);
                SqlParameter priceParam = new SqlParameter("@price", price);
                SqlParameter dateParam = new SqlParameter("@date", date);
                SqlParameter commentParam = !string.IsNullOrEmpty(comment)
                    ? new SqlParameter("@comment", comment)
                    : new SqlParameter("@comment", DBNull.Value);
                SqlParameter userParam = new SqlParameter("@user_id", userId);
                SqlParameter transactionTypeParam = new SqlParameter("@transaction_type_id", transactionTypeId);
                SqlParameter currencyParam = new SqlParameter("@currency_id", currencyId);
                SqlParameter tickerParam = new SqlParameter("@ticker_id", tickerId);
                string sqlQuery = "EXECUTE [dbo].[spTransactionAdd] @shares,@price,@date,@comment,@user_id,@transaction_type_id,@ticker_id,@ticker_id";
                var transactionId = _context.SpTransactionAdd.FromSqlRaw(sqlQuery, sharesParam, priceParam, dateParam, commentParam, userParam, transactionTypeParam, currencyParam, tickerParam)
                                     .ToListAsync().GetAwaiter().GetResult().FirstOrDefault().transaction_id;
                return transactionId;
            }
            catch
            {
                return -1;
            }
        }

        public int TransactionEdit(int transactionId, int shares, float price, DateTime date, string comment, int transactionTypeId, int currencyId, int tickerId)
        {
            try
            {
                SqlParameter transactionParam = new SqlParameter("@transaction_id", transactionId);
                SqlParameter sharesParam = new SqlParameter("@shares", shares);
                SqlParameter priceParam = new SqlParameter("@price", price);
                SqlParameter dateParam = new SqlParameter("@date", date);
                SqlParameter commentParam = !string.IsNullOrEmpty(comment)
                    ? new SqlParameter("@comment", comment)
                    : new SqlParameter("@comment", DBNull.Value);
                SqlParameter transactionTypeParam = new SqlParameter("@transaction_type_id", transactionTypeId);
                SqlParameter currencyParam = new SqlParameter("@currency_id", currencyId);
                SqlParameter tickerParam = new SqlParameter("@ticker_id", tickerId);
                string sqlQuery = "EXECUTE [dbo].[spTransactionEdit] @transaction_id,@shares,@price,@date,@comment,@transaction_type_id,@ticker_id,@ticker_id";
                var res = _context.SpTransactionEdit.FromSqlRaw(sqlQuery, transactionParam, sharesParam, priceParam, dateParam, commentParam, transactionTypeParam, currencyParam, tickerParam)
                                     .ToListAsync().GetAwaiter().GetResult().FirstOrDefault().transaction_id;
                return res;
            }
            catch
            {
                return -1;
            }
        }

        public int TransactionDelete(int transactionId)
        {
            try
            {
                SqlParameter transactionParam = new SqlParameter("@transaction_id", transactionId);
                string sqlQuery = "EXECUTE [dbo].[spTransactionDelete] @transaction_id";
                var res = _context.SpTransactionDelete.FromSqlRaw(sqlQuery, transactionParam)
                                     .ToListAsync().GetAwaiter().GetResult().FirstOrDefault().transaction_id;
                return res;
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region Tickers



        #endregion

        #region Currencies



        #endregion

        #region Transation types



        #endregion
    }
}
