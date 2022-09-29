using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Home_A_Heaven.Models;
using PayPal.Api;

namespace Home_A_Heaven.Controllers
{
    public class HomeController : Controller
    {
       
        private List<CartModel> cartList;
        public HomeController()
        {
            cartList = new List<CartModel>();
        }

        #region Product Management 
        public ActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            using (var db = new Home_A_HeavenDBEntities())
            {
                IEnumerable<CategoryModel> categories = db.Category1.Select(x => new CategoryModel
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    ImageURL = x.ImageURL
                }).ToList();
               
                ViewBag.Categories = categories;
                IEnumerable<SubCategoryModel> subCategories = db.SubCategories.Select(x => new SubCategoryModel
                {
                    Id = x.Id,
                    SubCateName = x.SubCateName,
                    ImageUrl = x.ImageUrl,
                    MainCateId = x.MainCateId
                }).ToList();
                IEnumerable<ProductModel> products = db.Products.Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Old_Price = x.Old_Price,
                    New_Price = x.New_Price,
                    Discount = x.Discount,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                ViewBag.SubCategories = subCategories;
                return View("Index",products);
            }
            
           
        }

        public ActionResult ProductCategoryWise(int id)
        {
            using (var db = new Home_A_HeavenDBEntities())
            {
                IEnumerable<CategoryModel> categories = db.Category1.Select(x => new CategoryModel
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    ImageURL = x.ImageURL
                }).ToList();

                ViewBag.Categories = categories;
                IEnumerable<SubCategoryModel> subCategories = db.SubCategories.Select(x => new SubCategoryModel
                {
                    Id = x.Id,
                    SubCateName = x.SubCateName,
                    ImageUrl = x.ImageUrl,
                    MainCateId = x.MainCateId
                }).ToList();
                IEnumerable<ProductModel> products = db.Products.Where(x=>x.CategoryId==id).Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Old_Price = x.Old_Price,
                    New_Price = x.New_Price,
                    Discount = x.Discount,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                ViewBag.SubCategories = subCategories;
                return View("Index", products);
            }
        }
        public ActionResult AllCollection()
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
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                return View(products);
            }
        }
        public ActionResult Collection(int id)
        {

            using (var db = new Home_A_HeavenDBEntities())
            {
                SubCategoryModel subcategory = db.SubCategories.Where(x => x.Id == id).Select(x => new SubCategoryModel
                {
                    Id = x.Id,
                    MainCateId = x.MainCateId,
                    SubCateName = x.SubCateName
                }).FirstOrDefault();
                IEnumerable<ProductModel> products = db.Products.Where(x => x.SubCategoryId == subcategory.Id && x.CategoryId == subcategory.MainCateId).Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Old_Price = x.Old_Price,
                    New_Price = x.New_Price,
                    Discount = x.Discount,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                ViewBag.SubCatId = id;
                // return View(products);
                return View("Collection", products);
            }

        }
        
        public ActionResult ProductDetail(int id)
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
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status

                }).FirstOrDefault();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Where(x => x.productId == id).Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                return View(product);
            }
            //return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string ProductImage(int ProductId)
        {
            string productImage = "";
            using (var db = new Home_A_HeavenDBEntities())
            {
                ProductImageModel image = db.ProductImages.Where(x => x.productId == ProductId).Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    ImageURL = x.ImageURL

                }).FirstOrDefault();
                productImage = image.ImageURL;
                return productImage;
            }

        }



        #endregion

        #region Search
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string search = "")
        {
            string searchString = Request["SearchString"];
            if (searchString != null)
            {
                using (var db = new Home_A_HeavenDBEntities())
                {

                    IEnumerable<ProductModel> products = db.Products.Where(x => x.Name.Contains(searchString)).Select(x => new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Old_Price = x.Old_Price,
                        New_Price = x.New_Price,
                        Discount = x.Discount,
                        CategoryId = x.CategoryId,
                        SubCategoryId = x.SubCategoryId,
                        Matrial = x.Matrial,
                        Color = x.Color,
                        Height = x.Height,
                        Size = x.Size,
                        Discription = x.Discription,
                        InsertDate = x.InsertDate,
                        Type = x.Type,
                        Selling = x.Selling,
                        Status = x.Status
                    }).ToList();
                    IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                    {
                        Id = x.Id,
                        productId = x.productId,
                        ImageURL = x.ImageURL
                    }).ToList();
                    ViewBag.ProductImages = productImages;
                    return View(products);
                }
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region sorting
        public ActionResult AtoZ(int id)
        {
            using (var db = new Home_A_HeavenDBEntities())
            {
                SubCategoryModel subcategory = db.SubCategories.Where(x => x.Id == id).Select(x => new SubCategoryModel
                {
                    Id = x.Id,
                    MainCateId = x.MainCateId,
                    SubCateName = x.SubCateName
                }).FirstOrDefault();
                IEnumerable<ProductModel> products = db.Products.Where(x => x.SubCategoryId == subcategory.Id && x.CategoryId == subcategory.MainCateId).OrderBy(x => x.Name).Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Old_Price = x.Old_Price,
                    New_Price = x.New_Price,
                    Discount = x.Discount,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                ViewBag.SubCatId = id;
                // return View(products);
                return View("Collection", products);
            }
        }
        public ActionResult ZtoA(int id)
        {
            using (var db = new Home_A_HeavenDBEntities())
            {
                SubCategoryModel subcategory = db.SubCategories.Where(x => x.Id == id).Select(x => new SubCategoryModel
                {
                    Id = x.Id,
                    MainCateId = x.MainCateId,
                    SubCateName = x.SubCateName
                }).FirstOrDefault();
                IEnumerable<ProductModel> products = db.Products.Where(x => x.SubCategoryId == subcategory.Id && x.CategoryId == subcategory.MainCateId).OrderByDescending(x => x.Name).Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Old_Price = x.Old_Price,
                    New_Price = x.New_Price,
                    Discount = x.Discount,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                ViewBag.SubCatId = id;
                // return View(products);
                return View("Collection", products);
            }
        }
        public ActionResult LowtoHigh(int id)
        {
            using (var db = new Home_A_HeavenDBEntities())
            {
                SubCategoryModel subcategory = db.SubCategories.Where(x => x.Id == id).Select(x => new SubCategoryModel
                {
                    Id = x.Id,
                    MainCateId = x.MainCateId,
                    SubCateName = x.SubCateName
                }).FirstOrDefault();
                IEnumerable<ProductModel> products = db.Products.Where(x => x.SubCategoryId == subcategory.Id && x.CategoryId == subcategory.MainCateId).OrderBy(x => x.New_Price).Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Old_Price = x.Old_Price,
                    New_Price = x.New_Price,
                    Discount = x.Discount,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                ViewBag.SubCatId = id;
                // return View(products);
                return View("Collection", products);
            }
        }
        public ActionResult HightoLow(int id)
        {
            using (var db = new Home_A_HeavenDBEntities())
            {
                SubCategoryModel subcategory = db.SubCategories.Where(x => x.Id == id).Select(x => new SubCategoryModel
                {
                    Id = x.Id,
                    MainCateId = x.MainCateId,
                    SubCateName = x.SubCateName
                }).FirstOrDefault();
                IEnumerable<ProductModel> products = db.Products.Where(x => x.SubCategoryId == subcategory.Id && x.CategoryId == subcategory.MainCateId).OrderByDescending(x => x.New_Price).Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Old_Price = x.Old_Price,
                    New_Price = x.New_Price,
                    Discount = x.Discount,
                    CategoryId = x.CategoryId,
                    SubCategoryId = x.SubCategoryId,
                    Matrial = x.Matrial,
                    Color = x.Color,
                    Height = x.Height,
                    Size = x.Size,
                    Discription = x.Discription,
                    InsertDate = x.InsertDate,
                    Type = x.Type,
                    Selling = x.Selling,
                    Status = x.Status
                }).ToList();
                IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                {
                    Id = x.Id,
                    productId = x.productId,
                    ImageURL = x.ImageURL
                }).ToList();
                ViewBag.ProductImages = productImages;
                ViewBag.SubCatId = id;
                // return View(products);
                return View("Collection", products);
            }
        }
        #endregion
       
        #region Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (var context = new Home_A_HeavenDBEntities())
            {
                bool isValid = context.Users.Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isValid)
                {
                    var result = context.Users.Where(x => x.Email == model.Email).Select(x => new UserModel()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email

                    }).FirstOrDefault();
                    Session["LogedinUsername"] = result.Username;
                    Session["LogedinID"] = result.Id;
                    Session["LoginStatus"] = "true";


                    var User = context.Users.FirstOrDefault(x => x.Id == result.Id);
                    if (result != null)
                    {
                        User.Status = "true";
                    }

                    context.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Massage = "Invalid Email and Password";
                    return View();
                }
            }
        }
        public ActionResult Profile()
        {
            if (Session["LoginStatus"] == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            User user = new User();
            user.Username = model.Username;
            user.Email = model.Email;
            user.Password = model.Password;
            user.ContactNo = model.ContactNo;
            user.Address = model.Address;
            user.Status = "False";

            using (var context = new Home_A_HeavenDBEntities())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            if (Session["LogedinID"] != null)
            {


                using (var context = new Home_A_HeavenDBEntities())
                {

                    int id = (int)Session["LogedinID"];
                    UserModel user = context.Users.Where(x => x.Id == id).Select(x => new UserModel()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email

                    }).FirstOrDefault();
                    //Session["LogedinUsername"] = result.Username;
                    //Session["LogedinUserID"] = result.Id;

                    User user1 = context.Users.FirstOrDefault(x => x.Id == user.Id);
                    if (user1 != null)
                    {
                        user1.Status = "False";
                        Session["LoginStatus"] = "False";
                    }

                    context.SaveChanges();

                }
            }

            return RedirectToAction("Index");

        }

        #endregion

        #region Order
        public ActionResult Order()
        {
            if (Session["LoginStatus"] == "true")
            {
                int userid =(int) Session["LogedinID"];
                using (var db=new Home_A_HeavenDBEntities())
                {
                    UserModel user = db.Users.Where(x => x.Id == userid).Select(x => new UserModel { 
                        Id=x.Id,
                        Username=x.Username,
                        Email=x.Email,
                        Address=x.Address,
                        ContactNo=x.ContactNo
                    }).FirstOrDefault();
                    ViewBag.User = user;
                    cartList = Session["CartItem"] as List<CartModel>;
                    return View(cartList);
                }

               
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult SaveOrder()
        {
            if (Session["LoginStatus"] == "true")
            {
                int orderid = 0;
                double subTotal = 0;
                int userid = (int)Session["LogedinID"];
                cartList = Session["CartItem"] as List<CartModel>;
                using (var db = new Home_A_HeavenDBEntities())
                {
                    UserModel user = db.Users.Where(x => x.Id == userid).Select(x => new UserModel
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email,
                        Address = x.Address,
                        ContactNo = x.ContactNo
                    }).FirstOrDefault();
                    Order order = new Order();
                    order.Id = new Random().Next(10000);
                    order.OrderDate = DateTime.Now;
                    order.UserId = user.Id;
                    order.SubTotal = "";
                    db.Orders.Add(order);
                    db.SaveChanges();
                    orderid = order.Id;
                    foreach (var item in cartList)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = orderid;
                        orderDetail.ProductId = item.ProductId;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.UnitPrice = item.Price.ToString();
                        orderDetail.Total = item.Total.ToString();
                        subTotal = subTotal + item.Total;
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                    }
                    Order order1 = db.Orders.Where(x=>x.Id==orderid).FirstOrDefault();
                    order1.SubTotal = subTotal.ToString();
                    db.SaveChanges();
                    Session.Remove("CartCounter");
                    Session.Remove("CartItem");

                    return RedirectToAction("Index");
                }


            }
            else
            {
                return RedirectToAction("Login");
            }

           
        }

        #endregion

        #region Payment
        public ActionResult Cart(int id)
        {

            using (var db = new Home_A_HeavenDBEntities())
            {
                CartModel cart = new CartModel();
                Product product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                if (Session["CartCounter"] != null)
                {
                    cartList = Session["CartItem"] as List<CartModel>;
                }
                if (cartList.Any(model => model.Id == id))
                {
                    cart = cartList.Single(model => model.Id == id);
                    cart.Quantity = cart.Quantity + 1;
                    cart.Total = cart.Quantity * cart.Price;
                }
                else
                {
                    cart.Id = id;
                    cart.ProductId = product.Id;
                    cart.Price = Convert.ToDouble(product.New_Price);
                    cart.Quantity = 1;
                    cart.Total = cart.Price;
                    cartList.Add(cart);
                }

                Session["CartCounter"] = cartList.Count;
                Session["CartItem"] = cartList;
            }

            return RedirectToAction("ShoppingCart");





        }

        public ActionResult ShoppingCart()
        {
            if (Session["CartCounter"] != null)
            {
                using (var db = new Home_A_HeavenDBEntities())
                {

                    List<ProductModel> products = db.Products.Select(x => new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Old_Price = x.Old_Price,
                        New_Price = x.New_Price,

                    }).ToList();
                    IEnumerable<ProductImageModel> productImages = db.ProductImages.Select(x => new ProductImageModel
                    {
                        Id = x.Id,
                        productId = x.productId,
                        ImageURL = x.ImageURL
                    }).ToList();
                    ViewBag.Products = products;
                    ViewBag.ProductImages = productImages;
                }
                cartList = Session["CartItem"] as List<CartModel>;
            }
            return View(cartList);

        }

        public ActionResult RemoveCart(int id)
        {

            if (Session["CartCounter"] != null)
            {
                cartList = Session["CartItem"] as List<CartModel>;
                CartModel item = cartList.Find(model => model.Id == id);
                cartList.Remove(item);
                Session["CartCounter"] = cartList.Count;
                Session["CartItem"] = cartList;
            }
            return RedirectToAction("ShoppingCart");
        }
        #region PayPal

        //private Payment payment;

        //private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        //{
        //    var listItem = new ItemList() { items = new List<Item>() };
        //    List<CartModel> listCarts = (List < CartModel >) Session["CartItem"];

        //    foreach (var cart in listCarts)
        //    {
        //        listItem.items.Add(new Item()
        //        {
        //            name = GetProduct(cart.ProductId).Name,
        //            currency = "USD",
        //            price = GetProduct(cart.ProductId).New_Price,
        //            quantity = cart.Quantity.ToString(),
        //            sku="sku"

        //        }) ;
        //    }
        //    var payer = new Payer() { payment_method = "paypal" };
        //    //do configuration RedirectURLs here with redirectURLs object
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url=redirectUrl,
        //        return_url=redirectUrl
        //    };

        //    //create detail object
        //    var details = new Details() {

        //        tax = "1",
        //        shipping = "2",
        //        //subtotal=listCarts.Sum(x=>x.Quantity*(Convert.ToInt32(GetProduct(x.ProductId)))).ToString()
        //        subtotal = "500"

        //    };
        //    var amount = new Amount()
        //    {
        //        currency="USD",
        //        total=(Convert.ToDouble(details.tax)+ Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),
        //        details=details
        //    };

        //    var transactionList = new List<Transaction>();
        //    transactionList.Add(new Transaction()
        //    {
        //        description = "Test transaction for Home A Heaven",
        //        invoice_number=Convert.ToString((new Random().Next(10000))),
        //        amount=amount,
        //        item_list=listItem,

        //    });

        //    payment = new Payment()
        //    {
        //        intent="sale",
        //        payer=payer,
        //        transactions=transactionList,
        //        redirect_urls=redirUrls
        //    };

        //    return this.payment.Create(apiContext);

        //}

        //private Payment ExecutePayment(APIContext apiContext, string payerId,string paymentId)
        //{
        //    var paymentExecution = new PaymentExecution() { payer_id=payerId};
        //    payment = new Payment() { id = paymentId };
        //    return payment.Execute(apiContext,paymentExecution);
        //}

        //public ActionResult PaymentWithPaypal()
        //{
        //    APIContext apiContext = PaypalConfiguration.GetAPIContext();
        //    try
        //    {
        //        string payerId = Request.Params["PayerID"];
        //        if (string.IsNullOrEmpty(payerId))
        //        {
        //            //create payment
        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "Home/PaymentWithPaypal?";
        //            var guid = Convert.ToString((new Random().Next(10000)));
        //            var createPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);
        //            //get links returned from paypal response to create call function
        //            var links = createPayment.links.GetEnumerator();
        //            string paypalRedirectUrl = string.Empty;
        //            while (links.MoveNext())
        //            {
        //                Links link = links.Current;
        //                if (link.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    paypalRedirectUrl = link.href;
        //                }
        //            }
        //            Session.Add(guid, createPayment.id);
        //            return Redirect(paypalRedirectUrl);

        //        }
        //        else {
        //            //this one willbe executed when we have recived all the payment params from privious call
        //            var guid = Request.Params["guid"];
        //            var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
        //            if (executedPayment.state.ToLower()!="approved")
        //            {
        //                return View("Faulure");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //PaypalLogger.Log("Error" + ex.Message);
        //        ViewBag.Error = ex.Message;
        //        return View("Faulure");
        //    }

        //    return View("Success");
        //}

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Faulure");
            }
            //on successful payment, show success page to user.  
            return RedirectToAction("Success");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //create itemlist and add item objects to it  
            //var listItem = new ItemList() { items = new List<Item>() };
            List<CartModel> listCarts = (List<CartModel>)Session["CartItem"];

            //foreach (var cart in listCarts)
            //{
            //    listItem.items.Add(new Item()
            //    {
            //        name = GetProduct(cart.ProductId).Name,
            //        currency = "USD",
            //        price = GetProduct(cart.ProductId).New_Price,
            //        quantity = cart.Quantity.ToString(),
            //        sku = "sku"

            //    });
            //}

            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });

            //foreach (var cart in listCarts)
            //{
            //    itemList.items.Add(new Item()
            //    {
            //        name = GetProduct(cart.ProductId).Name,
            //        currency = "USD",
            //        price = GetProduct(cart.ProductId).New_Price,
            //        quantity = cart.Quantity.ToString(),
            //        sku = "sku"
            //    });
            //}
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

        public ActionResult Success()
        {
            Session.Remove("CartCounter");
            Session.Remove("CartItem");
            return View("Success");
        }


        private Product GetProduct(int id)
        {
            using (var db = new Home_A_HeavenDBEntities())
            {
                Product product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                return product;
            }
        }


        private string GetSubtotal(List<CartModel> listCarts)
        {
            double subtotal = 0;
            foreach (CartModel cart in listCarts)
            {
                subtotal = subtotal + cart.Total;
            }

            return subtotal.ToString();
        }
        #endregion

        #endregion

    }
}