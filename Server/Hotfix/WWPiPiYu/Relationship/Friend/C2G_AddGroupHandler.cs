using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建删除群组信息
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_AddGroupHandler : AMRpcHandler<C2G_AddGroup, G2C_AddGroup>
    {
        protected override async void Run(Session session, C2G_AddGroup message, Action<G2C_AddGroup> reply)
        {
            G2C_AddGroup response = new G2C_AddGroup();
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
                        //添加群组信息
                        GroupInfo relationInfo = ComponentFactory.Create<GroupInfo>();
                        relationInfo._UseState = 1;
                        relationInfo._Name = message.Name;
                        relationInfo._InvAccountID = message.Account;
                        relationInfo._GroupPassword = message.GroupPassword;
                        relationInfo._GroupNumber = relationInfo.Id;
                        relationInfo._CreateDate = message.CreateDate;
                        relationInfo._ColorCode = message.ColorCode;

                        await dBProxyComponent.Save(relationInfo);

                        info._GroupList.Insert(0, relationInfo.Id);

                        response.IsSuccess = true;
                        response.Message = "添加群组信息成功";
                    }
                    else if (message.Type == 2)
                    {
                        var acountsG = await dBProxyComponent.Query<GroupInfo>("{'_id' : '" + message.DGroupid + "'}");
                        //改变群组信息的UserState 如果这个群组内有好友，则将好友群组编号设为0
                        GroupInfo groupInfo = acountsG[0] as GroupInfo;
                        groupInfo._UseState = 2;

                        var acountsF = await dBProxyComponent.Query<FriendInfo>("{'_GroupNumber' : '" + message.DGroupNumber + "'}");
                        foreach (FriendInfo item in acountsF)
                        {
                            item._GroupNumber = 0;

                            await dBProxyComponent.Save(item);
                        }

                        response.IsSuccess = true;
                        response.Message = "删除群组信息成功";
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
    /// 修改群组信息
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_UpdateGroupHandler : AMRpcHandler<C2G_UpdateGroup, G2C_UpdateGroup>
    {
        protected override async void Run(Session session, C2G_UpdateGroup message, Action<G2C_UpdateGroup> reply)
        {
            G2C_UpdateGroup response = new G2C_UpdateGroup();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<GroupInfo>("{'_GroupNumber' : '" + message.GroupNumber + "'}");
                if (acounts.Count > 0)
                {
                    GroupInfo info = acounts[0] as GroupInfo;

                    if (message.Name != "")
                    {
                        info._Name = message.Name;
                    }
                    if (message.ColorCode != "")
                    {
                        info._ColorCode = message.ColorCode;
                    }
                    if (message.GroupPassword != "")
                    {
                        info._GroupPassword = message.GroupPassword;
                    }
                    await dBProxyComponent.Save(info);

                    response.IsSuccess = true;
                    response.Message = "修改群组信息成功";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "好友列表数据库异常";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "群组信息数据库异常");
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