using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Data.DTO;
using Sample.Data.TenantDB;
namespace Sample.Service.Service.Document
{
    public interface IDocumentService
    {
        Task<DocumentDto> GetDocumentById(int id);
        Task<IEnumerable<DocumentDto>> GetAllDocuments();
        Task AddDocument(DocumentDto dto);
        Task UpdateDocument(DocumentDto dto);
        Task DeleteDocument(int id);
    }
}
