using Verse;

using RimWorld;

namespace AiUseableApparel
{
    public class Command_AbilityApparel : Command_Ability
    {
        private readonly CompAbility_ChargesApparel comp;

        public override string TopRightLabel => comp.LabelRemaining;
        public Command_AbilityApparel(Ability ability, Pawn pawn)
        : base(ability, pawn)
        {
            comp = ability.CompOfType<CompAbility_ChargesApparel>();
        }

        
    }
}