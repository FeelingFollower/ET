using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建聊天室消息内容
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_AddChatRoomMessageHandler : AMRpcHandler<C2G_AddChatRoomMessage, G2C_AddChatRoomMessage>
    {
        protected override async void Run(Session session, C2G_AddChatRoomMessage message, Action<G2C_AddChatRoomMessage> reply)
        {
            G2C_AddChatRoomMessage response = new G2C_AddChatRoomMessage();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                ChatRoomMessage relationInfo = ComponentFactory.Create<ChatRoomMessage>();
                relationInfo._InvAccountID = message.AccountID;
                relationInfo._SendDate = message.SendDate;
                relationInfo._ChatRoomID = message.ChatRoomID;
                relationInfo._Message = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Message);

                await dBProxyComponent.Save(relationInfo);

                //添加到对应的聊天室中，并广播给聊天室中的所有成员
                var acounts = await dBProxyComponent.Query<ChatRoom>("{ '_id': " + message.ChatRoomID + "}");
                if (acounts.Count > 0)
                {
                    ChatRoom chatRoom = acounts[0] as ChatRoom;
                    chatRoom._DateMessageIDList.Add(relationInfo._SendDate + "|" + relationInfo.Id);

                    foreach (long item in chatRoom._UserList)
                    {
                        //广播给所有该聊天室用户
                        Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "广播给：" + item);
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
    
    /// <summary>
    /// 查询聊天室消息内容
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_QueryChatRoomMessageHandler : AMRpcHandler<C2G_QueryChatRoomMessage, G2C_QueryChatRoomMessage>
    {
        protected override async void Run(Session session, C2G_QueryChatRoomMessage message, Action<G2C_QueryChatRoomMessage> reply)
        {
            G2C_QueryChatRoomMessage response = new G2C_QueryChatRoomMessage();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<ChatRoomMessage>("{ '_id': " + message.ChatRoomMessageID + "}");
                if (acounts.Count > 0)
                {
                    ChatRoomMessage user = acounts[0] as ChatRoomMessage;
                    response.MessageInfo = RepeatedFieldAndListChangeTool.ListToRepeatedField(user._Message);
                    response.AccountID = user._InvAccountID;
                    response.SendDate = user._SendDate;

                    response.IsSuccess = true;
                    response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室消息获取成功";
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "聊天室消息获取失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
}