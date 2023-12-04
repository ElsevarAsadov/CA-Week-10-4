using Microsoft.AspNetCore.Mvc.Filters;

namespace PustokBookStore.Areas.Admin.Repos.Abstract
{
    public interface IBaseValidation
    {
      public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);
    }

    
}
