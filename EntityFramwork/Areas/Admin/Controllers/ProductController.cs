using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace EntityFramwork.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork { get; }
        private IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            IEnumerable<SelectListItem> CategoryDropDown = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CategoryDropDown = CategoryDropDown;
            if (id == null)
            {
                //this is for create
                return View(product);
            }
            //this is for edit
            product = _unitOfWork.Product.Get(u => u.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }   

        [HttpPost]
        public IActionResult UpSert(Product obj, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath + Path.GetExtension(file.FileName);

                string productPath = Path.Combine(webRootPath, @"images\product");

                if (!Directory.Exists(productPath))
                {
                    Directory.CreateDirectory(productPath);
                }

                if (!string.IsNullOrEmpty(obj.ImageUrl))
                {
                    string upload = Path.Combine(productPath, file.FileName);
                    using (var fileStream = new FileStream(upload, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ImageUrl = @"\images\product\" + file.FileName;
                }
                else
                {
                    obj.ImageUrl = @"\images\product\default.png";
                }

                if (obj.Id == 0)
                {
                    _unitOfWork.Product.Add(obj);
                }
                else
                {
                    _unitOfWork.Product.Update(obj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product saved successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
