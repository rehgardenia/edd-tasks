namespace Models
{
    public class Medicamentos
    {
        private List<Medicamento> listaMedicamentos;

        public Medicamentos()
        {
            listaMedicamentos = new List<Medicamento>();
        }
        public void adicionar(Medicamento m)
        {
            listaMedicamentos.Add(m);
        }
        public void remover(Medicamento m)
        {
            if (!listaMedicamentos.Contains(m))
            {
                throw new Exception("Medicamento não encontrado");
            }
            else if(m.qtdDisponivel() > 0)
            {
                throw new Exception("Não é possível remover um medicamento com estoque disponível");
            }
            else
            {
                listaMedicamentos.Remove(m);
            }
           
        }
        public Medicamento buscar(Medicamento m)
        {
            foreach (var medicamento in listaMedicamentos)
            {
                if (medicamento.Id == id)
                {
                    return medicamento;
                }
            }
            return null;
        }
    }
}