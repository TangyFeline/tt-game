﻿using System.Linq;
using Highway.Data;
using TT.Domain.DTOs.Players;

namespace TT.Domain.Queries.Players
{
    public class GetPlayerByBotId : DomainQuerySingle<PlayerDetail>
    {
        public int BotId { get; set; }

        public override PlayerDetail Execute(IDataContext context)
        {
            ContextQuery = ctx =>
            {
                return ctx.AsQueryable<Entities.Players.Player>()
                            .Where(p => p.BotId == BotId)
                            .ProjectToFirstOrDefault<PlayerDetail>();
            };

            return ExecuteInternal(context);
        }

    }
}