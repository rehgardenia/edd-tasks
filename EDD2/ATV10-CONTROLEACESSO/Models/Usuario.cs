namespace ATV10_controleacesso.Models
{
    public class Usuario
    {
        public int Id { get; }
        public string Nome { get; }

        private List<Ambiente> ambientes;

        public IReadOnlyList<Ambiente> Ambientes => ambientes.AsReadOnly();
        public Usuario(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
            this.ambientes = new List<Ambiente>();
        }
        public bool concederPermissao(Ambiente ambiente)
        {
            if (ambientes.Any(amb => amb.Id == ambiente.Id))
            {
                return false;
            }
            ambientes.Add(ambiente);
            return true;
        }
        public bool revogarPermissao(Ambiente ambiente)
        {
            Ambiente? ambienteEncontrado = ambientes
                    .FirstOrDefault(amb => amb.Id == ambiente.Id);
            if (ambienteEncontrado == null)
            {
                return false;
            }
            ambientes.Remove(ambienteEncontrado);
            return true;
        }

    }
}