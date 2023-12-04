using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PustokBookStore.Areas.Admin.Repos.Abstract.Slider;
using PustokBookStore.Areas.Admin.Services;
using PustokBookStore.Data;
using PustokBookStore.Models;
using System.Net;

namespace PustokBookStore.Areas.Admin.Repos.Implementation.Slider
{
    public class ValidateDelete : ActionFilterAttribute, IValidationDelete
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Todo find nice way to get model...
            if (context.ActionArguments.ContainsKey("id") && context.ActionArguments["id"] is int sliderId)
            {
                var dbContext = (AppDbContext?) context.HttpContext.RequestServices.GetService(typeof(AppDbContext));

                if(dbContext is null)
                {
                    context.Result = new StatusCodeResult(500);

                    return;
                }
                if (Validate(sliderId, context.ModelState, dbContext, ref context))
                {
                    await next();

                    return;
                }
                else
                {
                    context.Result = new RedirectToActionResult("Show", "Slider", "");

                    return;
                }
            }
            else
            {
                context.Result = new BadRequestResult();
            }
        }


        private bool Validate(int sliderId, ModelStateDictionary modelState, AppDbContext dbContext, ref ActionExecutingContext requestContext)
        {

            BookSliderModel? model = dbContext.BookSliders.FirstOrDefault(x=>x.Id == sliderId);

            if (model is null) return false;

            //pass controller
            requestContext.HttpContext.Items["deletedModel"] = model;
            
            //check user credentials and privileges


            return true;
        }
    }


}
