namespace AsyncAPI.Models
{
    public class CreateEntryDto
    {
        public int FacilityId { get; set; }
        public long TransactionId { get; set; }
        public int AccountId { get; set; }
        public int Credit { get; set; }
        public int Debit { get; set; }

        public CreateEntryDto(int facilityId, long transactionId, int accountId, int credit, int debit)
        {
            FacilityId = facilityId;
            TransactionId = transactionId;
            AccountId = accountId;
            Credit = credit;
            Debit = debit;
        }
    }
}
