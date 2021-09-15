using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Scoreboard.Data
{
    /// <summary>
    /// Model holding data around participants records
    /// </summary>
    public class Participant
    {
        /// <summary>
        /// Mongo unique ID for entry
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Display name of golfer
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// DiscordGuild id record is part of
        /// </summary>
        public ulong ServerId { get; set; }

        /// <summary>
        /// Current score
        /// </summary>
        public int Score { get; set; }

        [BsonExtraElements]
        private BsonDocument CatchAll { get; set; }
    }
}
