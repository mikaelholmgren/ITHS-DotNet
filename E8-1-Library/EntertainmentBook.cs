namespace E8_1_Library
{
    public class EntertainmentBook : Book
    {
        public string Type { get; set; }
        public bool Anthology { get; set; }
        public EntertainmentBook(string author, string title, int numPages, string type, bool anthology)
        {
            Author = author;
            Title = title;
            NumPages = numPages;
            Type = type;
            Anthology = anthology;
        }

    }
}
