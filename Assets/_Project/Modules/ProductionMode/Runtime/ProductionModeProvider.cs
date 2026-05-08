using JetBrains.Annotations;
using Zenject;

namespace GameKit.ProductionMode
{
    [UsedImplicitly]
    public class ProductionModeProvider
    {
        public bool IsProduction { get; private set; }

        [Inject]
        private void Inject()
        {
#if IS_PRODUCTION
            IsProduction = true;
#else
            IsProduction = false;
#endif
        }
    }
}
