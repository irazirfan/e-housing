using System.ComponentModel.DataAnnotations;

namespace webApi.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage= "Only numerics are not allowed as Name")]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
    }
}