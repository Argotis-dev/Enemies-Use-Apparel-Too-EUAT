using Verse;
using EnemiesUseApparelToo.Utility;
using Verse.AI;

namespace EnemiesUseApparelToo
{

public class ThinkNode_ConditionalPawnHasJump : ThinkNode_Conditional
{
	protected override bool Satisfied(Pawn pawn)
	{
		return EnemiesUseApparelTooUtility.PawnHasJumpAbility(pawn, out var ability);
		
	}

	public override ThinkNode DeepCopy(bool resolve = true)
	{
		ThinkNode_ConditionalPawnHasJump obj = (ThinkNode_ConditionalPawnHasJump)base.DeepCopy(resolve);
		return obj;
	}
}

}