﻿using System.Collections.Generic;
using System.Linq;
using Highway.Data;
using TT.Domain.DTOs.Item;

namespace TT.Domain.Queries.Item
{
    public class GetItemsOwnedByPlayer : DomainQuery<ItemDetail>
    {

        public int OwnerId { get; set; }

        public override IEnumerable<ItemDetail> Execute(IDataContext context)
        {
            ContextQuery = ctx => ctx.AsQueryable<Entities.Items.Item>().ProjectToQueryable<ItemDetail>().Where(i => i.Owner.Id == OwnerId);
            return ExecuteInternal(context);
        }
    }
}