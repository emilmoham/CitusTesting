namespace AsyncAPI.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public bool Type { get; set; }

        public int FacilityId { get; set; }

        public int Balance { get; set; }

        public Account(string name, int number, bool type, int facilityId, int balance)
        {
            Name = name;
            Number = number;
            Type = type;
            FacilityId = facilityId;
            Balance = balance;
        }
    }
}
