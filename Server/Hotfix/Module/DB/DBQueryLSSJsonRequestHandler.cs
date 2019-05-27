using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class DBQueryLSSJsonRequestHandler : AMRpcHandler<DBQueryLSSJsonRequest, DBQueryLSSJsonResponse>
    {
        protected override async void Run(Session session, DBQueryLSSJsonRequest message, Action<DBQueryLSSJsonResponse> reply)
        {
            DBQueryLSSJsonResponse response = new DBQueryLSSJsonResponse();
            try
            {
                List<ComponentWithId> components = await Game.Scene.GetComponent<DBComponent>().GetLSSJson(message.CollectionName, message.LSS);
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