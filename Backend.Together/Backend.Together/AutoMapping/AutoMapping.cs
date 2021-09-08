using AutoMapper;
using Together.Data.DTOs;
using Together.Data.Models;

namespace Backend.Together.AutoMapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            /* Posts */
            CreateMap<PostModel, PostDTO>();
            CreateMap<PostDTO, PostModel>();
            /* Comments */
            CreateMap<CommentModel, CommentDTO>();
            CreateMap<CommentDTO, CommentModel>();
            /* Replies */
            CreateMap<ReplyModel, ReplyDTO>();
            CreateMap<ReplyDTO, ReplyModel>();
        }
    }
}
