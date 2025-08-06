using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Data.DTO;
using Sample.Service.Service.Document;
using System.Security.Claims;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<List<DocumentDto>> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocuments();
            return documents.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var documentDto = await _documentService.GetDocumentById(id);
            if (documentDto == null)
            {
                return NotFound();
            }
            return Ok(documentDto);
        }

        // Upload multiple files, userId is read from JWT token claims automatically
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> UploadDocuments(
            [FromForm] int clientId,
            [FromForm] string documentType,
            [FromForm] List<IFormFile> files)
        {
            if (files == null || !files.Any())
                return BadRequest("No files uploaded.");

            // Extract userId from JWT claims (case sensitive)
            var userIdClaim = User.FindFirst("userId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized("Invalid User ID in token.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uploadedFiles = new List<object>();

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var documentDto = new DocumentDto
                {
                    ClientId = clientId,
                    DocumentType = documentType,
                    FileName = fileName,
                    CreatedBy = userId, // Assuming CreatedBy is the UserId
                    //UserId = userId
                };

                await _documentService.AddDocument(documentDto);

                uploadedFiles.Add(new { message = "File uploaded successfully.", fileName = fileName });
            }

            return Ok(new { count = uploadedFiles.Count, files = uploadedFiles });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentDto documentDto)
        {
            if (documentDto == null || documentDto.Id != id)
                return BadRequest("Document data is invalid.");

            await _documentService.UpdateDocument(documentDto);
            return Ok(documentDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _documentService.GetDocumentById(id);
            if (document == null)
                return NotFound();

            await _documentService.DeleteDocument(id);
            return NoContent();
        }
    }
}
