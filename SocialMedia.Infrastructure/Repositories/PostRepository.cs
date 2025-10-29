using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enum;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Queries;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly IDapperContext _dapper;
        //private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
            //_context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostByUserAsync(int idUser)
        {
            var posts = await _entities.Where(x => x.UserId == idUser).ToListAsync();
            return posts;
        }


        public async Task<IEnumerable<Post>> GetAllPostDapperAsync(int limit = 10)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => PostQueries.PostQuerySqlServer,
                    DatabaseProvider.MySql => PostQueries.PostQueryMySQl,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Post>(sql, new { Limit = limit });
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<IEnumerable<PostComentariosUsersResponse>> GetPostCommentUserAsync()
        {
            try
            {
                var sql = PostQueries.PostComentadosUsuariosActivos;

                return await _dapper.QueryAsync<PostComentariosUsersResponse>(sql);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        //public async Task<Post> GetPostAsync(int id)
        //{
        //    var post = await _context.Posts.FirstOrDefaultAsync(
        //        x => x.Id == id);
        //    return post;
        //}

        //public async Task InsertPostAsync(Post post)
        //{
        //    _context.Posts.Add(post);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdatePostAsync(Post post)
        //{
        //    _context.Posts.Update(post);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeletePostAsync(Post post)
        //{
        //    _context.Posts.Remove(post);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<IEnumerable<UsuariosSinComentariosResponse>> GetUsuariosSinComentariosAsync()
        {
            var sql = PostQueries.UsuariosSinComentarios;
            return await _dapper.QueryAsync<UsuariosSinComentariosResponse>(sql);
        }

        public async Task<IEnumerable<CommentsLast3MonthsAdultsResponse>> GetCommentsLast3MonthsAdultsAsync()
        {
            var sql = PostQueries.CommentsLast3MonthsAdults;
            return await _dapper.QueryAsync<CommentsLast3MonthsAdultsResponse>(sql);
        }


        public async Task<IEnumerable<PostsSinUsuariosActivosResponse>> GetPostsSinUsuariosActivosAsync()
        {
            var sql = PostQueries.PostsSinUsuariosActivos;
            return await _dapper.QueryAsync<PostsSinUsuariosActivosResponse>(sql);
        }

        public async Task<IEnumerable<UsuariosActivosConComentariosResponse>> GetUsuariosActivosConComentariosAsync()
        {
            var sql = PostQueries.UsuariosActivosConComentarios;
            return await _dapper.QueryAsync<UsuariosActivosConComentariosResponse>(sql);
        }

        public async Task<IEnumerable<PostsSinComentariosResponse>> GetPostsSinComentariosAsync()
        {
            var sql = PostQueries.PostsSinComentarios;
            return await _dapper.QueryAsync<PostsSinComentariosResponse>(sql);
        }

        public async Task<IEnumerable<PostComentariosUsersResponse>> GetPostsComentadosUsuariosActivosAsync()
        {
            var sql = PostQueries.PostComentadosUsuariosActivos;
            return await _dapper.QueryAsync<PostComentariosUsersResponse>(sql);
        }
    }
}