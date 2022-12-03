using System.Data;

namespace FicheUEAuthApi.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}