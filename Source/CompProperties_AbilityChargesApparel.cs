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
    public class CompProperties_AbilityChargesApparel : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityChargesApparel()
        {
            compClass = typeof(CompAbility_ChargesApparel);
        }
    }
}
