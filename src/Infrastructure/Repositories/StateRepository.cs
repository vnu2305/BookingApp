﻿using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(
            ApplicationDbContext context,
            ILoggerManager logger
            ) : base(context, logger) { }

        public async Task<State> GetByUserIdAsync(Guid userId, bool tracking = false)
        {
            return await Search(x => x.UserId == userId,
                                tracking)
                        .FirstOrDefaultAsync();
        }
    }
}
