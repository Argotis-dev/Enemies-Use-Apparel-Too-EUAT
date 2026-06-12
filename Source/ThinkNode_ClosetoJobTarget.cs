using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using AiUseableApparel.Utility;
using Verse.AI;

namespace AiUseableApparel
{
	public class ThinkNode_ClosetoJobTarget : ThinkNode_Conditional
	{
		private float maxDistToJobTarget = 10f;

		public override ThinkNode DeepCopy(bool resolve = true)
		{
			ThinkNode_ClosetoJobTarget obj = (ThinkNode_ClosetoJobTarget)base.DeepCopy(resolve);
			obj.maxDistToJobTarget = maxDistToJobTarget;
			return obj;
		}

		protected override bool Satisfied(Pawn pawn)
		{
			if (pawn.mindState.enemyTarget != null)
			{
				return pawn.Position.InHorDistOf(pawn.mindState.enemyTarget.Position, maxDistToJobTarget);
			}
			return false;
		}
	}
}