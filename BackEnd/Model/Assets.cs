namespace Model
{
    public class Assets
    {
        public int customerId { get; set; }
        public string cryptoName { get; set; }
        public decimal buyPrice { get; set; }
        public DateTime buyDate { get; set; }
        public decimal stoploss { get; set; }
        public decimal takeprofit { get; set; }
    }
}