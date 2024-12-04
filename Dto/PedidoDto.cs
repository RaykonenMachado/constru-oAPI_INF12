namespace construcaoAPI_INF12.Api.NovaPasta
{
    public class PedidoDto
    {
        public int idCliente { get; set; }
        public DateTime dataPedido { get; set; }
        public string statusPedido { get; set; }
        public decimal valorTotalPedido { get; set; }
        public string observacoesPedido { get; set; }
        public int[] itensPedido { get; set; } = [];
    }
}
