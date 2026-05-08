using System;
using GameKit.Core;
using JetBrains.Annotations;
using NUnit.Framework;
using Zenject;

namespace GameKit.Energy.Tests
{
    [TestFixture]
    public class EnergyRestorationControllerTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.Bind<IEnergyService>().To<FakeEnergyService>().AsSingle();
            Container.Bind<IGameTickSource>().To<FakeGameTickSource>().AsSingle();
            Container.Bind<EnergyRestorationController>().AsSingle();
        }

        [Test]
        public void Tick_WhenGameTickSourceRaisesTick_ProcessesPendingRestoration()
        {
            var tickSource = (FakeGameTickSource)Container.Resolve<IGameTickSource>();
            var energyService = (FakeEnergyService)Container.Resolve<IEnergyService>();
            Container.Resolve<EnergyRestorationController>();

            tickSource.RaiseTicked();

            Assert.That(energyService.ProcessPendingRestorationCalls, Is.EqualTo(1));
        }

        [UsedImplicitly]
        private class FakeGameTickSource : IGameTickSource
        {
            public event Action Ticked;

            public void RaiseTicked()
            {
                Ticked?.Invoke();
            }
        }

        [UsedImplicitly]
        private class FakeEnergyService : IEnergyService
        {
            public int ProcessPendingRestorationCalls { get; private set; }
            public event Action Changed
            {
                add { }
                remove { }
            }

            public int Energy => 0;
            public bool IsRestorationInProgress => false;

            public bool TryAdd(int amount)
            {
                return false;
            }

            public bool TrySpend(int amount)
            {
                return false;
            }

            public TimeSpan GetRestorationTimer()
            {
                return TimeSpan.Zero;
            }

            public void ProcessPendingRestoration()
            {
                ProcessPendingRestorationCalls++;
            }
        }
    }
}
