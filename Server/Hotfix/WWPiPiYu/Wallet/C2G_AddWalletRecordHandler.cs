using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 创建钱包数据
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_AddWalletRecordHandler : AMRpcHandler<C2G_AddWalletRecord, G2C_AddWalletRecord>
    {
        protected override async void Run(Session session, C2G_AddWalletRecord message, Action<G2C_AddWalletRecord> reply)
        {
            G2C_AddWalletRecord response = new G2C_AddWalletRecord();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();


                WalletRecord WalletData = ComponentFactory.Create<WalletRecord>();

                WalletData._Amount = message.Amount;
                WalletData._CreateDate = message.CreateDate;
                WalletData._DealDate = message.DealDate;
                WalletData._Info = message.Info;
                WalletData._SpeiaclInfo = message.SpeiaclInfo;
                WalletData._State = message.State;
                WalletData._Type = message.Type;
                WalletData._WalletID = message.WalletID;

                await dBProxyComponent.Save(WalletData);
                await dBProxyComponent.SaveLog(WalletData);
                response.IsSuccess = true;

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "创建钱包交易记录失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询钱包数据
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_QueryWalletRecordHandler : AMRpcHandler<C2G_QueryWalletRecord, G2C_QueryWalletRecord>
    {
        protected override async void Run(Session session, C2G_QueryWalletRecord message, Action<G2C_QueryWalletRecord> reply)
        {
            G2C_QueryWalletRecord response = new G2C_QueryWalletRecord();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<WalletRecord>("{ '_WalletID': " + message.WalletID + "}");
                if (acounts.Count > 0)
                {
                    WalletRecord user = acounts[0] as WalletRecord;
                    response.Amount = user._Amount;
                    response.CreateDate = user._CreateDate;
                    response.Info = user._Info;
                    response.DealDate = user._DealDate;
                    response.SpeiaclInfo = user._SpeiaclInfo;
                    response.Type = user._Type;
                    response.State = user._State;

                    response.IsSuccess = true;
                    response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "钱包交易记录获取成功";
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "钱包交易记录获取失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
}