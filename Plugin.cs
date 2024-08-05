using BepInEx;
using EFT;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;

namespace Solarint.FikaCompatibility
{
    [BepInDependency("me.sol.sain")]
    [BepInDependency("com.fika.core")]
    [BepInPlugin("solarint.fikaCompat", "FikaCompatibility", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new AddComponentPatch().Enable();
        }
    }

    internal class AddComponentPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() => typeof(GameWorldUnityTickListener).GetMethod("Create");

        [PatchPostfix]
        public static void Patch(GameObject gameObject)
        {
            try {
                gameObject.AddComponent<FikaCompatComponent>();
            }
            catch (Exception ex) {
                Logger.LogError($"Init Fika Compat Error: {ex}");
            }
        }
    }
}