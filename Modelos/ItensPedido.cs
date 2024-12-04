using construcaoAPI_INF12.Api.Modelos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace construcaoAPI_INF12.Modelos;

public class ItensPedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idItemPedido { get; set; }
    public int idPedido { get; set; }
    public int idProduto { get; set; }
    public int quantidadeItemPedido { get; set; }
    public float precoUnitarioItemPedido { get; set; }
    public virtual Pedido Pedido { get; set; }
    public virtual Produto Produto { get; set; }
}
