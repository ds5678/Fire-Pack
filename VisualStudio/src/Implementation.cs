using MelonLoader;
using UnityEngine;

namespace FirePack
{
	internal class Implementation : MelonMod
	{
		public override void OnApplicationStart()
		{
			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
			Settings.instance.AddToModSettings("Fire Pack");
		}
	}
}
