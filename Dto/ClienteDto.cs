namespace construcaoAPI_INF12.Api.NovaPasta
{
    public class ClienteDto
    {
        public int idCliente { get; set; }
        public string? nomeCliente { get; set; }
        public string sobrenomeCliente { get; set; }
        public string emailCliente { get; set; }
        public string telefoneCliente { get; set; }
        public string enderecoCliente { get; set; }
        public string cidadeCliente { get; set; }
        public string estadoCliente { get; set; }
        public string cepCliente { get; set; }
        public string dataCadastroCliente { get; set; }
    }
}
