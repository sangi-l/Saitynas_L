using AutoMapper;
using Saitynas_L1.Data.Dtos.Auth;
using Saitynas_L1.Data.Dtos.Author;
using Saitynas_L1.Data.Dtos.Book;
using Saitynas_L1.Data.Dtos.Department;
using Saitynas_L1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<UpdateBookDto, Book>();

            CreateMap<User, UserDto>();
        }
    }
}
