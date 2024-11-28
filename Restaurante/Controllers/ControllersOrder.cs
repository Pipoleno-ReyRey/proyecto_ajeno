using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly DatabaseHelper _dbHelper;

    public OrdersController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var query = "SELECT * FROM Orders";
        var orders = await _dbHelper.ExecuteQueryAsync(query, reader =>
            new Order
            {
                Id = (int)reader["Id"],
                Date = (DateTime)reader["Date"],
                CustomerName = reader["CustomerName"].ToString(),
                TotalAmount = (decimal)reader["TotalAmount"]
            });

        return Ok(orders);
    }
}

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var query = $"SELECT * FROM Orders WHERE Id = {id}";
        var orders = await _dbHelper.ExecuteQueryAsync(query, reader =>
            new Order
            {
                Id = (int)reader["Id"],
                Date = (DateTime)reader["Date"],
                CustomerName = reader["CustomerName"].ToString(),
                TotalAmount = (decimal)reader["TotalAmount"]
            });

        var order = orders.FirstOrDefault();
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        var query = $"INSERT INTO Orders (CustomerName, Date, TotalAmount) VALUES ('{order.CustomerName}', '{order.Date}', {order.TotalAmount})";
        var rowsAffected = await _dbHelper.ExecuteNonQueryAsync(query);

        if (rowsAffected > 0)
        {
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        var query = $"UPDATE Orders SET CustomerName = '{order.CustomerName}', Date = '{order.Date}', TotalAmount = {order.TotalAmount} WHERE Id = {id}";
        var rowsAffected = await _dbHelper.ExecuteNonQueryAsync(query);

        if (rowsAffected > 0)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var query = $"DELETE FROM
    }
