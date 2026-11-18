namespace ATV11_TRANSPORTE.Models
{
   public class Garagem
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Stack<Veiculo> Estacionamento { get; private set; }

        public Garagem(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Estacionamento = new Stack<Veiculo>();
        }

        public void Estacionar(Veiculo veiculo)
        {
            Estacionamento.Push(veiculo);
        }

        public Veiculo LiberarProximo()
        {
            if (EstaVazia())
                throw new InvalidOperationException($"A garagem {Nome} está vazia.");
            return Estacionamento.Pop();
        }

        public bool EstaVazia()
        {
            return Estacionamento.Count == 0;
        }

        public int QuantidadeVeiculos()
        {
            return Estacionamento.Count;
        }

        public int PotencialTransporte()
        {
            return Estacionamento.Sum(v => v.Capacidade);
        }

        public void LimparEstacionamento()
        {
            Estacionamento.Clear();
        }

        public override string ToString()
        {
            return $"[{Id}] {Nome}";
        }
    }
}