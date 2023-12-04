using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBookStore.Areas.Admin.Repos.Implementation.Slider;
using PustokBookStore.Areas.Admin.Services;
using PustokBookStore.Data;
using PustokBookStore.Models;

namespace PustokBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext DB;
        public SliderController(AppDbContext dbContext)
        {
            DB = dbContext;
        }

        public async Task<IActionResult> Create() {

            return View();
        }


        [HttpPost, ValidateCreate]
        public async Task<IActionResult> Create(BookSliderModel model) {

            
            DB.BookSliders.Add(model);
            DB.SaveChanges();

            return RedirectToAction("Show");
        }

        public async Task<IActionResult> Show()
        {
            return View(await DB.BookSliders.ToListAsync());
        }

        public async Task<IActionResult> Update(int id) {

            BookSliderModel? model = DB.BookSliders.FirstOrDefault(x=>x.Id == id);

            if (model is null) return BadRequest();


            return View(model);
        
        }

        [HttpPost, ValidateUpdate]
        public async Task<IActionResult> Update(BookSliderModel model)
        {

            //just in case for memory corruption.Extremely rare case -> CVE-2020-0796

            if (!HttpContext.Items.ContainsKey("oldModel")) return StatusCode(500);

            //get the model
            BookSliderModel oldModel = (BookSliderModel)HttpContext.Items["oldModel"];


            if (model is null) return StatusCode(500);

            if(model.imageFile is not null)
            {
                FileManagerService.Delete(oldModel.ImageName);
                oldModel.ImageName = FileManagerService.Save(model.imageFile);
            }

            oldModel.AuthorName = model.AuthorName;
            oldModel.BookName = model.BookName;
            oldModel.BookName = model.ButtonText;

            DB.SaveChanges();
            return View(model);
        }

        [HttpPost, ValidateDelete]
        public async Task<IActionResult> Delete(int? id)
        {

            //just in case for memory corruption.Extremely rare case -> CVE-2020-0796

            if (! HttpContext.Items.ContainsKey("deletedModel")) return StatusCode(500);

            //get the model
            BookSliderModel model = (BookSliderModel)HttpContext.Items["deletedModel"];


            if(model is null) return StatusCode(500);


            FileManagerService.Delete(model.ImageName);

            DB.BookSliders.Remove(model);
            DB.SaveChanges();


            return RedirectToAction("Show");
        }


    }
}
