using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Interfaces;
using Repository.Interfaces;
using Repository.Services;

namespace Repository
{
    public class CommentRepository : BaseService, ICommentRepository
    {
        protected readonly BlogContext Db;
        private readonly IPostRepository _postRepository;

        public CommentRepository(BlogContext db, IPostRepository postRepository, INotificador notificador) : base(notificador)
        {
            Db = db;
            _postRepository = postRepository;
        }

        public IEnumerable<Comment> GetAll()
        {
            return Db.Set<Comment>().ToList();
        }

        public Comment Get(Guid id)
        {
            return Db.Find<Comment>(id);
        }

        public Comment Create(Comment comment)
        {
            if (_postRepository.Get(comment.PostId) == null)
            {
                Notificar("Não existe um Post com o Id Informado");
                return comment;
            }

            Db.Add<Comment>(comment);
            Db.SaveChanges();
            return comment;
        }

        public Comment Update(Guid id, Comment comment)
        {
            Db.Update<Comment>(comment);
            Db.SaveChanges();
            return comment;
        }

        public bool Delete(Guid id)
        {
            Db.Set<Comment>().Remove(Get(id));
            Db.SaveChanges();
            return true;
        }

        public IEnumerable<Comment> GetByPostId(Guid postId)
        {
            return Db.Set<Comment>().Where(x => x.PostId == postId).ToList();
        }
    }
}