using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace construcaoAPI_INF12.Api.Modelos
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public string descricaoProduto { get; set; }
        public decimal precoProduto { get; set; }
        public int estoqueProduto { get; set; }
        public DateTime dataProduto { get; set; }
    }
}
