using HarmonyLib;
using LudeonTK;
using RimWorld;
using RimWorld.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using Verse.AI;
using EnemiesUseApparelToo.Utility;

namespace EnemiesUseApparelToo
{
    
    public class CompAbility_AiJump : CompAbilityEffect
    {
        public new CompProperties_AbilityAiJump Props => (CompProperties_AbilityAiJump)props;

        public float minDistToTarget = 8f;
    
        private static List<Thing> tmpHostileSpots = new List<Thing>();

        public float thresholdPercent = 0.50f;


	
        private Apparel JumpItem => EnemiesUseApparelTooUtility.GetAbilityApparelSource(parent, out Apparel apparelwithability);

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            float effectiveJumpRange = parent.verb.EffectiveRange;
            if(!parent.pawn.IsPlayerControlled)
            {
           
                if (parent.pawn.equipment?.Primary?.def.IsMeleeWeapon == true && EnemiesUseApparelTooUtility.isHealthy(parent.pawn, thresholdPercent ))
                {
                    var destination = RCellFinder.BestOrderedGotoDestNear(target.Pawn.Position, parent.pawn, (c) => JumpUtility.ValidJumpTarget(parent.pawn, parent.pawn.Map, c) && JumpUtility.CanHitTargetFrom(parent.pawn, parent.pawn.Position, c, effectiveJumpRange));
                    if (boolAiValidJumpRange(parent.pawn, destination, effectiveJumpRange))
                    {
                        dest = destination;
                        target = destination;
                    }
                }
                else if( !EnemiesUseApparelTooUtility.isHealthy(parent.pawn, thresholdPercent ))
                {
                    if(TryFindRelocatePosition(parent, parent.pawn, out var destination2 , effectiveJumpRange ))
                    {
                        dest = destination2;
                        target = destination2;
                    }
                   
                }
                else if(boolTryFindShootingPosition(parent.pawn, out var destination3))
                {
                    if(boolAiValidJumpRange(parent.pawn, destination3, effectiveJumpRange))
                    {
                        dest = destination3;
                        target = destination3;
                    }
                    
                }
            }

            base.Apply(target, dest);
        }
        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            Ability ability = null;
            if (EnemiesUseApparelTooUtility.PawnHasApparelwithAbility(parent.def, parent.pawn, out ability) != true)
            {
                return false;;
            }
            if (!ability.CanCast)
            {
                return false;;
            }
            if (parent.pawn.mindState?.enemyTarget == null)
            {
                return false;;
            }
            return true;
        }

        private bool TryFindRelocatePosition(Ability jump, Pawn pawn, out IntVec3 relocatePosition, float maxDistance)
        {
            tmpHostileSpots.Clear();
            tmpHostileSpots.AddRange(from a in pawn.Map.attackTargetsCache.GetPotentialTargetsFor(pawn)
                where !a.ThreatDisabled(pawn)
                select a.Thing);
            relocatePosition = CellFinderLoose.GetFallbackDest(pawn, tmpHostileSpots, maxDistance, 5f, 5f, 20, (IntVec3 c) => jump.verb.ValidateTarget(c, showMessages: false));
            tmpHostileSpots.Clear();
            return relocatePosition.IsValid;
        }

        private bool boolTryFindShootingPosition(Pawn pawn, out IntVec3 dest, Verb verbToUse = null)
        {
            Verb verb = verbToUse ?? pawn.TryGetAttackVerb(null, !pawn.IsColonist);
            if (verb == null)
            {
                dest = IntVec3.Invalid;
                return false;
            }
            return CastPositionFinder.TryFindCastPosition(new CastPositionRequest
            {
                caster = pawn,
                target = pawn.mindState.enemyTarget,
                verb = verb,
                maxRangeFromTarget = verb.EffectiveRange,
                wantCoverFromTarget = (verb.EffectiveRange > 5f)
            }, out dest);
        }

        private bool boolAiValidJumpRange(Pawn pawn, IntVec3 dest, float effectiverange)
        {
            if (dest == null)
            {
                return false;
            }   
            float num = parent.pawn.Position.DistanceTo(dest);
            if (num < minDistToTarget || num > effectiverange || !GenSight.LineOfSight(parent.pawn.Position, dest, parent.pawn.Map))
            {
                return false;
            }
            return true;
        }

    }
}
