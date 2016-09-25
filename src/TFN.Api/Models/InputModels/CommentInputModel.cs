using System.ComponentModel.DataAnnotations;

namespace TFN.Api.Models.InputModels
{
    public class CommentInputModel
    {
        [Required]
        public string Text { get; set; }
    }
}
