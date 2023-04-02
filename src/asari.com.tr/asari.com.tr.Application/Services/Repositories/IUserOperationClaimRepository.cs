﻿using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace asari.com.tr.Application.Services.Repositories;

public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim>, IRepository<UserOperationClaim>
{
}