using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModSettings;

namespace FirePack
{
    internal class FirePackSettings : JsonModSettings
    {
        [Name("Pull Torches From Fire")]
        [Description("Default = Yes")]
        public bool pullTorches = true;

        [Name("Consume Torch On Firestart")]
        [Description("When starting a fire with a lit torch, it will be consumed in the process.")]
        public bool consumeTorchOnFirestart = false;

        [Name("No Wood Matches Anywhere")]
        [Description("The player starts with a set of pack matches and cannot obtain any wood matches. After the pack matches are consumed, the player must use renewable firestarting tools. Warning: turning this on will delete your wood matches.")]
        public bool noWoodMatches = false;
    }
    internal static class Settings
    {
        internal static FirePackSettings options;
        internal static void OnLoad()
        {
            options = new FirePackSettings();
            options.AddToModSettings("Fire Pack", MenuType.Both);
        }
    }
}
