using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TuCartera.DBModel;
using TuCartera.DBModel.Contexts.Entities;
using TuCartera.Models;
using TuCartera.Services;

namespace TuCartera.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        #region Initialization

        private readonly IAdapter _adapter;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly ILogger<PortfoliosController> _logger;

        public PortfoliosController(
            IAdapter adapter,
            IUsersService usersService,
            IMapper mapper,
            ILogger<PortfoliosController> logger
        ) {
            _adapter = adapter;
            _usersService = usersService;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Portfolios


        [HttpGet]
        [Route("")]
        public IActionResult List()
        {
            var userId = _usersService.getLoggedUserId();

            List<IGrouping<int, SpPortfolioItemResult>> portfolios = _adapter.PortfolioList(userId.Value)
                                                                             .GroupBy(pt => pt.portfolio_id)
                                                                             .ToList();
            List<PortfolioDTO> res = _mapper.Map<List<PortfolioDTO>>(portfolios);
            return Ok(res);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Item([FromRoute] int id)
        {
            List<IGrouping<int, SpPortfolioItemResult>> portfolioTickers = _adapter.PortfolioItem(id)
                                                                                   .GroupBy(pt => pt.portfolio_id)
                                                                                   .ToList();
            if (portfolioTickers != null && portfolioTickers.Any())
            {
                PortfolioDTO portfolio = _mapper.Map<List<PortfolioDTO>>(portfolioTickers).FirstOrDefault();
                return Ok(portfolio);
            }
            else
            {
                _logger.LogError("Item: Portfolio does not exist");
                return NotFound();
            }
        }


        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] PortfolioAddParameters param)
        {
            var userId = _usersService.getLoggedUserId();
            int portfolioId = _adapter.PortfolioAdd(
                param.Name,
                param.Description,
                userId.Value,
                param.TickerIds
            );

            if (portfolioId != -1)
            {
                return NoContent();
            }
            else
            {
                _logger.LogError("Add: Portfolio could not be added");
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("")]
        public IActionResult Edit([FromBody] PortfolioEditParameters param)
        {
            int portfolioId = _adapter.PortfolioEdit(
                param.Id,
                param.Name,
                param.Description,
                param.TickerIds
            );

            if (portfolioId != -1)
            {
                return NoContent();
            }
            else
            {
                _logger.LogError("Add: Portfolio could not be edited");
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            int portfolioId = _adapter.PortfolioDelete(id);

            if (portfolioId != -1)
            {
                return NoContent();
            }
            else
            {
                _logger.LogError("Add: Portfolio could not be deleted");
                return BadRequest();
            }
        }

        #endregion
    }
}
