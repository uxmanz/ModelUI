using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace ModelUI;

internal static class ActiproHarmonyPatcher
{
    internal static void Patch()
    {
        var asm = typeof(ActiproSoftware.Properties.Shared.Internal.LicenseStatusView).Assembly;
        var licType = asm.GetType("ActiproSoftware.Licensing.ActiproLicense");

        var h = new Harmony("your.mod.guid");

        // 1. Patch Kind getter -> always return Full
        h.Patch(AccessTools.Property(licType, "Kind").GetGetMethod(nonPublic: true),
            transpiler: new HarmonyMethod(typeof(ActiproHarmonyPatcher), nameof(ReturnFull)));

        // 2. Patch Error getter -> always return None
        h.Patch(AccessTools.Property(licType, "Error").GetGetMethod(nonPublic: true),
            transpiler: new HarmonyMethod(typeof(ActiproHarmonyPatcher), nameof(ReturnNone)));

        // 3. Patch IsValid getter -> return true + set fields
        h.Patch(AccessTools.Property(licType, "IsValid").GetGetMethod(nonPublic: true),
            prefix: new HarmonyMethod(typeof(ActiproHarmonyPatcher), nameof(IsValidPrefix)));
        Debug.WriteLine("Prefix called");
    }

    /* ---------- helpers ---------- */

    // replaces the whole getter body with: ldc.i4.2 (Full)  ret
    private static IEnumerable<CodeInstruction> ReturnFull(IEnumerable<CodeInstruction> _, ILGenerator __)
    {
        yield return new CodeInstruction(OpCodes.Ldc_I4_2); // ActiproLicenseKind.Full == 2
        yield return new CodeInstruction(OpCodes.Ret);
    }

    // replaces the whole getter body with: ldc.i4.0 (None)  ret
    private static IEnumerable<CodeInstruction> ReturnNone(IEnumerable<CodeInstruction> _, ILGenerator __)
    {
        yield return new CodeInstruction(OpCodes.Ldc_I4_0); // ActiproLicenseError.None == 0
        yield return new CodeInstruction(OpCodes.Ret);
    }

    // prefix for IsValid: set fields and return true
    private static bool IsValidPrefix(object __instance, ref bool __result)
    {
        var t = __instance.GetType();

        // HU  (ActiproLicenseKind) -> Full
        var fKind = AccessTools.Field(t, "HU");
        fKind?.SetValue(__instance, (byte)2); // Full == 2

        // Qv  (ActiproLicenseError) -> None
        var fError = AccessTools.Field(t, "Qv");
        fError?.SetValue(__instance, (byte)0); // None == 0

        __result = true;
        return false; // skip original getter
    }
}