using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly DatabaseHelper dbHelper;

    public DishesController(DatabaseHelper dbHelper)
    {
        dbHelper = dbHelper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
    {
        var query = "SELECT * FROM Dishes";
        var dishes = await dbHelper.ExecuteQueryAsync(query, reader =>
            new Dish
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Price = (decimal)reader["Price"]
            });

        return Ok(dishes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dish>> GetDish(int id)
    {
        var query = $"SELECT * FROM Dishes WHERE Id = {id}";
        var dishes = await dbHelper.ExecuteQueryAsync(query, reader =>
            new Dish
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Price = (decimal)reader["Price"]
            });

        var dish = dishes.FirstOrDefault();
        if (dish == null)
        {
            return NotFound();
        }

        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult<Dish>> PostDish(Dish dish)
    {
        var query = $"INSERT INTO Dishes (Name, Description, Price) VALUES ('{dish.Name}', '{dish.Description}', {dish.Price})";
        var rowsAffected = await dbHelper.ExecuteNonQueryAsync(query);

        if (rowsAffected > 0)
        {
            return CreatedAtAction(nameof(GetDish), new { id = dish.Id }, dish);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDish(int id, Dish dish)
    {
        var query = $"UPDATE Dishes SET Name = '{dish.Name}', Description = '{dish.Description}', Price = {dish.Price} WHERE Id = {id}";
        var rowsAffected = await dbHelper.ExecuteNonQueryAsync(query);

        if (rowsAffected > 0)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDish(int id)
    {
        var query = $"DELETE FROM Dishes WHERE Id = {id}";
        var rowsAffected = await dbHelper.ExecuteNonQueryAsync(query);

        if (rowsAffected > 0)
        {
            return NoContent();
        }

        return NotFound();
    }
}
