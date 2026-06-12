using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using AiUseableApparel.Utility;
using Verse.AI;

namespace AiUseableApparel
{
	public class ThinkNode_PawnisMelee : ThinkNode_Conditional
	{
		protected override bool Satisfied(Pawn pawn)
		{
			return pawn.equipment?.Primary?.def.IsMeleeWeapon == true;
			//return true;
		}
	}
}