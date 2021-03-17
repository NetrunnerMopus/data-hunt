﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.effects.corp
{
    public class Draw : IEffect
    {
        private int cards;
        public bool Impactful => true;
        public event Action<IEffect, bool> ChangedImpact;
        IEnumerable<string> IEffect.Graphics => new string[] { "Images/UI/card-draw" };

        public Draw(int cards)
        {
            this.cards = cards;
        }

        async Task IEffect.Resolve(Game game)
        {
            var rd = game.corp.zones.rd;
            var hq = game.corp.zones.hq;
            rd.Draw(cards, hq);
            await Task.CompletedTask;
        }
    }
}
