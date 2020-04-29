using System;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader.Audio;
using SoundType = Terraria.ModLoader.SoundType;

namespace Joe303Mod.Bosses {
    [AutoloadBossHead]
    class JoeBiden : ModNPC
    {
        private bool hitOwie;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Joe Biden");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            //11 is Skeleton's head, dungeon guardian

            //npc.aiStyle = 31;
            npc.aiStyle = -1;
            npc.life = 3030;
            npc.lifeMax = 3030;
            npc.damage = 5; //base damage value boss on Normal
            npc.defense = 25;
            npc.knockBackResist = 0f;
            npc.width = 140;
            npc.height = 145;
            npc.value = Item.buyPrice(1, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true; //will not collide with the tiles.
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            //LegacySoundStyle style = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Joe303BossTheme");
            //style.SoundId
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Joe303BossTheme");

            //mod.Logger.InfoFormat("Music id is: {0}" , music.ToString());
            music = MusicID.Boss1;
            //npc.DeathSound = new LegacySoundStyle(0, 1, SoundType.Music);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1.0;
            npc.spriteDirection = npc.direction;
            
            //if (npc.frameCounter / 2.0);
            //if (frame >= Main.npcFrameCount[npc.type]) frame = 0;

            if (hitOwie)
            {
                mod.Logger.InfoFormat("HIT OWIE");
                npc.frame.Y = frameHeight * 3;
            }
            else
            {
                npc.frameCounter %= 2.0;
                if (npc.frameCounter < 1.0) {
                    npc.frame.Y = frameHeight;
                } else {
                    npc.frame.Y = frameHeight * 2;
                }
            }
        }

        public override void AI()
        {
            Terraria.Player player = Main.player[npc.target];
            npc.SimpleFlyMovement(npc.DirectionTo(player.position + new Vector2((float)(-(double)npc.ai[2] * 300.0), -200f)) * 7.5f, 1.5f);

            mod.Logger.InfoFormat("Health is: {0}", npc.life);
            if (npc.life <= 1500) 
            {
                hitOwie = true;
            }

            if (npc.life == 0)
            {
                //unload music
                SoundEffect currSound = mod.GetSound("Sounds/Music/Joe303BossTheme");
                currSound.Dispose();
                //SoundEffectInstance currSoundInstance Soun
                //Main.GetActiveSound(mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Joe303BossTheme")).Stop();

            }

            base.AI();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
