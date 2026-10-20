namespace Models
{
    public class Medicamento
    {
        private int Id ;
        private string Nome ;
        private string Laboratorio ;
        private Queue<Lote> Lotes ;

        public Medicamento()
        {
            Lotes = new Queue<Lote>();
        }
        public Medicamento(int id, string nome, string laboratorio)
        {
            Id = id;
            Nome = nome;
            Laboratorio = laboratorio;
            Lotes = new Queue<Lote>();
        }
        public int qtdDisponivel()
        {
            int total = 0;
            foreach (var lote in Lotes)
            {
                total += lote.Qtde;
            }
            return total;
        }
        public void comprar(Lote l)
        {
            Lotes.Enqueue(l);
        }
        public bool vender(int qtde)
        {
            if (qtde > qtdDisponivel())
            {
                return false;
                throw new Exception("Quantidade indisponível");
            }
            while (qtde > 0)
            {
                Lote loteAtual = Lotes.Peek();
                if (loteAtual.Qtde <= qtde)
                {
                    qtde -= loteAtual.Qtde;
                    Lotes.Dequeue();
                }
                else
                {
                    loteAtual.Qtde -= qtde;
                    qtde = 0;
                }
            }
            return true;
        }
        public override string ToString()
        {
            return $"{Id} - {Nome} - {Laboratorio} - {qtdDisponivel()}";
        }
     
        public override bool Equals(object obj)
        {
            if (obj is Medicamento other)
            {
                return Id == other.Id && Nome == other.Nome && Laboratorio == other.Laboratorio;
            }
            return false;
        }
        
      
    }
}