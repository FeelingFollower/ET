using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ETModel
{
    [ObjectSystem]
    public class DBQueryLSSJsonTaskAwakeSystem : AwakeSystem<DBQueryLSSJsonTask, string, string, ETTaskCompletionSource<List<ComponentWithId>>>
    {
        public override void Awake(DBQueryLSSJsonTask self, string collectionName, string lss, ETTaskCompletionSource<List<ComponentWithId>> tcs)
        {
            self.CollectionName = collectionName;
            self.Tcs = tcs;
            self.LSS = lss;
        }
    }

    public sealed class DBQueryLSSJsonTask : DBTask
    {
        public string CollectionName { get; set; }

        public string LSS { get; set; }

        public string Json { get; set; }

        public int skip { get; set; }

        public int limit { get; set; }

        public string sort { get; set; }

        public ETTaskCompletionSource<List<ComponentWithId>> Tcs { get; set; }

        public override async ETTask Run()
        {
            DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            try
            {
                int s = 0;
                string[] st = LSS.Split('|');
                Json = st[0];
                int.TryParse(st[1], out s);
                skip = s;
                int.TryParse(st[2], out s);
                limit = s;
                sort = st[3];
                //Log.Debug("DBQueryLSSJsonTask Json: " + Json);
                //Log.Debug("DBQueryLSSJsonTask 从第几个拿skip: " + skip);
                //Log.Debug("DBQueryLSSJsonTask 获取多少个limit: "+limit);
                //Log.Debug("DBQueryLSSJsonTask Sort: "+ sort);
                // 执行查询数据库任务
                FilterDefinition<ComponentWithId> filterDefinition = new JsonFilterDefinition<ComponentWithId>(this.Json);
                IAsyncCursor<ComponentWithId> cursor = await dbComponent.GetCollection(this.CollectionName).Find(filterDefinition).Skip(skip).Limit(limit).Sort(sort).ToCursorAsync();
                List<ComponentWithId> components = await cursor.ToListAsync();
                this.Tcs.SetResult(components);
            }
            catch (Exception e)
            {
                this.Tcs.SetException(new Exception($"查询数据库异常! {CollectionName} {this.Json}", e));
            }
        }
    }
}