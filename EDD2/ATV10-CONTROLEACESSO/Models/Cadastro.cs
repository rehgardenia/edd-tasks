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
           if ( usuarioEncontrado == null)
           {
                return false;
           }
           if(usuarioEncontrado.Ambientes.Count > 0)
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
        public void upload(){}
        public void download(){}
    }
}