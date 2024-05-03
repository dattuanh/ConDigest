using ConDigest.API.Models.DTO.NewsImageDTOs;
using ConDigest.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace ConDigest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsImageController : ControllerBase
    {
        public readonly INewsImageRepository imageRepository;
        public NewsImageController(INewsImageRepository newsImageRepository)
        {
            imageRepository = newsImageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] NewsImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // convert DTO to Domain model
                //var imageDomainModel = new Image
                //{
                //    File = request.File,
                //    FileExtension = Path.GetExtension(request.File.FileName),
                //    FileSizeInBytes = request.File.Length,
                //    FileName = request.FileName,
                //    FileDescription = request.FileDescription,
                //};


                // User repository to upload image
                //await imageRepository.Upload(imageDomainModel);

                //return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }


        private void ValidateFileUpload(NewsImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
