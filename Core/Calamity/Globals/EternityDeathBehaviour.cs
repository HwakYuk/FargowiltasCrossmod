﻿using FargowiltasCrossmod.Core.Calamity.Systems;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasCrossmod.Core.Calamity.Globals
{
    public abstract class EternityDeathBehaviour : GlobalNPC
    {
        public NPCMatcher Matcher;

        public override bool InstancePerEntity => true;

        public sealed override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return lateInstantiation && Matcher.Satisfies(entity.type);
        }

        public override void Load()
        {
            Matcher = CreateMatcher();
            base.Load();
        }

        public abstract NPCMatcher CreateMatcher();

        public override GlobalNPC NewInstance(NPC target) //the cursed beast
        {
            return CalDLCWorldSavingSystem.EternityDeath && CalDLCWorldSavingSystem.E_EternityRev ? base.NewInstance(target) : null;
        }

        public bool FirstTick = true;
        public virtual void OnFirstTick(NPC npc) { }

        public virtual bool SafePreAI(NPC npc) => base.PreAI(npc);
        public sealed override bool PreAI(NPC npc)
        {
            if (!(CalDLCWorldSavingSystem.EternityDeath && CalDLCWorldSavingSystem.E_EternityRev) || !DLCCalamityConfig.Instance.EternityPriorityOverRev)
            {
                return true;
            }
            if (FirstTick)
            {
                FirstTick = false;

                OnFirstTick(npc);
            }
            return SafePreAI(npc);
        }


        protected static void NetSync(NPC npc, bool onlySendFromServer = true)
        {
            if (onlySendFromServer && Main.netMode != NetmodeID.Server)
                return;

            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
        }

    }
}
