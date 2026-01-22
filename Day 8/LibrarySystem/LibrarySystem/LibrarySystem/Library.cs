using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Library
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Borrower> Borrowers { get; set; } = new List<Borrower>();

        public void AddBook(Book book) => Books.Add(book);

        public void RegisterBorrower(Borrower borrower) => Borrowers.Add(borrower);

        public void BorrowBook(string isbn, string libraryCardNumber)
        {
            var book = Books.FirstOrDefault(b => b.ISBN == isbn && !b.IsBorrowed);
            var borrower = Borrowers.FirstOrDefault(br => br.LibraryCardNumber == libraryCardNumber);

            if (book != null && borrower != null)
            {
                book.Borrow();
                borrower.BorrowBook(book);
            }
        }

        public void ReturnBook(string isbn, string libraryCardNumber)
        {
            var borrower = Borrowers.FirstOrDefault(br => br.LibraryCardNumber == libraryCardNumber);
            var book = borrower?.BorrowedBooks.FirstOrDefault(b => b.ISBN == isbn);

            if (book != null && borrower != null)
            {
                book.Return();
                borrower.ReturnBook(book);
            }
        }

        public List<Book> ViewBooks() => Books;
        public List<Borrower> ViewBorrowers() => Borrowers;
    }
}
