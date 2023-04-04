// See https://aka.ms/new-console-template for more information
using ConsoleTables;
using E_Commerce_Database;
using E_Commerce_Database.Seller;
using E_Commerce_Database.Seller_Model;
using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

dbContext db = new dbContext();
Random random = new Random();
SellerDetail sellerFunction = new SellerDetail();

#region AutoAddCategory
var checkCategoryQuantity = db.category.Count();
if (checkCategoryQuantity == 0)
{
    Category autoAddCategory = new Category();
    autoAddCategory.CategoryName = "Beverages";
    autoAddCategory.CategoryDescription = "Soft drinks, coffees, teas, beers, and ales";
    db.category.Add(autoAddCategory);
    db.SaveChanges();
    Category autoAddCategory2 = new Category();
    autoAddCategory2.CategoryName = "Condiments";
    autoAddCategory2.CategoryDescription = "Sweet and savory sauces, relishes, spreads, and seasonings";
    db.category.Add(autoAddCategory2);
    db.SaveChanges();
    Category autoAddCategory3 = new Category();
    autoAddCategory3.CategoryName = "Confections";
    autoAddCategory3.CategoryDescription = "Desserts, candies, and sweet breads";
    db.category.Add(autoAddCategory3);
    db.SaveChanges();
    Category autoAddCategory4 = new Category();
    autoAddCategory4.CategoryName = "Dairy Products";
    autoAddCategory4.CategoryDescription = "Cheeses";
    db.category.Add(autoAddCategory4);
    db.SaveChanges();
    Category autoAddCategory5 = new Category();
    autoAddCategory5.CategoryName = "Grains/Cereals";
    autoAddCategory5.CategoryDescription = "Breads, crackers, pasta, and cereal";
    db.category.Add(autoAddCategory5);
    db.SaveChanges();
    Category autoAddCategory6 = new Category();
    autoAddCategory6.CategoryName = "Meat/Poultry";
    autoAddCategory6.CategoryDescription = "Prepared meats";
    db.category.Add(autoAddCategory6);
    db.SaveChanges();
    Category autoAddCategory7 = new Category();
    autoAddCategory7.CategoryName = "Produce";
    autoAddCategory7.CategoryDescription = "Dried fruit and bean curd";
    db.category.Add(autoAddCategory7);
    db.SaveChanges();
    Category autoAddCategory8 = new Category();
    autoAddCategory8.CategoryName = "Seafood";
    autoAddCategory8.CategoryDescription = "Seaweed and fish";
    db.category.Add(autoAddCategory8);
    db.SaveChanges();
}
#endregion

