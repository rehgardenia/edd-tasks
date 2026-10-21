using Models;
using System.Globalization;

namespace atv9_medicamentos
{
    public class Program
    {
        private static readonly Medicamentos medicamentos = new();

        private static void Menu()
        {
            Console.WriteLine("=== Sistema de Controle de Medicamentos ===");
            Console.WriteLine("0. Finalizar processo");
            Console.WriteLine("1. Cadastrar medicamento");
            Console.WriteLine("2. Consultar medicamento (sintético)");
            Console.WriteLine("3. Consultar medicamento (analítico)");
            Console.WriteLine("4. Comprar medicamento (cadastrar lote)");
            Console.WriteLine("5. Vender medicamento (abater do lote mais antigo)");
            Console.WriteLine("6. Listar medicamentos");
        }

        private static int LerInteiro(string mensagem)
        {
            Console.WriteLine(mensagem);
            if (!int.TryParse(Console.ReadLine(), out int valor))
                throw new FormatException("Digite um número inteiro válido.");
            return valor;
        }

        private static void Cadastrar()
        {
            try
            {
                int id = LerInteiro("Digite o ID do medicamento:");
                Console.WriteLine("Digite o nome do medicamento:");
                string nome = Console.ReadLine()?.Trim() ?? "";
                Console.WriteLine("Digite o laboratório do medicamento:");
                string laboratorio = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(laboratorio))
                    throw new ArgumentException("Nome e laboratório são obrigatórios.");
                medicamentos.adicionar(new Medicamento(id, nome, laboratorio));
                Console.WriteLine("Medicamento cadastrado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro ao cadastrar medicamento: " + ex.Message); }
        }

        private static void Consultar(bool analitico)
        {
            try
            {
                Medicamento? medicamento = medicamentos.buscar(LerInteiro("Digite o ID do medicamento para consulta:"));
                if (medicamento == null) { Console.WriteLine("Medicamento não encontrado."); return; }
                Console.WriteLine(medicamento);
                if (analitico) Console.WriteLine("Lotes: " + string.Join(", ", medicamento.lotesView));
            }
            catch (Exception ex) { Console.WriteLine("Erro ao consultar medicamento: " + ex.Message); }
        }

        private static void Comprar()
        {
            try
            {
                Medicamento? medicamento = medicamentos.buscar(LerInteiro("Digite o ID do medicamento para adicionar lote:"));
                if (medicamento == null) { Console.WriteLine("Medicamento não encontrado."); return; }
                int quantidade = LerInteiro("Digite a quantidade do lote:");
                Console.WriteLine("Digite a data de validade do lote (dd/MM/yyyy):");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validade))
                    throw new FormatException("Data inválida. Use dd/MM/yyyy.");
                int idLote = medicamento.lotesView.Count == 0 ? 1 : medicamento.lotesView.Max(l => l.Id) + 1;
                medicamento.adicionarLote(new Lote(idLote, quantidade, validade));
                Console.WriteLine("Lote adicionado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro ao adicionar lote: " + ex.Message); }
        }

        private static void Vender()
        {
            try
            {
                Medicamento? medicamento = medicamentos.buscar(LerInteiro("Digite o ID do medicamento para venda:"));
                if (medicamento == null) { Console.WriteLine("Medicamento não encontrado."); return; }
                int quantidade = LerInteiro("Digite a quantidade a ser vendida:");
                Console.WriteLine(medicamento.vender(quantidade) ? "Venda realizada com sucesso!" : "Quantidade inválida ou estoque insuficiente.");
            }
            catch (Exception ex) { Console.WriteLine("Erro ao realizar venda: " + ex.Message); }
        }

        private static void Listar()
        {
            if (medicamentos.ListaMedicamentos.Count == 0) { Console.WriteLine("Nenhum medicamento cadastrado."); return; }
            foreach (Medicamento medicamento in medicamentos.ListaMedicamentos) Console.WriteLine(medicamento);
        }

        public static void Main(string[] args)
        {
            while (true)
            {
                Menu();
                if (!int.TryParse(Console.ReadLine(), out int opcao))
                {
                    Console.WriteLine("Opção inválida. Digite um número de 0 a 6.");
                    continue;
                }
                switch (opcao)
                {
                    case 0: Console.WriteLine("Finalizando processo..."); return;
                    case 1: Cadastrar(); break;
                    case 2: Consultar(false); break;
                    case 3: Consultar(true); break;
                    case 4: Comprar(); break;
                    case 5: Vender(); break;
                    case 6: Listar(); break;
                    default: Console.WriteLine("Opção inválida. Tente novamente."); break;
                }
            }
        }
    }
}
