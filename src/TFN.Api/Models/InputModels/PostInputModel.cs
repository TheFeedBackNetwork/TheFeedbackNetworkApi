using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TFN.Api.Models.InputModels
{
    public class PostInputModel
    {
        [Required]
        [MinLength(5)]
        public string Text { get; set; }
        [Required]
        [MinLength(10)]
        public string TrackUrl { get; set; }
        public IReadOnlyList<string> Tags { get; set; }
        [Required]
        public GenreEnumModel Genre { get; set; }
    }
}
