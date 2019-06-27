using Lab2.DTOs;
using Lab2.Models;
using Lab2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {

        private IExpenseService expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }


       // [HttpGet]
       // public IEnumerable<Expense> GetExpenses()
       // {
       //     return context.Expenses;
       // }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {   
            var found = expenseService.GetById(id);

            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }


        [HttpPost]
        public void Post([FromBody] PostExpenseDto expense)
        {
            expenseService.Create(expense);   
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var result = expenseService.Upsert(id, expense);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var result = expenseService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// Get all the expenses
        /// </summary>
        /// <param name="from">Optional, filter by min</param>
        /// <param name="to">Optional, filter by max</param>
        /// <param name="type">Optional, filter by type</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<GetExpenseDto> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]TypeEnum? type)
        {
            return expenseService.GetAll(from, to, type);

        }

    }
}

