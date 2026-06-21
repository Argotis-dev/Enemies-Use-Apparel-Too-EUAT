using System.Collections.Generic;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.Sound;

using RimWorld;

namespace EnemiesUseApparelToo
{
    public class Command_AbilityGroup : Command_Ability
    {
        public bool drawRadius = true;
        public Command_AbilityGroup(Ability ability, Pawn pawn)
        : base(ability, pawn)
        {
           
        }

        public override void ProcessInput(Event ev)
        {
           
            if (CurActivateSound != null)
                {
                    CurActivateSound.PlayOneShotOnCamera();
                }
            SoundDefOf.Tick_Tiny.PlayOneShotOnCamera();
		    Targeter targeter = Find.Targeter; 

            if (ability.verb.CasterIsPawn && Find.Targeter.targetingSource != null && Find.Targeter.targetingSource.GetVerb.verbProps == ability.verb.verbProps)
            {
                Pawn casterPawn = ability.pawn;
                if (!Find.Targeter.IsPawnTargeting(casterPawn))
                {
                    Find.Targeter.targetingSourceAdditionalPawns.Add(casterPawn);
                }
            }
            
        }
/*
	public override void ProcessInput(Event ev)
	{
		base.ProcessInput(ev);
		SoundDefOf.Tick_Tiny.PlayOneShotOnCamera();
		if (ability.def.targetRequired)
		{
			Find.DesignatorManager.Deselect();
			if (!ability.def.targetWorldCell)
			{
				if (groupedCasts.NullOrEmpty())
				{
					Find.Targeter.BeginTargeting(ability.verb);
				}
				else
				{
					Find.Targeter.BeginTargeting(ability.verb, null, allowNonSelectedTargetingSource: false, GetBetterTargetingSource);
				}
				return;
			}
			CameraJumper.TryJump(CameraJumper.GetWorldTarget(ability.pawn));
			Find.WorldTargeter.BeginTargeting(delegate(GlobalTargetInfo t)
			{
				if (ability.ValidateGlobalTarget(t))
				{
					ability.QueueCastingJob(t);
					return true;
				}
				return false;
			}, canTargetTiles: true, ability.def.uiIcon, !ability.pawn.IsCaravanMember(), null, ability.WorldMapExtraLabel, ability.ValidateGlobalTarget);
		}
		else
		{
			ability.QueueCastingJob(ability.pawn, LocalTargetInfo.Invalid);
		}
	}*/

            
    }
}