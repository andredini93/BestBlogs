using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    public interface IPostRepository
    {
        public IEnumerable<Post> GetAll();
        public Post Get(Guid id);
        public Post Create(Post post);
        public Post Update(Guid id, Post post);
        public bool Delete(Guid id);
        public IEnumerable<Comment> GetByPostId(Guid id);
    }
}
