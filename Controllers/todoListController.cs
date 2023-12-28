using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Services;

namespace todoList.Controllers;

[ApiController]
[Route("[controller]")]
public class todoListController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<task>> Get()
    {
        return todoListServices.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<task> Get(int id)
    {
        var task = todoListServices.GetById(id);
        if (task == null)
            return NotFound();
        return task;
    }

    [HttpPost]
    public ActionResult Post(task newTask)
    {
        var newId = todoListServices.Add(newTask);

        return CreatedAtAction("Post", 
            new {id = newId}, todoListServices.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,task newTask)
    {
        var result = todoListServices.Update(id, newTask);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
