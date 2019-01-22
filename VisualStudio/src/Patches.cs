using Harmony;
using System.Collections.Generic;

using System.Reflection;
using System.Reflection.Emit;

namespace FirePack
{
    [HarmonyPatch(typeof(Panel_ActionPicker), "ShowFeedFireActionPicker")]
    internal class Panel_ActionPicker_ShowFeedFireActionPicker
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);

            FieldInfo fieldInfo = typeof(Panel_ActionPicker).GetField("m_ActionPickerItemDataList", BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo methodInfoEnableWithCurrentList = typeof(Panel_ActionPicker).GetMethod("EnableWithCurrentList", BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo methodInfoAddFeedFireActions = typeof(TakeEmbers).GetMethod("AddFeedFireActions", BindingFlags.Static | BindingFlags.NonPublic);

            if (fieldInfo == null || methodInfoAddFeedFireActions == null || methodInfoEnableWithCurrentList == null)
            {
                Implementation.Log("Could not patch Panel_ActionPicker.ShowFeedFireActionPicker. FieldInfo or MethodInfos are NULL.");
                return instructions;
            }

            for (int i = 0; i < codes.Count; i++)
            {
                if (methodInfoEnableWithCurrentList == codes[i].operand)
                {
                    codes.Insert(i, new CodeInstruction(OpCodes.Ldarg_0));
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldfld, fieldInfo));
                    codes.Insert(i + 2, new CodeInstruction(OpCodes.Call, methodInfoAddFeedFireActions));
                    break;
                }
            }

            return codes;
        }
    }
}