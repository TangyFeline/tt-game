﻿using System;
using NUnit.Framework;
using FluentAssertions;
using TT.Tests.Builders.Item;
using TT.Tests.Builders.AI;
using TT.Tests.Builders.Assets;
using System.Linq;
using TT.Domain.Assets.Commands;
using TT.Domain.Assets.Entities;
using TT.Domain.Exceptions;
using TT.Domain.Statics;

namespace TT.Tests.Assets.Commands
{
    [TestFixture]
    public class UpdateRestockItemTests : TestBase
    {

        RestockItem restockItem;

        [SetUp]
        public void Init()
        {
            restockItem = new RestockItemBuilder().With(ri => ri.Id, 13).BuildAndSave();
        }

        [Test]
        public void Should_update_RestockItem()
        {
            var item = new ItemSourceBuilder().With(cr => cr.Id, 222).BuildAndSave();

            var cmdEdit = new UpdateRestockItem { RestockItemId = 13, AmountBeforeRestock = 25, BaseItemId = item.Id, AmountToRestockTo = 50, BotId = AIStatics.LindellaBotId };

            Repository.Execute(cmdEdit);

            var editedRestockItem = DataContext.AsQueryable<RestockItem>().FirstOrDefault(cr => cr.Id == 13);

            editedRestockItem.Id.Should().Be(13);
            editedRestockItem.AmountBeforeRestock.Should().Be(25);
            editedRestockItem.AmountToRestockTo.Should().Be(50);
            editedRestockItem.BaseItem.Id.Should().Be(222);
            editedRestockItem.BotId.Should().Be(AIStatics.LindellaBotId);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Should_throw_error_when_base_item_id_is_invalid(int id)
        {
            var cmd = new UpdateRestockItem { AmountBeforeRestock = 0, BaseItemId = 0, AmountToRestockTo = 5, BotId = AIStatics.LindellaBotId };

            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("Base item Id must be greater than 0");
        }


        [Test]
        public void Should_throw_error_when_base_item_is_not_found()
        {
            const int id = 17;
            var cmd = new UpdateRestockItem { AmountBeforeRestock = 0, BaseItemId = id, AmountToRestockTo = 5, BotId = AIStatics.LindellaBotId };

            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage($"Base item with Id {id} could not be found");
        }

        [Test]
        public void Should_throw_error_when_invalid_amount_to_restock()
        {
            var amount = -1;

            var item = new ItemSourceBuilder().With(cr => cr.Id, 1).BuildAndSave();

            var cmd = new UpdateRestockItem { AmountBeforeRestock = amount, BaseItemId = item.Id, AmountToRestockTo = 5, BotId = AIStatics.LindellaBotId };

            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("Minimum amount before restock must be 0");
        }

        [Test]
        public void Should_not_throw_error_when_amount_to_restock_is_zero()
        {
            var amount = 0;

            var item = new ItemSourceBuilder().With(cr => cr.Id, 215).BuildAndSave();

            var cmd = new UpdateRestockItem { AmountBeforeRestock = amount, BaseItemId = item.Id, AmountToRestockTo = 5, BotId = AIStatics.LindellaBotId };

            var action = new Action(() => { Repository.Execute(cmd); });

            var editedRestockItem = DataContext.AsQueryable<RestockItem>().FirstOrDefault(cr => cr.Id == 13);

            editedRestockItem.Id.Should().Be(13);
        }

        [Test]
        public void Should_throw_error_when_invalid_amount_to_restockTo()
        {
            var amount = 0;

            var item = new ItemSourceBuilder().With(cr => cr.Id, 1).BuildAndSave();
            var npc = new NPCBuilder().With(n => n.Id, 7).BuildAndSave();

            var cmd = new UpdateRestockItem { AmountBeforeRestock = 1, BaseItemId = item.Id, AmountToRestockTo = amount, BotId = AIStatics.LindellaBotId };

            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("Minimum amount to restock to must be 1");
        }

    }
}