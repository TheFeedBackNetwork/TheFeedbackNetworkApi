using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TFN.Api.Models.InputModels
{
    public class PostInputModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string TrackUrl { get; set; }
        public IReadOnlyList<string> Tags { get; set; }
        [Required]
        public GenreEnumModel Genre { get; set; }
    }
}
