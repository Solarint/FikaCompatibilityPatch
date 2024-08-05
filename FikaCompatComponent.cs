using EFT;
using SAIN.Components;
using System;
using UnityEngine;

namespace Solarint.FikaCompatibility
{
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

        private void Update()
        {
            handlePackets();
            ClientFunctions.Update();
            HostFunctions.Update();
        }

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
    }
}