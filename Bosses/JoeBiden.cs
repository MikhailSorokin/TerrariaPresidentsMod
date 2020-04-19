using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.Audio;
using SoundType = Terraria.ModLoader.SoundType;

namespace Joe303Mod.Bosses {
    [AutoloadBossHead]
    class JoeBiden : ModNPC {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Joe Biden");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = 11; //Skeletron's head, dungeon guardian
            npc.lifeMax = 3030;
            npc.boss = true;
            npc.width = 140;
            npc.height = 470 / 3;
            /*npc.damage = 5; //base damage value boss on Normal
            npc.defense = 25;
            npc.width = 140;
            npc.height = 470 / 3;
            npc.value = 10000;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true; //will not collide with the tiles.
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;*/
            //LegacySoundStyle style = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Joe303BossTheme");
            //style.SoundId
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Joe303BossTheme");
            //mod.Logger.InfoFormat("Music id is: {0}" , music.ToString());
            //music = MusicID.Boss1;
            //npc.DeathSound = new LegacySoundStyle(0, 1, SoundType.Music);
        }

        /*public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 120;
            int frame = (int) (npc.frameCounter / 40.0);
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;
            npc.frame.Y = frame * frameHeight;
        }*/

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
