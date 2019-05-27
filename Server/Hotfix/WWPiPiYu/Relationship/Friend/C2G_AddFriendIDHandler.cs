using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建和删除好友信息数据
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_AddFriendIDHandler : AMRpcHandler<C2G_AddFriendID, G2C_AddFriendID>
    {
        protected override async void Run(Session session, C2G_AddFriendID message, Action<G2C_AddFriendID> reply)
        {
            G2C_AddFriendID response = new G2C_AddFriendID();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RelationInfo>("{'_Account' : '" + message.Account + "'}");
                var acountsf = await dBProxyComponent.Query<RelationInfo>("{'_Account' : '" + message.FriendID + "'}");
                if (acounts.Count > 0 && acountsf.Count > 0)
                {
                    RelationInfo info = acounts[0] as RelationInfo;
                    RelationInfo infoF = acountsf[0] as RelationInfo;
                    if (message.Type == 1)
                    {
                        //添加好友 双方都添加这条好友信息
                        var acountsF = await dBProxyComponent.Query<FriendInfo>("{'_InvAccountID' : '" + message.Account + "','_ByInvAccountID' : '"+ message.FriendID + "'}");
                        if (acountsF.Count != 0)
                        {
                            FriendInfo friendInfo = acountsF[0] as FriendInfo;
                            friendInfo._State = 1;

                            await dBProxyComponent.Save(friendInfo);

                            info._FriendIDList.Insert(0, friendInfo.Id);
                            infoF._FriendIDList.Insert(0, friendInfo.Id);
                            response.IsSuccess = true;
                            response.Message = "添加好友信息数据成功";
                        }
                        else
                        {
                            FriendInfo relationInfo = ComponentFactory.Create<FriendInfo>();
                            relationInfo._InvAccountID = message.Account;
                            relationInfo._ByInvAccountID = message.FriendID;
                            relationInfo._NickName = "";
                            relationInfo._DateMessageIDList = new List<string>();
                            relationInfo._LastDate = "";
                            relationInfo._LiveMassageList = new List<string>();
                            relationInfo._GroupNumber = 0;
                            relationInfo._State = 1;

                            await dBProxyComponent.Save(relationInfo);

                            info._FriendIDList.Insert(0, relationInfo.Id);
                            infoF._FriendIDList.Insert(0, relationInfo.Id);
                            response.IsSuccess = true;
                            response.Message = "添加好友信息数据成功";
                        }
                    }
                    else if (message.Type == 2)
                    {
                        //删除好友 双方都删除这条好友信息
                        var acountsF = await dBProxyComponent.Query<FriendInfo>("{'_id' : '" + message.FriendInfoID + "'}");
                        FriendInfo friendInfo = acountsF[0] as FriendInfo;
                        friendInfo._State = 2;

                        await dBProxyComponent.Save(friendInfo);

                        info._FriendIDList.Remove(message.FriendInfoID);
                        infoF._FriendIDList.Remove(message.FriendInfoID);
                        response.IsSuccess = true;
                        response.Message = "删除好友信息数据成功";
                    }
                    await dBProxyComponent.Save(info);
                    await dBProxyComponent.Save(infoF);
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

    /// <summary>
    /// 修改好友信息数据
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_UpdateFriendIDHandler : AMRpcHandler<C2G_UpdateFriendID, G2C_UpdateFriendID>
    {
        protected override async void Run(Session session, C2G_UpdateFriendID message, Action<G2C_UpdateFriendID> reply)
        {
            G2C_UpdateFriendID response = new G2C_UpdateFriendID();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<FriendInfo>("{'+id' : '" + message.FriendInfoID + "'}");
                if (acounts.Count > 0)
                {
                    FriendInfo info = acounts[0] as FriendInfo;
                    if (message.NickName != "")
                    {
                        info._NickName = message.NickName;
                    }
                    if (message.LastDate != "")
                    {
                        info._LastDate = message.LastDate;
                    }
                    //if (message.DateMessage != "")
                    //{
                    //    info._DateMessageIDList.Add(message.DateMessage);
                    //}
                    //if (message.LiveMassage != "")
                    //{
                    //    info._LiveMassageList.Add(message.LiveMassage);
                    //}
                    if (message.GroupNumber != 0)
                    {
                        info._GroupNumber = message.GroupNumber;
                    }
                    if (message.State != 0)
                    {
                        info._State = message.State;
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

    /// <summary>
    /// 修改好友信息留言列表数据
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_FriendInfoDateMessageHandler : AMRpcHandler<C2G_FriendInfoDateMessage, G2C_FriendInfoDateMessage>
    {
        protected override async void Run(Session session, C2G_FriendInfoDateMessage message, Action<G2C_FriendInfoDateMessage> reply)
        {
            G2C_FriendInfoDateMessage response = new G2C_FriendInfoDateMessage();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<FriendInfo>("{'+id' : '" + message.FriendInfoID + "'}");
                if (acounts.Count > 0)
                {
                    FriendInfo info = acounts[0] as FriendInfo;
                    foreach (string item in info._LiveMassageList)
                    {
                        info._DateMessageIDList.Add(item);
                    }
                    info._LiveMassageList.Clear();

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