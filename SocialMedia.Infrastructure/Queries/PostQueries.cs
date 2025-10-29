using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Queries
{
    public static class PostQueries
    {
        public static string PostQuerySqlServer = @"
                        select Id, UserId, Date, Description, Imagen 
                        from post 
                        order by Date desc
                        OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";
        public static string PostQueryMySQl = @"
                        select Id, UserId, Date, Description, Imagen 
                        from post 
                        order by Date desc
                        LIMIT @Limit
                    ";
        public static string PostComentadosUsuariosActivos = @"
                     SELECT 
                        p.Id AS PostId,
                        p.Description,
                    COUNT(c.Id) AS TotalComentarios
                    FROM [Post] p
                    INNER JOIN [Comment] c ON p.Id = c.PostId
                    INNER JOIN [User] u ON c.UserId = u.Id
                    WHERE u.IsActive = 1
                    GROUP BY p.Id, p.Description
                    ORDER BY TotalComentarios DESC;           
                    ";

        // Usuarios que nunca comentaron
        public const string UsuariosSinComentarios = @"
            SELECT 
                u.Id AS UserId,
                CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto
            FROM [User] u
            LEFT JOIN Comment c ON u.Id = c.UserId
            WHERE c.Id IS NULL;
        ";

        public static string CommentsLast3MonthsAdults = @"
            SELECT 
                u.Id AS UserId,
                (u.FirstName + ' ' + u.LastName) AS NombreCompleto,
            COUNT(c.Id) AS TotalComentarios
            FROM [User] u
            INNER JOIN [Comment] c ON u.Id = c.UserId
            WHERE 
                DATEDIFF(MONTH, c.Date, GETDATE()) <= 3
                AND DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) >= 18
                AND u.IsActive = 1
            GROUP BY u.Id, u.FirstName, u.LastName
            ORDER BY TotalComentarios DESC;
            ";


        // Posts sin usuarios activos (posts con comentarios de usuarios inactivos)
        public const string PostsSinUsuariosActivos = @"
            SELECT 
                DISTINCT p.Id AS PostId,
                p.Description AS Description,
                CASE WHEN u.IsActive = 0 THEN 'Inactivo' ELSE 'Activo' END AS EstadoUsuario
            FROM Post p
            INNER JOIN Comment c ON p.Id = c.PostId
            INNER JOIN [User] u ON c.UserId = u.Id
            WHERE u.IsActive = 0;
        ";

        // Usuarios activos con comentarios (total de comentarios por usuario activo)
        public const string UsuariosActivosConComentarios = @"
            SELECT 
                u.Id AS UserId,
                CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                COUNT(c.Id) AS TotalComentarios
            FROM [User] u
            INNER JOIN Comment c ON u.Id = c.UserId
            WHERE u.IsActive = 1
            GROUP BY u.Id, u.FirstName, u.LastName;
        ";

        // Posts sin comentarios
        public const string PostsSinComentarios = @"
            SELECT 
                p.Id AS PostId,
                p.Description AS Description
            FROM Post p
            LEFT JOIN Comment c ON p.Id = c.PostId
            WHERE c.Id IS NULL;
        ";
    }
}