using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using System.Linq;

namespace EnemiesUseApparelToo
{

    public class EnemiesUseApparelTooModSettings : ModSettings
	{
        private static EnemiesUseApparelTooModSettings _instance;
		public bool EUAT_UseHarmonyPatch = true;
        public static bool UseHarmonyPatch => _instance.EUAT_UseHarmonyPatch;

        public EnemiesUseApparelTooModSettings() {
            _instance = this;
        }

        public override void ExposeData()
		{
			Scribe_Values.Look(ref EUAT_UseHarmonyPatch, "EUAT_UseHarmonyPatch", defaultValue: true);
            base.ExposeData();
		}
	}
	public class EnemiesUseApparelTooSettings : Mod
	{
		private EnemiesUseApparelTooModSettings settings;

		public EnemiesUseApparelTooSettings(ModContentPack content)
			: base(content)
		{
			settings = GetSettings<EnemiesUseApparelTooModSettings>();
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("Changes only apply after you restart the game.");
			listingStandard.Gap();
            listingStandard.CheckboxLabeled("Harmony Apparel Verb Targeting:", ref settings.EUAT_UseHarmonyPatch, "Enables a harmony patch that considers all violent apparel verbs for use. This may have unintended consequence with other mods, so use at your own risk. This will work for all vanilla items.");
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);

		}

		public override string SettingsCategory() => "Enemies Use Apparel Too";
	}
}