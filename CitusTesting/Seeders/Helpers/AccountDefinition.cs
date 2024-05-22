namespace CitusTesting.Seeders.Helpers
{
    public class AccountDefinition
    {

        public string Name { get; set; }
        public int Number { get; set; }
        public bool CreditNormal { get; set; }

        public AccountDefinition(string name, int number, bool creditNormal)
        {
            Name = name;
            Number = number;
            CreditNormal = creditNormal;
        }

        public static AccountDefinition[] StandardAccounts= new AccountDefinition[]
        {
            new AccountDefinition("Assets",       1000, false),
            new AccountDefinition("Liabilities",  2000, true),
            new AccountDefinition("Equity",       3000, true),
            new AccountDefinition("Revenue",      4000, true),
            new AccountDefinition("Expenses",     5000, false)
        };
    }
}
