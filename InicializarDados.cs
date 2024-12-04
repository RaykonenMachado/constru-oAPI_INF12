using construcaoAPI_INF12.Api.Modelos;
using construcaoAPI_INF12.Data;

namespace construcaoAPI_INF12.Api
{
    public static class InicializarDados
    {

        public static void Semear(LojaClientesDbContext banco)
        {
            // populando o banco com dados presetados para mostrar no GET as informações
            if (!banco.Produtos.Any())
            {
                banco.Produtos.AddRange(

                new Produto()
                {
                    nomeProduto = "Meia do homem aranha",
                    descricaoProduto = "Meia infantil",
                    estoqueProduto = 8,
                    precoProduto = 12.00M,
                    dataProduto = DateTime.Now,
                },
                new Produto()
                {
                    nomeProduto = "Laxante",
                    descricaoProduto = "Constipação intestinal",
                    estoqueProduto = 7,
                    precoProduto = 10.90M,
                    dataProduto = DateTime.Now,
                },
                new Produto()
                {
                    nomeProduto = "Camiseta Polo",
                    descricaoProduto = "Camiseta leve e confortavel",
                    estoqueProduto = 7,
                    precoProduto = 299.90M,
                    dataProduto = DateTime.Now,
                }
                );

                banco.SaveChanges();
            }
        }
    }
}
