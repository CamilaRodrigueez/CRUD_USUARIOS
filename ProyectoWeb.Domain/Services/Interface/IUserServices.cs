using Infraestructure.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Domain.Services.Interface
{
    public interface IUserServices
    {
        Task<List<UserDto>> GetAllUsers();
        IEnumerable<SelectListItem> GetAllSexos();

        //ResponseDto InsertUser(UserDto userInsert);
        Task<ResponseDto> InsertUser(UserDto userInsert);

        Task<ResponseDto> UpdateUser(UserDto updateUser);
        Task<ResponseDto> DeleteUser(int idUser);
    }
}
