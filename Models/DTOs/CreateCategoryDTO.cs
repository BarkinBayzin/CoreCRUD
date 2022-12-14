using System.ComponentModel.DataAnnotations;

namespace CoreCRUD.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Must to type into category name")]
        [MinLength(3, ErrorMessage ="Minimum lenght is 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must to type into category description")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Description { get; set; }
    }
}
