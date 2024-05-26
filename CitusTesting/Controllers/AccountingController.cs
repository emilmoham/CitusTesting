using AutoMapper;
using CitusTesting.Models;
using CitusTesting.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CitusTesting.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly IFacilitiesRepository _facilitiesRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEntriesRepository _entriesRepository;
        private readonly IMapper _mapper;

        public AccountingController(
            IFacilitiesRepository facilitiesRepository,
            IAccountsRepository accountsRepository,
            ITransactionRepository transactionRepository,
            IEntriesRepository entriesRepository,
            IMapper mapper
        )
        {
            _facilitiesRepository = facilitiesRepository;
            _accountsRepository = accountsRepository;
            _transactionRepository = transactionRepository;
            _entriesRepository = entriesRepository;
            _mapper = mapper;
        }

        [HttpGet("facilities/{id}", Name="GetFacility")]
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
            await _facilitiesRepository.SaveChangesAsync();

            return CreatedAtRoute("GetFacility", new { id = facilityEntity.Id}, facilityEntity);
        }
    }
}
