using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetAll();
        public Comment Get(Guid id);
        public Comment Create(Comment comment);
        public Comment Update(Guid id, Comment comment);
        public bool Delete(Guid id);
        public IEnumerable<Comment> GetByPostId(Guid postId);
    }
}
