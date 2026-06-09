using HarmonyLib;
using LudeonTK;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AiUseableApparel
{
    public class CompProperties_AbilityRequiresApparelCharges : CompProperties_AbilityEffect
    {

        public ThingDef ApparelDef;

        public CompProperties_AbilityRequiresApparelCharges()
        {
            compClass = typeof(CompAbility_RequiresApparelCharges);
        }
    }
}
