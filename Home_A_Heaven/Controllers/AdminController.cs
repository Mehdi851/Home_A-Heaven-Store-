using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Home_A_Heaven.Models;

namespace Home_A_Heaven.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        #region Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminModel model)
        {
            using (var context = new Home_A_HeavenDBEntities())
            {
                bool isValid = context.Admins.Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isValid)
                {
                    var result = context.Admins.Where(x => x.Email == model.Email).Select(x => new AdminModel()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email

                    }).FirstOrDefault();
                    Session["LogedinAdminUsername"] = result.Username;
                    Session["LogedinAdminID"] = result.Id;
                    Session["LoginAdminStatus"] = "true";


                    var User = context.Admins.FirstOrDefault(x => x.Id == result.Id);
                    if (result != null)
                    {
                        User.Status = "true";
                    }

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username and Password");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {

            if (Session["LogedinUserID"] != null)
            {


                using (var context = new Home_A_HeavenDBEntities())
                {

                    int id = (int)Session["LogedinAdminID"];
                    AdminModel admin = context.Admins.Where(x => x.Id == id).Select(x => new AdminModel()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email

                    }).FirstOrDefault();
                    //Session["LogedinUsername"] = result.Username;
                    //Session["LogedinUserID"] = result.Id;

                    Admin admin1 = context.Admins.FirstOrDefault(x => x.Id == admin.Id);
                    if (User != null)
                    {
                        admin1.Status = "False";
                        Session["LogedinAdminID"] = "False";
                    }

                    context.SaveChanges();

                }
            }

            return RedirectToAction("Login");


        }
        #endregion

        #region Admin Product Managment
        public ActionResult Index()
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    IEnumerable<ProductModel> products = db.Products.Select(x => new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Old_Price = x.Old_Price,
                        New_Price = x.New_Price,
                        Discount = x.Discount,
                        Matrial = x.Matrial,
                        Color = x.Color,
                        Height = x.Height,
                        Size = x.Size,
                        Discription = x.Discription,
                        InsertDate = x.InsertDate,
                        Type = x.Type,
                        Selling = x.Selling,
                        Status = x.Status,
                    }).ToList();
                    return View(products);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
           
            
        }
        public ActionResult ProductDetail(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    ProductModel product = db.Products.Where(x => x.Id == id).Select(x => new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Old_Price = x.Old_Price,
                        New_Price = x.New_Price,
                        Discount = x.Discount,
                        Matrial = x.Matrial,
                        Color = x.Color,
                        Height = x.Height,
                        Size = x.Size,
                        Discription = x.Discription,
                        InsertDate = x.InsertDate,
                        Type = x.Type,
                        Selling = x.Selling,
                        Status = x.Status,
                    }).FirstOrDefault();
                    return View(product);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
          
           
        }
        public ActionResult ProductUpdate(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    ProductModel product = db.Products.Where(x => x.Id == id).Select(x => new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Old_Price = x.Old_Price,
                        New_Price = x.New_Price,
                        Discount = x.Discount,
                        Matrial = x.Matrial,
                        Color = x.Color,
                        Height = x.Height,
                        Size = x.Size,
                        Discription = x.Discription,
                        InsertDate = x.InsertDate,
                        Type = x.Type,
                        Selling = x.Selling,
                        Status = x.Status,
                    }).FirstOrDefault();
                    IEnumerable<CategoryModel> categories = db.Category1.Select(x => new CategoryModel
                    {
                        Id = x.Id,
                        CategoryName = x.CategoryName,


                    }).ToList();
                    ViewBag.Categories = categories;
                    return View(product);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
           

        }
        [HttpPost]
        public ActionResult ProductUpdate(int id,ProductModel model)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                string categoryName = Request["Category"];

                using (var context = new Home_A_HeavenDBEntities())
                {
                    Product product = context.Products.Where(x => x.Id == id).FirstOrDefault();
                    product.Name = model.Name;
                    product.Old_Price = model.Old_Price;
                    product.New_Price = model.New_Price;
                    product.Discount = model.Discount;
                    product.Matrial = model.Matrial;
                    product.Color = model.Color;
                    product.Height = model.Height;
                    product.Size = model.Size;
                    product.Discription = model.Discription;
                    product.InsertDate = DateTime.Now;
                    product.Type = model.Type;
                    product.Selling = "";
                    product.Status = model.Status;
                    Category1 cat = context.Category1.Where(x => x.CategoryName == categoryName).FirstOrDefault();
                    product.CategoryId = cat.Id;
                    //context.Products.Add(product);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
           
            
        }
        public ActionResult AddProducts()
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    IEnumerable<CategoryModel> categories = db.Category1.Select(x => new CategoryModel
                    {
                        Id = x.Id,
                        CategoryName = x.CategoryName,


                    }).ToList();

                    ViewBag.Categories = categories;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }
        [HttpPost]
        public ActionResult AddProducts(ProductModel model)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                string subcategory = Request["subCategory"];
                string category = Request["Category"];
                Product product = new Product();
                product.Name = model.Name;
                product.Old_Price = model.Old_Price;
                product.New_Price = model.New_Price;
                product.Discount = model.Discount;
                product.Matrial = model.Matrial;
                product.Color = model.Color;
                product.Height = model.Height;
                product.Size = model.Size;
                product.Discription = model.Discription;
                product.InsertDate = DateTime.Now;
                product.Type = model.Type;
                product.Selling = "";
                product.Status = "Active";
                using (var context = new Home_A_HeavenDBEntities())
                {
                    Category1 category1 = context.Category1.Where(x => x.CategoryName == category).FirstOrDefault();
                    SubCategoryModel subCategory = context.SubCategories.Where(x => x.SubCateName == subcategory&&x.MainCateId==category1.Id).Select(x => new SubCategoryModel
                    {
                        Id = x.Id,
                        MainCateId = x.MainCateId,
                    }).FirstOrDefault();
                    product.CategoryId = category1.Id;
                    product.SubCategoryId = subCategory.Id;
                    context.Products.Add(product);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult ProductDelete(int id)
        {
            using (var db = new Home_A_HeavenDBEntities())
            {
                Product product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                List<ProductImage> images = db.ProductImages.Where(x => x.productId == id).ToList();
                foreach (ProductImage item in images)
                {
                    db.ProductImages.Remove(item);
                }
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");   
            }
          
        }

        public JsonResult GetSubCategories(string CategoryName)
        {
           
                using (var db = new Home_A_HeavenDBEntities())
                {
                    CategoryModel category = db.Category1.Where(x => x.CategoryName == CategoryName).Select(x => new CategoryModel
                    {

                        Id = x.Id,

                    }).FirstOrDefault();
                    List<SubCategoryModel> subCategories = db.SubCategories.Where(x => x.MainCateId == category.Id).Select(x => new SubCategoryModel
                    {
                        Id = x.Id,
                        SubCateName = x.SubCateName,
                        MainCateId = x.MainCateId


                    }).ToList();
                    db.Configuration.ProxyCreationEnabled = false;
                    return Json(subCategories, JsonRequestBehavior.AllowGet);
                }
            }
            
        #endregion

        #region Categories
        public ActionResult Categories()
        {

            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    IEnumerable<CategoryModel> categories = db.Category1.Select(x => new CategoryModel
                    {
                        Id = x.Id,
                        CategoryName=x.CategoryName,
                        ImageURL=x.ImageURL
                       

                    }).ToList();
                    return View(categories);
                }

            }
            else
            {
                return RedirectToAction("Login");
            }


        }
        public ActionResult AddCategorie()
        {
            if (Session["LoginAdminStatus"] == "true")
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        [HttpPost]
        public ActionResult AddCategorie(CategoryModel model)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.ImageURL = "~/Images/CategoryImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/CategoryImages/"), fileName);
                model.ImageFile.SaveAs(fileName);
                Category1 category = new Category1();
                category.CategoryName = model.CategoryName;
                category.ImageURL = model.ImageURL;
               
                using (var context = new Home_A_HeavenDBEntities())
                {
                    context.Category1.Add(category);
                    context.SaveChanges();
                }
                return RedirectToAction("Categories");

            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult DeleteCategory(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    var category = db.Category1.Where(x => x.Id == id).FirstOrDefault();
                    IEnumerable <SubCategory> subCategories= db.SubCategories.Where(x => x.MainCateId == id).ToList();
                    IEnumerable<Product> products= db.Products.Where(x => x.CategoryId == id).ToList();
                    foreach (SubCategory item in subCategories)
                    {
                        db.SubCategories.Remove(item);
                    }
                    foreach (Product product in products)
                    {
                        db.Products.Remove(product);
                    }
                    db.Category1.Remove(category);
                    db.SaveChanges();
                    return RedirectToAction("Categories");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
           

        }
        #endregion

        #region Sub Category 

        public ActionResult SubCategory(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    IEnumerable<SubCategoryModel> subCategories = db.SubCategories.Where(x => x.MainCateId == id).Select(x => new SubCategoryModel
                    {

                        Id = x.Id,
                        MainCateId = x.MainCateId,
                        SubCateName = x.SubCateName,
                        ImageUrl = x.ImageUrl
                    }).ToList();
                    ViewBag.CategoryId = id;
                    return View(subCategories);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            
           
        }

        public ActionResult AddSubCategorie(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                ViewBag.CategoryId = id;
            return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        [HttpPost]
        public ActionResult AddSubCategorie(SubCategoryModel model)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            model.ImageUrl = "~/Images/CategoryImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/CategoryImages/"), fileName);
            model.ImageFile.SaveAs(fileName);
            SubCategory subCategory = new SubCategory();
            subCategory.SubCateName = model.SubCateName;
            subCategory.ImageUrl = model.ImageUrl;
            subCategory.MainCateId = model.MainCateId;


            using (var context = new Home_A_HeavenDBEntities())
            {
                context.SubCategories.Add(subCategory);
                context.SaveChanges();
            }
            return RedirectToAction("Categories");

            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult DeleteSubCategory(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    var subCategory = db.SubCategories.Where(x => x.Id == id).FirstOrDefault();
                    IEnumerable<Product> products = db.Products.Where(x => x.SubCategoryId == id).ToList();
                    foreach (Product item in products)
                    {
                        db.Products.Remove(item);
                    }
                    db.SubCategories.Remove(subCategory);
                    db.SaveChanges();
                    return RedirectToAction("Categories");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
           

        }
        #endregion

        #region Product Images
        public ActionResult ProductImages(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    IEnumerable<ProductImageModel> ProductImages = db.ProductImages.Where(x => x.productId == id).Select(x => new ProductImageModel
                    {
                        Id = x.Id,
                        ImageURL = x.ImageURL

                    }).ToList();
                    ViewBag.ProductId = id;
                    return View(ProductImages);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        public ActionResult AddProductImages(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    ProductModel product = db.Products.Where(x => x.Id == id).Select(x => new ProductModel
                    {

                        Id = x.Id,
                        Name = x.Name
                    }).FirstOrDefault();
                    ViewBag.Product = product;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        [HttpPost]
        public ActionResult AddProductImages(ProductImageModel model)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.ImageURL = "~/Images/ProductImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/ProductImages/"), fileName);
                model.ImageFile.SaveAs(fileName);
                ProductImage image = new ProductImage();
                image.productId = model.productId;
                image.ImageURL = model.ImageURL;
                image.ImageFile = model.ImageFile;
                using (var db = new Home_A_HeavenDBEntities())
                {
                    db.ProductImages.Add(image);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        #endregion

        #region Order Management
        public ActionResult ViewOrders()
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    IEnumerable<OrderModel> orders = db.Orders.Select(x => new OrderModel
                    {
                        Id = x.Id,
                        OrderDate = x.OrderDate,
                        SubTotal = x.SubTotal,
                        UserId = x.UserId,

                    }).ToList();
                    IEnumerable<UserModel> users = db.Users.Select(x => new UserModel
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Username = x.Username,
                        Address = x.Address,
                        ContactNo = x.ContactNo
                    }).ToList();
                    ViewBag.Users = users;
                    return View(orders);
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }
        public ActionResult OrderDetail(int id)
        {
            if (Session["LoginAdminStatus"] == "true")
            {
                using (var db = new Home_A_HeavenDBEntities())
                {
                    IEnumerable<OrderDetailModel> orderDetails = db.OrderDetails.Where(x => x.OrderId == id).Select(x => new OrderDetailModel
                    {
                        Id = x.Id,
                        OrderId = x.OrderId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        Total = x.Total

                    }).ToList();
                    IEnumerable<ProductModel> products = db.Products.Select(x => new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name

                    }).ToList();
                    ViewBag.Products = products;
                    return View(orderDetails);
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }
        #endregion
    }
}