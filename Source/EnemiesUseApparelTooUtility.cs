using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;


using RimWorld;

namespace EnemiesUseApparelToo.Utility
{
    
    public static class EnemiesUseApparelTooUtility
    {
            public static Apparel GetAbilityApparelSource(Ability ability, out Apparel apparelwithability)
            {
                apparelwithability = null;
                if (ability.pawn.apparel != null)
                {   
                    foreach (Apparel apparelitem in ability.pawn.apparel.WornApparel)
                    {
                        foreach (Ability abilityitem in apparelitem.AllAbilitiesForReading)
                        {
                            if (ability == abilityitem)
                            {
                                apparelwithability = apparelitem;
                            }
                        }
                    }
                }
                return apparelwithability;
            }

            public static bool PawnHasApparelwithAbility(AbilityDef abilitydef, Pawn pawn, out Ability ability)
            {
                ability = null;
                if (pawn.apparel.WornApparel != null)
                {   
                    foreach (Apparel apparelitem in pawn.apparel.WornApparel)
                    {
                        if (apparelitem.AllAbilitiesForReading != null)
                        {
                            foreach (Ability abilityitem in apparelitem.AllAbilitiesForReading)
                            {
                                if (abilityitem.def == abilitydef)
                                {
                                    ability = abilityitem;
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

    public static void ForceAttack(Pawn attacker, Thing target, int durationTicks = 0, int forcedAttackCount = 1, bool requiresLineOfSight = false, bool forceMelee = false)
	{
		if (attacker is not { Spawned: true } || target is not { Spawned: true }) return;
		
		try
		{
		Job job;
		if (forceMelee || (attacker.CurrentEffectiveVerb?.verbProps.IsMeleeAttack ?? true))
		{
			job = JobMaker.MakeJob(JobDefOf.AttackMelee, new LocalTargetInfo(target));
			if (attacker.Faction == Faction.OfPlayer) job.playerForced = true;
		}
		else
		{
			job = JobMaker.MakeJob(JobDefOf.AttackStatic, new LocalTargetInfo(target));
			job.maxNumStaticAttacks = forcedAttackCount;
			job.endIfCantShootTargetFromCurPos = requiresLineOfSight;
		}
		if (durationTicks > 0)
		{
			job.expiryInterval = durationTicks;
		}

		attacker.jobs.StopAll();
		attacker.jobs.StartJob(job, JobCondition.InterruptForced);
		}
		catch (Exception e)
		{
		Log.Warning($"{attacker} was unable to force-attack {target}: {e}");
		}
	}

    }
}