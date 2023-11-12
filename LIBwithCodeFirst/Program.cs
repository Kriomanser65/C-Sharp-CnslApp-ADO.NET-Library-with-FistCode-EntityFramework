using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace LIBwithCodeFirst
{
    public class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDebtor { get; set; }

        public virtual ICollection<UserBook> UserBooks { get; set; }
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<UserBook> UserBooks { get; set; }
    }
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
    public class UserBook
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new LibraryContext())
            {
                var debtors = db.Users.Where(u => u.IsDebtor);
                Console.WriteLine("Debt list:");
                foreach (var debtor in debtors)
                {
                    Console.WriteLine(debtor.Name);
                }

                // Завдання 2
                var bookAuthors = db.Books.Find(3).Authors;
                Console.WriteLine("Book Authors No3:");
                foreach (var author in bookAuthors)
                {
                    Console.WriteLine(author.Name);
                }

                // Завдання 3
                var availableBooks = db.Books.Where(b => !b.UserBooks.Any());
                Console.WriteLine("Available Book list:");
                foreach (var book in availableBooks)
                {
                    Console.WriteLine(book.Title);
                }
                var userBooks = db.UserBooks.Where(ub => ub.UserId == 2).Select(ub => ub.Book);
                Console.WriteLine("User`s Book list No2:");
                foreach (var book in userBooks)
                {
                    Console.WriteLine(book.Title);
                }
            }
        }
    }
}
