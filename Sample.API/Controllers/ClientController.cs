using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Data.DTO;
using Sample.Service.Service.Client;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAllClients()
        {
            var clients = await _clientService.GetAllCLients();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var clientDtos = _clientService.GetById(id);
            if (clientDtos == null || clientDtos.Count == 0)
                return NotFound();

            // Assuming GetById returns a List<ClientDto> with a single item
            return Ok(clientDtos[0]);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
                return BadRequest("Client data is null.");

            var createdClient = await _clientService.Add(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDto clientDto)
        {
            if (clientDto == null || clientDto.Id != id)
                return BadRequest("Client data is invalid.");

            var updatedClient = await _clientService.UpdateClient(clientDto);
            if (updatedClient == null)
                return NotFound();

            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _clientService.DeleteClient(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
