using MongoDB.Driver;
using Scoreboard.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Scoreboard.Data
{
    public class MongoDataAccess
    {
        private readonly string ParticipantCollectionName = "scores";
        private readonly string ArchiveCollectionName = "archive";
        private readonly string ConnectionString = "mongodb://192.168.1.69:27018";
        private readonly string DatabaseName = "shame_golf";
        private const ulong DANA_SERVER_ID = 854088068306829332;

        private readonly IMongoCollection<Participant> particpantCollection;
        private readonly IMongoCollection<Archive> archiveCollection;

        public MongoDataAccess()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            particpantCollection = database.GetCollection<Participant>(ParticipantCollectionName);
            particpantCollection.Indexes.CreateOne(new CreateIndexModel<Participant>(Builders<Participant>.IndexKeys.Ascending(x => x.DisplayName)));

            archiveCollection = database.GetCollection<Archive>(ArchiveCollectionName);
            archiveCollection.Indexes.CreateOne(new CreateIndexModel<Archive>(Builders<Archive>.IndexKeys.Ascending(x => x.Id)));
        }

        /// <summary>
        /// Returns an unsorted list of all particpant entries for the current season
        /// </summary>
        public List<Participant> GetParticipants() => particpantCollection.Find(p => p.ServerId == DANA_SERVER_ID).ToList();

        /// <summary>
        /// Returns a specific participant
        /// </summary>
        public Participant GetParticipant(ulong userId) => GetParticipants().Find(p => p.UserId == userId);

        /// <summary>
        /// Gets all archives from the specified server
        /// </summary>
        public List<Archive> GetArchives() => archiveCollection.Find(x => x.ServerId == DANA_SERVER_ID).ToList();

        /// <summary>
        /// Returns an unsorted list of all particpant entries for a specified season
        /// </summary>
        /// <returns></returns>
        public Archive GetArchive(string archiveId)
        {
            return archiveCollection.Find(a => a.Id == archiveId).FirstOrDefault();
        }

        /// <summary>
        /// Gets all history events for user in current season
        /// </summary>
        public List<Event> GetActiveHistory(ulong userId) => GetParticipants().Find(p => p.UserId == userId).EventHistory;

        public List<Event> GetArchiveHistory(ulong userId, string archiveId)
        {
            var results = GetArchive(archiveId).Participants.Find(p => p.UserId == userId).EventHistory;

            return results == null ? new List<Event>() : results;
        }
    }
}
