using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

using RimWorld;

namespace AiUseableApparel.Utility
{
    public static class AiUseableApparelUtility
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

    }
}