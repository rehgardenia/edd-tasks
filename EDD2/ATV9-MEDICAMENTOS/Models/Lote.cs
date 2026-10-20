namespace Models
{
    public class Lote
    {
        private int Id ;
        private int Qtde ;
        private DateTime Venc ;

        public Lote()
        { 
        }
        public Lote(int id, int qtde, DateTime venc)
        {
            Id = id;
            Qtde = qtde;
            Venc = venc;
        }
        public override string ToString()
        {
            return $"{Id} -  {Qtde} - {Venc.ToString("dd/MM/yyyy")}";
        }
    }
}