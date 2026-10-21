namespace Models
{
    public class Lote
    {
        public int Id { get; }
        public int Qtde { get; set; }
        public DateTime Venc { get; }

        public Lote(int id, int qtde, DateTime venc)
        {
            if (qtde <= 0) throw new ArgumentOutOfRangeException(nameof(qtde), "A quantidade deve ser positiva.");
            Id = id;
            Qtde = qtde;
            Venc = venc;
        }

        public override string ToString() => $"{Id} - {Qtde} - {Venc:dd/MM/yyyy}";
    }
}
