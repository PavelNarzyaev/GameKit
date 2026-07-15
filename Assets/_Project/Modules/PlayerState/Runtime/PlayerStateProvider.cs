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
        public PlayerStateDto Data { get; private set; }
        public bool IsDirty { get; private set; }
        public event Action Replaced;

        private readonly ReactiveProperty<int> m_softCurrency = new(0);
        private readonly ReactiveProperty<int> m_hardCurrency = new(0);

        private readonly IPlayerStateStorage m_playerStateStorage;
        private readonly IProductionModeProvider m_productionModeProvider;
        private readonly IPlayerStateSerializer m_playerStateSerializer;
        private readonly IPlayerStateValidator m_playerStateValidator;
        private readonly IPlayerStateFactory m_playerStateFactory;

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

        public ReadOnlyReactiveProperty<int> SoftCurrency => m_softCurrency;
        public ReadOnlyReactiveProperty<int> HardCurrency => m_hardCurrency;

        public void SetSoftCurrency(int value)
        {
            if (Data.Currencies.SoftCurrency == value)
            {
                return;
            }

            Data.Currencies.SoftCurrency = value;
            m_softCurrency.Value = value;
            IsDirty = true;
        }

        public void SetHardCurrency(int value)
        {
            if (Data.Currencies.HardCurrency == value)
            {
                return;
            }

            Data.Currencies.HardCurrency = value;
            m_hardCurrency.Value = value;
            IsDirty = true;
        }

        public void Edit(Action<PlayerStateDto> edit)
        {
            edit(Data);
            IsDirty = true;
            RefreshReactiveProperties();
        }

        public void Save()
        {
            m_playerStateStorage.Save(Data);
            IsDirty = false;
        }

        public void Reset()
        {
            Delete();
            Initialize();
            Save();
            RefreshReactiveProperties();
            Replaced?.Invoke();
        }

        public void ReplaceFromJson(string json)
        {
            Data = DeserializeAndValidate(json);
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
                Data = m_playerStateStorage.Load();
                m_playerStateValidator.Validate(Data);
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
            Data = m_playerStateFactory.Create();
            IsDirty = true;
        }

        public string ExportJson()
        {
            return m_playerStateSerializer.Serialize(m_playerStateStorage.Load());
        }

        public void Delete()
        {
            m_playerStateStorage.Delete();
        }

        public void Dispose()
        {
            m_softCurrency.Dispose();
            m_hardCurrency.Dispose();
        }

        private PlayerStateDto DeserializeAndValidate(string json)
        {
            var state = m_playerStateSerializer.Deserialize(json);
            m_playerStateValidator.Validate(state);
            return state;
        }

        private void RefreshReactiveProperties()
        {
            m_softCurrency.Value = Data.Currencies.SoftCurrency;
            m_hardCurrency.Value = Data.Currencies.HardCurrency;
        }
    }
}
