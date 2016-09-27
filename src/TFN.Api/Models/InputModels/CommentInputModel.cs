using System.ComponentModel.DataAnnotations;

namespace TFN.Api.Models.InputModels
{
    public class CommentInputModel
    {
        [Required]
        [MinLength(5)]
        public string Text { get; set; }
    }
}
