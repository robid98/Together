using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Data.Models;

namespace Together.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostModel> AddNewPost(PostModel post);

        Task<List<PostModel>> GetAllPosts();
    }
}
