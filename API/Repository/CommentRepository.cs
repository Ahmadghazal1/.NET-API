﻿using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var exisitingComment = await _context.Comments.FindAsync(id);
            if (exisitingComment == null)
                return null;
            exisitingComment.Title = comment.Title;
            exisitingComment.Content = comment.Content;
            await _context.SaveChangesAsync();
            return exisitingComment;
        }
        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
                return null;
            _context.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

       
    }
}
