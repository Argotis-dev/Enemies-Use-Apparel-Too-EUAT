
using RimWorld.Utility;
using UnityEngine;
using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using AiUseableApparel.Utility;
using Verse.AI;

namespace AiUseableApparel
{
public class JobGiver_AIJumpToTargetEnemy : ThinkNode_JobGiver
{
	public AbilityDef ability;

	public float minDistToTarget = 10f;

	private bool ignoreNonCombatants = false;

	private bool humanlikesOnly = true;
	

	protected override Job TryGiveJob(Pawn pawn)
	{
		Ability ability = null;
		if (AiUseableApparelUtility.PawnHasApparelwithAbility(this.ability, pawn, out ability) != true)
		{
			Log.Error("Ability not found");
			return null;
		}
		if (!ability.CanCast)
		{
			Log.Error("can't cast");
			return null;
		}
		float num = float.MaxValue;
		Thing thing = null;
		List<IAttackTarget> potentialTargetsFor = pawn.Map.attackTargetsCache.GetPotentialTargetsFor(pawn);
		for (int i = 0; i < potentialTargetsFor.Count; i++)
		{
			IAttackTarget attackTarget = potentialTargetsFor[i];
			if (!attackTarget.ThreatDisabled(pawn) && AttackTargetFinder.IsAutoTargetable(attackTarget) && (!humanlikesOnly || !(attackTarget is Pawn pawn2) || pawn2.RaceProps.Humanlike) && (!(attackTarget.Thing is Pawn pawn3) || pawn3.IsCombatant() || (!ignoreNonCombatants && GenSight.LineOfSightToThing(pawn.Position, pawn3, pawn.Map))) && (pawn.Faction == null || !pawn.Faction.IsPlayer || !attackTarget.Thing.Position.Fogged(pawn.Map)))
			{
				Thing thing2 = (Thing)attackTarget;
				int num2 = thing2.Position.DistanceToSquared(pawn.Position);
				if ((float)num2 < num && pawn.CanReach(thing2, PathEndMode.OnCell, Danger.Deadly))
				{
					num = num2;
					thing = thing2;
				}
			}
		}
		if (thing != null)
		{
			if (thing.PositionHeld == pawn.PositionHeld || pawn.CanReachImmediate(thing, PathEndMode.Touch))
			{
				return null;
			}
			/*if (pawn.mindState?.enemyTarget == null)
			{
				Log.Error("no target");
				return null;
			}
			Thing enemytarget = pawn.mindState?.enemyTarget;*/
			IntVec3 result = thing.Position;
			float num3 = pawn.Position.DistanceTo(result);
			Log.Message("Enemy Target:" +thing + " Distance:" +num);
			if (num3 < minDistToTarget )
			{
				//|| num3 > ability.verb.EffectiveRange || !GenSight.LineOfSight(pawn.Position, result, pawn.Map)
				Log.Error("target params not met, target distance:" +num3);
				return null;
			}
			var destination = RCellFinder.BestOrderedGotoDestNear(result, pawn, (c) => JumpUtility.ValidJumpTarget(pawn, pawn.Map, c) );
			//&& JumpUtility.CanHitTargetFrom(pawn, pawn.Position, c, ability.verb.EffectiveRange)	
			/*if (!RCellFinder.TryFindGoodAdjacentSpotToTouch(pawn, enemytarget, out result))
			{
				Log.Error("no available space");
				return null;
			}*/
			//if (ability.verb.ValidateTarget(enemytarget, showMessages: false))
			if (destination != null)
			{
				Log.Error("Launching to target:" +thing);
				//return ability.GetJob(destination, destination);
				Job job = ability.GetJob(destination, destination);
				//return ability.GetJob(result, result);
				pawn.jobs.StartJob(job, JobCondition.InterruptForced, null, resumeCurJobAfterwards: true);
				//FleckMaker.Static(result, pawn.Map, FleckDefOf.FeedbackGoto);
			}
		}
		Log.Error("no destination");
		return null;
	}

	public virtual bool CanJumpToTarget(Pawn pawn, LocalTargetInfo target)
	{
		return true;
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		JobGiver_AIJumpToTargetEnemy obj = (JobGiver_AIJumpToTargetEnemy)base.DeepCopy(resolve);
		obj.ability = ability;
		obj.minDistToTarget = minDistToTarget;
		return obj;
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