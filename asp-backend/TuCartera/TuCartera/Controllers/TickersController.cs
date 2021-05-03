using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TickersController(
            IAdapter adapter,
            IUsersService usersService,
            IFinancialApiService financialApiService,
            IMapper mapper
        ) {
            _adapter = adapter;
            _usersService = usersService;
            _financialApiService = financialApiService;
            _mapper = mapper;
        }

        #endregion

        #region Tickers

        [HttpGet]
        [Route("states")]
        public IActionResult TickersState()
        {
            var userId = _usersService.getLoggedUserId();

            List<SpTickerStateResult> tickersUsed = _adapter.TickersStateList(userId.Value);
            List<TickerStateDTO> res = _mapper.Map<List<TickerStateDTO>>(tickersUsed);

            return Ok(res);
        }


        [HttpGet]
        [Route("values")]
        public async Task<IActionResult> TickersValue()
        {
            var userId = _usersService.getLoggedUserId();

            List<SpTickerItemResult> tickersUsed = _adapter.TickersUsedList(userId.Value);
            List<TickerDTO> tickersMapped = _mapper.Map<List<TickerDTO>>(tickersUsed);
            var res = await _financialApiService.tickerCurrentValues(tickersMapped);

            return Ok(res);
        }

        #endregion
    }
}
