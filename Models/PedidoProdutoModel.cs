namespace Fiap.Api.Alunos.Models
{
    public class PedidoProdutoModel
    {
        public int PedidoId { get; set; }
        public PedidoModel Pedido { get; set; }

        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }
    }
}
