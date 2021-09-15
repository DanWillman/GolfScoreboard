using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Scoreboard.Data
{
    public class MongoDataAccess
    {
        private readonly string ParticipantCollectionName = "scores";
        private readonly string ConnectionString = "mongodb://192.168.1.69:27018";
        private readonly string DatabaseName = "shame_golf";
        private const ulong DANA_SERVER_ID = 854088068306829332;

        private readonly IMongoCollection<Participant> particpantCollection;

        public MongoDataAccess()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            particpantCollection = database.GetCollection<Participant>(ParticipantCollectionName);
            particpantCollection.Indexes.CreateOne(new CreateIndexModel<Participant>(Builders<Participant>.IndexKeys.Ascending(x => x.DisplayName)));
        }

        /// <summary>
        /// Returns an unsorted list of all particpant entries for a specified server
        /// </summary>
        public List<Participant> GetParticipants() => particpantCollection.Find(x => x.ServerId == DANA_SERVER_ID).ToList();    
    }
}
