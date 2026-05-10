using System;
using GameKit.CurrentTime.Contracts;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode.Contracts;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using Zenject;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateProvider : IPlayerStateProvider
    {
        private static readonly JsonSerializerSettings s_jsonSerializerSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public PlayerStateDto Data { get; set; }
        public bool IsDirty { get; private set; }
        public event Action RefreshedFromJson;

        [Inject] private IPlayerStateStorage m_playerStateStorage;
        [Inject] private IProductionModeProvider m_productionModeProvider;
        [Inject] private ICurrentTimeProvider m_currentTimeProvider;

        public void MarkAsDirty()
        {
            IsDirty = true;
        }

        public void Save()
        {
            SaveJsonToFile(SerializeState(Data));
        }

        public void Set(string json)
        {
            Data = DeserializeState(json);
            SaveJsonToFile(SerializeState(Data));
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
                Data = DeserializeState(json);
                if (Data == null || string.IsNullOrEmpty(Data.UserId))
                {
                    throw new FormatException("Saved state format is incompatible.");
                }
            }
            catch (Exception e)
            {
                if (m_productionModeProvider.IsProduction)
                {
                    throw;
                }

                Initialize();
                Debug.LogWarning($"Failed to apply saved state: \"{e}\". State has been reset.");
            }
        }

        private void Initialize()
        {
            Data = new PlayerStateDto
            {
                UserId = Guid.NewGuid().ToString(),
                FirstLaunchTimestamp = m_currentTimeProvider.GetTimestamp()
            };

            // TODO: <remove_temporary_code>
            Data.Currencies.SoftCurrency = UnityEngine.Random.Range(1, 100);
            Data.Currencies.HardCurrency = UnityEngine.Random.Range(1, 100);
            Data.EnergyData.Energy = UnityEngine.Random.Range(1, 100);
            // TODO: </remove_temporary_code>

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

        private static string SerializeState(PlayerStateDto state)
        {
            return JsonConvert.SerializeObject(state, s_jsonSerializerSettings);
        }

        private static PlayerStateDto DeserializeState(string json)
        {
            var state = JsonConvert.DeserializeObject<PlayerStateDto>(json, s_jsonSerializerSettings);
            return state ?? throw new FormatException("Saved state format is incompatible.");
        }
    }
}
