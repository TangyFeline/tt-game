﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tfgame.dbModels.Models;

namespace tfgame.ViewModels
{

    public class MessageViewModel {
        public Message dbMessage { get; set;}
        public string SenderName { get; set;}
        public string SentToName { get; set; }
    }

    public class MessageBag
    {
        public List<MessageViewModel> Messages { get; set; }
        public List<MessageViewModel> SentMessages { get; set; }
        public int WearerId { get; set; }
        public string WearerName { get; set; }
    }
}