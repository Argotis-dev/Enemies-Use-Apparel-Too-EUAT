
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
public class JobGiver_AIDeploySpawnable : JobGiver_AICastAbility
{
	protected override Job TryGiveJob(Pawn pawn)
	{
		Ability ability = null;
		if (EnemiesUseApparelTooUtility.PawnHasApparelwithAbility(EUATDefOf.EUAT_DeployTurret, pawn, out ability) != true)
		{
			return null;
		}
		if (pawn.CurJob?.def == ability.def.jobDef)
		{
			return null;
		}
		if (ability == null || !ability.CanCast)
		{
			return null;
		}
		Log.Warning("prevalid");
		LocalTargetInfo target = GetTarget(pawn, ability);
		if (target.IsValid)
		{
			return ability.GetJob(target, target);;
		}
		
		return null;
	
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		JobGiver_AIDeploySpawnable obj = (JobGiver_AIDeploySpawnable)base.DeepCopy(resolve);
		return obj;
	}

	private static List<Pawn> potentialTargets = new List<Pawn>();

	protected override LocalTargetInfo GetTarget(Pawn caster, Ability ability)
	{
		potentialTargets.Clear();
		float maxDist = 15f + ability.verb.EffectiveRange;

		Thing target = (Thing)AttackTargetFinder.BestAttackTarget(caster, TargetScanFlags.NeedLOSToAll | TargetScanFlags.NeedReachableIfCantHitFromMyPos | TargetScanFlags.NeedThreat | TargetScanFlags.NeedAutoTargetable, IsGoodTarget, 0f, maxDist, default(IntVec3), float.MaxValue, canBashDoors: true);

		if (boolTryFindTurretPosition(caster, target, out var destination, ability.verb))
		{
			Log.Warning("valid dest");
			return new LocalTargetInfo(destination);
		}

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
	
	private static bool boolTryFindTurretPosition(Pawn pawn, TargetInfo target, out IntVec3 dest, Verb verbToUse = null)
	{
		Verb verb = verbToUse ?? pawn.TryGetAttackVerb(null, !pawn.IsColonist);
		if (verb == null)
		{
			dest = IntVec3.Invalid;
			return false;
		}
		return CastPositionFinder.TryFindCastPosition(new CastPositionRequest
		{
			caster = pawn,
			target = target.Thing,
			verb = verb,
			maxRangeFromTarget = 15f,
			wantCoverFromTarget = (verb.EffectiveRange > 5f)
		}, out dest);
	}


	
}
}