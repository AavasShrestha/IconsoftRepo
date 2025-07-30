using CBS.Data.TenantDB;
using Sample.Data.DTO;
using Sample.Data.TenantDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Service.Client
{
    public interface IClientService
    {
        List<ClientDto> GetById(int id);
        Task<List<ClientDto>> GetAllCLients();
        Task<ClientDto> Add(ClientDto dto);
        Task<ClientDto> UpdateClient(ClientDto dto);
        Task<bool> DeleteClient(int id);
    }
}
