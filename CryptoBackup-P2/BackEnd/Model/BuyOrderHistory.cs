namespace Model

{
    public class BuyOrderHistory
    {
    public int customerId { get; set; }
    public string cryptoName { get; set; }
    public decimal buyPrice { get; set; }
    public DateTime buyDate { get; set; }
    public decimal quantity { get; set; }
    public decimal total { get; set; }
    }
    
}