using BepInEx;
using EFT;
using EFT.Interactive;
using SAIN.Components;
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

    internal class FikaCompatComponent : MonoBehaviour
    {
        private static FikaCompatComponent _instance;
        private GameWorldComponent _sainGameWorld;
        private GameWorld _eftGameWorld;
        private bool? _isHost = null;

        static FikaCompatComponent()
        {
            GameWorld.OnDispose += Dispose;
        }

        private void Start()
        {
            _instance = this;
            checkIsHost();

            _sainGameWorld = GetComponent<GameWorldComponent>();
            _eftGameWorld = GetComponent<GameWorld>();

            if (_sainGameWorld == null) {
                Console.WriteLine($"SAIN GameWorld Null");
                Console.WriteLine($"SAIN GameWorld Null");
                Console.WriteLine($"SAIN GameWorld Null");
                Console.WriteLine($"SAIN GameWorld Null");
            }
            else {
                Console.WriteLine("Got SAIN Gameworld");
                Console.WriteLine("Got SAIN Gameworld");
                Console.WriteLine("Got SAIN Gameworld");
                Console.WriteLine("Got SAIN Gameworld");
            }
        }

        private void checkIsHost()
        {
            // find out if the player is hosting
            if (true) {
                _isHost = true;
            }
            else if (false) {
                _isHost = false;
            }
        }

        private void doorsDisabled(bool value)
        {
        }

        private void doorStateChanged(Door door, EDoorState state, bool invertedOpenAngle)
        {
        }

        private void Update()
        {
            checkSubscribe();
            handlePackets();
        }

        private void checkSubscribe()
        {
            if (_isHost == true &&
                !_subscribed &&
                _sainGameWorld != null) {
                var doors = _sainGameWorld.Doors;
                if (doors != null) {
                    doors.OnDoorsDisabled += doorsDisabled;
                    doors.OnDoorStateChanged += doorStateChanged;
                    _subscribed = true;
                }
            }
        }

        private bool _subscribed;

        private void OnDestroy()
        {
            if (_sainGameWorld != null) {
                var doors = _sainGameWorld.Doors;
                if (doors == null) {
                    return;
                }
                doors.OnDoorsDisabled -= doorsDisabled;
                doors.OnDoorStateChanged -= doorStateChanged;
            }
        }

        private static void Dispose()
        {
            if (_instance != null) Destroy(_instance);
        }

        private void handlePackets()
        {
            if (_isHost == true) {
            }
            else if (_isHost == false) {
            }
            else {
                // isHost is null, haven't found out if we are host or not
            }
        }

        private void checkHostDisabledDoors()
        {
            // receive packet from host, disable all doors if host enables config option
        }

        private void clientDoorStateChange(int doorId, EDoorState state, bool invertedOpenAngle)
        {
            // receive packet from host, find door, and change state to match
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