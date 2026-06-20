using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using EnemiesUseApparelToo.Utility;
using Verse.AI;

namespace EnemiesUseApparelToo
{

public class ThinkNode_ConditionalApparelHasAbility : ThinkNode_Conditional
{
	public AbilityDef ability;

	protected override bool Satisfied(Pawn pawn)
	{
		return EnemiesUseApparelTooUtility.PawnHasApparelwithAbility(ability, pawn, out Ability abilitysource) == true;
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		ThinkNode_ConditionalApparelHasAbility obj = (ThinkNode_ConditionalApparelHasAbility)base.DeepCopy(resolve);
		obj.ability = ability;
		return obj;
	}
}

}