using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuCartera.DBModel;
using TuCartera.DBModel.Contexts.Entities;
using TuCartera.Models;

namespace TuCartera.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SelectorsController : ControllerBase
    {
        #region Initialization

        private readonly IAdapter _adapter;
        private readonly IMapper _mapper;

        public SelectorsController(
            IAdapter adapter,
            IMapper mapper
        ) {
            _adapter = adapter;
            _mapper = mapper;
        }

        #endregion

        #region Authentication


        [HttpGet]
        [Route("currencies")]
        public IActionResult CurrencyList()
        {
            List<SpCurrencyItemResult> res = _adapter.CurrencyList();
            List<CurrencyDTO> currencies = _mapper.Map<List<CurrencyDTO>>(res);
            return Ok(currencies);
        }


        [HttpGet]
        [Route("tickers")]
        public IActionResult TickerList()
        {
            List<SpTickerItemResult> res = _adapter.TickerList();
            List<TickerDTO> tickers = _mapper.Map<List<TickerDTO>>(res);
            return Ok(tickers);
        }


        [HttpGet]
        [Route("transaction-types")]
        public IActionResult TransactionTypeList()
        {
            List<SpTransactionTypeItemResult> res = _adapter.TransactionTypeList();
            List<TransactionTypeDTO> transactionTypes = _mapper.Map<List<TransactionTypeDTO>>(res);
            return Ok(transactionTypes);
        }

        #endregion
    }
}
