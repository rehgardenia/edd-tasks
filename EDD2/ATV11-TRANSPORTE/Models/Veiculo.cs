namespace ATV11_TRANSPORTE.Models
{
public class Veiculo
    {
        public int Id { get; private set; }
        public string Placa { get; private set; }
        public int Capacidade { get; private set; }
        public int PassageirosTransportados { get; private set; }

        public Veiculo(int id, string placa, int capacidade)
        {
            Id = id;
            Placa = placa;
            Capacidade = capacidade;
            PassageirosTransportados = 0;
        }

        public void RegistrarViagemCompleta()
        {
            PassageirosTransportados += Capacidade;
        }

        public void ZerarPassageiros()
        {
            PassageirosTransportados = 0;
        }

        public override string ToString()
        {
            return $"Van #{Id} | Placa: {Placa} | Lotação: {Capacidade} pass.";
        }
    }
}