using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SercPag_sorting.Context;
using SercPag_sorting.Models;

namespace SercPag_sorting.Controllers
{
    public class UserController : Controller
    {

        private readonly PagingContext _pagingContext;

        public UserController(PagingContext pagingContext)
        {
            _pagingContext = pagingContext;
        }


        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("addUser")]
        public IActionResult Create(User user)
        {

            var use = new User
            {
                Id = $"t" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
                Name = user.Name,
                Email = user.Email,
            };
            try
            {
                _pagingContext.Add(use);
                _pagingContext.SaveChanges();
                return Content("added");
            }
            catch (Exception ex)
            {
                return Content("fail");
            }
        }


        [HttpGet]
        public IActionResult ListOfUsers(string term = "", string sortOrder = "name_asc", int page = 1, int pageSize = 10)
        {
            // Convert search term to lowercase
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

            // Create an instance of the view model
            var UserViewModel = new PagUSerViewModel();

            // Get the list of users, applying search term filtering
            var users = from user in _pagingContext.user
                        where term == "" || user.Name.ToLower().StartsWith(term)
                        select new User
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email
                        };

            // Sorting logic
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.Name);
                    break;
                case "email_asc":
                    users = users.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(u => u.Email);
                    break;
                default: // name_asc
                    users = users.OrderBy(u => u.Name);
                    break;
            }

            // Pagination logic
            int totalUsers = users.Count();
            users = users.Skip((page - 1) * pageSize).Take(pageSize);

            // Assign filtered, sorted, and paginated users to the view model
            UserViewModel.user = users;
            UserViewModel.CurrentPage = page;
            UserViewModel.TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);
            UserViewModel.SortOrder = sortOrder;
            UserViewModel.SearchTerm = term;

            return View(UserViewModel);
        }





        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


            //var users = (from user in _noteBdContext.user select user).ToList();
        [HttpGet]
        public IActionResult AllUsers(string term="" , string OrderBy = "", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term)?"": term.ToLower();
            var UserViewModel = new UserViewModel();
            UserViewModel.NameSortingOrd = string.IsNullOrEmpty(OrderBy)? "name_decen": "";
            UserViewModel.EmailSortingOrd = OrderBy == "email" ? "email_decen" : "email";
            var users = (from user in _pagingContext.user
                         where term =="" || user.Name.ToLower().StartsWith(term)
                         select new User
                         {
                             Id = user.Id,
                             Name = user.Name,
                             Email = user.Email,
                         });
            switch (OrderBy)
            {
                case "name_decen":
                    users = users.OrderByDescending(x => x.Name);
                    break;
                case "email_decen":
                    users = users.OrderByDescending(x => x.Email);
                    break;
                case "email":
                    users = users.OrderBy(x => x.Email);
                    break;
                default:
                    users = users.OrderBy(x => x.Name);
                    break;

            }

            int totalrecords =  users.Count();
            int pagaSize = 5;
            int totalPages = (int)Math.Ceiling(totalrecords / (double)pagaSize);
            //currentpage 1 skip(1 -1 =0) take =5,  currentpage 2 skip(2 -1)*5  skip =  5
            users = users.Skip(totalPages);
            UserViewModel.user = users; 
            UserViewModel.CurrentPage = currentPage;
            UserViewModel.PageSize = pagaSize;
            UserViewModel.TotalPages = totalPages;
            UserViewModel.Term = term;
            UserViewModel.OrderBy = OrderBy;

            ViewBag.SearchTerm = term;
            return View(UserViewModel);
        }


        [HttpGet]
        public ActionResult UploadUsers()
        {
            return View();
        }

        ////Uploding User List of Docuument
        //[HttpPost]
        //[ActionName("UploadUser")]
        public async Task<IActionResult> UploadUsers(IFormFile userFile, IFormFile documentFile)
        {
            if (userFile != null && userFile.Length > 0)
            {
                var users = new List<User>();
                try
                {
                    // Process Excel file using EPPlus
                    using (var stream = new MemoryStream())
                    {
                        await userFile.CopyToAsync(stream);
                        stream.Position = 0;

                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first sheet
                            int rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++) // Assuming row 1 is the header
                            {
                                var user = new User
                                {
                                    Id = worksheet.Cells[row, 1].Text.Trim(),
                                    Name = worksheet.Cells[row, 2].Text.Trim(),
                                    Email = worksheet.Cells[row, 3].Text.Trim()
                                };
                                users.Add(user);
                            }
                        }
                    }

                    // Add users to the database
                    foreach (var user in users)
                    {
                        _pagingContext.Add(user);
                    }
                    await _pagingContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Content($"Error while processing Excel file: {ex.Message}");
                }
            }

            // Handle document upload (same as above)
            if (documentFile != null && documentFile.Length > 0)
            {
                try
                {
                    var documentPath = Path.Combine("wwwroot", "uploads", documentFile.FileName);
                    using (var stream = new FileStream(documentPath, FileMode.Create))
                    {
                        await documentFile.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    return Content($"Error while uploading document: {ex.Message}");
                }
            }

            return Content("Upload successful");
        }



        //// uploding List user from file
        //[HttpPost]
        //[ActionName("UploadUsers")]
        public async Task<IActionResult> Upload(IFormFile userFile, IFormFile documentFile)
        {
            if (userFile != null && userFile.Length > 0)
            {
                var users = new List<User>();
                try
                {
                    // Process the CSV/Excel file for user data
                    using (var stream = new MemoryStream())
                    {
                        await userFile.CopyToAsync(stream);
                        stream.Position = 0;

                        // Assuming the file is CSV for simplicity. You can add Excel (.xlsx) support here.
                        using (var reader = new StreamReader(stream))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                var values = line.Split(','); // Assuming CSV format: Id,Name,Email
                                if (values.Length == 3)
                                {
                                    var user = new User
                                    {
                                        Id = values[0],
                                        Name = values[1],
                                        Email = values[2]
                                    };
                                    users.Add(user);
                                }
                            }
                        }
                    }

                    // Add users to the database
                    foreach (var user in users)
                    {
                        _pagingContext.Add(user);
                    }
                    await _pagingContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Content($"Error while processing user file: {ex.Message}");
                }
            }

            if (documentFile != null && documentFile.Length > 0)
            {
                try
                {
                    // Process the document (PDF or Word) and save it somewhere (e.g., file system, database)
                    var documentPath = Path.Combine("wwwroot", "uploads", documentFile.FileName);
                    using (var stream = new FileStream(documentPath, FileMode.Create))
                    {
                        await documentFile.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    return Content($"Error while uploading document: {ex.Message}");
                }
            }

            return Content("Upload successful");
        }

    }
}
