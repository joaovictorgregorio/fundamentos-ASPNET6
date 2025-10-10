using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context)
            => Ok(context.Todos.ToList());

        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var todos = context.Todos.FirstOrDefault(x => x.Id == id);

            if (todos == null)
                return NotFound();

            return Ok(todos);
        }

        [HttpPost("/")]
        public IActionResult Post([FromBody] List<TodoModel> todos, [FromServices] AppDbContext context)
        {
            context.Todos.AddRange(todos);
            context.SaveChanges();

            return Created(uri: $"/", value: todos);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/{id:int}")] 
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
                return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
} 