using GameKit.ProductionMode.Contracts;
using JetBrains.Annotations;

namespace GameKit.ProductionMode
{
    [UsedImplicitly]
    public class ProductionModeProvider : IProductionModeProvider
    {
        public bool IsProduction { get; private set; }

        public ProductionModeProvider()
        {
#if IS_PRODUCTION
            IsProduction = true;
#else
            IsProduction = false;
#endif
        }
    }
}
