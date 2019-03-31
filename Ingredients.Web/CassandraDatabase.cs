using System;
using Cassandra;

namespace Ingredients.Web
{
    static class CassandraDatabase 
    {
        private static Lazy<ISession> _session;

        static CassandraDatabase() 
        {
            _session = new Lazy<ISession>(() => {
                var cluster = Cluster.Builder()
                    .WithCredentials("cassandra", "cassandra")
                    .AddContactPoint("cassandra-seed")
                    .AddContactPoint("cassandra-node-1")
                    .AddContactPoint("cassandra-node-2")
                    .Build();

                var session = cluster.Connect("cookbook");
                
                return session;
            });
        }

        public static ISession GetSession () 
        {
            return _session.Value;
        }
    }
}