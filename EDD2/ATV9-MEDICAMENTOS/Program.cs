namespace atv9_medicamentos
{
    public class Program
    {
        static void Menu()
        {
            Console.WriteLine("=== Sistema de Controle de Medicamentos ===");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("0. Finalizar processo");
            Console.WriteLine("1. Cadastrar medicamento");
            Console.WriteLine("2. Consultar medicamento(sintético)");
            Console.WriteLine("3. Consultar medicamento(abálitico)");
            Console.WriteLine("4. Comprar medicamento(cadastrar lote)");
            Console.WriteLine("5. Vender medicamento(abater do lote mais antigo)");
            Console.WriteLine("6. Listar medicamentos (dados sintéticos)");
        }


        static void Main(string[] args)
        {
            Medicamentos medicamentos = new Medicamentos();

            while(true)
            {
                Menu();
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Finalizando processo...");
                        return;
                    case 1:
                        // Cadastrar medicamento
                        Console.WriteLine("Digite o ID do medicamento:");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o nome do medicamento:");
                        string nome = Console.ReadLine();
                        Console.WriteLine("Digite o laboratório do medicamento:");
                        string laboratorio = Console.ReadLine();

                        Medicamento novoMedicamento = new Medicamento(id, nome, laboratorio);
                        medicamentos.adicionar(novoMedicamento);
                        Console.WriteLine("Medicamento cadastrado com sucesso!");
                        break;
                    case 2:
                        // Consultar medicamento(sintético)
                        Console.WriteLine("Digite o ID do medicamento para consulta:");
                        int idConsulta = int.Parse(Console.ReadLine());
                        Medicamento medicamentoConsulta = medicamentos.buscar(new Medicamento(idConsulta, "", ""));
                        if (medicamentoConsulta != null)
                        {
                            Console.WriteLine(medicamentoConsulta.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;
                    case 3:
                        // Consultar medicamento(análitico)
                        Console.WriteLine("Digite o ID do medicamento para consulta:");
                        int idConsultaAnalitico = int.Parse(Console.ReadLine());
                        Medicamento medicamentoConsultaAnalitico = medicamentos.buscar(new Medicamento(idConsultaAnalitico, "", ""));
                        if (medicamentoConsultaAnalitico != null)
                        {
                            Console.WriteLine(medicamentoConsultaAnalitico.ToString() + " - Lotes: " + string.Join(", ", medicamentoConsultaAnalitico.lotes.Select(l => l.ToString())));
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;
                    case 4:
                        // Comprar medicamento(cadastrar lote)
                        // Implementação para adicionar um lote a um medicamento existente
                        break;
                    case 5:
                        // Vender medicamento(abater do lote mais antigo)
                        // Implementação para vender uma quantidade de um medicamento
                        break;
                    case 6:
                        // Listar medicamentos (dados sintéticos)
                        foreach (var med in medicamentos.listaMedicamentos)
                        {
                            Console.WriteLine(med.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }

          
        }
    }
}