using Common.Utils.Const;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Domain.Dto;
using ProyectoWeb.Domain.Services.Interface;
using ProyectoWeb.Domain.ViewModel;
using ProyectoWeb.Handlers;
using Serilog;

namespace ProyectoWeb.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class UserController : Controller
    {
        #region Attribute
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Views
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {
            UserMV UserMV = new UserMV()
            {
                User = new UserDto(),

                ListSexos = _userServices.GetAllSexos()
            };
             
            return View(UserMV);
        }
        #endregion

        #region Methods
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(UserMV userMV)
        {
            ResponseDto result = new ResponseDto();
            if (ModelState.IsValid)
            {
                result = await _userServices.InsertUser(userMV.User);
                if (result.IsSuccess)
                {
                    string logMessage = $"[Service]: Api/User/Create [LogMessage]: Insert";
                    Log.Warning(logMessage);
                    TempData[Constants.EXITOSO] = result.Message;
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    //Se llena nuevamente la lista de sexo
                    userMV.ListSexos = _userServices.GetAllSexos();
                    userMV.User.StrFecha_Nacimiento = userMV.User.Fecha_Nacimiento.ToString("yyyy-MM-dd");
                    TempData[Constants.ERROR] = result.Message;
                    return View(userMV);
                }
            }
            else
            {
                TempData[Constants.ERROR] = result.Message;
                return View(userMV);
            }
        }
        #endregion

        #region Methods Rest
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {

            List<UserDto> list = await _userServices.GetAllUsers();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(UserDto userInsert)
        {
            IActionResult response;

            ResponseDto result = await _userServices.InsertUser(userInsert);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            string logMessage = $"[Service]: User/InsertUser [LogMessage]: Insert";
            Log.Warning(logMessage);

            return response;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto updateUser)
        {
            IActionResult response;

            ResponseDto result = await _userServices.UpdateUser(updateUser);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            string logMessage = $"[Service]: User/UpdateUser [LogMessage]: Update";
            Log.Warning(logMessage);

            return response;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int idUser)
        {
            IActionResult response;

            ResponseDto result = await _userServices.DeleteUser(idUser);
            if (result.IsSuccess)
                response = Ok(result);
            else
                response = BadRequest(result);

            string logMessage = $"[Service]: User/DeleteUser [LogMessage]: Delete";
            Log.Warning(logMessage);

            return response;
        }
        [HttpGet]
        public IActionResult GetSexo()
        {
            IEnumerable<SelectListItem> list = _userServices.GetAllSexos();

            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            string logMessage = $"[Service]: User/GetSexo [LogMessage]: GetSexo";
            Log.Warning(logMessage);

            return Ok(response);
        }
        #endregion
    }
}
