﻿using Highway.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT.Domain.Identity.Entities;
using TT.Domain.Identity.QueryRequests;

namespace TT.Domain.Identity.RequestHandlers
{
    public class IsValidUserRequestHandler : IAsyncRequestHandler<IsValidUserRequest, bool>
    {
        private readonly IDataContext context;

        public IsValidUserRequestHandler(IDataContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(IsValidUserRequest message)
        {
            var userQuery = from u in context.AsQueryable<User>()
                            where u.Id == message.UserNameId
                            select u;

            return await userQuery.AnyAsync();
        }
    }
}