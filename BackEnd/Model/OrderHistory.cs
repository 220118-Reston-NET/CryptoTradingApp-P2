namespace Model

{
    public class OrderHistory
    {
    public int customerId { get; set; }
    public string cryptoName { get; set; }
    public decimal buyPrice { get; set; }
    public DateTime buyDate { get; set; }
    public decimal sellPrice { get; set; }
    public DateTime sellDate { get; set; }
    public decimal totalReturn { get; set; }
    }
    
}