namespace construcaoAPI_INF12.Api.NovaPasta
{
    public class PedidoGetDto
    {
        public int idPedido { get; set; }
        public int idCliente { get; set; }
        public DateTime dataPedido { get; set; }
        public string statusPedido { get; set; }
        public decimal valorTotalPedido { get; set; }
        public string observacoesPedido { get; set; }
        public List<ItensPedidoGetDto> itensPedido { get; set; } = new List<ItensPedidoGetDto> { };
    }

    public class ItensPedidoGetDto
    {
        public int idItemPedido { get; set; }
        public string nomeProduto { get; set; }
    }
}
