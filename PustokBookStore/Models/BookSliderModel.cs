using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokBookStore.Models
{
    public class BookSliderModel
    {
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 50)]
        public string AuthorName { get; set; }
        [Required, StringLength(maximumLength: 50)]
        public string BookName { get; set; }
        [Required, StringLength(maximumLength: 30)]
        public string ButtonText { get; set; }
        [Required]
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile? imageFile { get; set; }
    
    }
}
