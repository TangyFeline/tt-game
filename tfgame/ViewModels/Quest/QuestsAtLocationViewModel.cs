﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tfgame.dbModels.Models;

namespace tfgame.ViewModels.Quest
{
    public class QuestsAtLocationViewModel
    {
        public IEnumerable<QuestStart> AllQuests { get; set; }
        public IEnumerable<QuestStart> AvailableQuests { get; set; }
    }
}