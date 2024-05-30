using AutoMapper;
using AsyncAPI.Models;
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
        public async Task<IActionResult> CreateFacility([FromBody] CreateFacilityDto facilityForCreation)
        {
            Entities.Facility facilityEntity = _mapper.Map<Entities.Facility>(facilityForCreation);

            _facilitiesRepository.AddFacility(facilityEntity);
            if (await _facilitiesRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Facility could not be created");

            return CreatedAtRoute("GetFacility", new { id = facilityEntity.Id }, facilityEntity);
        }

        [HttpGet("accounts/{id}", Name = "GetAccount")]
        public async Task<IActionResult> GetAccount(int id)
        {
            Entities.Account? entity = await _accountsRepository.GetAccountAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto accountForCreation)
        {
            Entities.Account accountEntity = _mapper.Map<Entities.Account>(accountForCreation);

            _accountsRepository.AddAccount(accountEntity);
            if (await _facilitiesRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Account could not be created");

            return CreatedAtRoute("GetAccount", new { id = accountEntity.Id }, accountEntity);
        }

        [HttpGet("transactions/{id}", Name = "GetTransaction")]
        public async Task<IActionResult> Gettransaction(int id)
        {
            Entities.Transaction? entity = await _transactionsRepository.GetTransactionAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost("transactions")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto transactionForCreation)
        {
            Entities.Transaction transactionEntity = _mapper.Map<Entities.Transaction>(transactionForCreation);

            _transactionsRepository.AddTransaction(transactionEntity);
            if (await _facilitiesRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Transaction could not be created");

            return CreatedAtRoute("GetTransaction", new { id = transactionEntity.Id }, transactionEntity);
        }

        [HttpGet("entries/{id}", Name = "GetEntries")]
        public async Task<IActionResult> GetEntries(int id)
        {
            Entities.Entry? entity = await _entriesRepository.GetEntryAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost("entries")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryDto entryForCreation)
        {
            Entities.Entry entryEntity = _mapper.Map<Entities.Entry>(entryForCreation);

            _entriesRepository.AddEntry(entryEntity);
            if (await _facilitiesRepository.SaveChangesAsync() == false)
                return StatusCode(500, "Entry could not be created");

            return CreatedAtRoute("GetFacility", new { id = entryEntity.Id }, entryEntity);
        }
    }
}
