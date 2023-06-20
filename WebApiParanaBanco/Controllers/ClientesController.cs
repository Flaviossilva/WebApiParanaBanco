using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using WebApiParanaBanco.Models;
using WebApiParanaBanco.Repositorio;

namespace WebApiParanaBanco.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpPost]
        public IActionResult Criar(Cliente cliente)
        {
            var clienteCriado = _clienteRepository.Criar(cliente);
            return CreatedAtAction(nameof(_clienteRepository.ObterPorId), new { id = clienteCriado.Id }, clienteCriado);
        }
        [HttpGet("All")]
        public IActionResult ObterTodos()
        {
            var clientes = _clienteRepository.ObterTodos();
            return Ok(clientes);
        }


        [HttpGet("DDD+Telefone")]
        public IActionResult ObterPorTelefone(int DDD,int Numero)
        {
            var cliente = _clienteRepository.ObterPorTelefone(DDD,Numero);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        [HttpPut("email")]
        public IActionResult AtualizarEmail(int id, [FromBody] string novoEmail)
        {
            var cliente = _clienteRepository.ObterPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            cliente.Email = novoEmail;
            _clienteRepository.AtualizarEmail(id,novoEmail);
            return Ok(cliente);
        }
        [HttpPut("telefone")]
        public IActionResult AtualizarTelefone(int id, Telefone telefone,int novoDDD,int novoNumero)
        {
            var cliente = _clienteRepository.ObterPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            var telefoneExistente = cliente.Telefones.FirstOrDefault(t => t.DDD == telefone.DDD && t.Numero == telefone.Numero);
            if (telefoneExistente == null)
            {
                return NotFound();
            }
            telefoneExistente.Tipo = telefone.Tipo;
            _clienteRepository.AtualizarTelefone(id,telefoneExistente.Numero,novoDDD,novoNumero);
            return Ok(cliente);
        }
        [HttpDelete("email")]
        public IActionResult Excluir(string email)
        {
            _clienteRepository.Excluir(email);
            return NoContent();
        }


    }

}
