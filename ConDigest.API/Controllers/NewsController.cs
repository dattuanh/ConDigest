using AutoMapper;
using ConDigest.API.Models.Domain;
using ConDigest.API.Models.DTO.NewsDTOs;
using ConDigest.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConDigest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;

        public IMapper _mapper { get; }

        public NewsController(INewsRepository newsRepository, IMapper Imapper)
        {
            _newsRepository = newsRepository;
            _mapper = Imapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNewsAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageSize = 1, [FromQuery] int pageNumber = 1000)
        {
            var news = await _newsRepository.GetAllNewsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageSize, pageNumber);
            
            return Ok(_mapper.Map<List<NewsDto>>(news));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetNewsByIdAsync([FromRoute] Guid id)
        {
            var news = await _newsRepository.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NewsDto>(news));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsAsync([FromBody] AddNewsRequestDto news)
        {
            var newsDomain = _mapper.Map<News>(news);

            await _newsRepository.CreateNewsAsync(newsDomain);

            var createdNews = _mapper.Map<NewsDto>(newsDomain);

            return CreatedAtAction(nameof(GetNewsByIdAsync), new {id = createdNews.Id}, createdNews);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNewsAsync([FromRoute] Guid id, [FromBody] UpdateNewsRequestDto news)
        {
            var newsDomain = _mapper.Map<News>(news);
            var updatedNews = await _newsRepository.UpdateNewsAsync(id, newsDomain);
            if (updatedNews == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NewsDto>(newsDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNewsAsync([FromRoute] Guid id)
        {
            var deletedNews = await _newsRepository.DeleteNewsAsync(id);
            if (deletedNews == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NewsDto>(deletedNews));
        }
    }
}
