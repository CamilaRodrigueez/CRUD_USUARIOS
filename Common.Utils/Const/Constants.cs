using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Const
{
    public static class Constants
    {
        public const string SEXO_FEMENINO = "F";
        public const string SEXO_MASCULINO = "M";
        public const string NAME_CONEXION = "ConnectionStringSQLServer";
        public const string EXITOSO = "Exitoso";
        public const string ERROR = "Error";

        public static readonly IEnumerable<string> ListaSexos = new ReadOnlyCollection<string>(
            new List<string>
            {
               SEXO_FEMENINO,SEXO_MASCULINO
            });
    }
}
