using System;
using ATV11_TRANSPORTE.Models;

namespace ATV11_TRANSPORTE
{
       
    class Program
    {
        static void Main(string[] args)
        {
            var sistema = new SistemaTransporte();
            int opcao = -1;

            do
            {
                ExibirMenu(sistema.JornadaIniciada);
                Console.Write("Escolha uma opção: ");
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida. Digite um número de 0 a 9.\n");
                    opcao = -1;
                    continue;
                }

                Console.WriteLine();
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o sistema...");
                        break;

                    case 1:
                        CadastrarVeiculoUI(sistema);
                        break;

                    case 2:
                        CadastrarGaragemUI(sistema);
                        break;

                    case 3:
                        if (sistema.IniciarJornada(out string msgInicio))
                        {
                            Console.WriteLine(msgInicio);
                            foreach (var g in sistema.Garagens)
                            {
                                Console.WriteLine($"  {g.Nome}: {g.QuantidadeVeiculos()} veículo(s)");
                            }
                        }
                        else
                        {
                            Console.WriteLine(msgInicio);
                        }
                        break;

                    case 4:
                        if (sistema.EncerrarJornada(out List<string> relatorio, out string msgFim))
                        {
                            Console.WriteLine("=== RELATÓRIO DE ENCERRAMENTO DA JORNADA ===");
                            foreach (var linha in relatorio)
                            {
                                Console.WriteLine("  " + linha);
                            }
                            Console.WriteLine(msgFim);
                        }
                        else
                        {
                            Console.WriteLine(msgFim);
                        }
                        break;

                    case 5:
                        LiberarViagemUI(sistema);
                        break;

                    case 6:
                        ListarVeiculosGaragemUI(sistema);
                        break;

                    case 7:
                        InformarQuantidadeViagensUI(sistema);
                        break;

                    case 8:
                        ListarViagensUI(sistema);
                        break;

                    case 9:
                        InformarQuantidadePassageirosUI(sistema);
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine();
            } while (opcao != 0);
        }

        static void ExibirMenu(bool jornadaIniciada)
        {
            string status = jornadaIniciada ? "INICIADA" : "ENCERRADA";
            Console.WriteLine("=================================================");
            Console.WriteLine($"  PROJETO TRANSPORTE  [Jornada: {status}]");
            Console.WriteLine("=================================================");
            Console.WriteLine("0. Finalizar");
            Console.WriteLine("1. Cadastrar veículo");
            Console.WriteLine("2. Cadastrar garagem");
            Console.WriteLine("3. Iniciar jornada");
            Console.WriteLine("4. Encerrar jornada");
            Console.WriteLine("5. Liberar viagem (origem -> destino)");
            Console.WriteLine("6. Listar veículos em determinada garagem");
            Console.WriteLine("7. Informar quantidade de viagens (origem -> destino)");
            Console.WriteLine("8. Listar viagens efetuadas (origem -> destino)");
            Console.WriteLine("9. Informar quantidade de passageiros (origem -> destino)");
            Console.WriteLine("-------------------------------------------------");
        }

        static void ExibirGaragens(SistemaTransporte sistema)
        {
            Console.WriteLine("Garagens disponíveis:");
            foreach (var g in sistema.Garagens)
            {
                Console.WriteLine($"  {g}");
            }
        }

        static void CadastrarVeiculoUI(SistemaTransporte sistema)
        {
            Console.Write("Informe a placa do veículo: ");
            string placa = Console.ReadLine();

            Console.Write("Informe a lotação (capacidade de passageiros): ");
            if (!int.TryParse(Console.ReadLine(), out int capacidade))
            {
                Console.WriteLine("Capacidade inválida.");
                return;
            }

            sistema.CadastrarVeiculo(placa, capacidade, out string msg);
            Console.WriteLine(msg);
        }

        static void CadastrarGaragemUI(SistemaTransporte sistema)
        {
            Console.Write("Informe o nome/aeroporto da garagem: ");
            string nome = Console.ReadLine();
            sistema.CadastrarGaragem(nome, out string msg);
            Console.WriteLine(msg);
        }

        static void LiberarViagemUI(SistemaTransporte sistema)
        {
            ExibirGaragens(sistema);
            Console.Write("ID da garagem de origem: ");
            if (!int.TryParse(Console.ReadLine(), out int idOrigem))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.Write("ID da garagem de destino: ");
            if (!int.TryParse(Console.ReadLine(), out int idDestino))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            sistema.LiberarViagem(idOrigem, idDestino, out string msg);
            Console.WriteLine(msg);
        }

        static void ListarVeiculosGaragemUI(SistemaTransporte sistema)
        {
            ExibirGaragens(sistema);
            Console.Write("Informe o ID da garagem: ");
            if (!int.TryParse(Console.ReadLine(), out int idGaragem))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var garagem = sistema.BuscarGaragem(idGaragem);
            if (garagem == null)
            {
                Console.WriteLine("Garagem não encontrada.");
                return;
            }

            Console.WriteLine($"\nGaragem: {garagem.Nome}");
            Console.WriteLine($"Quantidade de veículos: {garagem.QuantidadeVeiculos()}");
            Console.WriteLine($"Potencial de transporte: {garagem.PotencialTransporte()} passageiros");

            if (garagem.EstaVazia())
            {
                Console.WriteLine("  (Garagem vazia)");
            }
            else
            {
                Console.WriteLine("Ordem de saída (topo da pilha -> fundo):");
                int pos = 1;
                foreach (var v in garagem.Estacionamento)
                {
                    Console.WriteLine($"  {pos++}º a sair: {v}");
                }
            }
        }

        static bool LerOrigemDestino(SistemaTransporte sistema, out int idOrigem, out int idDestino)
        {
            idOrigem = 0;
            idDestino = 0;
            ExibirGaragens(sistema);

            Console.Write("ID da garagem de origem: ");
            if (!int.TryParse(Console.ReadLine(), out idOrigem))
            {
                Console.WriteLine("ID inválido.");
                return false;
            }

            Console.Write("ID da garagem de destino: ");
            if (!int.TryParse(Console.ReadLine(), out idDestino))
            {
                Console.WriteLine("ID inválido.");
                return false;
            }

            return true;
        }

        static void InformarQuantidadeViagensUI(SistemaTransporte sistema)
        {
            if (!LerOrigemDestino(sistema, out int idOrigem, out int idDestino)) return;
            int qtd = sistema.ContarViagens(idOrigem, idDestino);
            Console.WriteLine($"Quantidade de viagens efetuadas: {qtd}");
        }

        static void ListarViagensUI(SistemaTransporte sistema)
        {
            if (!LerOrigemDestino(sistema, out int idOrigem, out int idDestino)) return;
            var viagens = sistema.ListarViagens(idOrigem, idDestino);

            if (viagens.Count == 0)
            {
                Console.WriteLine("Nenhuma viagem registrada para este trajeto.");
                return;
            }

            Console.WriteLine($"Viagens efetuadas ({viagens.Count}):");
            foreach (var v in viagens)
            {
                Console.WriteLine("  " + v);
            }
        }

        static void InformarQuantidadePassageirosUI(SistemaTransporte sistema)
        {
            if (!LerOrigemDestino(sistema, out int idOrigem, out int idDestino)) return;
            int total = sistema.ContarPassageiros(idOrigem, idDestino);
            Console.WriteLine($"Total de passageiros transportados no trajeto: {total}");
        }
    }
}