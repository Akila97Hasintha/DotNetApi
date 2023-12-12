using AutoMapper;
using ContosoUniversity.DataAccess.Entity;
using ContosoUniversity.Entity;
using ContosoUniversity.Models;
using ContosoUniversity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Course, CourseModel>().ReverseMap();
            // CreateMap<CourseModel, Course>();
            CreateMap<Student, StudentModel>().ReverseMap();
            //CreateMap<StudentModel, Student>();
            CreateMap<Enrollment, EnrollmentModel>().ReverseMap()
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade.HasValue ? src.Grade.Value : default(Models.Grade?))); ;
            CreateMap<Admin, AdminModel>().ReverseMap();
            //CreateMap<EnrollmentModel, Enrollment>();
        }
    }
}
