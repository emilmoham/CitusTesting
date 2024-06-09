namespace AsyncAPI.Models.Args
{
    public class CreateTransaction
    {
        public int FacilityId { get; set; }

        public CreateTransaction(int facilityId)
        {
            FacilityId = facilityId;
        }
    }
}
