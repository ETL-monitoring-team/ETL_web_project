using AutoMapper;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;
using ETL_web_project.Handlers;

namespace ETL_web_project.Mappings
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, UserDto>();

            CreateMap<RegisterDto, UserAccount>()
                          .ForMember(dest => dest.PasswordHash,
                              opt => opt.MapFrom(src => PasswordHashHandler.HashPassword(src.Password)));
            CreateMap<UserAccount, AdminUserDto>();

        }
    }
}
