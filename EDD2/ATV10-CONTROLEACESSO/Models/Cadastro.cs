using System.Text.Json;
using ControleAcesso.Models;

namespace ATV10_controleacesso.Models
{
    public class Cadastro
    {
        private List<Usuario> usuarios;
        private List<Ambiente> ambientes;

        public Cadastro()
        {
            this.usuarios = new List<Usuario>();
            this.ambientes = new List<Ambiente>();
        }

        public void AdicionarUsuario(Usuario u)
        {
            bool usuarioExistente = usuarios.Any(usuario => usuario.Id == u.Id);
            if (!usuarioExistente)
            {
                usuarios.Add(u);
            }
        }
        public bool RemoverUsuario(Usuario u)
        {
            Usuario? usuarioEncontrado = usuarios.FirstOrDefault(usuario => usuario.Id == u.Id);
            if (usuarioEncontrado == null)
            {
                return false;
            }
            if (usuarioEncontrado.Ambientes.Count > 0)
            {
                return false;
            }
            return usuarios.Remove(usuarioEncontrado);

        }
        public Usuario? PesquisarUsuario(Usuario u)
        {
            return usuarios.FirstOrDefault(usuario => usuario.Id == u.Id);
        }
        public void AdicionarAmbiente(Ambiente a)
        {
            bool ambienteExistente = ambientes.Any(ambiente => ambiente.Id == a.Id);
            if (!ambienteExistente)
            {
                ambientes.Add(a);
            }
        }
        public Ambiente? PesquisarAmbiente(Ambiente a)
        {
            return ambientes.FirstOrDefault(ambiente => ambiente.Id == a.Id);
        }

        public bool RemoverAmbiente(Ambiente a)
        {
            Ambiente? ambienteEncontrado = ambientes.FirstOrDefault(ambiente => ambiente.Id == a.Id);
            if (ambienteEncontrado == null)
            {
                return false;
            }
            foreach (Usuario usuario in usuarios)
            {
                usuario.revogarPermissao(ambienteEncontrado);
            }
            return ambientes.Remove(ambienteEncontrado);
        }
        public void upload()
        {
            DadosPersistencia dados = new DadosPersistencia();

            // Salvar usuários e permissões
            foreach (Usuario usuario in usuarios)
            {
                dados.Usuarios.Add(new UsuarioDados
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome
                });

                foreach (Ambiente ambiente in usuario.Ambientes)
                {
                    dados.Permissoes.Add(new PermissaoDados
                    {
                        UsuarioId = usuario.Id,
                        AmbienteId = ambiente.Id
                    });
                }
            }

            // Salvar ambientes e logs
            foreach (Ambiente ambiente in ambientes)
            {
                dados.Ambientes.Add(new AmbienteDados
                {
                    Id = ambiente.Id,
                    Nome = ambiente.Nome
                });

                foreach (Log log in ambiente.Logs)
                {
                    dados.Logs.Add(new LogDados
                    {
                        UsuarioId = log.Usuario.Id,
                        AmbienteId = ambiente.Id,
                        DtAcesso = log.DataHora,
                        TipoAcesso = log.TipoAcesso
                    });
                }
            }

            JsonSerializerOptions opcoes = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(dados, opcoes);

            File.WriteAllText("dados.json", json);

            Console.WriteLine("Dados salvos com sucesso!");
        }
        public void download()
        {
            if (!File.Exists("dados.json"))
            {
                Console.WriteLine("Nenhum arquivo de dados encontrado.");
                return;
            }

            string json = File.ReadAllText("dados.json");

            DadosPersistencia? dados =
                JsonSerializer.Deserialize<DadosPersistencia>(json);

            if (dados == null)
            {
                Console.WriteLine("Arquivo de dados inválido.");
                return;
            }

            List<Usuario> novosUsuarios = new();
            List<Ambiente> novosAmbientes = new();

            // 1 - Recriar ambientes
            foreach (AmbienteDados a in dados.Ambientes)
            {
                novosAmbientes.Add(new Ambiente(a.Id, a.Nome));
            }

            // 2 - Recriar usuários
            foreach (UsuarioDados u in dados.Usuarios)
            {
                novosUsuarios.Add(new Usuario(u.Id, u.Nome));
            }

            // 3 - Restaurar permissões
            foreach (PermissaoDados p in dados.Permissoes)
            {
                Usuario? usuario = novosUsuarios
                    .FirstOrDefault(u => u.Id == p.UsuarioId);

                Ambiente? ambiente = novosAmbientes
                    .FirstOrDefault(a => a.Id == p.AmbienteId);

                if (usuario != null && ambiente != null)
                {
                    usuario.concederPermissao(ambiente);
                }
            }

            // 4 - Restaurar logs
            foreach (LogDados l in dados.Logs)
            {
                Usuario? usuario = novosUsuarios
                    .FirstOrDefault(u => u.Id == l.UsuarioId);

                Ambiente? ambiente = novosAmbientes
                    .FirstOrDefault(a => a.Id == l.AmbienteId);

                if (usuario != null && ambiente != null)
                {
                    Log log = new Log(
                        usuario,
                        l.TipoAcesso,
                        l.DtAcesso
                    );

                    ambiente.RegistrarLog(log);
                }
            }

            // 5 - Atualizar o cadastro
            usuarios = novosUsuarios;
            ambientes = novosAmbientes;

            Console.WriteLine("Dados carregados com sucesso!");
        }
    }
}