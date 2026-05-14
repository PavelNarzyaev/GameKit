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
        public PlayerStateDto Data { get; private set; }
        public bool IsDirty { get; private set; }
        public event Action Replaced;

        [Inject] private IPlayerStateStorage m_playerStateStorage;
        [Inject] private IProductionModeProvider m_productionModeProvider;
        [Inject] private IPlayerStateSerializer m_playerStateSerializer;
        [Inject] private IPlayerStateValidator m_playerStateValidator;
        [Inject] private IPlayerStateFactory m_playerStateFactory;

        public void Edit(Action<PlayerStateDto> edit)
        {
            edit(Data);
            IsDirty = true;
        }

        public void Replace(PlayerStateDto state)
        {
            Data = state;
            IsDirty = true;
            Replaced?.Invoke();
        }

        public void Save()
        {
            SaveJsonToFile(m_playerStateSerializer.Serialize(Data));
        }

        public void ReplaceFromJson(string json)
        {
            Data = DeserializeAndValidate(json);
            SaveJsonToFile(m_playerStateSerializer.Serialize(Data));
            Replaced?.Invoke();
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
                var json = LoadJsonFromFile();
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

        public string ExportJson()
        {
            return LoadJsonFromFile();
        }

        private string LoadJsonFromFile()
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
