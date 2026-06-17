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
    
    public class CompAbility_AiJump : CompAbilityEffect
    {
        public new CompProperties_AbilityAiJump Props => (CompProperties_AbilityAiJump)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if(!parent.pawn.IsPlayerControlled)
            {
               EnemiesUseApparelTooUtility.AiFindJumpCell(parent, Props.thresholdPercent, Props.minDistToTarget, out var destination);
               target = destination;
               dest = destination;
                
            }

            base.Apply(target, dest);
        }


    }
}
