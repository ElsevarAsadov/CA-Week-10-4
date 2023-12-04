using PustokBookStore.Models;

namespace PustokBookStore.Areas.Admin.Repos.Abstract.Slider
{
    public interface IValidationCreate : IBaseValidation
    {
        public bool HasValidSliderImage(IFormFile model);
    }
}
