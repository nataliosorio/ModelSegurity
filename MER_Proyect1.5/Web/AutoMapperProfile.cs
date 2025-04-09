using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace Entity.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Ejemplo de mapeo
            CreateMap<Form, FormDto>();
            CreateMap<FormDto, Form>();
            CreateMap<Module, ModuleDto>();
            CreateMap<ModuleDto, Module>();
            //.ForMember(dest => dest.formname, opt => opt.MapFrom(src => src.Form.name))
            //.ForMember(dest => dest.modulename, opt => opt.MapFrom(src => src.Module.name))
            //.ReverseMap();
        }
    }
}
