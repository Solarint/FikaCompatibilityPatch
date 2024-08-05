using EFT;
using SAIN.Components;

namespace Solarint.FikaCompatibility
{
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
}