using RimWorld;
using System.Collections.Generic;
using Verse;
using EnemiesUseApparelToo.Utility;
using UnityEngine;

namespace EnemiesUseApparelToo
{
    
public class CompAbilityEffect_MarkDeadlifeGasAtDest : CompAbilityEffect_WithDuration
{
	public new CompProperties_AbilityEffectMarkDeadlifeGasAtDest Props => (CompProperties_AbilityEffectMarkDeadlifeGasAtDest)props;

	private int TotalGas => Mathf.CeilToInt(Props.cellsToFill * 255);

	public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
	{
		Pawn pawn = parent.pawn;
		if (Props.gasType == GasType.DeadlifeDust)
		{
			GasUtility.MarkDeadlifeCorpsesForFaction(target.Cell, pawn.Map, pawn.Faction, TotalGas);
			MarkDeadlifePawnsForFaction(target.Cell, pawn.Map, pawn.Faction, TotalGas);
		}
		base.Apply(target, dest);
	}

	private void MarkDeadlifePawnsForFaction(IntVec3 cell, Map map, Faction faction, int amount)
	{
		map.gasGrid.EstimateGasDiffusion(cell, GasType.DeadlifeDust, amount, delegate(IntVec3 c)
		{
			foreach (Thing thing in c.GetThingList(map))
			{
				if (thing is Pawn pawn)
				{
					pawn.MarkDeadlifeDustForFaction(faction);
				}
			}
		});
	}
}
}


