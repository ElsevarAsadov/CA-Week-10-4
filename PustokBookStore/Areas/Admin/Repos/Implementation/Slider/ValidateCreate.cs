using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PustokBookStore.Models;
using PustokBookStore.Areas.Admin.Repos.Abstract;
using PustokBookStore.Areas.Admin.Repos.Abstract.Slider;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Mime;
using Microsoft.AspNetCore.HttpLogging;
using PustokBookStore.Areas.Admin.Services;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateCreateAttribute : ActionFilterAttribute, IValidationCreate
{

    //should be .env file configuration
    public readonly string[] ValidImageTypes = { MediaTypeNames.Image.Jpeg};

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //Todo: find nice way to get model...
        if (context.ActionArguments.ContainsKey("model") && context.ActionArguments["model"] is BookSliderModel newSlider)
        {
            if (Validate(newSlider, context.ModelState))
            {
                await next();
            }
            else
            {
                context.Result = new ViewResult();
            }
        }
        else
        {
            context.Result = new BadRequestResult();
        }
    }

    private bool Validate(BookSliderModel newSlider, ModelStateDictionary modelState)
    {
        //need dto
        modelState.Remove("ImageName");

        

        if (modelState.IsValid)
        {
            if (!HasValidSliderImage(newSlider.imageFile)) modelState.AddModelError("ImageFile", "Invalid Image");
            //image saving should not be saved here but app doesnt have dto 
            newSlider.ImageName = FileManagerService.Save(newSlider.imageFile);
            return true;
        }

        

        return false;
    }


    public bool HasValidSliderImage(IFormFile file)
    {
        return ValidImageTypes.Contains(file.ContentType) && file.Length < 5 * 1024 * 1024;
    }



}
