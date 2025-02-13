namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Foreign key to the User service
        public string Item { get; set; }
        public decimal Price { get; set; }
    }
}
