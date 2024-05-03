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
    public class OrderController : ControllerBase
    {
        private readonly ConDigestDBContext dbContext;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderController(ConDigestDBContext _dbContext, 
            IOrderRepository _orderRepository,
            IMapper _mapper)
        {
            this.dbContext = _dbContext;
            this.orderRepository = _orderRepository;
            this.mapper = _mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateOrder([FromBody] AddOrderRequestDto addOrderRequestDto)
        {
            var orderDomainModel = mapper.Map<Order>(addOrderRequestDto);
            var orderItemDomainModels = new List<OrderDetail>();
            foreach (var addOrderItemRequestDto in addOrderRequestDto.OrderDetail)
            {
                var orderItemDomainModel = mapper.Map<OrderDetail>(addOrderItemRequestDto);
                orderItemDomainModels.Add(orderItemDomainModel);
            }

            orderDomainModel = await orderRepository.CreateAsync(orderDomainModel, orderItemDomainModels);

            var orderDto = mapper.Map<OrderDto>(orderDomainModel);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var ordersDomainModel = await orderRepository.GetAllAsync(filterOn, filterQuery, sortBy,
                    isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<OrderDto>>(ordersDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var ordersDomainModel = await orderRepository.GetByIdAsync(id);

            if (ordersDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<OrderDto>(ordersDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedOrderDomainModel = await orderRepository.DeleteAsync(id);

            if (deletedOrderDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<OrderDto>(deletedOrderDomainModel));
        }
    }
}
