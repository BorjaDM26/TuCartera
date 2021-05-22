using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SelectorsController> _logger;

        public SelectorsController(
            IAdapter adapter,
            IMapper mapper,
            ILogger<SelectorsController> logger
        ) {
            _adapter = adapter;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Authentication


        [HttpGet]
        [Route("currencies")]
        public IActionResult CurrencyList()
        {
            List<SpCurrencyItemResult> res = _adapter.CurrencyList();

            if (res != null)
            {
                List<CurrencyDTO> currencies = _mapper.Map<List<CurrencyDTO>>(res);
                return Ok(currencies);
            }
            else
            {
                _logger.LogError("CurrencyList: List of currencies could not be loaded");
                return NotFound();
            }
        }


        [HttpGet]
        [Route("tickers")]
        public IActionResult TickerList()
        {
            List<SpTickerItemResult> res = _adapter.TickerList();

            if (res != null)
            {
                List<TickerDTO> tickers = _mapper.Map<List<TickerDTO>>(res);
                return Ok(tickers);
            }
            else
            {
                _logger.LogError("TickerList: List of tickers could not be loaded");
                return NotFound();
            }
        }


        [HttpGet]
        [Route("transaction-types")]
        public IActionResult TransactionTypeList()
        {
            List<SpTransactionTypeItemResult> res = _adapter.TransactionTypeList();

            if (res != null)
            {
                List<TransactionTypeDTO> transactionTypes = _mapper.Map<List<TransactionTypeDTO>>(res);
                return Ok(transactionTypes);
            }
            else
            {
                _logger.LogError("TransactionTypeList: List of transaction types could not be loaded");
                return NotFound();
            }
        }

        #endregion
    }
}
