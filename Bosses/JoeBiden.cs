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
        private double frameCooldown;
        private float moveTimer;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Joe Biden");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override int SpawnNPC(int tileX, int tileY)
        {
            npc.FaceTarget();
            Terraria.Player player = Main.player[npc.target];

            npc.position = player.Center + new Vector2(0, -100);

            return base.SpawnNPC(tileX, tileY);
        }

        public override void SetDefaults()
        {
            //11 is Skeleton's head, dungeon guardian

            hitOwie = false;
            //npc.aiStyle = 11;
            npc.aiStyle = -1;
            npc.life = 3030;
            npc.lifeMax = 3030;
            npc.damage = 5; //base damage value boss on Normal
            npc.defense = 25;
            npc.knockBackResist = 5f;
            npc.width = 140;
            npc.height = 145;
            npc.value = Item.buyPrice(1, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.stinky = true;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true; //will not collide with the tiles.
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Joe303BossTheme");

            mod.Logger.InfoFormat("Music id is: {0}" , music.ToString());
        }

        // Assume 30 frames per second (which is probably wrong)
        public override void FindFrame(int frameHeight) {
            npc.frameCounter += 1.0;
            npc.frameCounter %= 60.0;
            npc.spriteDirection = -npc.direction;

            if (hitOwie && frameCooldown >= 0) {
                mod.Logger.InfoFormat("HIT OWIE");
                npc.frame.Y = frameHeight * 3;
                // hold for 5 seconds
                frameCooldown -= 1.0;
            } else {
	            hitOwie = false;
                // First 15 frames show as one frame
                if (npc.frameCounter < 30.0) {
                    npc.frame.Y = 0;
                }
                // Next 15 show as second frame
                else {
                    npc.frame.Y = frameHeight;
                }
            }
        }
        
        private float shootCooldown = 2f * 60;

        public override void AI()
        {
            npc.TargetClosest(true);
            
            Terraria.Player player = Main.player[npc.target];

            // Set the direction towards the player
            Vector2 moveTo = player.Center - npc.Center;

            if (moveTimer != 0)
            {
                npc.velocity = moveTo / 300f;
            }

            if (moveTimer <= 0) {
                moveTimer += 0.1f;
                if (moveTimer >= 3f)
                {
                    moveTimer = 0;
                }
                npc.netUpdate = true;
            }
   
            mod.Logger.InfoFormat("Health is: {0}", npc.life);

            shootCooldown += 1f;
            if (shootCooldown >= 2f*60)
            {
	            // Calculate new speeds for other projectiles.
	            // Rebound at 40% to 70% speed, plus a random amount between -8 and 8
	            float speedX = -120f * Main.rand.NextFloat(.4f, .7f) + Main.rand.NextFloat(-8f, 8f);
	            float speedY =
		            -120f * Main.rand.Next(40, 70) * 0.01f +
		            Main.rand.Next(-20, 21) * 0.4f; // This is Vanilla code, a little more obscure.

	            Projectile.NewProjectile(npc.position.X, npc.position.Y, speedX, speedY, ProjectileID.EyeLaser, 3, 0.3f,
		            Main.myPlayer, 0f, 0f);

	            frameCooldown = 0.5 * 60.0;
	            hitOwie = true;
	            
	            shootCooldown = 0f;
            }

            /*if (npc.life <= 1500) 
            {
                hitOwie = true;
            }

            if (npc.life == 0)
            {
                //unload music
                SoundEffect currSound = mod.GetSound("Sounds/Music/Joe303BossTheme");
                currSound.Dispose();
            }*/

            base.AI();
        }

        private float laser1;
        private float laser2;
        private float laserTimer;
        
        /*private void ExpertLaser() {
        	laserTimer--;
        	if (laserTimer <= 0 && Main.netMode != 1) {
        		if (npc.localAI[0] == 2f) {
        			int laser1Index;
        			int laser2Index;
        			if (laser1 < 0) {
        				laser1Index = npc.whoAmI;
        			}
        			else {
        				for (laser1Index = 0; laser1Index < 200; laser1Index++) {
        					if (laser1 == Main.npc[laser1Index].ai[1]) {
        						break;
        					}
        				}
        			}
        			if (laser2 < 0) {
        				laser2Index = npc.whoAmI;
        			}
        			else {
        				for (laser2Index = 0; laser2Index < 200; laser2Index++) {
        					if (laser2 == Main.npc[laser2Index].ai[1]) {
        						break;
        					}
        				}
        			}
        			Vector2 pos = Main.npc[laser1Index].Center;
        			int damage = Main.npc[laser1Index].damage / 2;
        			if (Main.expertMode) {
        				damage = (int)(damage / Main.expertDamage);
        			}
        			Projectile.NewProjectile(pos.X, pos.Y, 0f, 0f, ProjectileType<ElementLaser>(), damage, 0f, Main.myPlayer, laser1Index, laser2Index);
        		}
        		else {
        			npc.localAI[0] = 2f;
        		}
        		laserTimer = 500 + Main.rand.Next(100);
        		laserTimer = 60 + laserTimer * npc.life / npc.lifeMax;
        		laser1 = Main.rand.Next(6) - 1;
        		laser2 = Main.rand.Next(5) - 1;
        		if (laser2 >= laser1) {
        			laser2++;
        		}
        	}
        }*/

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
