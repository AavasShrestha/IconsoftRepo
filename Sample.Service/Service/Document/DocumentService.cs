using CBS.Repository;
using Sample.Data.DTO;
using Sample.Data.TenantDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Service.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddDocument(DocumentDto dto)
        {
            var document = new Sample.Data.TenantDB.Document
            {
                Id = dto.Id,
                ClientId = dto.ClientId,
                DocumentType = dto.DocumentType,
                FileName = dto.FileName,
                CreatedDate = DateTime.Now,
                CreatedBy = dto.CreatedBy, // Assuming CreatedBy is the UserId
            };

            _unitOfWork.DocumentRepository.Add(document);
            _unitOfWork.Commit();
            // dto.Id = document.Id; 

            return Task.FromResult(dto);

        }

        public Task DeleteDocument(int id)
        {
            var docuemnt = _unitOfWork.DocumentRepository.GetQuerable()
                .FirstOrDefault(d => d.Id == id);

            if (docuemnt == null)
                return Task.FromResult(false);

            _unitOfWork.DocumentRepository.Delete(docuemnt);
            _unitOfWork.Commit();

            return Task.FromResult(true);
        }

        public Task<IEnumerable<DocumentDto>> GetAllDocuments()
        {
            var documents = _unitOfWork.DocumentRepository.GetQuerable();
            var documentDtos = documents.Select(d => new DocumentDto
            {
                Id = d.Id,
                ClientId = d.ClientId,
                DocumentType = d.DocumentType,
                FileName = d.FileName,
                CreatedBy = d.CreatedBy
            });

            return Task.FromResult(documentDtos.AsEnumerable());
        }

        public Task<DocumentDto> GetDocumentById(int id)
        {
            var document = _unitOfWork.DocumentRepository.GetQuerable()
                .FirstOrDefault(d => d.Id == id);

            if (document == null)
                return null;

            var documentDto = new DocumentDto
            {
                Id = document.Id,
                ClientId = document.ClientId,
                DocumentType = document.DocumentType,
                FileName = document.FileName,
                CreatedBy = document.CreatedBy
            };

            return Task.FromResult(documentDto);
        }

        public Task UpdateDocument(DocumentDto dto)
        {
            var document = _unitOfWork.DocumentRepository.GetQuerable()
                .FirstOrDefault(d => d.Id == dto.Id);

            if (document == null)
                return Task.FromResult<DocumentDto>(null);

            document.ClientId = dto.ClientId;
            document.DocumentType = dto.DocumentType;
            document.FileName = dto.FileName;
            document.CreatedBy = dto.CreatedBy; // Assuming CreatedBy is the UserId


            _unitOfWork.DocumentRepository.Update(document);
            _unitOfWork.Commit();
            return Task.FromResult(dto);

        }
    }
}
