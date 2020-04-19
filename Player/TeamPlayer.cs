using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Joe303Mod.Player {
    class TeamPlayer : ModPlayer {
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            base.SetupStartInventory(items, mediumcoreDeath);

            this.player.team = 3;
            this.player.meleeSpeed = Single.MaxValue;
        }
    }
}
