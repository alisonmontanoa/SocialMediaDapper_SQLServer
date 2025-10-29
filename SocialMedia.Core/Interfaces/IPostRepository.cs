﻿using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostByUserAsync(int idUser);
        Task<IEnumerable<Post>> GetAllPostDapperAsync(int limit = 10);
        Task<IEnumerable<PostComentariosUsersResponse>> GetPostCommentUserAsync();
        //Task<Post> GetPostAsync(int id);
        //Task InsertPostAsync(Post post);
        //Task UpdatePostAsync(Post post);
        //Task DeletePostAsync(Post post);

        Task<IEnumerable<PostComentariosUsersResponse>> GetPostsComentadosUsuariosActivosAsync();
        Task<IEnumerable<UsuariosSinComentariosResponse>> GetUsuariosSinComentariosAsync();
        Task<IEnumerable<PostsSinUsuariosActivosResponse>> GetPostsSinUsuariosActivosAsync();
        Task<IEnumerable<UsuariosActivosConComentariosResponse>> GetUsuariosActivosConComentariosAsync();
        Task<IEnumerable<PostsSinComentariosResponse>> GetPostsSinComentariosAsync();
        Task<IEnumerable<CommentsLast3MonthsAdultsResponse>> GetCommentsLast3MonthsAdultsAsync();

    }
}