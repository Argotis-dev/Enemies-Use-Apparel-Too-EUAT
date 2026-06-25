using System.Xml;
using Verse;

namespace EnemiesUseApparelToo
{
    public class EUAT_PatchIfHarmonyEnabled : PatchOperationSequence
    {
        public bool invertHarmony = false;
        public bool invertAbilities = false;
        protected override bool ApplyWorker(XmlDocument xml) {
            
            if (EnemiesUseApparelTooModSettings.UseHarmonyPatch != invertHarmony || EnemiesUseApparelTooModSettings.KeepModAbilities != invertAbilities)
            {
                return base.ApplyWorker(xml);
            }
            return true;
        }
    }
}