using EFT.Interactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solarint.FikaCompatibility
{
    internal class ClientDoorSync : FikaCompatBase
    {
        private bool _hostDisabledDoors;
        private bool _doorsDisabled;
        private float _nextGetDoorsTime;
        private Door[] _allDoors;
        private readonly Dictionary<string, Door> _doorDictionary = new Dictionary<string, Door>();

        internal ClientDoorSync(FikaCompatComponent component) : base(component)
        {
        }

        public void Update()
        {
            if (_nextGetDoorsTime < Time.time ||
                _allDoors == null ||
                _allDoors.Length == 0) {
                getAllDoors();
            }
        }

        public void HandlePackets()
        {
            _hostDisabledDoors = false;
        }

        public void Dispose()
        {
        }

        private void checkHostDisabledDoors()
        {
            // receive packet from host, disable all doors if host enables config option

            // update value
            if (_doorsDisabled != _hostDisabledDoors) {
                _doorsDisabled = _hostDisabledDoors;
                SAINGameworld.Doors?.HostDisabledDoors(_doorsDisabled);
            }
        }

        private void clientDoorStateChange(string doorId, EDoorState state, bool invertedOpenAngle)
        {
            // receive packet from host, find door, and change state to match
            if (!_doorDictionary.TryGetValue(doorId, out Door door)) {
                Console.Error.WriteLine($"Cant find Door of ID {doorId}!");
                return;
            }
            var doors = SAINGameworld.Doors;
            if (doors != null) {
                doors.ChangeDoorState(door, state, invertedOpenAngle);
                Console.WriteLine($"Changed Door State! [{doorId} : {state} : {invertedOpenAngle}]");
            }
        }

        private void getAllDoors()
        {
            _nextGetDoorsTime = Time.time + 30f;
            _allDoors = GameObject.FindObjectsOfType<Door>();
            Console.WriteLine($"Found {_allDoors.Length} Doors");
            _doorDictionary.Clear();
            foreach (Door door in _allDoors) {
                _doorDictionary.Add(door.Id, door);
            }
        }
    }
}