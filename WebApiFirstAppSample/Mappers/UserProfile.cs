using System.Linq;
using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data.Entities;
using WebApiFirstAppSample.Models.Requests;

namespace WebApiFirstAppSample.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleNames, 
                    opt => opt.MapFrom(src => src.UserRoles.Select(role => role.Role.Name)));

        }
    }
}
