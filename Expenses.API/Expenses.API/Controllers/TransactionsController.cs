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
            try
            {
                var allTransactions = transactionservice.GetAll();
                return Ok(allTransactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching transactions", error = ex.Message });
            }
        }

        [HttpGet("Details/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid transaction ID" });
                }

                var transaction = transactionservice.GetById(id);

                if (transaction == null)
                {
                    return NotFound(new { message = "Transaction not found" });
                }

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching transaction", error = ex.Message });
            }
        }

        [HttpPost("Create")]
        public IActionResult CreateTransaction([FromBody] PostTransactionDto payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var oneYearAgo = DateTime.Now.AddYears(-1);

                if (payload.CreatedAt > DateTime.Now)
                {
                    return BadRequest(new { message = "CreatedAt cannot be in the future" });
                }

                if (payload.CreatedAt < oneYearAgo)
                {
                    return BadRequest(new { message = "CreatedAt cannot be older than 1 year" });
                }

                var newTransaction = transactionservice.Add(payload);

                if (newTransaction == null)
                {
                    return NotFound(new { message = "Error creating transaction" });
                }

                return Ok(new { message = "Transaction created successfully", data = newTransaction });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating transaction", error = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] PutTransactionDto payload)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid transaction ID" });
                }


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingTransaction = transactionservice.GetById(id);
                if (existingTransaction == null)
                {
                    return NotFound(new { message = "Transaction not found" });
                }

                if (payload.CreatedAt.HasValue)
                {
                    var oneYearAgo = DateTime.Now.AddYears(-1);

                    if (payload.CreatedAt > DateTime.Now)
                    {
                        return BadRequest(new { message = "CreatedAt cannot be in the future" });
                    }

                    if (payload.CreatedAt < oneYearAgo)
                    {
                        return BadRequest(new { message = "CreatedAt cannot be older than 1 year" });
                    }
                }

                var updatedTransaction = transactionservice.Update(id, payload);

                if (updatedTransaction == null)
                {
                    return NotFound(new { message = "Error updating transaction" });
                }

                return Ok(new { message = "Transaction updated successfully", data = updatedTransaction });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating transaction", error = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid transaction ID" });
                }

                var existingTransaction = transactionservice.GetById(id);
                if (existingTransaction == null)
                {
                    return NotFound(new { message = "Transaction not found" });
                }

                transactionservice.Delete(id);
                return Ok(new { message = "Transaction deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting transaction", error = ex.Message });
            }
        }

    }
}