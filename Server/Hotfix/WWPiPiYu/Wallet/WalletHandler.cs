using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.WWPiPiYu.Wallet
{
    #region WalletData 钱包数据 增 改 查
    ///// <summary>
    ///// 创建账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_AddWalletDataHandler : AMRpcHandler<C2G_AddWalletData, G2C_AddWalletData>
    //{
    //    protected override async void Run(Session session, C2G_AddWalletData message, Action<G2C_AddWalletData> reply)
    //    {
    //        G2C_AddWalletData response = new G2C_AddWalletData();
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            WalletData WalletData = ComponentFactory.Create<WalletData>();

    //            WalletData._AccountID = message.AccountID;
    //            WalletData._PayPassword = message.PayPassword;
    //            WalletData._Diamond = message.Diamond;
    //            WalletData._Point = message.Point;
    //            WalletData._CreateDate = message.CreateDate;
    //            WalletData._WalletType = message.WalletType;
    //            WalletData._OffList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.OffList);
    //            WalletData._State = message.State;
    //            WalletData._WalletTipList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.WalletTipList);

    //            await dBProxyComponent.Save(WalletData);
    //            await dBProxyComponent.SaveLog(WalletData);
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
    //public class C2G_UpdateWalletDataHandler : AMRpcHandler<C2G_UpdateWalletData, G2C_UpdateWalletData>
    //{
    //    protected override async void Run(Session session, C2G_UpdateWalletData message, Action<G2C_UpdateWalletData> reply)
    //    {
    //        G2C_UpdateWalletData response = new G2C_UpdateWalletData();
    //        WalletData WalletData = null;
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            var acounts = await dBProxyComponent.Query<WalletData>("{ '_AccountID': " + message.AccountID + "}");

    //            if (acounts.Count <= 0)
    //            {
    //                //修改的时候查询不到说明数据库有问题
    //            }
    //            else
    //            {
    //                WalletData = acounts[0] as WalletData;

    //                WalletData._AccountID = message.AccountID;
    //                WalletData._PayPassword = message.PayPassword;
    //                WalletData._Diamond = message.Diamond;
    //                WalletData._Point = message.Point;
    //                WalletData._CreateDate = message.CreateDate;
    //                WalletData._WalletType = message.WalletType;
    //                WalletData._OffList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.OffList);
    //                WalletData._State = message.State;
    //                WalletData._WalletTipList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.WalletTipList);
    //            }

    //            await dBProxyComponent.Save(WalletData);
    //            await dBProxyComponent.SaveLog(WalletData);
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
    ///// 查询账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_QueryWalletDataHandler : AMRpcHandler<C2G_QueryWalletData, G2C_QueryWalletData>
    //{
    //    protected override async void Run(Session session, C2G_QueryWalletData message, Action<G2C_QueryWalletData> reply)
    //    {
    //        G2C_QueryWalletData response = new G2C_QueryWalletData();
    //        WalletData WalletData = null;
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            var acounts = await dBProxyComponent.Query<WalletData>("{ '_AccountID': " + message.AccountID + "}");

    //            if (acounts.Count <= 0)
    //            {
    //                WalletData Info = ComponentFactory.Create<WalletData>();
                    
    //                Info._PayPassword = "";
    //                Info._Diamond = 0;
    //                Info._Point = "";
    //                Info._CreateDate = "";
    //                Info._WalletType = 0;
    //                Info._OffList = new List<int>();
    //                Info._State = 0;
    //                Info._WalletTipList = new List<int>();

    //                await dBProxyComponent.Save(Info);
    //                await dBProxyComponent.SaveLog(Info);
    //            }
    //            else
    //            {
    //                WalletData = acounts[0] as WalletData;

    //                response.PayPassword = WalletData._PayPassword;
    //                response.Diamond = WalletData._Diamond;
    //                response.Point = WalletData._Point;
    //                response.CreateDate = WalletData._CreateDate;
    //                response.WalletType = WalletData._WalletType;
    //                response.OffList = RepeatedFieldAndListChangeTool.ListToRepeatedField(WalletData._OffList);
    //                response.State = WalletData._State;
    //                response.WalletTipList = RepeatedFieldAndListChangeTool.ListToRepeatedField(WalletData._WalletTipList);

    //            }

    //            await dBProxyComponent.Save(WalletData);
    //            await dBProxyComponent.SaveLog(WalletData);
    //            reply(response);
    //        }
    //        catch (Exception e)
    //        {
    //            response.Message = "数据库异常";
    //            ReplyError(response, e, reply);
    //        }
    //    }
    //}
    #endregion

    #region WalletRecord 钱包交易记录 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddWalletRecordHandler : AMRpcHandler<C2G_AddWalletRecord, G2C_AddWalletRecord>
    {
        protected override async void Run(Session session, C2G_AddWalletRecord message, Action<G2C_AddWalletRecord> reply)
        {
            G2C_AddWalletRecord response = new G2C_AddWalletRecord();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                WalletRecord WalletRecord = ComponentFactory.Create<WalletRecord>();

                WalletRecord._WalletID = message.WalletID;
                WalletRecord._Amount = message.Amount;
                WalletRecord._Info = message.Info;
                WalletRecord._CreateDate = message.CreateDate;
                WalletRecord._DealDate = message.DealDate;
                WalletRecord._SpeiaclInfo = message.SpeiaclInfo;
                WalletRecord._Type = message.Type;
                WalletRecord._State = message.State;

                await dBProxyComponent.Save(WalletRecord);
                await dBProxyComponent.SaveLog(WalletRecord);
                reply(response);
            }
            catch (Exception e)
            {
                //response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 修改账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateWalletRecordHandler : AMRpcHandler<C2G_UpdateWalletRecord, G2C_UpdateWalletRecord>
    {
        protected override async void Run(Session session, C2G_UpdateWalletRecord message, Action<G2C_UpdateWalletRecord> reply)
        {
            G2C_UpdateWalletRecord response = new G2C_UpdateWalletRecord();
            WalletRecord WalletRecord = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<WalletRecord>("{ '_AccountID': " + message.WalletID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                }
                else
                {
                    WalletRecord = acounts[0] as WalletRecord;

                    WalletRecord._WalletID = message.WalletID;
                    WalletRecord._Amount = message.Amount;
                    WalletRecord._Info = message.Info;
                    WalletRecord._CreateDate = message.CreateDate;
                    WalletRecord._DealDate = message.DealDate;
                    WalletRecord._SpeiaclInfo = message.SpeiaclInfo;
                    WalletRecord._Type = message.Type;
                    WalletRecord._State = message.State;
                }

                await dBProxyComponent.Save(WalletRecord);
                await dBProxyComponent.SaveLog(WalletRecord);
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
    public class C2G_QueryWalletRecordHandler : AMRpcHandler<C2G_QueryWalletRecord, G2C_QueryWalletRecord>
    {
        protected override async void Run(Session session, C2G_QueryWalletRecord message, Action<G2C_QueryWalletRecord> reply)
        {
            G2C_QueryWalletRecord response = new G2C_QueryWalletRecord();
            WalletRecord WalletRecord = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<WalletRecord>("{ '_AccountID': " + message.WalletID + "}");

                if (acounts.Count <= 0)
                {
                    WalletRecord Info = ComponentFactory.Create<WalletRecord>();
                    
                    Info._WalletID = message.WalletID;
                    Info._Amount = 0;
                    Info._Info = "";
                    Info._CreateDate = "";
                    Info._DealDate = "";
                    Info._SpeiaclInfo = "";
                    Info._Type = 0;
                    Info._State = 0;

                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    WalletRecord = acounts[0] as WalletRecord;

                    response.Amount = WalletRecord._Amount;
                    response.Info = WalletRecord._Info;
                    response.CreateDate = WalletRecord._CreateDate;
                    response.DealDate = WalletRecord._DealDate;
                    response.SpeiaclInfo = WalletRecord._SpeiaclInfo;
                    response.Type = WalletRecord._Type;
                    response.State = WalletRecord._State;


                }

                await dBProxyComponent.Save(WalletRecord);
                await dBProxyComponent.SaveLog(WalletRecord);
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