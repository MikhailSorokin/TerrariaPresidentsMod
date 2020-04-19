using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Joe303Mod.Items
{
	public class DeezNuts : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("DeezNuts"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This is a nut that is displayed on DEEZ, HA GOTEM.");
		}

        public override void UpdateInventory(Terraria.Player player)
        {
            base.UpdateInventory(player);

            player.team = 3;
        }

        public override void SetDefaults() 
		{
            item.damage = 9995;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.alpha = 244;
        }

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperPickaxe, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}