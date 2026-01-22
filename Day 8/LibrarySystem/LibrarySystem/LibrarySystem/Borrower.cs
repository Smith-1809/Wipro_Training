using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Borrower
    {
        public string Name { get; set; }
        public string LibraryCardNumber { get; set; }
        public List<Book> BorrowedBooks { get; set; } = new List<Book>();

        public Borrower(string name, string cardNumber)
        {
            Name = name;
            LibraryCardNumber = cardNumber;
        }

        public void BorrowBook(Book book) => BorrowedBooks.Add(book);
        public void ReturnBook(Book book) => BorrowedBooks.Remove(book);
    }
}
