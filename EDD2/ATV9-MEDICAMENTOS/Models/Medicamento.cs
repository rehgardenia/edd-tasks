namespace Models
{
    public class Medicamento
    {
        private readonly Queue<Lote> lotes = new();

        public int Id { get; }
        public string Nome { get; }
        public string Laboratorio { get; }
        public IReadOnlyCollection<Lote> lotesView => lotes.ToArray();

        public Medicamento(int id, string nome, string laboratorio)
        {
            Id = id;
            Nome = nome;
            Laboratorio = laboratorio;
        }

        public int qtdDisponivel() => lotes.Sum(lote => lote.Qtde);

        public void adicionarLote(Lote lote)
        {
            ArgumentNullException.ThrowIfNull(lote);
            if (lote.Qtde <= 0) throw new ArgumentOutOfRangeException(nameof(lote), "A quantidade deve ser positiva.");
            lotes.Enqueue(lote);
        }

        public void comprar(Lote lote) => adicionarLote(lote);

        public bool vender(int qtde)
        {
            if (qtde <= 0 || qtde > qtdDisponivel()) return false;

            while (qtde > 0)
            {
                Lote loteAtual = lotes.Peek();
                if (loteAtual.Qtde <= qtde)
                {
                    qtde -= loteAtual.Qtde;
                    lotes.Dequeue();
                }
                else
                {
                    loteAtual.Qtde -= qtde;
                    qtde = 0;
                }
            }
            return true;
        }

        public override string ToString() => $"{Id} - {Nome} - {Laboratorio} - {qtdDisponivel()}";

        public override bool Equals(object? obj) => obj is Medicamento outro && Id == outro.Id;
        public override int GetHashCode() => Id.GetHashCode();
    }
}
