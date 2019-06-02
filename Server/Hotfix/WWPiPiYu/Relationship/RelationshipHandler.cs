using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.WWPiPiYu.Relationship
{
    #region ChatRoom 聊天室 增 改 查
    ///// <summary>
    ///// 创建账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_AddChatRoomHandler : AMRpcHandler<C2G_AddChatRoom, G2C_AddChatRoom>
    //{
    //    protected override async void Run(Session session, C2G_AddChatRoom message, Action<G2C_AddChatRoom> reply)
    //    {
    //        G2C_AddChatRoom response = new G2C_AddChatRoom();
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            ChatRoom ChatRoom = ComponentFactory.Create<ChatRoom>();
    //            ChatRoom._InvAccountID = message.InvAccountID;
    //            ChatRoom._ManagePassword = message.ManagePassword;
    //            ChatRoom._PublicBorad = message.PublicBorad;
    //            ChatRoom._Name = message.Name;
    //            ChatRoom._UserList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.UserList);
    //            ChatRoom._CreateDate = message.CreateDate;
    //            ChatRoom._GroupType = message.GroupType;
    //            ChatRoom._State = message.State;

    //            await dBProxyComponent.Save(ChatRoom);
    //            await dBProxyComponent.SaveLog(ChatRoom);
    //            reply(response);
    //        }
    //        catch (Exception e)
    //        {
    //            response.IsOk = false;
    //            response.Message = "数据库异常";
    //            ReplyError(response, e, reply);
    //        }
    //    }
    //}

    ///// <summary>
    ///// 修改账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_UpdateChatRoomHandler : AMRpcHandler<C2G_UpdateChatRoom, G2C_UpdateChatRoom>
    //{
    //    protected override async void Run(Session session, C2G_UpdateChatRoom message, Action<G2C_UpdateChatRoom> reply)
    //    {
    //        G2C_UpdateChatRoom response = new G2C_UpdateChatRoom();
    //        ChatRoom ChatRoom = null;
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            var acounts = await dBProxyComponent.Query<ChatRoom>("{ '_AccountID': " + message.InvAccountID + "}");

    //            if (acounts.Count <= 0)
    //            {
    //                //修改的时候查询不到说明数据库有问题
    //            }
    //            else
    //            {
    //                ChatRoom = acounts[0] as ChatRoom;

    //                ChatRoom._InvAccountID = message.InvAccountID;
    //                ChatRoom._ManagePassword = message.ManagePassword;
    //                ChatRoom._PublicBorad = message.PublicBorad;
    //                ChatRoom._Name = message.Name;
    //                ChatRoom._UserList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.UserList);
    //                ChatRoom._CreateDate = message.CreateDate;
    //                ChatRoom._GroupType = message.GroupType;
    //                ChatRoom._State = message.State;
    //            }

    //            await dBProxyComponent.Save(ChatRoom);
    //            await dBProxyComponent.SaveLog(ChatRoom);
    //            reply(response);
    //        }
    //        catch (Exception e)
    //        {
    //            response.IsOk = false;
    //            response.Message = "数据库异常";
    //            ReplyError(response, e, reply);
    //        }
    //    }
    //}

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryChatRoomHandler : AMRpcHandler<C2G_QueryChatRoom, G2C_QueryChatRoom>
    {
        protected override async void Run(Session session, C2G_QueryChatRoom message, Action<G2C_QueryChatRoom> reply)
        {
            G2C_QueryChatRoom response = new G2C_QueryChatRoom();
            ChatRoom ChatRoom = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<ChatRoom>("{ '_AccountID': " + message.Acount + "}");

                if (acounts.Count <= 0)
                {
                    ChatRoom Info = ComponentFactory.Create<ChatRoom>();
                    
                    Info._InvAccountID = message.Acount;
                    Info._ManagePassword = "";
                    Info._PublicBorad = "";
                    Info._Name = "";
                    Info._UserList = new List<long>();
                    Info._CreateDate = "";
                    Info._GroupType = 0;
                    Info._State = 0;


                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    ChatRoom = acounts[0] as ChatRoom;
                    
                    response.ManagePassword = ChatRoom._ManagePassword;
                    response.PublicBorad = ChatRoom._PublicBorad;
                    response.Name = ChatRoom._Name;
                    response.UserList = RepeatedFieldAndListChangeTool.ListToRepeatedField(ChatRoom._UserList);
                    response.CreateDate = ChatRoom._CreateDate;
                    response.GroupType = ChatRoom._GroupType;
                    //response.State = ChatRoom._State;


                }

                await dBProxyComponent.Save(ChatRoom);
                await dBProxyComponent.SaveLog(ChatRoom);
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion



    #region FriendInfo 好友消息 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddFriendInfoHandler : AMRpcHandler<C2G_AddFriendInfo, G2C_AddFriendInfo>
    {
        protected override async void Run(Session session, C2G_AddFriendInfo message, Action<G2C_AddFriendInfo> reply)
        {
            G2C_AddFriendInfo response = new G2C_AddFriendInfo();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                FriendInfo FriendInfo = ComponentFactory.Create<FriendInfo>();
                FriendInfo._InvAccountID = message.InvAccountID;
                FriendInfo._ByInvAccountID = message.ByInvAccountID;
                //FriendInfo._DateMessageIDList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.DateMessageIDList);
                FriendInfo._LastDate = message.LastDate;
                //FriendInfo._LiveMassageList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.LiveMassageList);
                //FriendInfo._GroupNumber = message.GroupNumber;

                await dBProxyComponent.Save(FriendInfo);
                await dBProxyComponent.SaveLog(FriendInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 修改账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateFriendInfoHandler : AMRpcHandler<C2G_UpdateFriendInfo, G2C_UpdateFriendInfo>
    {
        protected override async void Run(Session session, C2G_UpdateFriendInfo message, Action<G2C_UpdateFriendInfo> reply)
        {
            G2C_UpdateFriendInfo response = new G2C_UpdateFriendInfo();
            FriendInfo FriendInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<FriendInfo>("{ '_AccountID': " + message.InvAccountID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                }
                else
                {
                    FriendInfo = acounts[0] as FriendInfo;

                    FriendInfo._InvAccountID = message.InvAccountID;
                    FriendInfo._ByInvAccountID = message.ByInvAccountID;
                    //FriendInfo._DateMessageIDList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.DateMessageIDList);
                    FriendInfo._LastDate = message.LastDate;
                    //FriendInfo._LiveMassageList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.LiveMassageList);
                    //FriendInfo._GroupNumber = message.GroupNumber;
                }

                await dBProxyComponent.Save(FriendInfo);
                await dBProxyComponent.SaveLog(FriendInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryFriendInfoHandler : AMRpcHandler<C2G_QueryFriendInfo, G2C_QueryFriendInfo>
    {
        protected override async void Run(Session session, C2G_QueryFriendInfo message, Action<G2C_QueryFriendInfo> reply)
        {
            G2C_QueryFriendInfo response = new G2C_QueryFriendInfo();
            FriendInfo FriendInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<FriendInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    FriendInfo Info = ComponentFactory.Create<FriendInfo>();
                    
                    Info._InvAccountID = message.AccountID;
                    Info._ByInvAccountID = 0;
                    Info._DateMessageIDList = new List<string>();
                    Info._LastDate = "";
                    Info._LiveMassageList = new List<string>();
                    Info._GroupNumber = 0;


                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    FriendInfo = acounts[0] as FriendInfo;
                    
                    response.ByInvAccountID = FriendInfo._ByInvAccountID;
                    //response.DateMessageIDList = RepeatedFieldAndListChangeTool.ListToRepeatedField(FriendInfo._DateMessageIDList);
                    response.LastDate = FriendInfo._LastDate;
                    //response.LiveMassageList = RepeatedFieldAndListChangeTool.ListToRepeatedField(FriendInfo._LiveMassageList);
                    //response.GroupNumber = FriendInfo._GroupNumber;



                }

                await dBProxyComponent.Save(FriendInfo);
                await dBProxyComponent.SaveLog(FriendInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion



    #region GroupInfo 群组信息 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddGroupInfoHandler : AMRpcHandler<C2G_AddGroupInfo, G2C_AddGroupInfo>
    {
        protected override async void Run(Session session, C2G_AddGroupInfo message, Action<G2C_AddGroupInfo> reply)
        {
            G2C_AddGroupInfo response = new G2C_AddGroupInfo();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                GroupInfo GroupInfo = ComponentFactory.Create<GroupInfo>();
                GroupInfo._InvAccountID = message.InvAccountID;
                GroupInfo._GroupPassword = message.GroupPassword;
                GroupInfo._ColorCode = message.ColorCode;
                GroupInfo._Name = message.Name;
                GroupInfo._CreateDate = message.CreateDate;
                GroupInfo._GroupNumber = message.GroupNumber;
                GroupInfo._UseState = message.UseState;

                await dBProxyComponent.Save(GroupInfo);
                await dBProxyComponent.SaveLog(GroupInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 修改账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateGroupInfoHandler : AMRpcHandler<C2G_UpdateGroupInfo, G2C_UpdateGroupInfo>
    {
        protected override async void Run(Session session, C2G_UpdateGroupInfo message, Action<G2C_UpdateGroupInfo> reply)
        {
            G2C_UpdateGroupInfo response = new G2C_UpdateGroupInfo();
            GroupInfo GroupInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<GroupInfo>("{ '_AccountID': " + message.InvAccountID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                }
                else
                {
                    GroupInfo = acounts[0] as GroupInfo;

                    GroupInfo._InvAccountID = message.InvAccountID;
                    GroupInfo._GroupPassword = message.GroupPassword;
                    GroupInfo._ColorCode = message.ColorCode;
                    GroupInfo._Name = message.Name;
                    GroupInfo._CreateDate = message.CreateDate;
                    GroupInfo._GroupNumber = message.GroupNumber;
                    GroupInfo._UseState = message.UseState;
                }

                await dBProxyComponent.Save(GroupInfo);
                await dBProxyComponent.SaveLog(GroupInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryGroupInfoHandler : AMRpcHandler<C2G_QueryGroupInfo, G2C_QueryGroupInfo>
    {
        protected override async void Run(Session session, C2G_QueryGroupInfo message, Action<G2C_QueryGroupInfo> reply)
        {
            G2C_QueryGroupInfo response = new G2C_QueryGroupInfo();
            GroupInfo GroupInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<GroupInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    GroupInfo Info = ComponentFactory.Create<GroupInfo>();
                    
                    Info._InvAccountID = message.AccountID;
                    Info._GroupPassword = "";
                    Info._ColorCode = "";
                    Info._Name = "";
                    Info._CreateDate = "";
                    Info._GroupNumber = 0;
                    Info._UseState = 0;

                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    GroupInfo = acounts[0] as GroupInfo;
                    
                    response.GroupPassword = GroupInfo._GroupPassword;
                    response.ColorCode = GroupInfo._ColorCode;
                    response.Name = GroupInfo._Name;
                    response.CreateDate = GroupInfo._CreateDate;
                    response.UseState = GroupInfo._UseState;




                }

                await dBProxyComponent.Save(GroupInfo);
                await dBProxyComponent.SaveLog(GroupInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion



    #region RelationInfo 好友列表 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddRelationInfoHandler : AMRpcHandler<C2G_AddRelationInfo, G2C_AddRelationInfo>
    {
        protected override async void Run(Session session, C2G_AddRelationInfo message, Action<G2C_AddRelationInfo> reply)
        {
            G2C_AddRelationInfo response = new G2C_AddRelationInfo();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                RelationInfo RelationInfo = ComponentFactory.Create<RelationInfo>();

                RelationInfo._AccountID = message.InvAccountID;
                RelationInfo._InfoID = message.InfoID;
                RelationInfo._FriendIDList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.FriendIDList);
                RelationInfo._BlackIDList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.BlackIDList);
                RelationInfo._GroupList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.GroupList);
                RelationInfo._ChatRoomList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ChatRoomList);

                await dBProxyComponent.Save(RelationInfo);
                await dBProxyComponent.SaveLog(RelationInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 修改账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateRelationInfoHandler : AMRpcHandler<C2G_UpdateRelationInfo, G2C_UpdateRelationInfo>
    {
        protected override async void Run(Session session, C2G_UpdateRelationInfo message, Action<G2C_UpdateRelationInfo> reply)
        {
            G2C_UpdateRelationInfo response = new G2C_UpdateRelationInfo();
            RelationInfo RelationInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RelationInfo>("{ '_AccountID': " + message.InvAccountID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                }
                else
                {
                    RelationInfo = acounts[0] as RelationInfo;

                    RelationInfo._AccountID = message.InvAccountID;
                    RelationInfo._InfoID = message.InfoID;
                    RelationInfo._FriendIDList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.FriendIDList);
                    RelationInfo._BlackIDList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.BlackIDList);
                    RelationInfo._GroupList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.GroupList);
                    RelationInfo._ChatRoomList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ChatRoomList);
                }

                await dBProxyComponent.Save(RelationInfo);
                await dBProxyComponent.SaveLog(RelationInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryRelationInfoHandler : AMRpcHandler<C2G_QueryRelationInfo, G2C_QueryRelationInfo>
    {
        protected override async void Run(Session session, C2G_QueryRelationInfo message, Action<G2C_QueryRelationInfo> reply)
        {
            G2C_QueryRelationInfo response = new G2C_QueryRelationInfo();
            RelationInfo RelationInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RelationInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    RelationInfo Info = ComponentFactory.Create<RelationInfo>();

                    Info._AccountID = message.AccountID;
                    Info._InfoID = 0;
                    Info._FriendIDList = new List<long>();
                    Info._BlackIDList = new List<long>();
                    Info._GroupList = new List<long>();
                    Info._ChatRoomList = new List<long>();

                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    RelationInfo = acounts[0] as RelationInfo;

                    response.InfoID = RelationInfo._InfoID;
                    response.FriendIDList = RepeatedFieldAndListChangeTool.ListToRepeatedField(RelationInfo._FriendIDList);
                    response.BlackIDList = RepeatedFieldAndListChangeTool.ListToRepeatedField(RelationInfo._BlackIDList);
                    response.GroupList = RepeatedFieldAndListChangeTool.ListToRepeatedField(RelationInfo._GroupList);
                    response.ChatRoomList = RepeatedFieldAndListChangeTool.ListToRepeatedField(RelationInfo._ChatRoomList);
                    
                }

                await dBProxyComponent.Save(RelationInfo);
                await dBProxyComponent.SaveLog(RelationInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion



    #region RequestInfo 好友申请 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddRequestInfoHandler : AMRpcHandler<C2G_AddRequestInfo, G2C_AddRequestInfo>
    {
        protected override async void Run(Session session, C2G_AddRequestInfo message, Action<G2C_AddRequestInfo> reply)
        {
            G2C_AddRequestInfo response = new G2C_AddRequestInfo();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                RequestInfo RequestInfo = ComponentFactory.Create<RequestInfo>();

                RequestInfo._InvAccountID = message.InvAccountID;
                RequestInfo._ByInvAccountID = message.ByInvAccountID;
                RequestInfo._RequestMessage = message.RequestMessage;
                RequestInfo._Note = message.Note;
                RequestInfo._StateCode = message.StateCode;
                RequestInfo._RequestDate = message.RequestDate;
                RequestInfo._ProcessDate = message.ProcessDate;

                await dBProxyComponent.Save(RequestInfo);
                await dBProxyComponent.SaveLog(RequestInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 修改账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateRequestInfoHandler : AMRpcHandler<C2G_UpdateRequestInfo, G2C_UpdateRequestInfo>
    {
        protected override async void Run(Session session, C2G_UpdateRequestInfo message, Action<G2C_UpdateRequestInfo> reply)
        {
            G2C_UpdateRequestInfo response = new G2C_UpdateRequestInfo();
            RequestInfo RequestInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RequestInfo>("{ '_AccountID': " + message.InvAccountID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                }
                else
                {
                    RequestInfo = acounts[0] as RequestInfo;

                    RequestInfo._InvAccountID = message.InvAccountID;
                    RequestInfo._ByInvAccountID = message.ByInvAccountID;
                    RequestInfo._RequestMessage = message.RequestMessage;
                    RequestInfo._Note = message.Note;
                    RequestInfo._StateCode = message.StateCode;
                    RequestInfo._RequestDate = message.RequestDate;
                    RequestInfo._ProcessDate = message.ProcessDate;
                }

                await dBProxyComponent.Save(RequestInfo);
                await dBProxyComponent.SaveLog(RequestInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryRequestInfoHandler : AMRpcHandler<C2G_QueryRequestInfo, G2C_QueryRequestInfo>
    {
        protected override async void Run(Session session, C2G_QueryRequestInfo message, Action<G2C_QueryRequestInfo> reply)
        {
            G2C_QueryRequestInfo response = new G2C_QueryRequestInfo();
            RequestInfo RequestInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<RequestInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    RequestInfo Info = ComponentFactory.Create<RequestInfo>();

                    Info._InvAccountID = message.AccountID;
                    Info._ByInvAccountID = 0;
                    Info._RequestMessage = "";
                    Info._Note = "";
                    Info._StateCode = 0;
                    Info._RequestDate = "";
                    Info._ProcessDate = "";

                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    RequestInfo = acounts[0] as RequestInfo;

                    response.ByInvAccountID = RequestInfo._ByInvAccountID;
                    response.RequestMessage = RequestInfo._RequestMessage;
                    response.Note = RequestInfo._Note;
                    response.StateCode = RequestInfo._StateCode;
                    response.RequestDate = RequestInfo._RequestDate;
                    response.ProcessDate = RequestInfo._ProcessDate;

                }

                await dBProxyComponent.Save(RequestInfo);
                await dBProxyComponent.SaveLog(RequestInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion




}
