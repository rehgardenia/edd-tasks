using ATV10_controleacesso;
using ATV10_controleacesso.Models;

internal class Program
{
    public static void Menu()
    {
        Console.WriteLine("\n===== CONTROLE DE ACESSO =====");
        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("0. Sair");
        Console.WriteLine("1. Cadastrar Ambiente");
        Console.WriteLine("2. Consultar Ambiente");
        Console.WriteLine("3. Remover Ambiente");
        Console.WriteLine("4. Cadastrar Usuário");
        Console.WriteLine("5. Consultar Usuário");
        Console.WriteLine("6. Remover Usuário");
        Console.WriteLine("7. Conceder permissão de acesso a um usuário"); // informa ambiente e usuario
        Console.WriteLine("8. Revogar permissão de acesso a um usuário"); // informa ambiente e usuario
        Console.WriteLine("9. Registrar Acesso"); // informa ambiente, usuario e tipo de acesso  (registrar log de acesso)
        Console.WriteLine("10. Consultar Logs de Acesso"); // informa ambiente e exibe todos os logs de acesso do ambiente, filtrando por tipo de acesso (autorizado ou negado)
    }

    public static void CadastrarAmbiente(Cadastro cadastro)
    {
        Console.Write("ID do ambiente: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Console.Write("Nome do ambiente: ");
        string nome = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("O nome não pode ser vazio!");
            return;
        }

        Ambiente ambiente = new Ambiente(id, nome);

        cadastro.AdicionarAmbiente(ambiente);

        Console.WriteLine("Operação de cadastro processada!");
    }
    public static void ConsultarAmbiente(Cadastro cadastro)
    {
        Console.Write("Digite o ID do ambiente: ");

        if (!int.TryParse(Console.ReadLine(), out int idPesquisa))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Ambiente? ambientePesquisa = new Ambiente(idPesquisa, "");
        ambientePesquisa = cadastro.PesquisarAmbiente(ambientePesquisa);

        if (ambientePesquisa == null)
        {
            Console.WriteLine("Ambiente não encontrado!");
        }
        else
        {
            Console.WriteLine("\n=== DADOS DO AMBIENTE ===");
            Console.WriteLine($"ID: {ambientePesquisa.Id}");
            Console.WriteLine($"Nome: {ambientePesquisa.Nome}");
            Console.WriteLine($"Quantidade de logs: {ambientePesquisa.Logs.Count}");
        }
    }
    public static void RemoverAmbiente(Cadastro cadastro)
    {
        Console.Write("ID do ambiente: ");

        if (!int.TryParse(Console.ReadLine(), out int idRemover))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Ambiente pesquisa = new Ambiente(idRemover, "");

        Ambiente? encontrado =
            cadastro.PesquisarAmbiente(pesquisa);

        if (encontrado == null)
        {
            Console.WriteLine("Ambiente não encontrado!");
            return;
        }

        cadastro.RemoverAmbiente(encontrado);

        Console.WriteLine("Ambiente removido com sucesso!");
    }
    public static void CadastrarUsuario(Cadastro cadastro)
    {
         Console.Write("ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Console.Write("Nome do usuário: ");
        string nome = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("O nome não pode ser vazio!");
            return;
        }

        Usuario usuario = new Usuario(id, nome);

        cadastro.AdicionarUsuario(usuario);

        Console.WriteLine("Operação de cadastro processada!");
    }
    public static void ConsultarUsuario(Cadastro cadastro)
    {
         Console.Write("Digite o ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int idPesquisa))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Usuario? usuarioPesquisa = new Usuario(idPesquisa, "");
        usuarioPesquisa = cadastro.PesquisarUsuario(usuarioPesquisa);

        if (usuarioPesquisa == null)
        {
            Console.WriteLine("Usuário não encontrado!");
        }
        else
        {
            Console.WriteLine("\n=== DADOS DO USUÁRIO ===");
            Console.WriteLine($"ID: {usuarioPesquisa.Id}");
            Console.WriteLine($"Nome: {usuarioPesquisa.Nome}");
        }
    }
    public static void RemoverUsuario(Cadastro cadastro)
    {
         Console.Write("ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int idRemover))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Usuario pesquisa = new Usuario(idRemover, "");

        Usuario? encontrado =
            cadastro.PesquisarUsuario(pesquisa);

        if (encontrado == null)
        {
            Console.WriteLine("Usuário não encontrado!");
            return;
        }

        cadastro.RemoverUsuario(encontrado);

        Console.WriteLine("Usuário removido com sucesso!");
    }
    public static void ConcederPermissao(Cadastro cadastro)
    {
        Console.Write("ID do usuário: ");
        if (!int.TryParse(Console.ReadLine(), out int idUsuario))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        Console.Write("ID do ambiente: ");
        if (!int.TryParse(Console.ReadLine(), out int idAmbiente))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        Usuario? usuario = cadastro.PesquisarUsuario(new Usuario(idUsuario, ""));
        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado!");
            return;
        }
        Ambiente? ambiente = cadastro.PesquisarAmbiente(new Ambiente(idAmbiente, ""));
        if (ambiente == null)
        {
            Console.WriteLine("Ambiente não encontrado!");
            return;
        }
        usuario.concederPermissao(ambiente);
        Console.WriteLine("Permissão concedida com sucesso!");

    }
    public static void RevogarPermissao(Cadastro cadastro)
    {
        Console.Write("ID do usuário: ");
        if (!int.TryParse(Console.ReadLine(), out int idUsuario))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        Console.Write("ID do ambiente: ");
        if (!int.TryParse(Console.ReadLine(), out int idAmbiente))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        Usuario? usuario = cadastro.PesquisarUsuario(new Usuario(idUsuario, ""));
        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado!");
            return;
        }
        Ambiente? ambiente = cadastro.PesquisarAmbiente(new Ambiente(idAmbiente, ""));
        if (ambiente == null)
        {
            Console.WriteLine("Ambiente não encontrado!");
            return;
        }
        usuario.revogarPermissao(ambiente);
        Console.WriteLine("Permissão revogada com sucesso!");
    }
    public static void RegistrarAcesso(Cadastro cadastro)
    {
        Console.Write("ID do usuário: ");
        if (!int.TryParse(Console.ReadLine(), out int idUsuario))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        Console.Write("ID do ambiente: ");
        if (!int.TryParse(Console.ReadLine(), out int idAmbiente))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        Usuario? usuario = cadastro.PesquisarUsuario(new Usuario(idUsuario, ""));
        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado!");
            return;
        }
        Ambiente? ambiente = cadastro.PesquisarAmbiente(new Ambiente(idAmbiente, ""));
        if (ambiente == null)
        {
            Console.WriteLine("Ambiente não encontrado!");
            return;
        }
        bool acessoPermitido = usuario.Ambientes.Any(a => a.Id == ambiente.Id);
        Log log = new Log(usuario, acessoPermitido);
        ambiente.RegistrarLog(log);
        Console.WriteLine($"Acesso {(acessoPermitido ? "permitido" : "negado")} e registrado com sucesso!");
    }
    public static void ConsultarLogsAcesso(Cadastro cadastro)
    {
        Console.Write("ID do ambiente: ");
        if (!int.TryParse(Console.ReadLine(), out int idAmbiente))
        {
            Console.WriteLine("ID inválido!");
            return;
        }
        Ambiente? ambiente = cadastro.PesquisarAmbiente(new Ambiente(idAmbiente, ""));
        if (ambiente == null)
        {
            Console.WriteLine("Ambiente não encontrado!");
            return;
        }
        Console.Write("Deseja filtrar por tipo de acesso? (S/N): ");
        string filtro = Console.ReadLine()?.Trim().ToUpper() ?? "N";
        bool? tipoAcesso = null;
        if (filtro == "S")
        {
            Console.Write("Digite 'A' para autorizado ou 'N' para negado: ");
            string tipo = Console.ReadLine()?.Trim().ToUpper() ?? "";
            if (tipo == "A")
                tipoAcesso = true;
            else if (tipo == "N")
                tipoAcesso = false;
            else
            {
                Console.WriteLine("Tipo de acesso inválido!");
                return;
            }
        }
        var logsFiltrados = tipoAcesso.HasValue ? ambiente.Logs.Where(l => l.TipoAcesso == tipoAcesso.Value) : ambiente.Logs;
        Console.WriteLine($"\n=== LOGS DE ACESSO DO AMBIENTE {ambiente.Nome} ===");
        foreach (var log in logsFiltrados)
        {
            Console.WriteLine(log.ToString());
        }
    }

    private static void Main(string[] args)
    {
        Cadastro cadastro = new Cadastro();
        int opcao = -1;
        while (opcao != 0)
        {
            Menu();
            Console.Write("Digite a opção desejada: ");
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                continue;
            }
            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Saindo do programa...");
                    break;
                case 1:
                    CadastrarAmbiente(cadastro);
                    break;
                case 2:
                    ConsultarAmbiente(cadastro);
                    break;
                case 3:
                    RemoverAmbiente(cadastro);
                    break;
                case 4:
                    CadastrarUsuario(cadastro);
                    break;
                case 5:
                    ConsultarUsuario(cadastro);
                    break;
                case 6:
                    RemoverUsuario(cadastro);
                    break;
                case 7:
                    ConcederPermissao(cadastro);
                    break;
                case 8:
                    RevogarPermissao(cadastro);
                    break;
                case 9:
                    RegistrarAcesso(cadastro);
                    break;
                case 10:
                    ConsultarLogsAcesso(cadastro);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}