﻿using FargowiltasCrossmod.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using System.Security.Policy;
using Terraria.Graphics.Renderers;
using CalamityMod.Graphics.Renderers;
using Terraria.Graphics;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using FargowiltasCrossmod.Core.Calamity;
using FargowiltasCrossmod.Content.Calamity.Projectiles;
using CalamityMod.Projectiles.Turret;
using CalamityMod.Particles;
using Terraria.Audio;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Forces;
using FargowiltasSouls;

namespace FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class VictideEnchantment : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return FargowiltasCrossmod.EnchantLoadingEnabled;
        }
        public override Color nameColor => new Color(255, 233, 197);

        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<VictideEffect>(Item);
            
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("FargowiltasCrossmod:AnyVictideHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideBreastplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideGreaves>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.SnapClam>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.UrchinMace>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.OceanCrest>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class VictideEffect : AccessoryEffect
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return FargowiltasCrossmod.EnchantLoadingEnabled;
        }
        public override Header ToggleHeader => Header.GetHeader<DevastationHeader>();
        public override int ToggleItemType => ModContent.ItemType<VictideEnchantment>();
        
        public override void PostUpdateEquips(Player player)
        {
            //lmao this doesnt do anything its all in CalamityAddonGlobalItem
        }
    }
}
