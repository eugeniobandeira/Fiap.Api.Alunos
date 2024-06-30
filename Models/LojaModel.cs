namespace Fiap.Api.Alunos.Models
{
    public class LojaModel
    {
        public int LojaId { get; set; }
        public string LojaNome { get; set; }
        public string Endereco { get; set; }

        //Relacionamento com Pedido
        public List<PedidoModel> Pedidos { get; set; }
    }
}
