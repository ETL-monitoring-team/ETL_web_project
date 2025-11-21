using AutoMapper;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;

namespace ETL_web_project.Mappings
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            // Entity -> DTO
            CreateMap<UserAccount, UserDto>();

            // RegisterDto -> Entity
            CreateMap<RegisterDto, UserAccount>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            // Şimdilik hash yok, plain text saklıyoruz.
            // Sonra gerçek hash fonksiyonu ekleriz.
        }
    }
}
