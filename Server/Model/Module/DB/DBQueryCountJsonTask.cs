using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ETModel
{
    [ObjectSystem]
    public class DBQueryCountJsonTaskAwakeSystem : AwakeSystem<DBQueryCountJsonTask, string, string, ETTaskCompletionSource<long>>
    {
        public override void Awake(DBQueryCountJsonTask self, string collectionName, string json, ETTaskCompletionSource<long> tcs)
        {
            self.CollectionName = collectionName;
            self.Json = json;
            self.Tcs = tcs;
        }
    }

    public sealed class DBQueryCountJsonTask : DBTask
    {
        public string CollectionName { get; set; }

        public string Json { get; set; }

        public long count;

        public ETTaskCompletionSource<long> Tcs { get; set; }

        public override async ETTask Run()
        {
            DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            try
            {
                // 执行查询数据库任务
                FilterDefinition<ComponentWithId> filterDefinition = new JsonFilterDefinition<ComponentWithId>(this.Json);
                long count = await dbComponent.GetCollection(this.CollectionName).Find(filterDefinition).CountDocumentsAsync();

                this.Tcs.SetResult(count);
            }
            catch (Exception e)
            {
                this.Tcs.SetException(new Exception($"查询数据库异常! {CollectionName} {this.Json}", e));
            }
        }
    }
}