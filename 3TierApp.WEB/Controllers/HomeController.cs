using _3TierApp.BLL.DTO;
using _3TierApp.BLL.Interfaces;
using _3TierApp.WEB.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _3TierApp.BLL.Infrastructure;

namespace _3TierApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        IUserService userService;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(IUserService serv)
        {
            userService = serv;
        }

        //public IActionResult Index()
        //{
        //    IEnumerable<UserDTO> userDtos = userService.GetUsers();
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
        //    var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDtos);
        //    return View(users);
        //}
        //public IActionResult Index1(string sortOrder, string searchString, int page = 1)
        //{
        //    int pageSize = 3;   // количество элементов на странице
        //    IEnumerable<UserDTO> userDTOs = userService.GetUsers();
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
        //    var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDTOs).AsQueryable();

        //    Console.WriteLine("SortOrder: " + sortOrder + " " + String.IsNullOrEmpty(sortOrder));
        //    Console.WriteLine("1NameSortParam: " + ViewBag.NameSortParam + " " + String.IsNullOrEmpty(sortOrder));
        //    Console.WriteLine("1BirthSortParam: " + ViewBag.BirthSortParam + " " + String.IsNullOrEmpty(sortOrder));


        //    ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
        //    //ViewBag.NameSortParam = sortOrder == "Name" ? "Name_desc" : "Name";
        //    ViewBag.BirthSortParam = sortOrder == "Birth" ? "Birth_desc" : "Birth";


        //    Console.WriteLine("2NameSortParam: " + ViewBag.NameSortParam + " " + String.IsNullOrEmpty(sortOrder));
        //    Console.WriteLine("2BirthSortParam: " + ViewBag.BirthSortParam + " " + String.IsNullOrEmpty(sortOrder));
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        users = users.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "Name_desc":
        //            users = users.OrderByDescending(u => u.Name);
        //            break;
        //        case "Birth":
        //            users = users.OrderBy(u => u.Birth);
        //            break;
        //        case "Birth_desc":
        //            users = users.OrderByDescending(u => u.Birth);
        //            break;
        //        default:
        //            users = users.OrderBy(u => u.Name);
        //            break;
        //    }

        //    //IQueryable<UserDTO> source = userService.GetUsers().AsQueryable<UserDTO>();
        //    var count = users.Count();
        //    var items = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        //    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        //    IndexViewModel viewModel = new IndexViewModel
        //    {
        //        PageViewModel = pageViewModel,
        //        Users = items
        //    };
        //    return View(viewModel);
        //}
        public async Task<IActionResult> IndexAsync(string searchString, string sortName = "Name", string currentSort = "Name", bool changeSortOrder = true, int page = 1)
        {
            int pageSize = 3;   // количество элементов на странице
            IEnumerable<UserDTO> userDTOs = await userService.GetUsersAsync();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDTOs).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            ViewBag.SearchString = searchString;

            if (changeSortOrder)
            {
                if (sortName == "Name")
                {
                    ViewBag.CurrentSort = currentSort == "Name" ? "Name_desc" : "Name";
                }
                else if (sortName == "Birth")
                {
                    ViewBag.CurrentSort = currentSort == "Birth" ? "Birth_desc" : "Birth";
                }
                else
                {
                    ViewBag.CurrentSort = currentSort == "Name" ? "Name_desc" : "Name";
                }
            }
            else
            {
                ViewBag.CurrentSort = currentSort;
            }

            switch (ViewBag.CurrentSort)
            {
                case "Name":
                    users = users.OrderBy(u => u.Name);
                    break;
                case "Name_desc":
                    users = users.OrderByDescending(u => u.Name);
                    break;
                case "Birth":
                    users = users.OrderBy(u => u.Birth);
                    break;
                case "Birth_desc":
                    users = users.OrderByDescending(u => u.Birth);
                    break;
                default:
                    users = users.OrderBy(u => u.Name);
                    break;
            }



            //IQueryable<UserDTO> source = userService.GetUsers().AsQueryable<UserDTO>();
            var count = users.Count();
            var items = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };
            return View(viewModel);
        }

        public async Task<IActionResult> ShowAccessTimeAsync(int? id)
        {
            IEnumerable<AccessTimeDTO> accessTimeDTOs =  await userService.ShowAccessTimeAsync(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccessTimeDTO, AccessTimeViewModel>()).CreateMapper();
            var accessTimes = mapper.Map<IEnumerable<AccessTimeDTO>, List<AccessTimeViewModel>>(accessTimeDTOs);
            return View(accessTimes);
        }

        public ActionResult CreateUser()
        {
            try
            {
                //UserDTO user = new UserDTO { Name = name, Birth = birth, Role = role };
                //var order = new AccessTimeViewModel { UserId = user.ID };
                var userView = new UserViewModel { Name = "", Birth = DateTime.Now, Role = -1 };

                return View(userView);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(UserViewModel userView)
        {
            try
            {
                var userDTO = new UserDTO { Name = userView.Name, Birth = userView.Birth, Role = userView.Role };
                await userService.CreateUserAsync(userDTO);
                Console.WriteLine(userDTO.Name + " " + userDTO.Birth + " " + userDTO.Role);
                return Content("<h2>User Created!</h2>");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Property + " " + ex.Message);
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(userView);
        }


        public async Task<ActionResult> AddAccessTimeAsync(int? id)
        {
            try
            {
                UserDTO user = await userService.GetUserAsync(id);
                DateTime time = DateTime.Now;
                var accessTime = new AccessTimeViewModel { UserId = user.ID, AccessDateTime = time };

                return View(accessTime);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddAccessTimeAsync(AccessTimeViewModel accessTime)
        {
            try
            {
                var accessTimeDTO = new AccessTimeDTO { UserId = accessTime.UserId, AccessDateTime = accessTime.AccessDateTime };
                await userService.AddAccessTimeAsync(accessTimeDTO);
                return Content("<h2>AccessTime added</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(accessTime);
        }
        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }


        public async Task<ActionResult> DeleteUserAsync(int? id)
        {
            try
            {
                UserDTO userDTO = await userService.GetUserAsync(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                var userView = mapper.Map<UserDTO, UserViewModel>(userDTO);


                return View(userView);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteUserAsync(UserViewModel userView)
        {
            try
            {
                //var userDTO = new UserDTO { Name = userView.Name, Birth = userView.Birth, Role = userView.Role };
                await userService.DeleteUserAsync(userView.ID);
                return Content("<h2>User Deleted!</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(userView);
        }

        public async Task<ActionResult> UpdateUserAsync(int? id)
        {
            try
            {
                UserDTO userDTO = await userService.GetUserAsync(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                var userView = mapper.Map<UserDTO, UserViewModel>(userDTO);


                return View(userView);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateUserAsync(UserViewModel userView)
        {
            try
            {
                //var userDTO = new UserDTO { Name = userView.Name, Birth = userView.Birth, Role = userView.Role };
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserDTO >()).CreateMapper();
                var userDTO = mapper.Map<UserViewModel, UserDTO>(userView);
                Console.WriteLine(userView.Birth);
                await userService.UpdateUserAsync(userDTO);
                return Content("<h2>User Updated!</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(userView);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}