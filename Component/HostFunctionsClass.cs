using EFT.Interactive;

namespace Solarint.FikaCompatibility
{
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
}