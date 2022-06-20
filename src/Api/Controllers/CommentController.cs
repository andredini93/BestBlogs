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
    [Route("comments")]
    public class CommentController : MainController
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ILogger<CommentController> logger, 
                ICommentRepository commentRepository,
                INotificador notificador) : base(notificador)
        {
            _logger = logger;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetAll()
        {
            return CustomResponse(_commentRepository.GetAll());
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Comment> Get([FromRoute] Guid id)
        {
            var comment = _commentRepository.Get(id);

            if (comment == null) return NotFound();

            return comment;
        }

        [HttpPost]
        public ActionResult<Comment> Post([FromBody] Comment comment)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _commentRepository.Create(comment);

            return CustomResponse(comment);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] Comment comment)
        {
            if(id != comment.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(comment);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _commentRepository.Update(id, comment);

            return CustomResponse(comment);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var comment = _commentRepository.Get(id);

            if(comment == null) return NotFound();

            _commentRepository.Delete(id);

            return CustomResponse(comment);
        }
    }
}