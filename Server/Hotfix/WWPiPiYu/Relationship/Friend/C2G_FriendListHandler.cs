using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建和获取好友列表
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_FriendListHandler : AMRpcHandler<C2G_FriendList, G2C_FriendList>
    {
        protected override async void Run(Session session, C2G_FriendList message, Action<G2C_FriendList> reply)
        {
            G2C_FriendList response = new G2C_FriendList();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();


                var acounts = await dBProxyComponent.Query<RelationInfo>("{'_Account' : '" + message.Account + "'}");
                if (message.Type == 1)
                {
                    if (acounts.Count < 1)
                    {
                        RelationInfo relationInfo = ComponentFactory.Create<RelationInfo>();
                        relationInfo._InfoID = message.InfoID;
                        relationInfo._AccountID = message.Account;
                        relationInfo._BlackIDList = new List<long>();
                        relationInfo._ChatRoomList = new List<long>();
                        relationInfo._FriendIDList = new List<long>();
                        relationInfo._GroupList = new List<long>();

                        await dBProxyComponent.Save(relationInfo);

                        response.IsSuccess = true;
                        response.Message = "好友列表创建成功";

                        Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "好友列表创建成功");
                    }
                }
                else if (message.Type == 2)
                {
                    if (acounts.Count > 0)
                    {
                        RelationInfo item = acounts[0] as RelationInfo;
                        response.BlackIDList = RepeatedFieldAndListChangeTool.ListToRepeatedField(item._BlackIDList);
                        response.ChatRoomList = RepeatedFieldAndListChangeTool.ListToRepeatedField(item._ChatRoomList);
                        response.FriendIDList = RepeatedFieldAndListChangeTool.ListToRepeatedField(item._FriendIDList);
                        response.GroupList = RepeatedFieldAndListChangeTool.ListToRepeatedField(item._GroupList);

                        response.IsSuccess = true;
                        response.Message = "好友列表获取成功";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "好友列表数据库异常";
                        Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "好友列表数据库异常");
                    }
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