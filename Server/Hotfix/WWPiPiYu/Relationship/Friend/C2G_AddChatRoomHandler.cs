using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建聊天室
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_AddChatRoomHandler : AMRpcHandler<C2G_AddChatRoom, G2C_AddChatRoom>
    {
        protected override async void Run(Session session, C2G_AddChatRoom message, Action<G2C_AddChatRoom> reply)
        {
            G2C_AddChatRoom response = new G2C_AddChatRoom();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                var acounts = await dBProxyComponent.Query<RelationInfo>("{'_Account' : '" + message.Account + "'}");

                if (acounts.Count > 0)
                {

                    ChatRoom relationInfo = ComponentFactory.Create<ChatRoom>();
                    relationInfo._CreateDate = message.CreateDate;
                    relationInfo._GroupType = message.GroupType;
                    relationInfo._InvAccountID = message.Account;
                    relationInfo._ManagePassword = message.ManagePassword;
                    relationInfo._Name = message.Name;
                    relationInfo._PublicBorad = message.PublicBorad;
                    relationInfo._State = 1;
                    relationInfo._UserList = new List<long>();
                    relationInfo._DateMessageIDList = new List<string>();
                    relationInfo._UserList.Add(message.Account);

                    RelationInfo info = acounts[0] as RelationInfo;
                    //给聊天室创建者好友列表添加聊天室
                    info._ChatRoomList.Add(relationInfo.Id);
                    response.IsSuccess = true;
                    response.ChatRoomid = relationInfo.Id;
                    response.Message = "创建聊天室编号成功";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "创建聊天室编号成功");

                    await dBProxyComponent.Save(info);

                    await dBProxyComponent.Save(relationInfo);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "聊天室数据库异常";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室数据库异常");
                }
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室创建失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 删除聊天室
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_DeleteChatRoomHandler : AMRpcHandler<C2G_DeleteChatRoom, G2C_DeleteChatRoom>
    {
        protected override async void Run(Session session, C2G_DeleteChatRoom message, Action<G2C_DeleteChatRoom> reply)
        {
            G2C_DeleteChatRoom response = new G2C_DeleteChatRoom();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                var acountsC = await dBProxyComponent.Query<ChatRoom>("{'_id' : '" + message.ChatRoomID + "'}");
                if (acountsC.Count > 0)
                {
                    ChatRoom chatRoom = acountsC[0] as ChatRoom;
                    chatRoom._State = 2;
                    response.IsSuccess = true;
                    response.Message = "聊天室删除成功";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "聊天室数据库异常";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室数据库异常");
                }
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室删除失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 加入和退出聊天室
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_JoinChatRoomHandler : AMRpcHandler<C2G_JoinChatRoom, G2C_JoinChatRoom>
    {
        protected override async void Run(Session session, C2G_JoinChatRoom message, Action<G2C_JoinChatRoom> reply)
        {
            G2C_JoinChatRoom response = new G2C_JoinChatRoom();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RelationInfo>("{'_Account' : '" + message.Account + "'}");
                var acountsC = await dBProxyComponent.Query<ChatRoom>("{'_Account' : '" + message.ChatRoomID + "'}");
                if (acounts.Count > 0)
                {
                    RelationInfo info = acounts[0] as RelationInfo;
                    ChatRoom infoC = acountsC[0] as ChatRoom;
                    if (message.Type == 1)
                    {
                        //加入聊天室
                        info._ChatRoomList.Add(message.ChatRoomID);
                        infoC._UserList.Add(message.Account);
                        response.IsSuccess = true;
                        response.Message = "添加聊天室编号成功";
                    }
                    else if (message.Type == 2)
                    {
                        //退出聊天室
                        info._ChatRoomList.Remove(message.ChatRoomID);
                        infoC._UserList.Remove(message.Account);
                        response.IsSuccess = true;
                        response.Message = "删除聊天室编号成功";
                    }

                    await dBProxyComponent.Save(info);

                    await dBProxyComponent.Save(infoC);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "聊天室数据库异常";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室数据库异常");
                }
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室加入或退出失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
    
    /// <summary>
    /// 修改聊天室
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_UpdateChatRoomHandler : AMRpcHandler<C2G_UpdateChatRoom, G2C_UpdateChatRoom>
    {
        protected override async void Run(Session session, C2G_UpdateChatRoom message, Action<G2C_UpdateChatRoom> reply)
        {
            G2C_UpdateChatRoom response = new G2C_UpdateChatRoom();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                var acountsC = await dBProxyComponent.Query<ChatRoom>("{'_id' : '" + message.ChatRoomID + "'}");
                if (acountsC.Count > 0)
                {
                    ChatRoom chatRoom = acountsC[0] as ChatRoom;
                    if (message.GroupType != 0)
                    {
                        chatRoom._GroupType = message.GroupType;
                    }
                    if (message.ManagePassword != "")
                    {
                        chatRoom._ManagePassword = message.ManagePassword;
                    }
                    if (message.PublicBorad != "")
                    {
                        chatRoom._PublicBorad = message.PublicBorad;
                    }
                    if (message.Name != "")
                    {
                        chatRoom._Name = message.Name;
                    }
                    response.IsSuccess = true;
                    response.Message = "聊天室修改成功";
                    
                    await dBProxyComponent.Save(chatRoom);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "聊天室数据库异常";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室数据库异常");
                }
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室修改失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 获取聊天室
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_QueryChatRoomHandler : AMRpcHandler<C2G_QueryChatRoom, G2C_QueryChatRoom>
    {
        protected override async void Run(Session session, C2G_QueryChatRoom message, Action<G2C_QueryChatRoom> reply)
        {
            G2C_QueryChatRoom response = new G2C_QueryChatRoom();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                var acountsC = await dBProxyComponent.Query<ChatRoom>("{'_id' : '" + message.ChatRoomID + "'}");
                if (acountsC.Count > 0)
                {
                    ChatRoom chatRoom = acountsC[0] as ChatRoom;
                    if (chatRoom._State == 2)
                    {
                        var acounts = await dBProxyComponent.Query<RelationInfo>("{'_id' : '" + message.Acount + "'}");
                        RelationInfo info = acounts[0] as RelationInfo;
                        info._ChatRoomList.Remove(message.ChatRoomID);

                        response.IsSuccess = true;
                        response.Message = "聊天室已删除退出聊天室成功";
                        await dBProxyComponent.Save(info);
                    }
                    else
                    {
                        response.GroupType = chatRoom._GroupType;
                        response.CreateDate = chatRoom._CreateDate;
                        response.ManagePassword = chatRoom._ManagePassword;
                        response.PublicBorad = chatRoom._PublicBorad;
                        response.Name = chatRoom._Name;
                        response.UserList = RepeatedFieldAndListChangeTool.ListToRepeatedField(chatRoom._UserList);
                        response.DateMessageIDList = RepeatedFieldAndListChangeTool.ListToRepeatedField(chatRoom._DateMessageIDList);

                        response.IsSuccess = true;
                        response.Message = "聊天室获取成功";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "聊天室数据库异常";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室数据库异常");
                }
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室获取失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
}