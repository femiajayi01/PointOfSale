using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using PosCore.Interfaces;
using StackExchange.Redis;

namespace PosCore.Repository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisRepository( IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public IEnumerable<T> GetAllRecords<T>()
        {
            var db = _redis.GetDatabase();
            var completeSet = db.HashGetAll("hashPOS");
            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<T>(val.Value)).ToList();
                return obj;
            }
            return null;
        }


        public RedisValue GetRecord(string id)
        {
            var db = _redis.GetDatabase();
            var record = db.HashGet("hashPOS", id);
            return record;
        }

        public object SetRecord(string id, object obj)
        {
            var db = _redis.GetDatabase();
            var serializedRecord = JsonSerializer.Serialize(obj);
            db.HashSet($"hashPOS", new HashEntry[]
                {new HashEntry(id, serializedRecord)});
            return obj;
        }



    }
}
