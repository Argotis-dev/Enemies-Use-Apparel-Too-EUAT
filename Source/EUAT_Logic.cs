using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;
using Verse.Noise;
using Verse.Grammar;
using RimWorld;
using RimWorld.Planet;

// *Uncomment for Harmony*
 using System.Reflection;
 using HarmonyLib;

namespace EnemiesUseApparelToo
{

    [StaticConstructorOnStartup]
    public static class Start
    {
        static Start()
        {
            Log.Message("Mod template loaded successfully!");

            // *Uncomment for Harmony*
            new Harmony("EnemiesUseApparelToo").PatchAll();
        }
    }

/*
    // *Uncomment for Harmony*
    [HarmonyPatch(typeof(Pawn), "TryGetAttackVerb")]
    public static class PatchTryGetAttackVerb
    {
        public static bool Prefix(ref Verb __result, Pawn __instance, Thing target, bool allowManualCastWeapons, bool allowTurrets)
        {
            if (allowManualCastWeapons && __instance.apparel != null)
            {
                IEnumerable<Verb> list = __instance.apparel.AllApparelVerbs;
                foreach(Verb verb in list)
                {
                    //Verb firstApparelVerb = __instance.apparel.FirstApparelVerb;
                    if (verb != null && verb.Available() && verb.CanHitTarget(target))
                    {
                        __result = verb;
                        return false;
                    }
                }
            }
            return true;
        }
    }*/

        // *Uncomment for Harmony*
    [HarmonyPatch(typeof(JobGiver_AIFightEnemy), "GetAbilityJob")]
    public static class PatchGetAbilityJob
    {
        public static bool Prefix(ref Job __result, JobGiver_AIFightEnemy __instance, Pawn pawn, Thing enemyTarget)
        {
            if (pawn.apparel != null)
            {
                IEnumerable<Verb> list = pawn.apparel.AllApparelVerbs;
                foreach(Verb verb in list)
                {
                    if (verb != null && verb.Available() && verb.verbProps.violent == true)
                    {
                        if (verb.CanHitTarget(enemyTarget))
                        {

                                Job job = JobMaker.MakeJob(JobDefOf.UseVerbOnThing, enemyTarget);
                                job.verbToUse = verb;
                                job.maxNumStaticAttacks = 1;
                                job.expiryInterval = 2000;
                                job.endIfCantShootTargetFromCurPos = true;
                                job.endIfCantShootInMelee = true;
                                __result = job;
                                return false;
                        }
                        
                    }
                }
                
            }
            return true;
        }
    }

}