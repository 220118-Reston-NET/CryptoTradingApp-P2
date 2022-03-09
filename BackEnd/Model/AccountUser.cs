namespace Model
{
    public class AccountUser
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public DateTime dateCreated { get; set; }
        public int isBanned { get; set; }
        public int isAdmin { get; set; }
        public byte[] _password;
        
    }
}