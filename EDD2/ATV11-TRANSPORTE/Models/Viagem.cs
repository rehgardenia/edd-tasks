 namespace ATV11_TRANSPORTE.Models
{
 public class Viagem
    {
        public int Id { get; private set; }
        public Garagem Origem { get; private set; }
        public Garagem Destino { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public int QuantidadePassageiros { get; private set; }
        public DateTime DataHora { get; private set; }

        public Viagem(int id, Garagem origem, Garagem destino, Veiculo veiculo)
        {
            Id = id;
            Origem = origem;
            Destino = destino;
            Veiculo = veiculo;
            QuantidadePassageiros = veiculo.Capacidade;
            DataHora = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Viagem #{Id} | {Origem.Nome} -> {Destino.Nome} | {Veiculo.Placa} (Van #{Veiculo.Id}) | {QuantidadePassageiros} pass. | {DataHora:HH:mm:ss}";
        }
    }
}