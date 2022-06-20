using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public record Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }

        [StringLength(120, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        public string Content { get; set; }

        [StringLength(30, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
    }
}