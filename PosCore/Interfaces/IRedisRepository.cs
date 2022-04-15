using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosCore.Models;
using StackExchange.Redis;

namespace PosCore.Interfaces
{
    public  interface IRedisRepository
    {
         Object SetRecord(string id, Object obj);
         RedisValue GetRecord(string id);
         IEnumerable<T> GetAllRecords<T>();
    }
}
