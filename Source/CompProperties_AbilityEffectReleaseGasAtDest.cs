using RimWorld;
using Verse;


namespace EnemiesUseApparelToo
{
    
public class CompProperties_AbilityEffectMarkDeadlifeGasAtDest : CompProperties_AbilityEffectWithDuration
{
	public GasType gasType;
	public int cellsToFill;
	public CompProperties_AbilityEffectMarkDeadlifeGasAtDest()
	{
		compClass = typeof(CompAbilityEffect_MarkDeadlifeGasAtDest);
	}
}

}
