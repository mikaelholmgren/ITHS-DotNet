namespace E8_1_Library
{
    public class ChildrensBook : Book
    {
        public bool YoungAdults { get; set; }
        public bool PictureBook { get; set; }
        public ChildrensBook(string author, string title, int numPages, bool youngAdults, bool pictureBook)
        {
            Author = author;
            Title = title;
            NumPages = numPages;
            YoungAdults = youngAdults;
            PictureBook = pictureBook;
        }

    }
}
