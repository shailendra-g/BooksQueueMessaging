using System;

namespace BooksQueueMessaging
{

    public class Books
    {        
        //public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
    }
}

