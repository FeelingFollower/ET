using ETModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace ETHotfix
{
    public static class MangoDBHelper
    {
        public static async ETTask<List<ComponentWithId>> QueryLss<T>(this DBProxyComponent self, string LSS) where T : ComponentWithId
        {

            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryLSSJsonResponse dBQueryWhereJsonResponse = (DBQueryLSSJsonResponse)await session.Call(new DBQueryLSSJsonRequest { CollectionName = typeof(T).Name, LSS = LSS });
            return dBQueryWhereJsonResponse.Components;
        }

        public static async ETTask<long> QueryCount<T>(this DBProxyComponent self, string json) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryCountJsonResponse dBQueryWhereJsonResponse = (DBQueryCountJsonResponse)await session.Call(new DBQueryCountJsonRequest { CollectionName = typeof(T).Name, Json = json });
            return dBQueryWhereJsonResponse.Components;
        }
    }
}