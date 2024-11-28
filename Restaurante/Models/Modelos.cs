namespace Models{
    
public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public List<Dish> Dishes { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalAmount 
    { 
        get { return Dishes.Sum(dish => dish.Price); } 
    }
}
}