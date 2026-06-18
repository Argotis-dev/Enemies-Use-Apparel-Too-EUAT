
using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using EnemiesUseApparelToo.Utility;
using Verse.AI;

namespace EnemiesUseApparelToo
{
public class JobGiver_AIAbilityJump : ThinkNode_JobGiver
{
	public float minDistToTarget = 10f;

	public float thresholdPercent = 0.50f;
	

	protected override Job TryGiveJob(Pawn pawn)
	{
		Ability ability = null;
		if (EnemiesUseApparelTooUtility.PawnHasJumpAbility(pawn, out ability) != true)
		{
			return null;
		}
		if (EnemiesUseApparelTooUtility.AiFindJumpCell(ability, thresholdPercent, minDistToTarget, out var destination))
		{
			Job job = ability.GetJob(destination, destination);
			pawn.pather?.StopDead();
			pawn.jobs.StartJob(job, JobCondition.InterruptForced, null, resumeCurJobAfterwards: false);
		}
		return null;
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		JobGiver_AIAbilityJump obj = (JobGiver_AIAbilityJump)base.DeepCopy(resolve);
		obj.minDistToTarget = minDistToTarget;
		obj.thresholdPercent = thresholdPercent;
		return obj;
	}
	
}
}