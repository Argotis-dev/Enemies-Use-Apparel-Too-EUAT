
using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using EnemiesUseApparelToo.Utility;
using Verse.AI;

using System.Collections.Generic;
using System.Linq;
using Verse.Noise;

namespace EnemiesUseApparelToo
{
public class JobGiver_AIAbilityJump : JobGiver_AICastAbility
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
		if (pawn.CurJob?.ability == ability)
		{
			return null;
		}
		if (ability == null || !ability.CanCast)
		{
			return null;
		}
		LocalTargetInfo target = GetTarget(pawn, ability);
		if (!target.IsValid)
		{
			return null;
		}
		return ability.GetJob(target, target);
	
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		JobGiver_AIAbilityJump obj = (JobGiver_AIAbilityJump)base.DeepCopy(resolve);
		obj.minDistToTarget = minDistToTarget;
		obj.thresholdPercent = thresholdPercent;
		return obj;
	}

	private static List<Pawn> potentialTargets = new List<Pawn>();

	/*private static readonly SimpleCurve DistanceSquaredToTargetSelectionWeightCurve = new SimpleCurve
	{
		new CurvePoint(100f, 1f),
		new CurvePoint(400f, 0.1f),
		new CurvePoint(625f, 0f)
	};*/
	protected override LocalTargetInfo GetTarget(Pawn caster, Ability ability)
	{
		potentialTargets.Clear();
		/*IEnumerable<Thing> hostiles = from x in caster.Map.attackTargetsCache.GetPotentialTargetsFor(caster)
			select x.Thing;
		if (hostiles.EnumerableNullOrEmpty())
		{
			return LocalTargetInfo.Invalid;
		}
		foreach (Pawn item in caster.Map.mapPawns.AllPawnsSpawned)
		{
			if (caster.HostileTo(item))
			{
				potentialTargets.Add(item);
			}
		}
		if (potentialTargets.TryRandomElementByWeight(delegate(Pawn x)
		{
			float num = ability.verb.EffectiveRange;
			foreach (Thing item2 in hostiles)
			{
				if (item2.Spawned)
				{
					float num2 = item2.Position.DistanceToSquared(x.Position);
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return DistanceSquaredToTargetSelectionWeightCurve.Evaluate(num);
		}, out var result))
		{*/
			bool isMeleeAttack = caster.CurrentEffectiveVerb.IsMeleeAttack;
			float maxDist = ability.verb.EffectiveRange;
			if(!isMeleeAttack)
			{
				maxDist = maxDist + Mathf.Clamp(caster.CurrentEffectiveVerb.EffectiveRange * 0.66f, 2f, 20f);
			}

			Thing target = (Thing)AttackTargetFinder.BestAttackTarget(caster, TargetScanFlags.NeedLOSToAll | TargetScanFlags.NeedReachableIfCantHitFromMyPos | TargetScanFlags.NeedThreat | TargetScanFlags.NeedAutoTargetable, IsGoodTarget, 0f, maxDist, default(IntVec3), float.MaxValue, canBashDoors: true);
	

			if (EnemiesUseApparelTooUtility.AiFindJumpCell(ability, target, thresholdPercent, minDistToTarget, out var destination))
			{
				return new LocalTargetInfo(destination);
			}
		//}
		return LocalTargetInfo.Invalid;
	}


		
	protected virtual bool IsGoodTarget(Thing thing)
	{
		if (thing is Pawn { Spawned: not false, Downed: false } pawn)
		{
			return !pawn.IsPsychologicallyInvisible();
		}
		return false;
	}
	
	
}
}