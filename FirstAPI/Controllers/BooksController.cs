using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        static private List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell", YearPublished = 1949 },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", YearPublished = 1960 },
            new Book { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", YearPublished = 1925 },
            new Book { Id = 4, Title = "Brave New World", Author = "Aldous Huxley", YearPublished = 1932 },
            new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", YearPublished = 1951 }
        };
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookbyId(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            books.Add(newBook);
            return CreatedAtAction(nameof(GetBookbyId), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            books.Remove(book);
            return NoContent();
        }
    }
}
