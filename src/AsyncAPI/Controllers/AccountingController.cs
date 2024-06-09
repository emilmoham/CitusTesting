using AutoMapper;
using AsyncAPI.Models.DTOs;
using AsyncAPI.Models.Args;
using AsyncAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
                new Entities.Account("Assets", 1000, false, facilityEntity.Id, 1000),
                new Entities.Account("Liabilities", 2000, false, facilityEntity.Id, 500),
                new Entities.Account("Equity", 3000, false, facilityEntity.Id, 10000),
                new Entities.Account("Revenue", 4000, false, facilityEntity.Id, 0),
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
    }
}
