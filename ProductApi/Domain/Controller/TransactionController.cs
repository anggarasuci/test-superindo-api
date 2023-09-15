using Microsoft.AspNetCore.Mvc;
using ProductApi.Data.Entity;
using ProductApi.Data.Service;

namespace ProductApi.Domain.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService service;

        public TransactionController(ITransactionService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<List<TransactionEntity>> Get()
        {
            return await service.GetTransactions();
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<TransactionEntity>> Get(string transactionId)
        {
            var detail = await service.GetTransactionByIdAsync(transactionId);
            if (detail is null)
            {
                return NotFound();
            }
            return detail;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TransactionEntity transactionEntity)
        {
            await service.AddTransaction(transactionEntity);
            return CreatedAtAction(nameof(Get), new
            {
                id = transactionEntity.Id
            }, transactionEntity);
        }

        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> Delete(string transactionId)
        {
            var result = await service.GetTransactionByIdAsync(transactionId);
            if (result is null)
            {
                return NotFound();
            }
            await service.DeleteTransaction(transactionId);
            return Ok();
        }
    }
}

