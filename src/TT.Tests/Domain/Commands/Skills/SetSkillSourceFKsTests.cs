﻿using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TT.Domain;
using TT.Domain.Commands.Skills;
using TT.Domain.Entities.Effects;
using TT.Domain.Entities.Forms;
using TT.Domain.Entities.Item;
using TT.Domain.Entities.Skills;
using TT.Tests.Builders.Effects;
using TT.Tests.Builders.Form;
using TT.Tests.Builders.Item;
using TT.Tests.Builders.Skills;

namespace TT.Tests.Domain.Commands.Skills
{
    [TestFixture]
    public class SetSkillSourceFKsTests : TestBase
    {

        private SkillSource skill;
        private FormSource form;
        private FormSource exclusiveToForm;
        private ItemSource exlusiveToItem;
        private EffectSource givesEffect;

        [SetUp]
        public void Init()
        {

            skill = new SkillSourceBuilder()
                .With(ss => ss.Id, 55)
                .BuildAndSave();

            form = new FormSourceBuilder()
                .With(f => f.Id, 3)
                .With(f => f.dbName, "bobform")
                .BuildAndSave();

            exclusiveToForm = new FormSourceBuilder()
                .With(f => f.Id, 7)
                .With(f => f.dbName, "exclusiveForm")
                .BuildAndSave();

            exlusiveToItem = new ItemSourceBuilder()
                .With(i => i.Id, 100)
                .With(i => i.DbName, "exclusiveToItem")
                .BuildAndSave();

            givesEffect = new EffectSourceBuilder()
                .With(i => i.Id, 78)
                .With(i => i.dbName, "givesEffect")
                .BuildAndSave();

        }

        [Test]
        public void should_save_skill_with_new_fks()
        {
            
            DomainRegistry.Repository.Execute(new SetSkillSourceFKs
            {
                SkillSourceId = 55,
                FormSource = form.dbName, 
                ExclusiveToFormSource = exclusiveToForm.dbName,
                ExclusiveToItemSource = exlusiveToItem.DbName,
                GivesEffectSource = givesEffect.dbName
            });

            var skillSource = DataContext.AsQueryable<SkillSource>().First(f => f.Id == 55);

            skillSource.FormSource.Id.Should().Be(3);
            skillSource.FormSource.dbName.Should().Be("bobform");

            skillSource.ExclusiveToFormSource.Id.Should().Be(7);
            skillSource.ExclusiveToFormSource.dbName.Should().Be("exclusiveForm");

            skillSource.ExclusiveToItemSource.Id.Should().Be(100);
            skillSource.ExclusiveToItemSource.DbName.Should().Be("exclusiveToItem");

            skillSource.GivesEffectSource.Id.Should().Be(78);
            skillSource.GivesEffectSource.dbName.Should().Be("givesEffect");

        }

        [Test]
        public void should_save_skill_with_new_fks_all_nulls()
        {

            DomainRegistry.Repository.Execute(new SetSkillSourceFKs
            {
                SkillSourceId = 55,
                FormSource = null,
                ExclusiveToFormSource = null,
                ExclusiveToItemSource = null,
                GivesEffectSource = null
            });

            var skillSource = DataContext.AsQueryable<SkillSource>().First(f => f.Id == 55);

            skillSource.FormSource.Should().Be(null);
            skillSource.ExclusiveToFormSource.Should().Be(null);
            skillSource.GivesEffect.Should().Be(null);
            skillSource.ExclusiveToItemSource.Should().Be(null);
        }

        [Test]
        public void should_throw_error_if_form_source_submitted_but_not_found()
        {
            var cmd = new SetSkillSourceFKs { SkillSourceId = 55, FormSource = "fake"};
            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("FormSource Source with name 'fake' could not be found.  Does it need to be published first?");
        }

        [Test]
        public void should_throw_error_if_exclusive_to_form_source_submitted_but_not_found()
        {

            var cmd = new SetSkillSourceFKs { SkillSourceId = 55, ExclusiveToFormSource = "fake"};
            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("ExclusiveToFormSource with name 'fake' could not be found");
        }

        [Test]
        public void should_throw_error_if_gives_effect_source_submitted_but_not_found()
        {

            var cmd = new SetSkillSourceFKs { SkillSourceId = 55, GivesEffectSource = "fake"};
            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("EffectSource with name 'fake' could not be found");
        }

        [Test]
        public void should_throw_error_if_exclusive_to_item_source_submitted_but_not_found()
        {

            var cmd = new SetSkillSourceFKs { SkillSourceId = 55, ExclusiveToItemSource = "fake" };
            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("ExclusiveToItemSource with name 'fake' could not be found");
        }

        [Test]
        public void should_throw_error_if_skill_source_not_found()
        {
            var cmd = new SetSkillSourceFKs { SkillSourceId = 3457};
            var action = new Action(() => { Repository.Execute(cmd); });

            action.ShouldThrowExactly<DomainException>().WithMessage("Skill Source with Id 3457 could not be found");
        }

    }
}