namespace Solarint.FikaCompatibility
{
    internal class ClientFunctionsClass : FikaCompatBase
    {
        private ClientDoorSync _doorSync { get; }

        internal ClientFunctionsClass(FikaCompatComponent component) : base(component)
        {
            _doorSync = new ClientDoorSync(component);
        }

        public void Update()
        {
            if (IsHost != false) {
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