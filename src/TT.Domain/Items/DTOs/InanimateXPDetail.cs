﻿using System;

namespace TT.Domain.Items.DTOs
{
    public class InanimateXPDetail
    {
        public decimal Amount { get; protected set; }
        public int TimesStruggled { get; protected set; }
        public DateTime LastActionTimestamp { get; protected set; }
        public int LastActionTurnstamp { get; protected set; }
    }
}