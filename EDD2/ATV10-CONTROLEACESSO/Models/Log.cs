namespace ATV10_controleacesso.Models
{
    public class Log
    {
        public DateTime DataHora { get; }
        public Usuario Usuario { get; }
        public bool TipoAcesso { get; } // true = autorizado, false = negado  

        public Log(Usuario usuario, bool tipoAcesso)
        {
            this.DataHora = DateTime.Now;
            this.Usuario = usuario;
            this.TipoAcesso = tipoAcesso;
        }
        public override string ToString()
        {
            string status = TipoAcesso ? "AUTORIZADO" : "NEGADO";

            return $"{DataHora:dd/MM/yyyy HH:mm:ss} | {Usuario.Nome} | {status}";
        }
    }
}