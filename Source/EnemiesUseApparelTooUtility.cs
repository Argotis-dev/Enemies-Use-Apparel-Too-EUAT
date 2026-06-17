using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;


using RimWorld;

namespace EnemiesUseApparelToo.Utility
{
    
    public static class EnemiesUseApparelTooUtility
    {
            public static Apparel GetAbilityApparelSource(Ability ability, out Apparel apparelwithability)
            {
                apparelwithability = null;
                if (ability.pawn.apparel != null)
                {   
                    foreach (Apparel apparelitem in ability.pawn.apparel.WornApparel)
                    {
                        foreach (Ability abilityitem in apparelitem.AllAbilitiesForReading)
                        {
                            if (ability == abilityitem)
                            {
                                apparelwithability = apparelitem;
                            }
                        }
                    }
                }
                return apparelwithability;
            }

            public static bool PawnHasApparelwithAbility(AbilityDef abilitydef, Pawn pawn, out Ability ability)
            {
                ability = null;
                if (pawn.apparel.WornApparel != null)
                {   
                    foreach (Apparel apparelitem in pawn.apparel.WornApparel)
                    {
                        if (apparelitem.AllAbilitiesForReading != null)
                        {
                            foreach (Ability abilityitem in apparelitem.AllAbilitiesForReading)
                            {
                                if (abilityitem.def == abilitydef)
                                {
                                    ability = abilityitem;
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            public static bool isHealthy(Pawn pawn, float thresholdPercent)
            {
                HediffSet hediffSet = pawn.health.hediffSet;
                float num = 0f;
                for (int i = 0; i < hediffSet.hediffs.Count; i++)
                {
                    if (hediffSet.hediffs[i] is Hediff_Injury)
                    {
                        num += hediffSet.hediffs[i].Severity;
                    }
                }
                return num / pawn.health.LethalDamageThreshold > thresholdPercent;
            }

    }
}