namespace Models
{
    public class Medicamentos
    {
        private readonly List<Medicamento> listaMedicamentos = new();
        public IReadOnlyList<Medicamento> ListaMedicamentos => listaMedicamentos;

        public void adicionar(Medicamento medicamento)
        {
            ArgumentNullException.ThrowIfNull(medicamento);
            if (listaMedicamentos.Any(item => item.Id == medicamento.Id))
                throw new InvalidOperationException("Já existe medicamento cadastrado com esse ID.");
            listaMedicamentos.Add(medicamento);
        }

        public void remover(Medicamento medicamento)
        {
            if (!listaMedicamentos.Contains(medicamento))
                throw new InvalidOperationException("Medicamento não encontrado.");
            if (medicamento.qtdDisponivel() > 0)
                throw new InvalidOperationException("Não é possível remover um medicamento com estoque disponível.");
            listaMedicamentos.Remove(medicamento);
        }

        public Medicamento? buscar(Medicamento medicamento) => buscar(medicamento.Id);

        public Medicamento? buscar(int id) => listaMedicamentos.FirstOrDefault(item => item.Id == id);
    }
}
