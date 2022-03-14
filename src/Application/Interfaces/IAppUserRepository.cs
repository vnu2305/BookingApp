﻿using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser>, IPageable<AppUser>
    {
        Task<AppUser> GetByTelegramIdAsync(long telegramId, bool tracking = false);
        Task<AppUser> GetByEmailAsync(string email, bool tracking = false);
    }
}
