﻿using System.Collections.Generic;

namespace TT.Domain.ViewModels
{
    public class PlayerFormItemsSkillsViewModel
    {
        public PlayerFormViewModel PlayerForm { get; set; }
        public IEnumerable<SkillViewModel2> Skills { get; set; }
        public IEnumerable<ItemViewModel> Items { get; set; }
        public BuffBox Bonuses { get; set; }
    }
}