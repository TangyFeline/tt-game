﻿using NUnit.Framework;
using TT.Domain;
using TT.Domain.Assets.Queries;
using TT.Domain.Statics;
using TT.Tests.Builders.Assets;
using TT.Tests.Builders.Item;

namespace TT.Tests.Assets.Queries
{
    [TestFixture]
    public class GetRestockItemsTests : TestBase
    {
        [Test]
        public void Should_fetch_all_available_RestockItems()
        {
            new RestockItemBuilder().With(cr => cr.Id, 7)
                .With(cr => cr.AmountBeforeRestock, 5)
                .With(cr => cr.AmountToRestockTo, 9)
                .With(cr => cr.BaseItem, new ItemSourceBuilder().With(cr => cr.Id, 35).BuildAndSave())
                .With(cr => cr.BotId, AIStatics.LindellaBotId)
                .BuildAndSave();

            new RestockItemBuilder().With(cr => cr.Id, 99)
                .With(cr => cr.AmountBeforeRestock, 1)
                .With(cr => cr.AmountToRestockTo, 3)
                .With(cr => cr.BaseItem, new ItemSourceBuilder().With(cr => cr.Id, 49).BuildAndSave())
                .With(cr => cr.BotId, AIStatics.LindellaBotId)
                .BuildAndSave();

            var cmd = new GetRestockItems();

            Assert.That(DomainRegistry.Repository.Find(cmd), Has.Exactly(2).Items);
        }

        [Test]
        public void Should_return_empty_list_if_no_RestockItems_found()
        {
            var cmd = new GetRestockItems();

            Assert.That(DomainRegistry.Repository.Find(cmd), Is.Empty);
        }

    }
}