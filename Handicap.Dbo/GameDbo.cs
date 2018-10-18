﻿using System;

namespace Handicap.Dbo
{
    public class GameDbo : BaseDbo
    {
        public PlayerDbo PlayerOne { get; set; }
        public PlayerDbo PlayerTwo { get; set; }
        public int Type { get; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}