﻿using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Persistence.Contexts;
using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace asari.com.tr.Persistence.Repositories;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim, BaseDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(BaseDbContext context) : base(context)
    {
        // BaseDbContext'i Ef içerisinde Base Context e yolladı
    }
}