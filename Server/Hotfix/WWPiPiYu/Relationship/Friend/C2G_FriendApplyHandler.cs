using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建好友申请
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_FriendApplyHandler : AMRpcHandler<C2G_FriendApply, G2C_FriendApply>
    {
        protected override async void Run(Session session, C2G_FriendApply message, Action<G2C_FriendApply> reply)
        {
            G2C_FriendApply response = new G2C_FriendApply();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                
                if (message.Type == 1)
                {
                    RequestInfo relationInfo = ComponentFactory.Create<RequestInfo>();
                    relationInfo._InvAccountID = message.Account;
                    relationInfo._ByInvAccountID = message.ByAccount;
                    relationInfo._Note = message.Note;
                    relationInfo._ProcessDate = message.ProcessDate;
                    relationInfo._RequestDate = message.RequestDate;
                    relationInfo._RequestMessage = message.RequestMessage;
                    relationInfo._StateCode = message.StateCode;

                    await dBProxyComponent.Save(relationInfo);

                    response.IsSuccess = true;
                    response.Message = "好友申请创建成功";
                    response.RequestInfoid = relationInfo.Id;

                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "好友申请创建成功");
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
    /// 好友申请操作
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_FriendApplyOperationHandler : AMRpcHandler<C2G_FriendApplyOperation, G2C_FriendApplyOperation>
    {
        protected override async void Run(Session session, C2G_FriendApplyOperation message, Action<G2C_FriendApplyOperation> reply)
        {
            G2C_FriendApplyOperation response = new G2C_FriendApplyOperation();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RequestInfo>("{'_id' : '" + message.RequestInfoid + "'}");
                if (acounts.Count > 0)
                {
                    RequestInfo item = acounts[0] as RequestInfo;
                    item._StateCode = message.StateCode;

                    await dBProxyComponent.Save(item);
                    response.IsSuccess = true;
                    response.Message = "好友申请修改成功";
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
    /// 获取自身好友申请id总列
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_FriendApplyCountHandler : AMRpcHandler<C2G_FriendApplyCount, G2C_FriendApplyCount>
    {
        protected override async void Run(Session session, C2G_FriendApplyCount message, Action<G2C_FriendApplyCount> reply)
        {
            G2C_FriendApplyCount response = new G2C_FriendApplyCount();
            response.IsSuccess = false;
            List<long> Idlist = new List<long>();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                
                var acounts = await dBProxyComponent.Query<RequestInfo>("{'_InvAccountID' : '" + message.Account + "','_ByInvAccountID' : '"+ message.Account +"'}");
                if (acounts.Count > 0)
                {
                    foreach (RequestInfo item in acounts)
                    {
                        Idlist.Add(item.Id);
                    }
                    response.RequestInfoidlist = RepeatedFieldAndListChangeTool.ListToRepeatedField(Idlist);
                    response.IsSuccess = true;
                    response.Message = "获取好友申请id列表成功";
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
    /// 获取对应id的好友申请内容
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_FriendApplyContentHandler : AMRpcHandler<C2G_FriendApplyContent, G2C_FriendApplyContent>
    {
        protected override async void Run(Session session, C2G_FriendApplyContent message, Action<G2C_FriendApplyContent> reply)
        {
            G2C_FriendApplyContent response = new G2C_FriendApplyContent();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                
                var acounts = await dBProxyComponent.Query<RequestInfo>("{'_id' : '" + message.RequestInfoid + "'}");
                if (acounts.Count > 0)
                {
                    RequestInfo item = acounts[0] as RequestInfo;
                    response.Account = item._InvAccountID;
                    response.ByAccount = item._ByInvAccountID;
                    response.Note = item._Note;
                    response.ProcessDate = item._ProcessDate;
                    response.RequestDate = item._RequestDate;
                    response.RequestMessage = item._RequestMessage;
                    response.StateCode = item._StateCode;
                        
                    response.IsSuccess = true;
                    response.Message = "好友申请信息获取成功";
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