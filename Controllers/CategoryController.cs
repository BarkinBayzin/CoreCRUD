using CoreCRUD.Infrastructure.EntityTypeConfiguration.Repositories.Concrete;
using CoreCRUD.Infrastructure.EntityTypeConfiguration.Repositories.Interface;
using CoreCRUD.Models.DTOs;
using CoreCRUD.Models.Entities.Abstract;
using CoreCRUD.Models.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreCRUD.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //GET url: localhost:4337/Category/Create
        public IActionResult Create()
        {            
            return View();
        }

        //Post işlemi yaparken HTTPPOST Attributu'u atmak zorundasınız!!
        [HttpPost]
        public IActionResult Create(CreateCategoryDTO model)
        {
            if(ModelState.IsValid) //parametre olarak aldığım sınıftaki validasyon ayarlarında geçebilmişse demek
            {
                Category category = new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                };
                _categoryRepository.Create(category);
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Validasyon ayarlarından geçemediniz..!");
                return View(model);
            }
        }

        public IActionResult List()
        {
            return View(_categoryRepository.GetByDefaults(x => x.Status != Status.Passive));
        }

        //Get url: localhost:5415/Category/Edit/1
        public IActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetByDefault(x => x.Id == id && x.Status != Status.Passive);

            if (category == null) return View("List");

            UpdateCategoryDTO model = new UpdateCategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                Category category = _categoryRepository.GetByDefault(x => x.Id == model.Id);

                category.Name = model.Name;
                category.Description = model.Description;
                _categoryRepository.Update(category);

                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Validasyon hataları mevcut..!");
                return View();
            }
        }

        //GET url: domain(localhost)/Category/Delete/1
        public IActionResult Delete(int id)
        {
            Category category = _categoryRepository.GetByDefault(x =>x.Id == id && x.Status != Status.Passive);

            if(category == null) return View("List");

            _categoryRepository.Delete(category);

            return RedirectToAction("List");
        }
    }
}
