namespace AsyncAPI.Models.Args
{
    public class CreateTransaction
    {
        public int FacilityId { get; set; }
        public int CreditAccountId { get; set; }
        public int DebitAccountId { get; set; }
        public int Amount { get; set; }

        public CreateTransaction(int facilityId, int creditAccountId, int debitAccountId, int amount)
        {
            FacilityId = facilityId;
            CreditAccountId = creditAccountId;
            DebitAccountId = debitAccountId;
            Amount = amount;
        }
    }
}
