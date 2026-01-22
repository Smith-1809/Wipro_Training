using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LibrarySystem;

namespace LibrarySystemTest
{
    [TestClass]
    public class LibraryTests
    {
        private Library _library;

        [TestInitialize]
        public void Setup()
        {
            _library = new Library();
        }

        [TestMethod]
        public void AddBook_ShouldIncreaseBookCount()
        {
            var book = new Book("The Great Gatsby", "F. Scott Fitzgerald", "12345");
            _library.AddBook(book);

            Assert.AreEqual(1, _library.ViewBooks().Count);
            Assert.AreEqual("12345", _library.ViewBooks()[0].ISBN);
        }

        [TestMethod]
        public void RegisterBorrower_ShouldIncreaseBorrowerCount()
        {
            var borrower = new Borrower("John Doe", "BR001");
            _library.RegisterBorrower(borrower);

            Assert.AreEqual(1, _library.ViewBorrowers().Count);
        }

        [TestMethod]
        public void BorrowBook_ShouldMarkBookAsBorrowedAndAddToBorrowerList()
        {
            // Arrange
            var book = new Book("C# Basics", "Wipro", "ISBN001");
            var borrower = new Borrower("Alice", "CARD01");
            _library.AddBook(book);
            _library.RegisterBorrower(borrower);

            // Act
            _library.BorrowBook("ISBN001", "CARD01");

            // Assert
            Assert.IsTrue(book.IsBorrowed);
            Assert.AreEqual(1, borrower.BorrowedBooks.Count);
            Assert.AreEqual("ISBN001", borrower.BorrowedBooks[0].ISBN);
        }

        [TestMethod]
        public void ReturnBook_ShouldMarkBookAvailableAndRemoveFromBorrowerList()
        {
            // Arrange
            var book = new Book("C# Basics", "Wipro", "ISBN001");
            var borrower = new Borrower("Alice", "CARD01");
            _library.AddBook(book);
            _library.RegisterBorrower(borrower);
            _library.BorrowBook("ISBN001", "CARD01");

            // Act
            _library.ReturnBook("ISBN001", "CARD01");

            // Assert
            Assert.IsFalse(book.IsBorrowed);
            Assert.AreEqual(0, borrower.BorrowedBooks.Count);
        }

        [TestMethod]
        public void ViewBooksAndBorrowers_ShouldReturnCorrectLists()
        {
            _library.AddBook(new Book("Book 1", "Author 1", "B1"));
            _library.RegisterBorrower(new Borrower("User 1", "U1"));

            Assert.IsNotNull(_library.ViewBooks());
            Assert.IsNotNull(_library.ViewBorrowers());
            Assert.AreEqual(1, _library.ViewBooks().Count);
            Assert.AreEqual(1, _library.ViewBorrowers().Count);
        }
    }
}
