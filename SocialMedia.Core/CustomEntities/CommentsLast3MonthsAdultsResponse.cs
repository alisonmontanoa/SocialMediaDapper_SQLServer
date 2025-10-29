using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class CommentsLast3MonthsAdultsResponse
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int TotalComentarios { get; set; }
    }
}
