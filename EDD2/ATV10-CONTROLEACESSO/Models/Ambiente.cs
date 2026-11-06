using ATV10_controleacesso.Models;

namespace ATV10_controleacesso
{
    public class Ambiente
    {
        public int Id { get; }
        public string Nome { get; }
        private Queue<Log> logs;

        public IReadOnlyCollection<Log> Logs => Array.AsReadOnly(logs.ToArray());

        private const int MAX_LOGS = 100;
        public Ambiente(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
            this.logs = new Queue<Log>();
        }
        public void registrarLog(Log log)
        {
            if (this.logs.Count >= MAX_LOGS)
            {
                this.logs.Dequeue();
            }
            this.logs.Enqueue(log);
        }
    }
}