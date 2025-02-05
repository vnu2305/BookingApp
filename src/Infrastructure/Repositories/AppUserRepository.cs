﻿using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(
            ApplicationDbContext context,
            ILoggerManager logger
            ) : base(context, logger)
        { }

        public async Task<AppUser> GetByTelegramIdAsync(long telegramId, bool tracking = false)
        {
            try
            {
                return await Search(x => x.TelegramId == telegramId, tracking).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetByTelegramIdAsync)} action {ex}");
                return new AppUser();
            }
        }

        public async Task<AppUser> GetByEmailAsync(string email, bool tracking = false)
        {
            try
            {
                return await Search(x => x.Email == email, tracking).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetByEmailAsync)} action {ex}");
                return new AppUser();
            }
        }

        public async Task<PagedList<AppUser>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await GetAll(tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedAsync)} action {ex}");
                return new PagedList<AppUser>(new List<AppUser>(), 0, 0, 0);
            }
        }
    }
}
