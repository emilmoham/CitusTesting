using AutoMapper;
using AsyncAPI.Models.DTOs;
using AsyncAPI.Models.Args;
using AsyncAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsyncAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly IFacilitiesRepository _facilitiesRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IEntriesRepository _entriesRepository;
        private readonly IMapper _mapper;

        public AccountingController(
            IFacilitiesRepository facilitiesRepository,
            IAccountsRepository accountsRepository,
            ITransactionsRepository transactionsRepository,
            IEntriesRepository entriesRepository,
            IMapper mapper
        )
        {
            _facilitiesRepository = facilitiesRepository;
            _accountsRepository = accountsRepository;
            _transactionsRepository = transactionsRepository;
            _entriesRepository = entriesRepository;
            _mapper = mapper;
        }
        
        [HttpGet("facilities/{id}", Name = "GetFacility")]
        public async Task<IActionResult> GetFacility(int id)
        {
            Entities.Facility? entity = await _facilitiesRepository.GetFacilityAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost("facilities")]
        public async Task<IActionResult> CreateFacility([FromBody] CreateFacility facilityForCreation)
        {
            Entities.Facility facilityEntity = _mapper.Map<Entities.Facility>(facilityForCreation);

            _facilitiesRepository.AddFacility(facilityEntity);
            if (await _facilitiesRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Facility could not be created");


            Entities.Account[] facilityAccounts = new Entities.Account[] {
                new Entities.Account("Assets", 1000, false, facilityEntity.Id, 10500),
                new Entities.Account("Liabilities", 2000, true, facilityEntity.Id, 500),
                new Entities.Account("Equity", 3000, true, facilityEntity.Id, 10000),
                new Entities.Account("Revenue", 4000, true, facilityEntity.Id, 0),
                new Entities.Account("Expenses", 5000, false, facilityEntity.Id, 0)
            };
            
            _accountsRepository.AddAccounts(facilityAccounts);
            if (await _accountsRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Accounts could not be created for facility");

            return CreatedAtRoute("GetFacility", new { id = facilityEntity.Id }, facilityEntity);
        }

        [HttpGet("facilities/{id}/balances")]
        public async Task<IActionResult> GetBalances(int id) {
            IEnumerable<Entities.Account> facilityAccounts = await _accountsRepository.GetAccountsAsync(id);
            
            if (!facilityAccounts.Any()) return NotFound();

            IEnumerable<Account> accounts = facilityAccounts.Select(a => new Account(a.Name, a.Number, a.Balance));

            BalanceSheet balanceSheet = new BalanceSheet(accounts);

            return Ok(balanceSheet);
        }

        [HttpPost("facilities/{id}/createTransaction")]
        public async Task<IActionResult> CreateTransaction(int id, [FromBody] CreateTransaction transactionForCreation)
        {
            if (id != transactionForCreation.FacilityId) 
                return BadRequest("FacilityIds do not match");

            Entities.Transaction transaction = _mapper.Map<Entities.Transaction>(transactionForCreation);

            IEnumerable<Entities.Account> facilityAccounts = await _accountsRepository.GetAccountsAsync(transactionForCreation.FacilityId);
            Entities.Account? creditAccount = facilityAccounts.FirstOrDefault(a => a.Id == transactionForCreation.CreditAccountId);
            Entities.Account? debitAccount = facilityAccounts.FirstOrDefault(a => a.Id == transactionForCreation.DebitAccountId);
            if (creditAccount == null || debitAccount == null)
                return BadRequest("Account(s) not found at this facility");

            _transactionsRepository.AddTransaction(transaction);
            if (await _transactionsRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Transaction could not be created");

            Entities.Entry[] transactionEntries = new Entities.Entry[]
            {
                new Entities.Entry(transaction.FacilityId, transaction.Id, transactionForCreation.CreditAccountId, transactionForCreation.Amount, 0),
                new Entities.Entry(transaction.FacilityId, transaction.Id, transactionForCreation.DebitAccountId, 0, transactionForCreation.Amount),
            };

            _entriesRepository.AddEntries(transactionEntries);
            if (await _entriesRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Transaction entries could not be created");

            creditAccount.Balance = creditAccount.Balance + (transactionForCreation.Amount * (creditAccount.Type ? 1 : -1));
            debitAccount.Balance = debitAccount.Balance + (transactionForCreation.Amount * (debitAccount.Type ? -1 : 1));

            Entities.Account[] accountsToUpdate = new Entities.Account[]
            {
                creditAccount,
                debitAccount
            };

            _accountsRepository.UpdateAccounts(accountsToUpdate);

            if (await _accountsRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Account balances could not be updated");

            return Ok();
        }

    }
}
