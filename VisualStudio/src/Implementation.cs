using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using MelonLoader;

namespace FirePack
{
    internal class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            Settings.OnLoad();
        }

        internal static void Log(string message)
        {
            MelonLoader.MelonLogger.Log(message);
        }
    }
}
