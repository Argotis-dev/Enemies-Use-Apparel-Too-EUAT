using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using EnemiesUseApparelToo.Utility;

namespace EnemiesUseApparelToo
{
    public class Verb_CastAbilityJumpApparel : Verb_CastAbilityJump
    {
        private float cachedEffectiveRange = -1f;

        private Apparel JumpApparelSource => EnemiesUseApparelTooUtility.GetAbilityApparelSource(ability, out Apparel apparelwithability);

        public override float EffectiveRange
        {
            get
            {
                if (cachedEffectiveRange < 0f)
                {
                    if (JumpApparelSource != null)
                    {
                        cachedEffectiveRange = JumpApparelSource.GetStatValue(StatDefOf.JumpRange);
                    }
                    else
                    {
                        cachedEffectiveRange = base.EffectiveRange;
                    }
                }
                return cachedEffectiveRange;
            }
        }

        protected override bool TryCastShot()
        {
            if (ability.Activate(currentTarget, currentDestination))
            {
                return JumpUtility.DoJump(CasterPawn, currentTarget, null, verbProps, ability, base.currentTarget, JumpFlyerDef);
            }
            return false;
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (caster == null)
            {
                return false;
            }
            if (!CanHitTarget(target) || !JumpUtility.ValidJumpTarget(CasterPawn, caster.Map, target.Cell))
            {
                return false;
            }
            if (!ReloadableUtility.CanUseConsideringQueuedJobs(CasterPawn, JumpApparelSource))
            {
                return false;
            }
            return true;
        }

    }
}