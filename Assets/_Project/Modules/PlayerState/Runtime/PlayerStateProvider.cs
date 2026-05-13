using System;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode.Contracts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateProvider : IPlayerStateProvider
    {
        public PlayerStateDto Data { get; set; }
        public bool IsDirty { get; private set; }
        public event Action RefreshedFromJson;

        [Inject] private IPlayerStateStorage m_playerStateStorage;
        [Inject] private IProductionModeProvider m_productionModeProvider;
        [Inject] private IPlayerStateSerializer m_playerStateSerializer;
        [Inject] private IPlayerStateValidator m_playerStateValidator;
        [Inject] private IPlayerStateFactory m_playerStateFactory;

        public void MarkAsDirty()
        {
            IsDirty = true;
        }

        public void Save()
        {
            SaveJsonToFile(m_playerStateSerializer.Serialize(Data));
        }

        public void Set(string json)
        {
            Data = DeserializeAndValidate(json);
            SaveJsonToFile(m_playerStateSerializer.Serialize(Data));
            RefreshedFromJson?.Invoke();
        }

        private void SaveJsonToFile(string json)
        {
            m_playerStateStorage.Save(json);
            IsDirty = false;
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
                LoadFromFile();
            }
        }

        private void LoadFromFile()
        {
            try
            {
                var json = Get();
                Data = DeserializeAndValidate(json);
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

        public string Get()
        {
            return m_playerStateStorage.Load();
        }

        public void Delete()
        {
            m_playerStateStorage.Delete();
        }

        private PlayerStateDto DeserializeAndValidate(string json)
        {
            var state = m_playerStateSerializer.Deserialize(json);
            m_playerStateValidator.Validate(state);
            return state;
        }
    }
}
