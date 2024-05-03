using AutoMapper;
using ConDigest.API.CustomActionFilters;
using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using ConDigest.API.Models.DTO;
using ConDigest.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConDigest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ConDigestDBContext dbContext;
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;

        public PaymentController(ConDigestDBContext _dbContext,
            IPaymentRepository _paymentRepository,
            IMapper _mapper)
        {
            this.dbContext = _dbContext;
            this.paymentRepository = _paymentRepository;
            this.mapper = _mapper;
        }


        [HttpGet]
        [Route("admin")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var paymentsDomain = await paymentRepository.GetAllAsync(filterOn, filterQuery, sortBy,
                    isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<PaymentDto>>(paymentsDomain));
        }

        [HttpGet]
        [Route("admin/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var paymentDomain = await paymentRepository.GetByIdAsync(id);

            if (paymentDomain == null)
            {
                return NotFound();
            }

            // Return DTO back to client
            return Ok(mapper.Map<PaymentDto>(paymentDomain));
        }

        [HttpPost]
        [Route("admin")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddPaymentRequestDto addPaymentRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var paymentDomainModel = mapper.Map<Payment>(addPaymentRequestDto);

            // Use Domain Model to create Region
            paymentDomainModel = await paymentRepository.CreateAsync(paymentDomainModel);

            // Map Domain model back to DTO
            var regionDto = mapper.Map<PaymentDto>(paymentDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("admin/{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePaymentRequestDto updatePaymentRequestDto)
        {

            // Map DTO to Domain Model
            var paymentDomainModel = mapper.Map<Payment>(updatePaymentRequestDto);

            // Check if region exists
            paymentDomainModel = await paymentRepository.UpdateAsync(id, paymentDomainModel);

            if (paymentDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PaymentDto>(paymentDomainModel));
        }

        [HttpDelete]
        [Route("admin/{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var paymentDomainModel = await paymentRepository.DeleteAsync(id);

            if (paymentDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PaymentDto>(paymentDomainModel));
        }

    }
}
