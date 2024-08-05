namespace Solarint.FikaCompatibility
{
    internal class HostFunctionsClass : FikaCompatBase
    {
        private HostDoorSync _doorSync { get; }

        internal HostFunctionsClass(FikaCompatComponent component) : base(component)
        {
            _doorSync = new HostDoorSync(component);
        }

        public void Update()
        {
            if (IsHost != true) {
                return;
            }
            _doorSync.Update();
        }

        public void Dispose()
        {
            _doorSync.Dispose();
        }

        public void HandlePackets()
        {
            _doorSync.HandlePackets();
        }
    }
}