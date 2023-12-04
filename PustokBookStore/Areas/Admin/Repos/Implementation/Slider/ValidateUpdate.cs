using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PustokBookStore.Areas.Admin.Repos.Abstract;
using PustokBookStore.Data;
using PustokBookStore.Models;
using System.Net;

namespace PustokBookStore.Areas.Admin.Repos.Implementation.Slider
{
    public class ValidateUpdate : ActionFilterAttribute, IBaseValidation
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Todo find nice way to get model...
            if (context.ActionArguments.ContainsKey("model") && context.ActionArguments["model"] is BookSliderModel updatedModel)
            {
                var dbContext = (AppDbContext?)context.HttpContext.RequestServices.GetService(typeof(AppDbContext));

                if (dbContext is null)
                {
                    context.Result =  new StatusCodeResult(500);

                    return;
                }
                if (Validate(updatedModel, dbContext, ref context))
                {
                    await next();

                    return;
                }
                else
                {
                    context.Result = new BadRequestResult();

                    return;
                }
            }
            else
            {
                context.Result = new BadRequestResult();
            }
        }


        public bool Validate(BookSliderModel updatedModel, AppDbContext dbContext, ref ActionExecutingContext requestContext)
        {

            BookSliderModel oldModel = dbContext.BookSliders.FirstOrDefault(x => x.Id == updatedModel.Id);

            if (oldModel is null) return false;


            //pass controller
            requestContext.HttpContext.Items["oldModel"] = oldModel;

            //check user credentials and privileges

            //needs dto
            requestContext.ModelState.Remove("ImageName");

            return requestContext.ModelState.IsValid;
        }
    }


   
}
