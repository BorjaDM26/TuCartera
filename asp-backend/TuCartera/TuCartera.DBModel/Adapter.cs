using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TuCartera.DBModel.Contexts;
using TuCartera.DBModel.Contexts.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using TuCartera.DBModel.Contexts.StoreProcedures;

namespace TuCartera.DBModel
{
    public interface IAdapter
    {
        #region Users



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
