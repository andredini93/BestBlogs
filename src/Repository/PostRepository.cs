using System;
using System.Collections.Generic;
using Model;
using Repository.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class PostRepository : IPostRepository
    {
        protected readonly BlogContext Db;

        public PostRepository(BlogContext db)
        {
            Db = db;
        }

        public IEnumerable<Post> GetAll()
        {
            return Db.Set<Post>().ToList();
        }

        public Post Get(Guid id)
        {
            return Db.Find<Post>(id);
        }

        public Post Create(Post post)
        {
            Db.Add<Post>(post);
            Db.SaveChanges();
            return post;
        }

        public Post Update(Guid id, Post post)
        {
            Db.Entry<Post>(Get(id)).CurrentValues.SetValues(post);
            Db.SaveChanges();
            return post;
        }

        public bool Delete(Guid id)
        {
            Db.Set<Post>().Remove(Get(id));
            Db.SaveChanges();
            return true;
        }

        public IEnumerable<Comment> GetByPostId(Guid id)
        {
            return Db.Set<Comment>().Where(x => x.PostId == id).ToList();
        }
    }
}