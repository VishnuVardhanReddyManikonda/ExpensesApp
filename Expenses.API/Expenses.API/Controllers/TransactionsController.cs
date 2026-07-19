using Expenses.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Expenses.API.Models;
using Expenses.API.Dtos;
using Expenses.API.Data.Services;
using Microsoft.AspNetCore.Cors;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TransactionsController(ITransactionService transactionservice) : ControllerBase
    {

        [HttpGet("All")]

        public IActionResult GetAll()
        {
            var allTransactions = transactionservice.GetAll();
            return Ok(allTransactions);
        }


        [HttpGet("Details/{id}")]

        public IActionResult Get(int id)
        {
            var transaction = transactionservice.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction); 
        }
        [HttpPost("Create")]
        public IActionResult CreateTransaction([FromBody] PostTransactionDto payload)
        {

            var newTransaction = transactionservice.Add(payload);

            if (newTransaction == null)
            {  return NotFound(); }
            return Ok();
        }
        [HttpPut("Update/{id}")]
        public IActionResult UpdateTransaction(int id ,[FromBody] PutTransactionDto payload)
        {
            var updatedTransaction = transactionservice.Update(id, payload);
            if (updatedTransaction == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("Delete/{id}")]

        public IActionResult DeleteTransaction(int id)
        {
            transactionservice.Delete(id);
            return Ok();

        }

    }
}
