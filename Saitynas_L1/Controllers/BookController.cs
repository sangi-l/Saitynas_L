using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Saitynas_L1.Data.Dtos.Book;
using Saitynas_L1.Data.Entities;
using Saitynas_L1.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Controllers
{
    [ApiController]
    [Route("api/department/{departmentId}/author/{authorId}/book")]
    public class BookController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAllAsync(int departmentId, int authorId)
        {
            var author = await _authorRepository.GetAsync(departmentId, authorId);
            if (author == null) authorId = 0;

            var book = await _bookRepository.GetAsync(authorId);
            return book.Select(o => _mapper.Map<BookDto>(o));
        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult<BookDto>> GetAsync(int departmentId, int authorId, int bookId)
        {
            var author = await _authorRepository.GetAsync(departmentId, authorId);
            if (author == null) authorId = 0;

            var book = await _bookRepository.GetAsync(authorId, bookId);
            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> PostAsync(int departmentId, int authorId, BookDto bookDto)
        {
            var author = await _authorRepository.GetAsync(departmentId, authorId);
            if (author == null) return NotFound($"Couldn't find an author with id of {authorId}");

            var book = _mapper.Map<Book>(bookDto);
            book.AuthorId = authorId;

            await _bookRepository.InsertAsync(book);

            return Created($"/api/department/{departmentId}/author/{authorId}/book/{book.Id}", _mapper.Map<BookDto>(book));
        }

        [HttpPut("{bookId}")]
        public async Task<ActionResult<BookDto>> PutAsync(int departmentId, int authorId, int bookId, UpdateBookDto bookDto)
        {
            var author = await _authorRepository.GetAsync(departmentId, authorId);
            if (author == null) return NotFound($"Couldn't find an author with id of {authorId}");

            var oldBook = await _bookRepository.GetAsync(authorId, bookId);
            if (oldBook == null)
                return NotFound();

            _mapper.Map(bookDto, oldBook);

            await _bookRepository.UpdateAsync(oldBook);

            return Ok(_mapper.Map<BookDto>(oldBook));
        }

        [HttpDelete("{bookId}")]
        public async Task<ActionResult> DeleteAsync(int departmentId, int authorId, int bookId)
        {
            var author = await _authorRepository.GetAsync(departmentId, authorId);
            if (author == null) return NotFound($"Couldn't find an author with id of {authorId}");

            var book = await _bookRepository.GetAsync(authorId, bookId);
            if (book == null)
                return NotFound();

            await _bookRepository.DeleteAsync(book);

            return NoContent();
        }
    }
}
