using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class DBQueryCountJsonRequestHandler : AMRpcHandler<DBQueryCountJsonRequest, DBQueryCountJsonResponse>
    {
        protected override async void Run(Session session, DBQueryCountJsonRequest message, Action<DBQueryCountJsonResponse> reply)
        {
            DBQueryCountJsonResponse response = new DBQueryCountJsonResponse();
            try
            {
                long components = await Game.Scene.GetComponent<DBComponent>().GetCountJson(message.CollectionName, message.Json);
                response.Components = components;

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}