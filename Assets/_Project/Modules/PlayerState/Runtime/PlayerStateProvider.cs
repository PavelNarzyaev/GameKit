using System;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode.Contracts;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateProvider : IPlayerStateProvider, IDisposable
    {
        public bool IsDirty { get; private set; }
        public event Action Replaced;

        private readonly ReactiveProperty<int> m_timeOffsetSeconds = new(0);
        private readonly ReactiveProperty<int> m_softCurrency = new(0);
        private readonly ReactiveProperty<int> m_hardCurrency = new(0);
        private readonly ReactiveProperty<int> m_energy = new(0);
        private readonly ReactiveProperty<long> m_energyNextRestoreTimestamp = new(0);

        private readonly IPlayerStateStorage m_playerStateStorage;
        private readonly IProductionModeProvider m_productionModeProvider;
        private readonly IPlayerStateSerializer m_playerStateSerializer;
        private readonly IPlayerStateValidator m_playerStateValidator;
        private readonly IPlayerStateFactory m_playerStateFactory;

        private PlayerStateDto m_data;

        public PlayerStateProvider(
            IPlayerStateStorage playerStateStorage,
            IProductionModeProvider productionModeProvider,
            IPlayerStateSerializer playerStateSerializer,
            IPlayerStateValidator playerStateValidator,
            IPlayerStateFactory playerStateFactory)
        {
            m_playerStateStorage = playerStateStorage;
            m_productionModeProvider = productionModeProvider;
            m_playerStateSerializer = playerStateSerializer;
            m_playerStateValidator = playerStateValidator;
            m_playerStateFactory = playerStateFactory;
        }

        public string UserId => m_data.UserId;
        public long FirstLaunchTimestamp => m_data.FirstLaunchTimestamp;
        public int LaunchesCounter => m_data.LaunchesCounter;
        public ReadOnlyReactiveProperty<int> TimeOffsetSeconds => m_timeOffsetSeconds;
        public ReadOnlyReactiveProperty<int> SoftCurrency => m_softCurrency;
        public ReadOnlyReactiveProperty<int> HardCurrency => m_hardCurrency;
        public ReadOnlyReactiveProperty<int> Energy => m_energy;
        public ReadOnlyReactiveProperty<long> EnergyNextRestoreTimestamp => m_energyNextRestoreTimestamp;

        public void IncrementLaunchesCounter()
        {
            m_data.LaunchesCounter++;
            IsDirty = true;
        }

        public void SetTimeOffsetSeconds(int value)
        {
            if (m_data.TimeOffsetSeconds == value)
            {
                return;
            }

            m_data.TimeOffsetSeconds = value;
            m_timeOffsetSeconds.Value = value;
            IsDirty = true;
        }

        public void SetSoftCurrency(int value)
        {
            if (m_data.Currencies.SoftCurrency == value)
            {
                return;
            }

            m_data.Currencies.SoftCurrency = value;
            m_softCurrency.Value = value;
            IsDirty = true;
        }

        public void SetHardCurrency(int value)
        {
            if (m_data.Currencies.HardCurrency == value)
            {
                return;
            }

            m_data.Currencies.HardCurrency = value;
            m_hardCurrency.Value = value;
            IsDirty = true;
        }

        public void SetEnergyState(int energy, long nextRestoreTimestamp)
        {
            if (m_data.EnergyData.Energy == energy &&
                m_data.EnergyData.NextRestoreTimestamp == nextRestoreTimestamp)
            {
                return;
            }

            m_data.EnergyData.Energy = energy;
            m_data.EnergyData.NextRestoreTimestamp = nextRestoreTimestamp;
            m_energy.Value = energy;
            m_energyNextRestoreTimestamp.Value = nextRestoreTimestamp;
            IsDirty = true;
        }

        public void Save()
        {
            m_playerStateStorage.Save(m_data);
            IsDirty = false;
        }

        public void Reset()
        {
            Delete();
            Initialize();
            IncrementLaunchesCounter();
            Save();
            RefreshReactiveProperties();
            Replaced?.Invoke();
        }

        public void ReplaceFromJson(string json)
        {
            m_data = DeserializeAndValidate(json);
            Save();
            RefreshReactiveProperties();
            Replaced?.Invoke();
        }

        public void Refresh()
        {
            var isFirstLaunch = !m_playerStateStorage.Exists();
            if (isFirstLaunch)
            {
                Initialize();
            }
            else
            {
                Load();
            }

            RefreshReactiveProperties();
        }

        private void Load()
        {
            try
            {
                m_data = m_playerStateStorage.Load();
                m_playerStateValidator.Validate(m_data);
                IsDirty = false;
            }
            catch (Exception e)
            {
                if (m_productionModeProvider.IsProduction)
                {
                    throw;
                }

#if !IS_PRODUCTION
                ResetAfterLoadFailure(e);
#else
                Debug.LogError(
                    $"Incorrect {nameof(IProductionModeProvider)} behaviour detected: " +
                    $"{nameof(IProductionModeProvider.IsProduction)} is false in production build.");
                throw;
#endif
            }
        }

#if !IS_PRODUCTION
        private void ResetAfterLoadFailure(Exception e)
        {
            Initialize();
            Debug.LogWarning($"Failed to apply saved state: \"{e}\". State has been reset.");
        }
#endif

        private void Initialize()
        {
            m_data = m_playerStateFactory.Create();
            IsDirty = true;
        }

        public string ExportJson()
        {
            return m_playerStateSerializer.Serialize(m_playerStateStorage.Load());
        }

        private void Delete()
        {
            m_playerStateStorage.Delete();
        }

        public void Dispose()
        {
            m_timeOffsetSeconds.Dispose();
            m_softCurrency.Dispose();
            m_hardCurrency.Dispose();
            m_energy.Dispose();
            m_energyNextRestoreTimestamp.Dispose();
        }

        private PlayerStateDto DeserializeAndValidate(string json)
        {
            var state = m_playerStateSerializer.Deserialize(json);
            m_playerStateValidator.Validate(state);
            return state;
        }

        private void RefreshReactiveProperties()
        {
            m_timeOffsetSeconds.Value = m_data.TimeOffsetSeconds;
            m_softCurrency.Value = m_data.Currencies.SoftCurrency;
            m_hardCurrency.Value = m_data.Currencies.HardCurrency;
            m_energy.Value = m_data.EnergyData.Energy;
            m_energyNextRestoreTimestamp.Value = m_data.EnergyData.NextRestoreTimestamp;
        }
    }
}
