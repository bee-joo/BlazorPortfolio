namespace BlazorPortfolio.Server.Models
{
    public class Block
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public int Semester { get; set; }
    }
}
