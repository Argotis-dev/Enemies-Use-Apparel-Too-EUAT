using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Utility;
using UnityEngine;
using Verse;
using AiUseableApparel.Utility;
using Verse.AI;

namespace AiUseableApparel
{
public class JobGiver_AIJumpApparelEscapeEnemies : ThinkNode_JobGiver
{
	public AbilityDef ability;

	private static List<Thing> tmpHostileSpots = new List<Thing>();

	protected override Job TryGiveJob(Pawn pawn)
	{
		Ability ability = null;
		if (AiUseableApparelUtility.PawnHasApparelwithAbility(this.ability, pawn, out ability) != true || !ability.CanCast)
		{
			return null;
		}
		if (TryFindRelocatePosition(ability , pawn, out var relocatePosition, ability.verb.EffectiveRange))
		{
			return ability.GetJob(relocatePosition, relocatePosition);
		}
		return null;
	}

	private bool TryFindRelocatePosition(Ability jump, Pawn pawn, out IntVec3 relocatePosition, float maxDistance)
	{
		tmpHostileSpots.Clear();
		tmpHostileSpots.AddRange(from a in pawn.Map.attackTargetsCache.GetPotentialTargetsFor(pawn)
			where !a.ThreatDisabled(pawn)
			select a.Thing);
		relocatePosition = CellFinderLoose.GetFallbackDest(pawn, tmpHostileSpots, maxDistance, 5f, 5f, 20, (IntVec3 c) => jump.verb.ValidateTarget(c, showMessages: false));
		tmpHostileSpots.Clear();
		return relocatePosition.IsValid;
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		JobGiver_AIJumpApparelEscapeEnemies obj = (JobGiver_AIJumpApparelEscapeEnemies)base.DeepCopy(resolve);
		obj.ability = ability;
		return obj;
	}
}
}