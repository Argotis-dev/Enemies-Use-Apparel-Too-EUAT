
using System.Collections.Generic;

using Verse;
using Verse.AI;

using RimWorld;

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
            if(EnemiesUseApparelTooModSettings.UseHarmonyPatch)
            {
                new Harmony("EnemiesUseApparelToo").PatchAll(); 
            }

        }
    }

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