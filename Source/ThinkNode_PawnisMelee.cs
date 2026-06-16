using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using EnemiesUseApparelToo.Utility;
using Verse.AI;

namespace EnemiesUseApparelToo
{
	public class ThinkNode_PawnisMelee : ThinkNode_Conditional
	{
		protected override bool Satisfied(Pawn pawn)
		{
			return pawn.equipment?.Primary?.def.IsMeleeWeapon == true;
		}
	}
}