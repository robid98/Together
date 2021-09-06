using AutoMapper;
using Together.Data.DTOs;
using Together.Data.Models;

namespace Backend.Together.AutoMapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PostModel, PostDTO>();
            CreateMap<PostDTO, PostModel>();
        }
    }
}
