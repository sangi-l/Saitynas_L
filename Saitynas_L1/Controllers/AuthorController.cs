using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Saitynas_L1.Data.Dtos.Author;
using Saitynas_L1.Data.Entities;
using Saitynas_L1.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Controllers
{
    [ApiController]
    [Route("api/department/{departmentId}/author")]
    public class AuthorController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IDepartmentRepository departmentRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAllAsync(int departmentId)
        {
            var author = await _authorRepository.GetAsync(departmentId);
            return author.Select(o => _mapper.Map<AuthorDto>(o));
        }

        [HttpGet("{authorId}")]
        public async Task<ActionResult<AuthorDto>> GetAsync(int departmentId, int authorId)
        {
            var author = await _authorRepository.GetAsync(departmentId, authorId);
            if (author == null) return NotFound();

            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAsync(int departmentId, AuthorDto authorDto)
        {
            var department = await _departmentRepository.GetAsync(departmentId);
            if (department == null) return NotFound($"Couldn't find a department with id of {departmentId}");

            var author = _mapper.Map<Author>(authorDto);
            author.DepartmentId = departmentId;

            await _authorRepository.InsertAsync(author);

            return Created($"/api/department/{departmentId}/author/{author.Id}", _mapper.Map<AuthorDto>(author));
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult<AuthorDto>> PutAsync(int departmentId, int authorId, UpdateAuthorDto authorDto)
        {
            var department = await _departmentRepository.GetAsync(departmentId);
            if (department == null) return NotFound($"Couldn't find a department with id of {departmentId}");

            var oldAuthor = await _authorRepository.GetAsync(departmentId, authorId);
            if (oldAuthor == null)
                return NotFound();

            _mapper.Map(authorDto, oldAuthor);

            await _authorRepository.UpdateAsync(oldAuthor);

            return Ok(_mapper.Map<AuthorDto>(oldAuthor));
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult> DeleteAsync(int departmentId, int authorId)
        {
            var author = await _authorRepository.GetAsync(departmentId, authorId);
            if (author == null)
                return NotFound();

            await _authorRepository.DeleteAsync(author);

            return NoContent();
        }
    }
}
