﻿namespace CitusTesting.Entities
{
    public class Entry
    {
        public long Id { get; set; }
        public int FacilityId { get; set; }
        public long TransactionId { get; set; }
        public int AccountId { get; set; }
        public int Credit { get; set; }
        public int Debit { get; set; }
    }
}