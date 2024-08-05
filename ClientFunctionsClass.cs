using EFT.Interactive;

namespace Solarint.FikaCompatibility
{
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
}