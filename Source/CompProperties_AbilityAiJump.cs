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

namespace EnemiesUseApparelToo
{
    public class CompProperties_AbilityAiJump : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityAiJump()
        {
            compClass = typeof(CompAbility_AiJump);
        }
    }
}
