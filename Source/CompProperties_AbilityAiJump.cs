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

        public float minDistToTarget = 8f;
        public float thresholdPercent = 0.50f;
        public CompProperties_AbilityAiJump()
        {
            compClass = typeof(CompAbility_AiJump);
        }
    }
}
