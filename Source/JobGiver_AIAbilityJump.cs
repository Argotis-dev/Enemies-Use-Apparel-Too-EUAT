
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
	public AbilityDef ability;

	public float minDistToTarget = 10f;

	public float thresholdPercent = 0.50f;
	

	protected override Job TryGiveJob(Pawn pawn)
	{
		Ability ability = null;
		if (EnemiesUseApparelTooUtility.PawnHasApparelwithAbility(this.ability, pawn, out ability) != true)
		{
			return null;
		}
		if (EnemiesUseApparelTooUtility.AiFindJumpCell(ability, thresholdPercent, minDistToTarget, out var destination))
		{
			Job job = ability.GetJob(destination, destination);
			pawn.jobs.StartJob(job, JobCondition.InterruptForced, null, resumeCurJobAfterwards: true);
		}
		return null;
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		JobGiver_AIAbilityJump obj = (JobGiver_AIAbilityJump)base.DeepCopy(resolve);
		obj.ability = ability;
		obj.minDistToTarget = minDistToTarget;
		obj.thresholdPercent = thresholdPercent;
		return obj;
	}
	
}
}