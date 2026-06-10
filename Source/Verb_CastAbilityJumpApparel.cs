using RimWorld.Utility;
using UnityEngine;
using Verse;
using RimWorld;
using AiUseableApparel.Utility;

namespace AiUseableApparel
{
    public class Verb_CastAbilityJumpApparel : Verb_CastAbilityJump
    {
        private float cachedEffectiveRange = -1f;

        private Apparel JumpApparelSource => AiUseableApparelUtility.GetAbilityApparelSource(ability, out Apparel apparelwithability);

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

    }
}