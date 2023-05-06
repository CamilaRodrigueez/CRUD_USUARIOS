using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Domain.ViewModel
{
    public class UserMV
    {
        public UserDto User { get; set; }
        public IEnumerable<SelectListItem>? ListSexos { get; set; }
    }
}
