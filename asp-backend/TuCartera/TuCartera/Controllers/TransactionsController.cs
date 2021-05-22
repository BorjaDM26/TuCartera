using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TuCartera.DBModel;
using TuCartera.DBModel.Contexts.Entities;
using TuCartera.Models;
using TuCartera.Services;

namespace TuCartera.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        #region Initialization

        private readonly IAdapter _adapter;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(
            IAdapter adapter,
            IUsersService usersService,
            IMapper mapper,
            ILogger<TransactionsController> logger
        ) {
            _adapter = adapter;
            _usersService = usersService;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Transactions


        [HttpGet]
        [Route("")]
        public IActionResult List()
        {
            var userId = _usersService.getLoggedUserId();

            List<SpTransactionItemResult> res = _adapter.TransactionList(userId.Value);
            List<TransactionDTO> transactions = _mapper.Map<List<TransactionDTO>>(res);
            return Ok(transactions);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Item([FromRoute] int id)
        {
            SpTransactionItemResult res = _adapter.TransactionItem(id);

            if (res != null)
            {
                TransactionDTO transactions = _mapper.Map<TransactionDTO>(res);
                return Ok(transactions);
            }
            else
            {
                _logger.LogError("Item: Transaction does no exist");
                return NotFound();
            }
        }


        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] TransactionAddParameters param)
        {
            var userId = _usersService.getLoggedUserId();
            int transactionId = _adapter.TransactionAdd(
                param.Shares, 
                param.UnitPrice,
                param.ExchangeToUSD,
                param.Date, 
                param.Comment, 
                userId.Value, 
                param.TransactionTypeId, 
                param.CurrencyId, 
                param.TickerId
            );

            if(transactionId != -1)
            {
                return NoContent();
            } 
            else
            {
                _logger.LogError("Add: Transaction could not be added");
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("")]
        public IActionResult Edit([FromBody] TransactionEditParameters param)
        {
            int transactionId = _adapter.TransactionEdit(
                param.Id,
                param.Shares,
                param.UnitPrice,
                param.ExchangeToUSD,
                param.Date,
                param.Comment,
                param.TransactionTypeId,
                param.CurrencyId,
                param.TickerId
            );

            if (transactionId != -1)
            {
                return NoContent();
            }
            else
            {
                _logger.LogError("Add: Transaction could not be updated");
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            int transactionId = _adapter.TransactionDelete(id);

            if (transactionId != -1)
            {
                return NoContent();
            }
            else
            {
                _logger.LogError("Add: Transaction could not be deleted");
                return BadRequest();
            }
        }

        #endregion
    }
}
