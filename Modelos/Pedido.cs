using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace construcaoAPI_INF12.Modelos
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPedido { get; set; }
        public int idCliente { get; set; }
        public DateTime dataPedido { get; set; }
        public string statusPedido { get; set; }
        public decimal valorTotalPedido { get; set; }
        public string observacoesPedido { get; set; }
        public virtual Cliente Cliente { get; set; }
        public ICollection<ItensPedido> itensPedidos { get; set; }
    }
}
