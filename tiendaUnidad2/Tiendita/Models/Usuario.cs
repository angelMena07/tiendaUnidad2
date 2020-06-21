using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tiendita.Models
{
    class Usuario
    {
        [System.ComponentModel.DataAnnotations.Key]
        public uint idusuario { get; set; }
        public string username { get; set; }
        public string password { get ; set; }

        public string tipo_usuario { get; set; }

    }
}
