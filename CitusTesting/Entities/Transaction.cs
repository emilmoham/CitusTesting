namespace CitusTesting.Entities
{
    public class Transaction
    {
        public long Id { get; set; }
        public int FacilityId { get; set; }

        public Transaction(int facilityId)
        {
            FacilityId = facilityId;
        }
    }
}
