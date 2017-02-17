using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TFN.Mvc.Attributes;

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

        [TagsValid(ErrorMessage = "One or more of tags specified are not URL safe.")]
        public IReadOnlyList<string> Tags { get; set; }

        [Required(ErrorMessage = "Genre required")]
        public GenreEnumModel Genre { get; set; }
    }
}