while (true)
{
    Console.WriteLine("1. Admin");
    Console.WriteLine("2. Seller");
    Console.WriteLine("3. Customer");
    Console.Write("Option : ");
    string firstOption = Console.ReadLine()!;
    Console.Clear();
    switch (firstOption.Trim())
    {
        case "1":
            break;
        case "2":
            bool InUp = true;
            while (InUp)
            {
                Console.WriteLine("[1. Sign In]\t[2. Sign Up]\t[3. Exit]");
                Console.Write("Option : ");
                string sellerOption = Console.ReadLine()!;
                Console.Clear();
                switch (sellerOption.Trim())
                {
                    case "1":
                        var sellerLogin = SignIn();
                        if (sellerLogin == null) 
                        {
                            Console.Clear();
                            break;
                        }
                        bool sellerLoginSuccessful = true;
                        while (sellerLoginSuccessful)
                        {
                            SellerCenter();
                            Console.Write("Option : ");
                            string sellerCenterOption = Console.ReadLine()!;
                            Console.Clear();
                            switch (sellerCenterOption.Trim())
                            {
                                case "1":
                                    bool ManageProduct = true;
                                    while (ManageProduct)
                                    {
                                        Products manageProducts = new Products();
                                        Console.WriteLine("Manage Products\n");
                                        ManageProductTable();
                                        Console.WriteLine("\n");
                                        Console.Write("Product ID : ");
                                        string manageProductId = Console.ReadLine()!;
                                        manageProductId = manageProductId.Trim();
                                        var getManageProductId = db.products.Where(p => p.ProductId.ToString() == manageProductId)
                                            .FirstOrDefault();
                                        if (getManageProductId == null)
                                        {
                                            Red("Product Does Not Exist");
                                            StopClear();
                                            continue;
                                        }
                                        manageProducts = getManageProductId;
                                        Console.Clear();
                                        EditProductTable(manageProducts);
                                        Console.WriteLine("[1. Edit Price]  [2. Edit Stock]  [3. Change Status]  [4. Exit]");
                                        string ManageProductOption = Console.ReadLine()!;
                                        Console.Clear();
                                        switch (ManageProductOption.Trim())
                                        {
                                            case "1":
                                                Console.WriteLine("Edit Price\n");
                                                EditPriceDetail(manageProducts);
                                                Console.WriteLine("[1. Price]\t[2. Discount Price]\t[3. Back]");
                                                string editPriceOption = Console.ReadLine()!;
                                                bool validateEditPrice = true;
                                                while (validateEditPrice)
                                                {
                                                    switch (editPriceOption.Trim())
                                                    {
                                                        case "1":
                                                            while (true)
                                                            {
                                                                Console.WriteLine("Edit Price\n");
                                                                EditPriceDetail(manageProducts);
                                                                Console.Write("\nPrice                  : ");
                                                                string editPrice = Console.ReadLine()!;
                                                                if (!Regex.IsMatch(editPrice, @"^[0-9]*(\.[0-9]{1,2})?$"))
                                                                {
                                                                    Red("Please Enter The Number");
                                                                    StopClear();
                                                                    continue;
                                                                }
                                                                manageProducts.Price = Convert.ToDecimal(editPrice);
                                                                db.SaveChanges();
                                                                validateEditPrice = false;
                                                                Console.Clear();
                                                                break;
                                                            }
                                                            break;
                                                        case "2":
                                                            while (true)
                                                            {
                                                                Console.WriteLine("Edit Discount Price\n");
                                                                EditPriceDetail(manageProducts);
                                                                Console.WriteLine("\nDiscount Price          : ");
                                                                string editDiscountPrice = Console.ReadLine()!;
                                                                if (!Regex.IsMatch(editDiscountPrice, @"^[0-9]*(\.[0-9]{1,2})?$"))
                                                                {
                                                                    Red("Please Enter The Number");
                                                                    StopClear();
                                                                    continue;
                                                                }
                                                                decimal checkDiscountPrice = manageProducts.Price * (decimal)0.8;
                                                                if (Convert.ToDecimal(editDiscountPrice) > checkDiscountPrice)
                                                                {
                                                                    Red("Discounted Price Cannot Exceed 80% Of The Original Price");
                                                                    StopClear();
                                                                    continue;
                                                                }
                                                                manageProducts.DiscountPrice = Convert.ToDecimal(editDiscountPrice);
                                                                db.SaveChanges();
                                                                validateEditPrice = false;
                                                                Console.Clear();
                                                                break;
                                                            }
                                                            break;
                                                        case "3":
                                                            validateEditPrice = false;
                                                            break;
                                                        default:
                                                            Red("Please Enter 1~3");
                                                            StopClear();
                                                            continue;
                                                    }
                                                }
                                                break;
                                            case "2":
                                                while (true)
                                                {
                                                    Console.WriteLine("Edit Stock\n");
                                                    Console.Write("Current Stock          : ");
                                                    Yellow((manageProducts.Stock).ToString());
                                                    Console.Write("Enter Stock            : ");
                                                    string editstock = Console.ReadLine()!;
                                                    if (Regex.IsMatch(editstock.Trim(), @"^\d+$"))
                                                    {
                                                        Red("Please Enter The Number");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    manageProducts.Stock = Convert.ToInt32(editstock.Trim());
                                                    db.SaveChanges();
                                                    Console.Clear();
                                                    break;
                                                }
                                                break;
                                            case "3":
                                                while (true)
                                                {
                                                    Console.WriteLine("Change Status\n");
                                                    Console.Write("Status                 : ");
                                                    Yellow(manageProducts.ProductStatus);
                                                    Console.WriteLine("\n[1. Activated]\t[2. Deactivated]\t[3. Back]");
                                                    string changeStatusOption = Console.ReadLine()!;
                                                    switch (changeStatusOption)
                                                    {
                                                        case "1":
                                                            manageProducts.ProductStatus = "Active";
                                                            db.SaveChanges();
                                                            Console.Clear();
                                                            break;
                                                        case "2":
                                                            manageProducts.ProductStatus = "Inactive";
                                                            db.SaveChanges();
                                                            Console.Clear();
                                                            break;
                                                        case "3":
                                                            break;
                                                        default:
                                                            Red("Please Enter 1~3");
                                                            StopClear();
                                                            continue;
                                                    }
                                                }
                                            case "4":
                                                ManageProduct = false;
                                                sellerLoginSuccessful = false;
                                                Console.Clear();
                                                break;
                                            default:
                                                Red("Please Enter 1~4");
                                                StopClear();
                                                continue;
                                        }
                                    }
                                    break;
                                case "2":
                                    bool AddNewProduct = true;
                                    Products newProducts = new Products();
                                    while (AddNewProduct)
                                    {
                                        Console.WriteLine("Add Product\n");
                                        Console.WriteLine("Product Name           : ");
                                        Console.WriteLine("Category               : ");
                                        Console.Write("\nEnter Product Name     : ");
                                        string newProductName = Console.ReadLine()!;
                                        newProductName = newProductName.Trim();
                                        newProducts.ProductName = newProductName;
                                        AddNewProduct = false;
                                        Console.Clear();
                                    }
                                    bool AddCategory = true;
                                    while (AddCategory)
                                    {
                                        Console.WriteLine("Add Product\n");
                                        CategoryTables();
                                        Console.Write("Product Name           : ");
                                        Yellow($"{newProducts.ProductName}");
                                        Console.WriteLine("Category               : ");
                                        Console.Write("\nEnter Category ID     : ");
                                        string newCategoryId = Console.ReadLine()!;
                                        var getCategoryId = db.category.Where(c => c.CategoryId.ToString() == newCategoryId.Trim()).FirstOrDefault();
                                        if (getCategoryId == null)
                                        {
                                            Red("Category Does Not Exist");
                                            StopClear();
                                            continue;
                                        }
                                        newProducts.CategoryId = getCategoryId.CategoryId;
                                        AddCategory = false;
                                        Console.Clear();
                                    }
                                    var CategoryName = db.category.Where(c => c.CategoryId == newProducts.CategoryId).FirstOrDefault();
                                    bool validatePrice = true;
                                    while (validatePrice)
                                    {
                                        Console.WriteLine("Add Product\n");
                                        Console.Write("Product Name           : ");
                                        Yellow($"{newProducts.ProductName}");
                                        Console.Write("Category               : ");
                                        Yellow($"{CategoryName.CategoryName}");
                                        Console.Write("Category Description   : ");
                                        Yellow($"{CategoryName.CategoryDescription}");
                                        Console.WriteLine("Price                  : ");
                                        Console.WriteLine("Discount Price         : ");
                                        Console.WriteLine("Stock                  : ");
                                        Console.WriteLine("Product Description    : \n");
                                        Console.Write("Enter Price            : ");
                                        string newProductPrice = Console.ReadLine()!;
                                        newProductPrice = newProductPrice.Trim();
                                        if (!Regex.IsMatch(newProductPrice, @"^[0-9]*(\.[0-9]{1,2})?$"))
                                        {
                                            Red("Please Enter The Number");
                                            StopClear();
                                            continue;
                                        }
                                        newProducts.Price = Convert.ToDecimal(newProductPrice);
                                        validatePrice = false;
                                        Console.Clear();
                                    }
                                    bool validateDiscountPrice = true;
                                    while (validateDiscountPrice)
                                    {
                                        Console.WriteLine("Add Product\n");
                                        Console.Write("Product Name           : ");
                                        Yellow($"{newProducts.ProductName}");
                                        Console.Write("Category               : ");
                                        Yellow($"{CategoryName.CategoryName}");
                                        Console.Write("Category Description   : ");
                                        Yellow($"{CategoryName.CategoryDescription}");
                                        Console.Write("Price                  : ");
                                        Yellow($"RM {newProducts.Price}");
                                        Console.WriteLine("Discount Price         : ");
                                        Console.WriteLine("Stock                  : ");
                                        Console.WriteLine("Product Description    : \n");
                                        Console.Write("Enter Special Price    : ");
                                        string newSpecialPrice = Console.ReadLine()!;
                                        newSpecialPrice = newSpecialPrice.Trim();
                                        if (!Regex.IsMatch(newSpecialPrice, @"^[0-9]*(\.[0-9]{1,2})?$"))
                                        {
                                            Red("Please Enter The Number");
                                            StopClear();
                                            continue;
                                        }
                                        decimal checkDiscountPrice = newProducts.Price * (decimal)0.8;
                                        if (Convert.ToDecimal(newSpecialPrice) > checkDiscountPrice)
                                        {
                                            Red("The Special Price Cannot Exceed The Original Price 80%");
                                            StopClear();
                                            continue;
                                        }
                                        newProducts.DiscountPrice = Convert.ToDecimal(newSpecialPrice);
                                        validateDiscountPrice = false;
                                        Console.Clear();
                                    }
                                    bool validateStock = true;
                                    while (validateStock)
                                    {
                                        Console.WriteLine("Add Product\n");
                                        Console.Write("Product Name           : ");
                                        Yellow($"{newProducts.ProductName}");
                                        Console.Write("Category               : ");
                                        Yellow($"{CategoryName.CategoryName}");
                                        Console.Write("Category Description   : ");
                                        Yellow($"{CategoryName.CategoryDescription}");
                                        Console.Write("Price                  : ");
                                        Yellow($"RM {newProducts.Price}");
                                        Console.Write("Discount Price         : ");
                                        Yellow($"RM {newProducts.DiscountPrice}");
                                        Console.WriteLine("Stock                  : ");
                                        Console.WriteLine("Product Description    : \n");
                                        Console.Write("Enter Stock            : ");
                                        string stock = Console.ReadLine()!;
                                        stock = stock.Trim();
                                        if (!Regex.IsMatch(stock, @"^\d+$"))
                                        {
                                            Red("Please Enter The Number");
                                            StopClear();
                                            continue;
                                        }
                                        else if (Convert.ToInt32(stock) <= 0)
                                        {
                                            Red("Cannot Be Less Than 0");
                                            StopClear();
                                            continue;
                                        }
                                        newProducts.Stock = Convert.ToInt32(stock);
                                        validateStock = false;
                                        Console.Clear();
                                    }
                                    #region  ProductDescription
                                    Console.WriteLine("Add Product\n");
                                    Console.Write("Product Name           : ");
                                    Yellow($"{newProducts.ProductName}");
                                    Console.Write("Category               : ");
                                    Yellow($"{CategoryName.CategoryName}");
                                    Console.Write("Category Description   : ");
                                    Yellow($"{CategoryName.CategoryDescription}");
                                    Console.Write("Price                  : ");
                                    Yellow($"RM {newProducts.Price}");
                                    Console.Write("Discount Price         : ");
                                    Yellow($"RM {newProducts.DiscountPrice}");
                                    Console.Write("Stock                  : ");
                                    Yellow($"{newProducts.Stock}");
                                    Console.WriteLine("Product Description    : \n");
                                    Console.Write("Enter Description      : ");
                                    string newProductDescription = Console.ReadLine()!;
                                    newProductDescription = newProductDescription.Trim();
                                    newProducts.Description = newProductDescription;
                                    Console.Clear();
                                    #endregion
                                    Console.WriteLine("Add Product\n");
                                    Console.Write("Product Name           : ");
                                    Yellow($"{newProducts.ProductName}");
                                    Console.Write("Category               : ");
                                    Yellow($"{CategoryName.CategoryName}");
                                    Console.Write("Category Description   : ");
                                    Yellow($"{CategoryName.CategoryDescription}");
                                    Console.Write("Price                  : ");
                                    Yellow($"RM {newProducts.Price}");
                                    Console.Write("Discount Price         : ");
                                    Yellow($"RM {newProducts.DiscountPrice}");
                                    Console.Write("Stock                  : ");
                                    Yellow($"{newProducts.Stock}");
                                    Console.Write("Product Description    : ");
                                    Yellow($"{newProducts.Description}");
                                    newProducts.SellerId = sellerLogin.SellerId;
                                    Console.WriteLine("\n");
                                    Console.WriteLine("\n[1. Confirm]\t\t[2. Deny]");
                                    Console.Write("Option : ");
                                    string AddProductOption = Console.ReadLine()!;
                                    switch (AddProductOption.Trim())
                                    {
                                        case "1":
                                            newProducts.ProductStatus = "Active";
                                            db.products.Add(newProducts);
                                            db.SaveChanges();
                                            Console.Clear();
                                            break;
                                        case "2":
                                            Console.Clear();
                                            break;
                                        default:
                                            Red("Please Enter 1~2");
                                            StopClear();
                                            continue;

                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("Product Lists\n");
                                    ProductTable();
                                    Console.WriteLine("Press [Enter] Back To Menu. . .");
                                    StopClear();
                                    break;
                                case "4":
                                    bool AccountSetting = true;
                                    while (AccountSetting)
                                    {
                                        Console.WriteLine("Account Settings\n");
                                        PrintSellerProfile(sellerLogin);
                                        Console.WriteLine("[1. Modify Email]  [2. Modify Phone Number]  [3. Modify Password]  [4. Back]");
                                        string accountSettingOption = Console.ReadLine()!;
                                        switch (accountSettingOption.Trim())
                                        {
                                            case "1":
                                                while (true)
                                                {
                                                    Console.WriteLine("Modify Email\n");
                                                    Console.Write("Email            : ");
                                                    string modifyEmail = Console.ReadLine()!;
                                                    modifyEmail = modifyEmail.Trim();
                                                    if (!Regex.IsMatch(modifyEmail, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
                                                    {
                                                        Red("Please Enter A Valid Email Address");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    var checkModifyEmail = db.sellerDetail.FirstOrDefault(c => c.SellerEmail == modifyEmail);
                                                    if (checkModifyEmail == null)
                                                    {
                                                        Red("Email Already Exists");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    sellerLogin.SellerEmail = modifyEmail;
                                                    db.SaveChanges();
                                                    Console.Clear();
                                                    break;
                                                }
                                                break;
                                            case "2":
                                                while (true)
                                                {
                                                    Console.WriteLine("Modify Phone Number\n");
                                                    Console.Write("Phone Number     : ");
                                                    string modifyPhoneNumber = Console.ReadLine()!;
                                                    modifyPhoneNumber = modifyPhoneNumber.Trim();
                                                    if (!Regex.IsMatch(modifyPhoneNumber, @"^\d+$"))
                                                    {
                                                        Red("Please Enter The Number");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    else if (!Regex.IsMatch(modifyPhoneNumber, @"^(\+?01)[0-46-9]-*[0-9]{7}$"))
                                                    {
                                                        Red("Please Enter A Valid Phone Number");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    var checkPhoneNumber = db.sellerDetail.FirstOrDefault(c => c.SellerPhoneNumber == modifyPhoneNumber);
                                                    if (checkPhoneNumber == null)
                                                    {
                                                        sellerLogin.SellerPhoneNumber = modifyPhoneNumber;
                                                        db.SaveChanges();
                                                        Console.Clear();
                                                        break;
                                                    }
                                                    else if (checkPhoneNumber != null)
                                                    {
                                                        Red("Phone Number Already Exists");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    break;
                                                }
                                                break;
                                            case "3":
                                                while (true)
                                                {
                                                    Console.WriteLine("Modify Password\n");
                                                    Console.Write("Current Password : ");
                                                    string currentPassword = Console.ReadLine()!;
                                                    currentPassword = currentPassword.Trim();
                                                    var checkPassword = db.sellerDetail.FirstOrDefault(c => c.SellerPassword == currentPassword);
                                                    if (checkPassword == null)
                                                    {
                                                        Red("Wrong Password");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    else if (!Regex.IsMatch(currentPassword, @"^(?=.*\d)(?=.*[a-zA-Z]).{8,}$"))
                                                    {

                                                        Red("Please Enter Password");
                                                        StopClear();
                                                        continue;
                                                    }
                                                    sellerLogin.SellerPassword = currentPassword;
                                                    break;
                                                }
                                                break;
                                            case "4":
                                                AccountSetting = false;
                                                Console.Clear();
                                                break;
                                            default:
                                                Red("Please Enter 1~4");
                                                StopClear();
                                                continue;
                                        }
                                    }
                                    break;
                                case "5":
                                    Console.WriteLine("Store Setting\n");
                                    SellerVoucher voucher = new SellerVoucher();
                                    PrintStoreDetail(sellerLogin, voucher);
                                    Console.WriteLine("\n[1. Modify Store Name]  [2. Set Voucher]  [3. Back]");
                                    string modifyStoreDetail = Console.ReadLine()!;
                                    switch (modifyStoreDetail.Trim())
                                    {
                                        case "1":
                                            Console.WriteLine("Modify Store Name");
                                            Console.Write("New Store Name : ");
                                            string newStoreName = Console.ReadLine()!;
                                            newStoreName = newStoreName.Trim();
                                            sellerLogin.SellerStoreName = newStoreName;
                                            db.SaveChanges();
                                            Console.WriteLine("Modify Successful");
                                            StopClear();
                                            break;
                                        case "2":
                                            Console.WriteLine("Set Voucher");
                                            StopClear();
                                            break;
                                        case "3":
                                            Console.Clear();
                                            break;
                                        default:
                                            Console.WriteLine("Please Enter 1~3");
                                            StopClear();
                                            continue;
                                    }
                                    break;
                                case "6":
                                    Console.WriteLine("Add Category\n");
                                    Category addCategory = new Category();
                                    Console.WriteLine("Enter Category Name");
                                    string newCategory = Console.ReadLine()!;
                                    Console.WriteLine("Enter Category Description");
                                    string newCategoryDescription = Console.ReadLine()!;
                                    break;
                                case "0":
                                    sellerLoginSuccessful = false;
                                    Console.Clear();
                                    break;
                                default:
                                    Red("Please Enter 0~5");
                                    StopClear();
                                    continue;
                            }
                        }
                        break;
                    case "2":
                        bool SignUp = true;
                        while (SignUp)
                        {
                            SellerDetail newSeller = new SellerDetail();
                            bool validateNewPhoneNumber = true;
                            while (validateNewPhoneNumber)
                            {
                                Console.WriteLine("Sign Up\n");
                                Console.WriteLine("Phone Number     : ");
                                Console.WriteLine("Email            : ");
                                Console.WriteLine("Password         : ");
                                Console.WriteLine("Confirm Password : ");
                                Yellow("\n\n[*Key In Q To Exit]\n");
                                Console.WriteLine("[Verification Code] Will Send To Your Phone Number");
                                Console.WriteLine("Example : 01x xxx xxxx");
                                Console.Write("Phone Number     : ");
                                string newPhoneNumber = Console.ReadLine()!;
                                newPhoneNumber = newPhoneNumber.Trim();
                                if (newPhoneNumber == "Q")
                                {
                                    validateNewPhoneNumber = false;
                                    Console.Clear();
                                }
                                else if (!Regex.IsMatch(newPhoneNumber, @"^\d+$"))
                                {
                                    Red("Please Enter The Number");
                                    StopClear();
                                    continue;
                                }
                                else if (!Regex.IsMatch(newPhoneNumber, @"^(\+?01)[0-46-9]-*[0-9]{7}$"))
                                {
                                    Red("Please Enter A Valid Phone Number");
                                    StopClear();
                                    continue;
                                }
                                var validatePhoneNumer = db.sellerDetail.Where(x => x.SellerPhoneNumber == newPhoneNumber).FirstOrDefault();
                                if (validatePhoneNumer != null)
                                {
                                    Red("The User Account Already Exists");
                                    StopClear();
                                    continue;
                                }
                                //
                                else if (newPhoneNumber != "0147204879") 
                                {
                                    Red("Phone Number Appears To Be Invalid");
                                    StopClear();
                                    continue;
                                }
                                else if (validatePhoneNumer == null)
                                {
                                    string OTP = GenerateOTP(6);
                                    SendOTP(OTP);
                                    Console.Write("Verification Code  : ");
                                    string OTPCode = Console.ReadLine()!;
                                    if (OTPCode.Trim() != OTP)
                                    {
                                        Red("Verification Code Error");
                                        StopClear();
                                        continue;
                                    }
                                    newSeller.SellerPhoneNumber = newPhoneNumber;
                                    validateNewPhoneNumber = false;
                                    Console.Clear();
                                }
                            }
                            bool validateNewEmail = true;
                            while (validateNewEmail)
                            {
                                Console.WriteLine("Sign Up\n");
                                Console.Write("Phone Number     : ");
                                Yellow(newSeller.SellerPhoneNumber);
                                Console.WriteLine("Email            : ");
                                Console.WriteLine("Password         : ");
                                Console.WriteLine("Confirm Password : ");
                                Yellow("\n\n[*Key In Q To Exit]\n");
                                Console.Write("Email            : ");
                                string newEmail = Console.ReadLine()!;
                                newEmail = newEmail.Trim();
                                if (newEmail == "Q")
                                {
                                    validateNewEmail = false;
                                }
                                else if (!Regex.IsMatch(newEmail, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
                                {
                                    Red("Please Enter A Valid Email Address");
                                    StopClear();
                                    continue;
                                }
                                var validateEmail = db.sellerDetail.Where(x => x.SellerEmail == newEmail).FirstOrDefault();
                                if (validateEmail != null)
                                {
                                    Red("The User Account Already Exists");
                                    StopClear();
                                    continue;
                                }
                                if (validateEmail == null)
                                {
                                    newSeller.SellerEmail = newEmail;
                                    validateNewEmail = false;
                                    Console.Clear();
                                }
                            }
                            bool validateNewPassword = true;
                            while (validateNewPassword)
                            {
                                Console.WriteLine("Sign Up\n");
                                Console.Write("Phone Number     : ");
                                Yellow(newSeller.SellerPhoneNumber);
                                Console.Write("Email            : ");
                                Yellow(newSeller.SellerEmail);
                                Console.WriteLine("Password         : ");
                                Console.WriteLine("Confirm Password : ");
                                Yellow("\n\n[*Key In Q To Exit]\n");
                                Console.WriteLine("At Least 8 Character");
                                Console.Write("Password         : ");
                                string newPassword = Console.ReadLine()!;
                                newPassword = newPassword.Trim();
                                if (newPassword == "Q")
                                {
                                    validateNewEmail = false;
                                }
                                else if (!Regex.IsMatch(newPassword, @"^(?=.*\d)(?=.*[a-zA-Z]).{8,}$"))
                                {

                                    Red("Please Enter Password");
                                    StopClear();
                                    continue;
                                }
                                Console.Clear();
                                bool validateNewConfrimPassword = true;
                                while (validateNewConfrimPassword)
                                {
                                    Console.WriteLine("Sign Up\n");
                                    Console.Write("Phone Number     : ");
                                    Yellow(newSeller.SellerPhoneNumber);
                                    Console.Write("Email            : ");
                                    Yellow(newSeller.SellerEmail);
                                    Console.Write("Password         : ");
                                    Yellow(HidePassword(newPassword.Length));
                                    Console.WriteLine("Confirm Password : ");
                                    Yellow("\n\n[*Key In Q To Exit]\n");
                                    Console.Write("Confirm Password : ");
                                    string confrimPassword = Console.ReadLine()!;
                                    confrimPassword = confrimPassword.Trim();
                                    if (confrimPassword == "")
                                    {
                                        Red("Please Enter Password Again");
                                        StopClear();
                                        continue;
                                    }
                                    else if (confrimPassword != newPassword) 
                                    {
                                        Red("The Password Entered Is Different");
                                        StopClear();
                                        continue;
                                    }
                                    validateNewConfrimPassword = false;
                                }
                                newSeller.LastLoginTime = DateTime.UtcNow;
                                newSeller.LoginTry = 3;
                                newSeller.SellerPassword = newPassword;
                                newSeller.SellerStoreName = GenerateSellerName();
                                newSeller.Status = "Active";
                                db.sellerDetail.Add(newSeller);
                                db.SaveChanges();
                                Console.Clear();
                                validateNewPassword = false;
                                SignUp = false;
                            }
                        }
                        break;
                    case "3":
                        InUp = false;
                        break;
                    default:
                        Console.WriteLine("Please Enter 1~3");
                        StopClear();
                        continue;
                }
            }
            break;
        case "3":
            break;
        default:
            break;
    }
}


void PrintStoreDetail(SellerDetail seller, SellerVoucher sellerVoucher)
{
    Console.Write("Store Name              : ");
    Yellow(seller.SellerStoreName);
    Console.Write("Voucher                 : ");
    Yellow("Not Set");
}
void PrintSellerProfile(SellerDetail seller)
{
    Console.Write("Login Email Address     : ");
    Yellow(seller.SellerEmail);
    Console.Write("Login Phone Number      : ");
    Yellow(seller.SellerPhoneNumber);
    Console.Write("Password                : ");
    Yellow(seller.SellerPassword);
    Console.WriteLine("\n");
}
void EditPriceDetail(Products EditProductsPrice)
{
    Console.Write("Price                   : ");
    Yellow($"RM {EditProductsPrice.Price}");
    Console.Write("Discount Price          : ");
    Yellow($"RM {EditProductsPrice.DiscountPrice}\n");
}
void EditProductTable(Products EditProducts)
{
    Console.Write("Product Name            : ");
    Yellow($"{EditProducts.ProductName}");
    Console.Write("Price                   : ");
    Yellow($"RM {EditProducts.Price}");
    Console.Write("Discount Price          : ");
    Yellow($"RM {EditProducts.DiscountPrice}");
    Console.Write("Stock                   : ");
    Yellow($"{EditProducts.Stock}");
}
void ManageProductTable()
{
    var ShowManageProductTable = new ConsoleTable("Product Id", "Product Name", "Price", "Stock", "Status");
    foreach (var m in db.products)
    {
        ShowManageProductTable.AddRow(m.ProductId, m.ProductName, $"RM {m.Price}", m.Stock, m.ProductStatus);
    }
    ShowManageProductTable.Write();
}
void ProductTable()
{
    var categoryJoinProduct = db.products.Join(db.category, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p,c});
    var showProductTable = new ConsoleTable("Product Id", "Category Name", "Product Name", "Price", "Discount Price", "Stock");
    foreach (var p in categoryJoinProduct)
    {
        showProductTable.AddRow(p.p.ProductId, p.c.CategoryName, p.p.ProductName, $"RM {p.p.Price}", $"RM {p.p.DiscountPrice}", p.p.Stock);
    }
    showProductTable.Write();
}
void CategoryTables()
{
    var categoryTables = new ConsoleTable("Id", "Category Name", "Description");
    foreach (var c in db.category)
    {
        categoryTables.AddRow(c.CategoryId, c.CategoryName, c.CategoryDescription);
    }
    categoryTables.Write();
}
string HidePassword(int passwordLenght) 
{
    string Asterisk = "";
    for (int i = 0; i < passwordLenght; i++) 
    {
        Asterisk += "*";
    }
    return Asterisk;
}
void Yellow(string msg)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(msg);
    Console.ResetColor();
}
void Red(string msg)
{
    Console.WriteLine("\n");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(msg);
    Console.ResetColor();
}
void StopClear()
{
    Console.ReadKey();
    Console.Clear();
}
string GenerateOTP(int lenght)
{
    Random random = new Random();
    string OTP = "";
    for (int i = 0; i < lenght; i++)
    {
        OTP += random.Next(0, 10).ToString();
    }
    return OTP;
}
void SendOTP(string OTP)
{
    string accountSid = "ACbdcbae990be4a8533af0ff9ea3a06e7c";
    string authToken = "5ef3873d9f6fa607867a6a4ebcb93476";

    TwilioClient.Init(accountSid, authToken);

    var message = MessageResource.Create(
        body: OTP,
        from: new Twilio.Types.PhoneNumber("+15856202634"),
        to: new Twilio.Types.PhoneNumber("+60147204879")
    );
}
string GenerateSellerName()
{
    string sellerName = "";
    sellerName += upperCaseLetter();
    sellerName += number();
    sellerName += upperCaseLetter();
    for (int i = 0; i < 2; i++)
        sellerName += lowerCaseLetter();
    sellerName += upperCaseLetter();
    for (int i = 0; i < 2; i++)
        sellerName += number();
    return sellerName;
    string upperCaseLetter()
    {
        int upperLetter = random.Next(65, 91);
        char uppercaseLetter = (char)upperLetter;
        string upperCaseLetter = uppercaseLetter.ToString();
        return upperCaseLetter;
    }
    string lowerCaseLetter()
    {
        int lowerLetter = random.Next(97, 123);
        char lowercaseLetter = (char)lowerLetter;
        string lowerCaseLetter = lowercaseLetter.ToString();
        return lowerCaseLetter;
    }
    string number()
    {
        int num = random.Next(0, 10);
        string number = num.ToString();
        return number;
    }
}
SellerDetail SignIn()
{
    while (true)
    {
        Console.WriteLine("Sign In\n");
        Console.WriteLine("[1. Login With Password]  [2. Login with SMS]  [3. Back]");
        Console.Write("Option : ");
        string Logintype = Console.ReadLine()!;
        Console.Clear();
        switch (Logintype)
        {
            case "1":
                while (true)
                {
                    Console.WriteLine("Login With Password\n");
                    Console.Write("Mobile Number/Email    : ");
                    string SignInEmail = Console.ReadLine()!;
                    SignInEmail = SignInEmail.Trim()!;
                    Console.Clear();
                    Console.WriteLine("Login With Password\n");
                    Console.WriteLine($"Mobile Number/Email    : {SignInEmail}");
                    Console.Write("Password               : ");
                    string SignInPassword = Console.ReadLine()!;
                    SignInPassword = SignInPassword.Trim()!;
                    var sellerLogin = db.sellerDetail.FirstOrDefault(x => x.SellerEmail == SignInEmail || x.SellerPhoneNumber == SignInEmail);
                    if (sellerLogin == null)
                    {
                        Red("Incorrect Username");
                        StopClear();
                        continue;
                    }
                    var checkSignInPassword = db.sellerDetail.FirstOrDefault(x => x.SellerPassword == SignInPassword);
                    if (checkSignInPassword == null)
                    {
                        sellerLogin.LoginTry--;
                        db.SaveChanges();
                        if (sellerLogin.LoginTry <= 0)
                        {
                            Red("Account Has Been Suspended");
                            StopClear();
                            sellerLogin = null;
                            return sellerLogin;
                        }
                        Red("Incorrect Password");
                        Console.WriteLine($"Remaining {sellerLogin.LoginTry} Tried");
                        StopClear();
                        continue;
                    }
                    else if (sellerLogin.LoginTry <= 0)
                    {
                        Red("Account Has Been Suspended");
                        StopClear();
                        sellerLogin = null;
                        return sellerLogin;
                    }
                    sellerLogin.LoginTry = 3;
                    Console.Clear();
                    db.SaveChanges();
                    return sellerLogin;
                }
            case "2":
                while (true)
                {
                    string OTP = "";
                    Console.WriteLine("Sign In\n");
                    Console.WriteLine("Phone Number           : +60 ");
                    string signInMobileNumber = Console.ReadLine()!;
                    signInMobileNumber = signInMobileNumber.Trim();
                    if (!Regex.IsMatch(signInMobileNumber, @"^(\+?01)[0-46-9]-*[0-9]{7}$"))
                    {
                        Red("Please Enter A Valid Phone Number");
                        StopClear();
                        continue;
                    }
                    var checksignInMobileNumber = db.sellerDetail.FirstOrDefault(x => x.SellerPhoneNumber == signInMobileNumber);
                    if (checksignInMobileNumber == null)
                    {
                        Red("Phone Number Does Not Exists");
                        StopClear();
                        continue;
                    }
                    OTP = GenerateOTP(6);
                    SendOTP(OTP);
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("Enter The Code\n");
                        Console.WriteLine($"Enter the 6-digit code sent to + 60 {signInMobileNumber}");
                        Console.WriteLine("Phone Number           : +60 ");
                        Console.Write("Verification Code          : ");
                        string VerificationCode = Console.ReadLine()!;
                        if (VerificationCode.Trim() != OTP)
                        {
                            Red("The Verification Code Is Wrong");
                            StopClear();
                            continue;
                        }
                        Console.Clear();
                        return checksignInMobileNumber;
                    }
                }
            case "3":
                SellerDetail back = new SellerDetail();
                back = null;
                return back;
            default:
                Red("Please Enter 1~2");
                StopClear();
                continue;
        }

    }
}
void SellerCenter()
{
    Console.WriteLine($" Seller Center");
    Console.WriteLine($"Welcome to ! Let's start your business right now!\n");
    Console.WriteLine("1. Manage Product");
    Console.WriteLine("2. Add Product");
    Console.WriteLine("3. Product Lists");
    Console.WriteLine("4. Account Settings");
    Console.WriteLine("5. Store Setting");
    Console.WriteLine("6. Add Category");
    Console.WriteLine("0. Exits");
    Console.WriteLine();
}
