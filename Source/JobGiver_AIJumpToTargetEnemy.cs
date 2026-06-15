
using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using EnemiesUseApparelToo.Utility;
using Verse.AI;

namespace EnemiesUseApparelToo
{
public class JobGiver_AIJumpToTargetEnemy : ThinkNode_JobGiver
{
	public AbilityDef ability;

	public float minDistToTarget = 10f;
	

	protected override Job TryGiveJob(Pawn pawn)
	{
		Ability ability = null;
		if (EnemiesUseApparelTooUtility.PawnHasApparelwithAbility(this.ability, pawn, out ability) != true)
		{
			return null;
		}
		if (!ability.CanCast)
		{
			return null;
		}
		if (pawn.mindState?.enemyTarget == null)
		{
			return null;
		}
		Thing enemytarget = pawn.mindState?.enemyTarget;
		IntVec3 result = enemytarget.Position;
		float num = pawn.Position.DistanceTo(result);
		//Log.Message("Enemy Target:" +enemytarget + " Distance:" +num);
		if (num < minDistToTarget || num > ability.verb.EffectiveRange || !GenSight.LineOfSight(pawn.Position, result, pawn.Map))
		{
			return null;
		}
		var destination = RCellFinder.BestOrderedGotoDestNear(result, pawn, (c) => JumpUtility.ValidJumpTarget(pawn, pawn.Map, c) && JumpUtility.CanHitTargetFrom(pawn, pawn.Position, c, ability.verb.EffectiveRange));
		if (destination != null)
		{
			Job job = ability.GetJob(destination, destination);
			pawn.jobs.StartJob(job, JobCondition.InterruptForced, null, resumeCurJobAfterwards: true);
		}
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
	
}
}