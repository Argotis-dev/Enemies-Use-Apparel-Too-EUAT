
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
public class JobGiver_FleePotentialExplosionJump : ThinkNode_JobGiver
{
	protected override Job TryGiveJob(Pawn pawn)
	{
		if ((int)pawn.RaceProps.intelligence < 2)
		{
			Log.Warning(pawn+ "is stupid");
			return null;
		}	
		if (pawn.mindState.knownExploder == null)
		{
			
			return null;
		}
		Log.Warning(pawn+ "can see exploder");
		if (!pawn.mindState.knownExploder.Spawned)
		{
			pawn.mindState.knownExploder = null;
			return null;
		}
		if (pawn.Downed && !pawn.health.CanCrawl)
		{
			return null;
		}
		Ability ability = null;
		if (EnemiesUseApparelTooUtility.PawnHasJumpAbility(pawn, out ability) != true)
		{
			return null;
		}
		Log.Warning("has ability");
		if (PawnUtility.PlayerForcedJobNowOrSoon(pawn))
		{
			return null;
		}
		Thing knownExploder = pawn.mindState.knownExploder;
		if ((float)(pawn.Position - knownExploder.Position).LengthHorizontalSquared > 81f)
		{
			Log.Warning("not seeing exploder");
			return null;
		}
		if (!EnemiesUseApparelTooUtility.TryFindRelocatePosition(ability, pawn, out var result, ability.verb.EffectiveRange))
		{
			return null;
		}
		Job job = ability.GetJob(result, result);
		return job;
	}
}
}