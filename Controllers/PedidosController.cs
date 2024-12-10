using construcaoAPI_INF12.Api.Dto;
using construcaoAPI_INF12.Api.NovaPasta;
using construcaoAPI_INF12.Data;
using construcaoAPI_INF12.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace construcaoAPI_INF12.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly LojaClientesDbContext _context;

        public PedidosController(LojaClientesDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var resultPedidos = await _context.Pedidos
                .Include(p => p.itensPedidos)
                .Select(p => new PedidoGetDto
                {
                    idPedido = p.idPedido,
                    idCliente = p.idCliente,
                    dataPedido = p.dataPedido,
                    statusPedido = p.statusPedido,
                    valorTotalPedido = p.valorTotalPedido,
                    observacoesPedido = p.observacoesPedido,
                    itensPedido = p.itensPedidos.Select(ip => new ItensPedidoGetDto
                    {
                        idItemPedido = ip.idItemPedido,
                        nomeProduto = ip.Produto.nomeProduto,
                    }
                                    ).ToList()
                })
                .ToListAsync();
            return Ok(resultPedidos);
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoGetDto>> GetPedido(int id)
        {

            var resultPedidos = await _context.Pedidos
                .Include(p => p.itensPedidos)
                .Where(p => p.idPedido == id)
                .Select(p => new PedidoGetDto
                {
                    idCliente = p.idCliente,
                    dataPedido = p.dataPedido,
                    statusPedido = p.statusPedido,
                    valorTotalPedido = p.valorTotalPedido,
                    observacoesPedido = p.observacoesPedido,
                    itensPedido = p.itensPedidos.Select(ip => new ItensPedidoGetDto
                    {
                        idItemPedido = ip.idItemPedido,
                        nomeProduto = ip.Produto.nomeProduto,
                    }
                                    ).ToList()
                })
                .FirstOrDefaultAsync();

            if (resultPedidos == null)
            {
                return NotFound();
            }

            return resultPedidos;
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, PedidoPutDto pedido)
        {

            var pedidoASerAtualizado = await _context.Pedidos
                .FirstAsync(p => p.idPedido == id);

            if (pedidoASerAtualizado == null)
            {
                return BadRequest("Pedido com esse id não existe");
            }

            var clienteProcurarNoBanco = await _context.Clientes.FindAsync(pedido.idCliente);

            if (clienteProcurarNoBanco == null)
            {
                return BadRequest("Cliente com esse id não existe");
            }

            pedidoASerAtualizado.idCliente = pedido.idCliente;
            pedidoASerAtualizado.statusPedido = pedido.statusPedido;
            pedidoASerAtualizado.dataPedido = pedido.dataPedido;
            pedidoASerAtualizado.observacoesPedido = pedido.observacoesPedido;

            foreach (var item in pedido.itensPedido)
            {
                if (item.incluir == true)
                {
                    var novoItemPedido = new ItensPedido()
                    {
                        Pedido = pedidoASerAtualizado,
                        idProduto = item.idProduto
                    };

                    await _context.AddAsync(novoItemPedido);
                }
                if (item.excluir == true)
                {
                    var pedidoItemExcluir = await _context.ItensPedidos
                        .FirstAsync(pi => pi.idItemPedido == item.idItemPedido);
                    _context.ItensPedidos.Remove(pedidoItemExcluir);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(PedidoDto pedido)
        {
            // Verificar se o cliente existe
            var ClienteNoBanco = await _context.Clientes.FindAsync(pedido.idCliente);
            if (ClienteNoBanco == null)
            {
                return BadRequest("Cliente com esse id não existe");
            }

            var novoPedido = new Pedido()
            {
                idCliente = pedido.idCliente,
                dataPedido = DateTime.Now,
                statusPedido = pedido.statusPedido,
                valorTotalPedido = pedido.valorTotalPedido,
                observacoesPedido = pedido.observacoesPedido,
            };

            await _context.Pedidos.AddAsync(novoPedido);
            await _context.SaveChangesAsync();

            foreach (var item in pedido.itensPedido)
            {

                var produtoExistente = await _context.Produtos.FindAsync(item);
                if (produtoExistente == null)
                {
                    return BadRequest($"Produto com o id {item} não existe.");
                }

                var novoItemComanda = new ItensPedido()
                {
                    idPedido = novoPedido.idPedido,
                    idProduto = item
                };

                await _context.ItensPedidos.AddAsync(novoItemComanda);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = novoPedido.idPedido }, pedido);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            await _context.Database.ExecuteSqlRawAsync("DELETE FROM itenspedidos WHERE idPedido = {0}", id);

            _context.Pedidos.Remove(pedido);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.idPedido == id);
        }
    }
}