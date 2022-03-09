namespace Model
{
    public class SellOrderHistory
    {
    public int customerId { get; set; }
    public string cryptoName { get; set; }
    public decimal sellPrice { get; set; }
    public DateTime sellDate { get; set; }
    public decimal quantity { get; set; }
    public decimal total { get; set; }
    }
}