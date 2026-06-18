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

        protected virtual IntRange ExpiryInterval_Ability => new IntRange(30, 30);

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if(!parent.pawn.IsColonist )
            {
                Pawn pawn = parent.pawn;
                

                if (EnemiesUseApparelTooUtility.AiFindJumpCell(parent, Props.thresholdPercent, Props.minDistToTarget, out var destination))
                {
                    Log.Warning("got to attempted jump");
                    Job job = parent.GetJob(destination, destination);
                    pawn.jobs.StopAll();
                    pawn.jobs.StartJob(job, JobCondition.InterruptForced);
                }
                else
                {
                    pawn.jobs.StopAll();
                    pawn.pather?.StopDead();
			        Job job = JobMaker.MakeJob(JobDefOf.Wait_Combat, ExpiryInterval_Ability.RandomInRange, checkOverrideOnExpiry: true);
                    pawn.jobs.StartJob(job, JobCondition.InterruptForced);
                }

            }
                
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return EnemiesUseApparelTooUtility.AiFindJumpCell(parent, Props.thresholdPercent, Props.minDistToTarget, out var destination);
        }


    }
}
