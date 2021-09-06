using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.DTOs;
using Together.Data.Models;

namespace Together.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> AddNewPost(PostModel post);

        Task<List<PostDTO>> GetAllPosts();
    }
}
