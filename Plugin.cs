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
        internal GameWorldComponent SAINGameworld;
        internal GameWorld EFTGameworld;
        internal bool? IsHost = null;

        internal ClientFunctionsClass ClientFunctions;
        internal HostFunctionsClass HostFunctions;

        static FikaCompatComponent()
        {
            GameWorld.OnDispose += Dispose;
        }

        private void Start()
        {
            _instance = this;
            checkIsHost();

            SAINGameworld = GetComponent<GameWorldComponent>();
            EFTGameworld = GetComponent<GameWorld>();

            if (SAINGameworld == null) {
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

            ClientFunctions = new ClientFunctionsClass(this);
            HostFunctions = new HostFunctionsClass(this);
        }

        private void checkIsHost()
        {
            // find out if the player is hosting
            if (true) {
                IsHost = true;
            }
            else if (false) {
                IsHost = false;
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
            if (IsHost == true &&
                !_subscribed &&
                SAINGameworld != null) {
                var doors = SAINGameworld.Doors;
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
            ClientFunctions?.Dispose();
            HostFunctions?.Dispose();
        }

        private static void Dispose()
        {
            if (_instance != null) Destroy(_instance);
        }

        private void handlePackets()
        {
            if (IsHost == true) {
                HostFunctions.HandlePackets();
            }
            else if (IsHost == false) {
                ClientFunctions.HandlePackets();
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

    internal class HostFunctionsClass : FikaCompatBase
    {
        private bool _subscribed;

        internal HostFunctionsClass(FikaCompatComponent component) : base(component)
        {
        }

        public void Update()
        {
            checkSubscribe();
        }

        public void Dispose()
        {
            if (SAINGameworld != null) {
                var doors = SAINGameworld.Doors;
                if (doors == null) {
                    return;
                }
                doors.OnDoorsDisabled -= doorsDisabled;
                doors.OnDoorStateChanged -= doorStateChanged;
            }
        }

        private void checkSubscribe()
        {
            if (IsHost == true &&
                !_subscribed &&
                SAINGameworld != null) {
                var doors = SAINGameworld.Doors;
                if (doors != null) {
                    doors.OnDoorsDisabled += doorsDisabled;
                    doors.OnDoorStateChanged += doorStateChanged;
                    _subscribed = true;
                }
            }
        }

        public void HandlePackets()
        {
        }

        private void doorsDisabled(bool value)
        {
        }

        private void doorStateChanged(Door door, EDoorState state, bool invertedOpenAngle)
        {
        }
    }

    internal class ClientFunctionsClass : FikaCompatBase
    {
        internal ClientFunctionsClass(FikaCompatComponent component) : base(component)
        {
        }

        public void Update()
        {
        }

        public void Dispose()
        {
        }

        public void HandlePackets()
        {
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

    internal abstract class FikaCompatBase
    {
        protected bool? IsHost => CompatComponent.IsHost;
        protected FikaCompatComponent CompatComponent { get; }
        protected GameWorldComponent SAINGameworld { get; }
        protected GameWorld EFTGameworld { get; }

        internal FikaCompatBase(FikaCompatComponent component)
        {
            CompatComponent = component;
            SAINGameworld = component.SAINGameworld;
            EFTGameworld = component.EFTGameworld;
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