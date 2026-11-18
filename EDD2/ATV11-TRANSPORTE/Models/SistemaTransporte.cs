 namespace ATV11_TRANSPORTE.Models
{
 public class SistemaTransporte
    {
        public List<Veiculo> Veiculos { get; private set; }
        public List<Garagem> Garagens { get; private set; }
        public List<Viagem> Viagens { get; private set; }
        public bool JornadaIniciada { get; private set; }

        private int _proximoIdVeiculo = 1;
        private int _proximoIdGaragem = 1;
        private int _proximoIdViagem = 1;

        public SistemaTransporte()
        {
            Veiculos = new List<Veiculo>();
            Garagens = new List<Garagem>();
            Viagens = new List<Viagem>();
            JornadaIniciada = false;

            InicializarDadosPadrao();
        }

        private void InicializarDadosPadrao()
        {
            // Garagens iniciais: Congonhas e Guarulhos
            Garagens.Add(new Garagem(_proximoIdGaragem++, "Congonhas (CGH)"));
            Garagens.Add(new Garagem(_proximoIdGaragem++, "Guarulhos (GRU)"));

            // Frota inicial de 8 vans (numeradas de 1 a 8)
            for (int i = 1; i <= 8; i++)
            {
                Veiculos.Add(new Veiculo(_proximoIdVeiculo++, $"VAN-000{i}", 15));
            }
        }

        public bool CadastrarVeiculo(string placa, int capacidade, out string mensagem)
        {
            if (JornadaIniciada)
            {
                mensagem = "Não é possível cadastrar veículos com a jornada diária iniciada. Encerre a jornada primeiro.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(placa) || capacidade <= 0)
            {
                mensagem = "Dados inválidos. Informe uma placa válida e capacidade maior que zero.";
                return false;
            }

            var veiculo = new Veiculo(_proximoIdVeiculo++, placa.Trim().ToUpper(), capacidade);
            Veiculos.Add(veiculo);
            mensagem = $"Veículo cadastrado com sucesso: {veiculo}";
            return true;
        }

        public bool CadastrarGaragem(string nome, out string mensagem)
        {
            if (JornadaIniciada)
            {
                mensagem = "Não é possível cadastrar garagens com a jornada diária iniciada. Encerre a jornada primeiro.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                mensagem = "Nome da garagem inválido.";
                return false;
            }

            var garagem = new Garagem(_proximoIdGaragem++, nome.Trim());
            Garagens.Add(garagem);
            mensagem = $"Garagem cadastrada com sucesso: {garagem}";
            return true;
        }

        public bool IniciarJornada(out string mensagem)
        {
            if (JornadaIniciada)
            {
                mensagem = "A jornada diária já foi iniciada.";
                return false;
            }

            if (Garagens.Count < 2)
            {
                mensagem = "São necessárias pelo menos 2 garagens cadastradas para iniciar a jornada.";
                return false;
            }

            if (Veiculos.Count == 0)
            {
                mensagem = "Não há veículos cadastrados na frota.";
                return false;
            }

            foreach (var g in Garagens)
            {
                g.LimparEstacionamento();
            }

            // Distribuição alternada dos veículos entre as garagens
            for (int i = 0; i < Veiculos.Count; i++)
            {
                Veiculos[i].ZerarPassageiros();
                Garagens[i % Garagens.Count].Estacionar(Veiculos[i]);
            }

            Viagens.Clear();
            _proximoIdViagem = 1;
            JornadaIniciada = true;

            mensagem = "Jornada diária iniciada! Veículos distribuídos alternadamente entre as garagens.";
            return true;
        }

        public bool EncerrarJornada(out List<string> relatorio, out string mensagem)
        {
            relatorio = new List<string>();

            if (!JornadaIniciada)
            {
                mensagem = "Nenhuma jornada está iniciada no momento.";
                return false;
            }

            foreach (var v in Veiculos)
            {
                relatorio.Add($"Van #{v.Id} ({v.Placa}): {v.PassageirosTransportados} passageiros transportados");
                v.ZerarPassageiros();
            }

            foreach (var g in Garagens)
            {
                g.LimparEstacionamento();
            }

            Viagens.Clear();
            JornadaIniciada = false;

            mensagem = "Jornada diária encerrada e ocorrências anteriores limpas.";
            return true;
        }

        public bool LiberarViagem(int idOrigem, int idDestino, out string mensagem)
        {
            if (!JornadaIniciada)
            {
                mensagem = "A jornada diária ainda não foi iniciada.";
                return false;
            }

            if (idOrigem == idDestino)
            {
                mensagem = "A garagem de origem e destino devem ser diferentes.";
                return false;
            }

            var origem = Garagens.FirstOrDefault(g => g.Id == idOrigem);
            var destino = Garagens.FirstOrDefault(g => g.Id == idDestino);

            if (origem == null || destino == null)
            {
                mensagem = "Garagem de origem ou destino não encontrada.";
                return false;
            }

            if (origem.EstaVazia())
            {
                mensagem = $"A garagem de {origem.Nome} está vazia. Aguarde o retorno de um veículo.";
                return false;
            }

            Veiculo veiculo = origem.LiberarProximo();
            veiculo.RegistrarViagemCompleta();
            destino.Estacionar(veiculo);

            var viagem = new Viagem(_proximoIdViagem++, origem, destino, veiculo);
            Viagens.Add(viagem);

            mensagem = $"Viagem liberada! {viagem}";
            return true;
        }

        public Garagem BuscarGaragem(int id)
        {
            return Garagens.FirstOrDefault(g => g.Id == id);
        }

        public List<Viagem> ListarViagens(int idOrigem, int idDestino)
        {
            return Viagens.Where(v => v.Origem.Id == idOrigem && v.Destino.Id == idDestino).ToList();
        }

        public int ContarViagens(int idOrigem, int idDestino)
        {
            return Viagens.Count(v => v.Origem.Id == idOrigem && v.Destino.Id == idDestino);
        }

        public int ContarPassageiros(int idOrigem, int idDestino)
        {
            return Viagens
                .Where(v => v.Origem.Id == idOrigem && v.Destino.Id == idDestino)
                .Sum(v => v.QuantidadePassageiros);
        }
    }
}