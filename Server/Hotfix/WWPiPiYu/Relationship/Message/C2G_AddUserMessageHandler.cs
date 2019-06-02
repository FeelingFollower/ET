using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建好友消息内容
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_AddUserMessageHandler : AMRpcHandler<C2G_AddUserMessage, G2C_AddUserMessage>
    {
        protected override async void Run(Session session, C2G_AddUserMessage message, Action<G2C_AddUserMessage> reply)
        {
            G2C_AddUserMessage response = new G2C_AddUserMessage();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                UserMessage relationInfo = ComponentFactory.Create<UserMessage>();
                relationInfo._ByInvAccountID = message.ByAccount;
                relationInfo._InvAccountID = message.Account;
                relationInfo._SendDate = message.SendDate;
                relationInfo._State = 1;
                relationInfo._Message = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Message);

                await dBProxyComponent.Save(relationInfo);

                //TODO 如果对方在线就添加好友信息的聊天日期数据ID列表并提醒用户，如果对方不在线则添加到留言消息列表


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
    /// 查询好友消息内容
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_QueryUserMessageHandler : AMRpcHandler<C2G_QueryUserMessage, G2C_QueryUserMessage>
    {
        protected override async void Run(Session session, C2G_QueryUserMessage message, Action<G2C_QueryUserMessage> reply)
        {
            G2C_QueryUserMessage response = new G2C_QueryUserMessage();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserMessage>("{ '_id': " + message.UserMessageID + "}");
                if (acounts.Count>0)
                {
                    UserMessage user = acounts[0] as UserMessage;
                    response.MessageInfo = RepeatedFieldAndListChangeTool.ListToRepeatedField(user._Message);
                    response.ByAccountID = user._ByInvAccountID;
                    response.AccountID = user._InvAccountID;
                    response.SendDate = user._SendDate;

                    response.IsSuccess = true;
                    response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "好友消息获取成功";
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