namespace AsyncAPI.Models.DTOs
{
    public class Account {
        public string Name { get; set; }
        public int Number { get; set;}
        public int Balance { get; set; }

        public Account(string name, int number, int balance) {
            Name = name;;
            Number = number;
            Balance = balance;
        }
    }
}