namespace Lab4.Models
{
    public class MyTask
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; }
    }
}