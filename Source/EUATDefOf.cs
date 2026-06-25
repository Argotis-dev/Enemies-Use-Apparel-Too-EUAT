using RimWorld;
using Verse;

namespace EnemiesUseApparelToo
{
[DefOf]
public static class EUATDefOf
{
	public static AbilityDef EUAT_DeployTurret;

	public static AbilityDef EUAT_JetJump;

	public static AbilityDef EUAT_LaunchIncendiaryPheonixArmor;

	public static AbilityDef EUAT_LaunchFragGrenadeApparel;

	public static AbilityDef EUAT_DeployHunterDrone;
	
	public static ThingDef Gun_TacticalTurret;

	static EUATDefOf()
	{
		DefOfHelper.EnsureInitializedInCtor(typeof(EUATDefOf));
	}
}
}
