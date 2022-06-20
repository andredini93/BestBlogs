using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.Interfaces;
using Repository.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : MainController
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepository;

        public PostController(ILogger<PostController> logger, 
            IPostRepository postRepository,
            INotificador notificador) : base(notificador)
        {
            _logger = logger;
            _postRepository = postRepository;
        }

        /// <summary>
        /// Retorna todos os Posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAll()
        {
            return CustomResponse(_postRepository.GetAll());
        }

        /// <summary>
        /// Retorna um post baseado no Id informado via parmetro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public ActionResult<Post> Get([FromRoute] Guid id)
        {
            var post = _postRepository.Get(id);

            if (post == null) return NotFound();

            return post;
        }

        /// <summary>
        /// Criar um Post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Post> Post([FromBody] Post post)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _postRepository.Create(post);

            return CustomResponse(post);
        }

        /// <summary>
        /// Atualiza um Post baseado no conteudo informado via parametro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public ActionResult<Post> Put([FromRoute] Guid id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(post);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _postRepository.Update(id, post);

            return CustomResponse(post);
        }

        /// <summary>
        /// Deleta um Post baseado no conteudo informado via parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var post = _postRepository.Get(id);

            if (post == null) return NotFound();

            _postRepository.Delete(id);

            return CustomResponse(post);
        }

        /// <summary>
        /// Retorna todos os Comentarios de um Post especifico de acordo com o Id informado via parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}/comments")]
        public ActionResult<IEnumerable<Comment>> GetComments([FromRoute] Guid id)
        {
            var comments = _postRepository.GetByPostId(id);

            if (comments == null) return NotFound();

            return CustomResponse(comments);
        }
    }
}