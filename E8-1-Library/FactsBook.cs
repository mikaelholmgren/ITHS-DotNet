namespace E8_1_Library
{
    public class FactsBook : Book
    {
        public string Topic { get; set; }
        public int Difficulty { get; set; }
        public FactsBook(string author, string title, int numPages, string topic, int difficulty)
        {
            Author = author;
            Title = title;
            NumPages = numPages;
            Topic = topic;
            Difficulty = difficulty;
        }
    }
}
