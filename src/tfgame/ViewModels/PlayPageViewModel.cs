﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tfgame.dbModels.Models;

namespace tfgame.ViewModels
{
    public class PlayPageViewModel
    {
        public PlayerFormViewModel You { get; set; }
        public IEnumerable<PlayerFormViewModel> PlayersHere { get; set; }

        public Location Location { get; set; }

        public int InventoryMaxSize { get; set; }

        public IEnumerable<LocationLog> LocationLog { get; set; }
        public IEnumerable<PlayerLog> PlayerLog { get; set; }
        public IEnumerable<PlayerLog> PlayerLogImportant { get; set; }

        public IEnumerable<SkillViewModel> Skills { get; set; }
        public IEnumerable<ItemViewModel> PlayerItems { get; set; }
        public IEnumerable<ItemViewModel> LocationItems { get; set; }
        public WorldStats WorldStats { get; set; }
        public int AttacksMade { get; set; }
        public decimal APSearchCost { get; set; }

        public MessageCountDataViewModel MessageCounts { get; set; }

        public DateTime LastUpdateTimestamp { get; set; }

        public PvPWorldStat PvPWorldStat { get; set; }

    }
}