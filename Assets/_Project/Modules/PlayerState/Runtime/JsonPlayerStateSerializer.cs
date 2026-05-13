using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class JsonPlayerStateSerializer : IPlayerStateSerializer
    {
        private static readonly JsonSerializerSettings s_jsonSerializerSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string Serialize(PlayerStateDto state)
        {
            return JsonConvert.SerializeObject(state, s_jsonSerializerSettings);
        }

        public PlayerStateDto Deserialize(string json)
        {
            var state = JsonConvert.DeserializeObject<PlayerStateDto>(json, s_jsonSerializerSettings);
            return state ?? throw new JsonSerializationException("Saved state JSON does not contain an object.");
        }
    }
}
