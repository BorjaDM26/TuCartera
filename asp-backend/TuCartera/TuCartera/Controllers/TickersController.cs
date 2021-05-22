using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuCartera.DBModel;
using TuCartera.DBModel.Contexts.Entities;
using TuCartera.Models;
using TuCartera.Services;

namespace TuCartera.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TickersController : ControllerBase
    {
        #region Initialization

        private readonly IAdapter _adapter;
        private readonly IUsersService _usersService;
        private readonly IFinancialApiService _financialApiService;
        private readonly IMapper _mapper;
        private readonly ILogger<TickersController> _logger;

        public TickersController(
            IAdapter adapter,
            IUsersService usersService,
            IFinancialApiService financialApiService,
            IMapper mapper,
            ILogger<TickersController> logger
        ) {
            _adapter = adapter;
            _usersService = usersService;
            _financialApiService = financialApiService;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Tickers

        [HttpGet]
        [Route("states")]
        public IActionResult TickersState()
        {
            var userId = _usersService.getLoggedUserId();
            List<SpTickerStateResult> tickersState = _adapter.TickersStateList(userId.Value);

            if (tickersState != null)
            {
                List<TickerStateDTO> res = _mapper.Map<List<TickerStateDTO>>(tickersState);
                return Ok(res);
            }
            else
            {
                _logger.LogError("TickersState: List of tickers state could not be loaded");
                return NotFound();
            }
        }


        [HttpGet]
        [Route("values")]
        public async Task<IActionResult> TickersValue()
        {
            var userId = _usersService.getLoggedUserId();

            List<SpTickerItemResult> tickersUsed = _adapter.TickersUsedList(userId.Value);

            if (tickersUsed != null)
            {
                List<TickerDTO> tickersMapped = _mapper.Map<List<TickerDTO>>(tickersUsed);
                var res = await _financialApiService.tickerCurrentValues(tickersMapped);
                return Ok(res);
            }
            else
            {
                _logger.LogError("TickersValue: List of tickers values could not be loaded");
                return NotFound();
            }
        }

        #endregion
    }
}
