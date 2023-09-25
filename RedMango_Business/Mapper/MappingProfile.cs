using AutoMapper;
using RedMango_DataLayer.Models;
using RedMango_Models.DTOs;

namespace RedMango_Business.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
    }
}
