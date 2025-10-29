using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class UsuariosActivosConComentariosResponse
    {
        public int UserId { get; set; }
        public string? NombreCompleto { get; set; }
        public int TotalComentarios { get; set; }
    }
}
