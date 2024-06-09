using System.Security.Cryptography.X509Certificates;

namespace AsyncAPI.Models.DTOs
{
    public class BalanceSheet {
        public IEnumerable<Account> Accounts { get; set; }

        public BalanceSheet(IEnumerable<Account> accounts) {
            Accounts = accounts;
        }
    }
}