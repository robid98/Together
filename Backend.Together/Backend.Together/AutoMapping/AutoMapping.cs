using AutoMapper;
using Together.API.AutoMapping;
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
            /* UserAuth */
            CreateMap<UserAuthenticationModel, UserAuthenticationDTO>();
            CreateMap<UserAuthenticationDTO, UserAuthenticationModel>();
            /* UserProfile */
            CreateMap<UserProfileModel, UserProfileDTO>();
            CreateMap<UserProfileDTO, UserProfileModel>();

            CreateMap<UserProfileModel, UserProfileModel>()
                .Ignore(record => record.UserProfileId)
                .Ignore(record => record.UserId)
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PostModel, PostModel>()
                .Ignore(record => record.PostId)
                .Ignore(record => record.UserProfileId)
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserAuthenticationModel, UserAuthenticationModel>()
                .Ignore(record => record.UserId)
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
