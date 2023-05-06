using Common.Utils.Const;
using Common.Utils.Resources;
using Infraestructure.Core.Dapper;
using Infraestructure.Entity;
using Microsoft.Extensions.Configuration;
using ProyectoWeb.Domain.Dto;
using ProyectoWeb.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Common.Utils.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ProyectoWeb.Domain.Services
{
    public class UserServices : IUserServices
    {

        #region Attributes
        private readonly IConfiguration _config;
        #endregion

        #region Builder
        public UserServices(IConfiguration config)
        {
            _config = config;
        }
        #endregion
        #region Methods
        public async Task<List<UserDto>> GetAllUsers()
        {
            List <UserDto> listUserDto = new List<UserDto>();
            string query = ResorcesStatementSQL.SP_Select;


            DapperHelper.Instancia.ConnectionString = _config.GetConnectionString(Constants.NAME_CONEXION);
            IEnumerable<UserDto> lista = await DapperHelper.Instancia.ExecuteQuerySelect<UserDto>(query, null);

            if (lista.Any())
            {
                listUserDto = lista.Select(x => new UserDto
                {
                    IdUser= x.IdUser,
                    Nombre= CapitalizeFirstLetter(x.Nombre),    
                    StrFecha_Nacimiento = CapitalizeFirstLetter( x.Fecha_Nacimiento.ToString("MMMM - dd, yyyy")),
                    Fecha_Nacimiento = x.Fecha_Nacimiento,
                    Sexo = x.Sexo,
                    SexoGeneral = x.Sexo.Equals("F")? "Femenino" :"Masculino",
                }).ToList();

                return listUserDto;
            }
            return null;
        } 
        private string CapitalizeFirstLetter(string dato)
        {
               return char.ToUpper(dato[0]) + dato.Substring(1);
        }
        public async Task<ResponseDto> InsertUser( UserDto userInsert)
        {
            ResponseDto response = new ResponseDto();
            int rest = 0;
            string query = ResorcesStatementSQL.SP_Insert;

            if (userInsert.Fecha_Nacimiento > DateTime.Now)
            {
                if (Convert.ToBoolean(userInsert?.viewDiff))
                {
                    response.Message = GeneralMessages.DateInvalid;
                    response.IsSuccess = false;
                    return response;
                }
                else
                {
                  throw new BusinessException(GeneralMessages.DateInvalid);
                }
            }
                
            
            var requet = new
            {
                Nombre = userInsert.Nombre,
                Fecha_Nacimiento = userInsert.Fecha_Nacimiento,
                Sexo = userInsert.Sexo,
            };
            DapperHelper.Instancia.ConnectionString = _config.GetConnectionString(Constants.NAME_CONEXION);

            response.IsSuccess = (rest = await DapperHelper.Instancia.ExecuteQuery(query, requet)) < 0;

            if (response.IsSuccess) 
                response.Message = GeneralMessages.ItemInserted;
            else response.Message = GeneralMessages.ItemNoInserted;
            return response;
        }

        public async Task<ResponseDto> UpdateUser(UserDto updateUser)
        {
            ResponseDto response = new ResponseDto();
            int rest = 0;
            string query = ResorcesStatementSQL.SP_Update;

            if (updateUser.Fecha_Nacimiento > DateTime.Now)
                throw new BusinessException(GeneralMessages.DateInvalid);

            var requet = new
            {   
                IdUser = updateUser.IdUser,
                Nombre = updateUser.Nombre,
                Fecha_Nacimiento = updateUser.Fecha_Nacimiento,
                Sexo = updateUser.Sexo,
            };
            DapperHelper.Instancia.ConnectionString = _config.GetConnectionString(Constants.NAME_CONEXION);

            response.IsSuccess = (rest = await DapperHelper.Instancia.ExecuteQuery(query, requet)) < 0;

            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemUpdated;
            else response.Message = GeneralMessages.ItemNoUpdated;
            return response;
        }

        public async Task<ResponseDto> DeleteUser(int idUser)
        {
            ResponseDto response = new ResponseDto();
            int rest = 0;
            string query = ResorcesStatementSQL.SP_Delete;
            var requet = new
            {
                IdUser= idUser,
            };
            DapperHelper.Instancia.ConnectionString = _config.GetConnectionString(Constants.NAME_CONEXION);
            response.IsSuccess = (rest = await DapperHelper.Instancia.ExecuteQuery(query, requet)) < 0;

            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemDeleted;
            else response.Message = GeneralMessages.ItemNoDeleted;
            return response;
        }
        public IEnumerable<SelectListItem> GetAllSexos()
        {
            IEnumerable<SelectListItem> lista = null;

            lista = Constants.ListaSexos.ToList().Select(l => new SelectListItem
            {
                Text = l.Equals("F")?"Femenino":"Masculino",
                Value = l
            });
            return lista;
        }
        #endregion
    }
}
