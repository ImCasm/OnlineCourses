using Aplicacion.Courses.DTO;
using AutoMapper;
using Dominio;
using System;
using System.Linq;

namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>().
                ForMember(c => c.Teachers, y => y.MapFrom(z => z.Teachers.Select(a => a.Teacher).ToList())).
                ForMember(c => c.Price, y => y.MapFrom(z => z.OfferPrice)).
                ForMember(c => c.Comments, y => y.MapFrom(z => z.Comments));
            CreateMap<Teacher, TeacherDTO>();   
            CreateMap<CourseTeacher, CourseTeacherDTO>();
            CreateMap<Price, PriceDTO>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}
