using System.Collections.Generic;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Xunit;

namespace Api.Tests
{
    public class CommentControllerTests
    {
        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;
        private Notificador _notificador;

        public CommentControllerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<BlogContext>().UseInMemoryDatabase("InMemoryDb").Options;
            var ctx = new BlogContext(contextOptions);
            _notificador = new Notificador();
            _postRepository = new PostRepository(ctx);
            _commentRepository = new CommentRepository(ctx, _postRepository, _notificador);            
        }

        [Fact]
        public void GetAll_Returns_Existing_Comments()
        {
            // Arrange
            var expected = new List<Comment>();

            // Act
            var actual = new CommentController(null, _commentRepository, _notificador).GetAll();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Contains("success = True", okObjectResult.Value.ToString());
        }
    }
}