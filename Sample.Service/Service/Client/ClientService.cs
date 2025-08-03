using CBS.Data.TenantDB;  
using CBS.Repository;     
using Sample.Data.DTO;     
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Data.TenantDB;

namespace Sample.Service.Service.Client
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ClientDto> Add(ClientDto dto)
        {
            var client = new Sample.Data.TenantDB.Client
            {
                ClientName = dto.ClientName,
                ClientType = dto.ClientType,
                OrganizationName = dto.OrganizationName,
                Email = dto.Email,
                Country = dto.Country,
                Mobile = dto.Mobile,
                Gender = dto.Gender,
                City = dto.City,
                Description = dto.Description
            };

            _unitOfWork.ClientRepository.Add(client);
            _unitOfWork.Commit();

            

            return Task.FromResult(dto);
        }

        public Task<bool> DeleteClient(int id)
        {
            // Get entity by id
            var client = _unitOfWork.ClientRepository.GetQuerable()
                .FirstOrDefault(c => c.ClientId == id);

            if (client == null)
                return Task.FromResult(false);

            _unitOfWork.ClientRepository.Delete(client);
            _unitOfWork.Commit();

            return Task.FromResult(true);
        }

        public Task<List<ClientDto>> GetAllCLients()
        {
            var clients = _unitOfWork.ClientRepository.GetQuerable();

            var clientDtos = clients.Select(c => new ClientDto
            {
                Id = c.ClientId,
                ClientName = c.ClientName,
                ClientType = c.ClientType,
                OrganizationName = c.OrganizationName,
                Email = c.Email,
                Country = c.Country,
                Mobile = c.Mobile,
                Gender = c.Gender,
                City = c.City,
                Description = c.Description
            }).ToList();

            return Task.FromResult(clientDtos);
        }

        public List<ClientDto> GetById(int id)
        {
            var client = _unitOfWork.ClientRepository.GetQuerable()
                .FirstOrDefault(c => c.ClientId == id);

            if (client == null)
                return null;

            var dto = new ClientDto
            {
                ClientName = client.ClientName,
                ClientType = client.ClientType,
                OrganizationName = client.OrganizationName,
                Email = client.Email,
                Country = client.Country,
                Mobile = client.Mobile,
                Gender = client.Gender,
                City = client.City,
                Description = client.Description
            };

            return new List<ClientDto> { dto };
        }

        public Task<ClientDto> UpdateClient(ClientDto dto)
        {
            var client = _unitOfWork.ClientRepository.GetQuerable()
                .FirstOrDefault(c => c.ClientId == dto.Id);  // Assume your ClientDto has Id

            if (client == null)
                return Task.FromResult<ClientDto>(null);

            // Update fields
            client.ClientName = dto.ClientName;
            client.ClientType = dto.ClientType;
            client.OrganizationName = dto.OrganizationName;
            client.Email = dto.Email;
            client.Country = dto.Country;
            client.Mobile = dto.Mobile;
            client.Gender = dto.Gender;
            client.City = dto.City;
            client.Description = dto.Description;

            _unitOfWork.ClientRepository.Update(client);
            _unitOfWork.Commit();

            return Task.FromResult(dto);
        }
    }
}
