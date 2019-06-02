using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2G_AddBlackIDHandler : AMRpcHandler<C2G_AddBlackID, G2C_AddBlackID>
    {
        protected override async void Run(Session session, C2G_AddBlackID message, Action<G2C_AddBlackID> reply)
        {
            G2C_AddBlackID response = new G2C_AddBlackID();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RelationInfo>("{'_Account' : '" + message.Account + "'}");
                if (acounts.Count > 0)
                {
                    RelationInfo info = acounts[0] as RelationInfo;
                    if (message.Type == 1)
                    {
                        //添加黑名单，删除好友列表
                        info._BlackIDList.Insert(0,message.BlackID);
                        info._FriendIDList.Remove(message.BlackID);
                        response.IsSuccess = true;
                        response.Message = "添加黑名单成功";
                    }
                    else if (message.Type == 2)
                    {
                        //删除黑名单，添加好友
                        info._BlackIDList.Remove(message.BlackID);
                        info._FriendIDList.Insert(0,message.BlackID);
                        response.IsSuccess = true;
                        response.Message = "删除黑名单成功";
                    }
                    await dBProxyComponent.Save(info);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "好友列表数据库异常";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "好友列表数据库异常");
                }
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "好友列表获取或创建失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
}