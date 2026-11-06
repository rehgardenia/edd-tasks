
namespace ControleAcesso.Models;

public class DadosPersistencia
{
    public List<UsuarioDados> Usuarios { get; set; } = new();
    public List<AmbienteDados> Ambientes { get; set; } = new();
    public List<PermissaoDados> Permissoes { get; set; } = new();
    public List<LogDados> Logs { get; set; } = new();
}

public class UsuarioDados
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
}

public class AmbienteDados
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
}

public class PermissaoDados
{
    public int UsuarioId { get; set; }
    public int AmbienteId { get; set; }
}

public class LogDados
{
    public int UsuarioId { get; set; }
    public int AmbienteId { get; set; }
    public DateTime DtAcesso { get; set; }
    public bool TipoAcesso { get; set; }
}
