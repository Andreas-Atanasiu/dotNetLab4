using Lab2.Models;
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
        private ExpensesDbContext context;

        public ExpenseController(ExpensesDbContext context)
        {
            this.context = context;
        }


       // [HttpGet]
       // public IEnumerable<Expense> GetExpenses()
       // {
       //     return context.Expenses;
       // }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //var existing = context.Expenses.FirstOrDefault(expense => expense.Id == id);
            var existing = context.Expenses.Include(exp => exp.Comments).FirstOrDefault(expense => expense.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }

        [HttpPost]
        public void Post([FromBody] Expense expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var existing = context.Expenses.AsNoTracking().FirstOrDefault(exp => exp.Id == id);

            if (existing == null)
            {
                context.Expenses.Add(expense);
                context.SaveChanges();
                return Ok(expense);
            }

            expense.Id = id;
            context.Expenses.Update(expense);
            context.SaveChanges();
            return Ok(expense);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Expenses.FirstOrDefault(expense => expense.Id == id);

            if (existing == null)
            {
                return NotFound();
            }
            context.Expenses.Remove(existing);
            context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Get all the expenses
        /// </summary>
        /// <param name="from">Optional, filter by min</param>
        /// <param name="to">Optional, filter by max</param>
        /// <param name="type">Optional, filter by type</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Expense> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]TypeEnum? type)
        {
            IEnumerable<Expense> result = context.Expenses; //Include(expense => expense.Comments);

            if (from != null)
            {
                result = result.Where(expense => expense.Date >= from);
            }

            if (to != null)
            {
                result = result.Where(expense => expense.Date <= to);
            }

            if (type != null)
            {
                result = result.Where(expense => expense.Type == type);
            }

            return result;
        }

    }
}

