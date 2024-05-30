namespace AsyncAPI.Models
{
    public class CreateTransactionDto
    {
        public int FacilityId { get; set; }

        public CreateTransactionDto(int facilityId)
        {
            FacilityId = facilityId;
        }
    }
}
