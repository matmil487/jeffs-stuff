using Marten;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;

namespace ShoppingApi.Controllers;

public class ShoppingListController : ControllerBase
{
    private readonly IDocumentSession _session;

    public ShoppingListController(IDocumentSession sesson)
    {
        this._session = sesson;
    }


    // GET /shopping-list
    [HttpGet("/shopping-list")]
    public async Task<ActionResult> GetShoppingList()
    {
        var response = await _session.Query<ShoppingItem>().ToListAsync();

        if (response is null)
        {
            return Ok(new List<ShoppingItem>());

        }
        return Ok(response);
    }

    [HttpPost("/shopping-list")]
    public async Task<ActionResult> AddShoppingItem([FromBody] ShoppingItemCreate request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newItem = new ShoppingItem(Guid.NewGuid(), request.Name);
        _session.Store(newItem);
        await _session.SaveChangesAsync();
        return Ok(newItem);
    }

}
