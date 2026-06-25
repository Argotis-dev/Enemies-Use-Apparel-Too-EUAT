using System.Xml;
using Verse;

namespace EnemiesUseApparelToo
{
    public class EUAT_PatchIfHarmonyEnabled : PatchOperationSequence
    {
        public bool invert = false;
        protected override bool ApplyWorker(XmlDocument xml) {
            
            if (EnemiesUseApparelTooModSettings.UseHarmonyPatch != invert)
            {
                return base.ApplyWorker(xml);
            }
            return true;
        }
    }
}