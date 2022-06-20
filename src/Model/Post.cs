using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public record Post
    {
        public Guid Id { get; set; }

        [StringLength(30, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        public string Title { get; set; }

        [StringLength(120, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}