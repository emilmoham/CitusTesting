namespace AsyncAPI.Models.Args
{
    public class CreateAccount
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public bool Type { get; set; }

        public int FacilityId { get; set; }

        public int Balance { get; set; }
        
        public CreateAccount(string name, int number, bool type, int facilityId, int balance)
        {
            Name = name;
            Number = number;
            Type = type;
            FacilityId = facilityId;
            Balance = balance;
        }
    }
}
