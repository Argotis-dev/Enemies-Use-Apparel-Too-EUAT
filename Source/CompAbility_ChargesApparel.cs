using HarmonyLib;
using LudeonTK;
using RimWorld;
using RimWorld.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using Verse.AI;
using EnemiesUseApparelToo.Utility;

namespace EnemiesUseApparelToo
{
    
    public class CompAbility_ChargesApparel : CompAbilityEffect
    {
        public new CompProperties_AbilityChargesApparel Props => (CompProperties_AbilityChargesApparel)props;

        private Apparel ReloadableItem => EnemiesUseApparelTooUtility.GetAbilityApparelSource(parent);

        private int RemainingCharges => ReloadableItem?.GetComp<CompApparelVerbOwner_Charged>()?.RemainingCharges ?? 0;
        private int MaxCharges => ReloadableItem?.GetComp<CompApparelVerbOwner_Charged>()?.MaxCharges ?? 0;

        public override bool CanCast => RemainingCharges > 0;

        public string LabelRemaining => $"{RemainingCharges} / {MaxCharges}";

        public override bool ShouldHideGizmo => true;
/*
        public override bool GizmoDisabled(out string reason)
        {
           reason = null;
            if (ReloadableItem == null)
            {
                reason = "No Apparel with ability found";
                return true;
            }
           if (RemainingCharges <= 0)
            {
                reason = "No Remaining Charges";
                return true;
            }
            return false;
        }
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {

            if(!GizmoDisabled(out var reason)){
                parent.maxCharges = MaxCharges;
                parent.RemainingCharges = RemainingCharges;
            }
            return base.CompGetGizmosExtra();
        }*/
        public override void PostApplied(List<LocalTargetInfo> targets, Map map)
        {
            ReloadableItem?.GetComp<CompApparelVerbOwner_Charged>()?.UsedOnce();
    
            base.PostApplied(targets, map);
            
        }

    }
}
