namespace AsyncAPI.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public bool Type { get; set; }

        public int FacilityId { get; set; }

        public Account(string name, int number, bool type, int facilityId)
        {
            Name = name;
            Number = number;
            Type = type;
            FacilityId = facilityId;
        }
    }
}
