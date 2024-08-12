using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //tell autoampper we want to map from (AppUser) and where we want to map to (MemberDTO)
        CreateMap<AppUser, MemberDTO>()
            //set up are age in member dto to map from the Caculate age extension method
            .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
             //automapper didnt know how to map our photourl so we need to set it up
             //so we want to map the ismain url from photo (which is in our app user) to the photo url in our memberdto 
             //use the ! before .url, this is the null forgiving operator wich tell our compiler this is ok
            .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
        //tell autoampper we want to map from (Photo) and where we want to map to (PhotoDTO)
        CreateMap<Photo, PhotoDTO>();
    }
}
