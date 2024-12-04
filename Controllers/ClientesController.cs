using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construcaoAPI_INF12.Modelos;
using construcaoAPI_INF12.Data;
using construcaoAPI_INF12.Api;
using construcaoAPI_INF12.Api.NovaPasta;

namespace construcaoAPI_INF12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly LojaClientesDbContext _context;

        public ClienteController(LojaClientesDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientesSemCampoPedido = await _context.Clientes.Select(c => new ClienteDto
            {
                idCliente = c.idCliente,
                nomeCliente = c.nomeCliente,
                sobrenomeCliente = c.sobrenomeCliente,
                emailCliente = c.emailCliente,
                telefoneCliente = c.telefoneCliente,
                enderecoCliente = c.enderecoCliente,
                cidadeCliente = c.cidadeCliente,
                estadoCliente = c.estadoCliente,
                cepCliente = c.cepCliente,
            }).ToListAsync();

            return Ok(clientesSemCampoPedido);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            //var cliente = await _context.Clientes.FindAsync(id);
            var clientesSemCampoPedido = await _context.Clientes.Where(c => c.idCliente == id)
                .Select(c => new ClienteDto
                {
                    idCliente = c.idCliente,
                    nomeCliente = c.nomeCliente,
                    sobrenomeCliente = c.sobrenomeCliente,
                    emailCliente = c.emailCliente,
                    telefoneCliente = c.telefoneCliente,
                    enderecoCliente = c.enderecoCliente,
                    cidadeCliente = c.cidadeCliente,
                    estadoCliente = c.estadoCliente,
                    cepCliente = c.cepCliente,
                }).FirstOrDefaultAsync();

            if (clientesSemCampoPedido == null)
            {
                return NotFound();
            }

            return clientesSemCampoPedido;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDto cliente)
        {
            if (id != cliente.idCliente)
            {
                return BadRequest();
            }

            //pega o cliente atual
            var clienteAtual = await _context.Clientes
                .FirstOrDefaultAsync(c => c.idCliente == id);

            clienteAtual.nomeCliente = cliente.nomeCliente;
            clienteAtual.sobrenomeCliente = cliente.sobrenomeCliente;
            clienteAtual.emailCliente = cliente.emailCliente;
            clienteAtual.telefoneCliente = cliente.telefoneCliente;
            clienteAtual.enderecoCliente = cliente.enderecoCliente;
            clienteAtual.cidadeCliente = cliente.cidadeCliente;
            clienteAtual.estadoCliente = cliente.estadoCliente;
            clienteAtual.cepCliente = cliente.cepCliente;

            _context.Entry(clienteAtual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDto cliente)
        {
            var novoCliente = new Cliente()
            {
                nomeCliente = cliente.nomeCliente,
                sobrenomeCliente = cliente.sobrenomeCliente,
                emailCliente = cliente.emailCliente,
                telefoneCliente = cliente.telefoneCliente,
                enderecoCliente = cliente.enderecoCliente,
                cidadeCliente = cliente.cidadeCliente,
                estadoCliente = cliente.estadoCliente,
                cepCliente = cliente.cepCliente,
                dataCadastroCliente = DateTime.Now
            };

            await _context.Clientes.AddAsync(novoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = novoCliente.idCliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.idCliente == id);
        }
    }
}