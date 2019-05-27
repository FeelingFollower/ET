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
    public class C2G_AddWalletDataHandler : AMRpcHandler<C2G_AddWalletData, G2C_AddWalletData>
    {
        protected override async void Run(Session session, C2G_AddWalletData message, Action<G2C_AddWalletData> reply)
        {
            G2C_AddWalletData response = new G2C_AddWalletData();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();


                WalletData WalletData = ComponentFactory.Create<WalletData>();

                WalletData._AccountID = message.AccountID;
                WalletData._PayPassword = message.PayPassword;
                WalletData._Diamond = message.Diamond;
                WalletData._Point = message.Point;
                WalletData._CreateDate = message.CreateDate;
                WalletData._WalletType = message.WalletType;
                WalletData._OffList = new List<int>();
                WalletData._State = 1;
                WalletData._WalletTipList = new List<int>();

                await dBProxyComponent.Save(WalletData);
                await dBProxyComponent.SaveLog(WalletData);
                response.IsSuccess = true;

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "创建钱包数据失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询钱包数据
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_QueryWalletDateHandler : AMRpcHandler<C2G_QueryWalletData, G2C_QueryWalletData>
    {
        protected override async void Run(Session session, C2G_QueryWalletData message, Action<G2C_QueryWalletData> reply)
        {
            G2C_QueryWalletData response = new G2C_QueryWalletData();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<WalletData>("{ '_AccountID': " + message.AccountID + "}");
                if (acounts.Count > 0)
                {
                    WalletData user = acounts[0] as WalletData;
                    response.PayPassword = user._PayPassword;
                    response.CreateDate = user._CreateDate;
                    response.Diamond = user._Diamond;
                    response.Point = user._Point;
                    response.WalletType = user._WalletType;
                    response.OffList = RepeatedFieldAndListChangeTool.ListToRepeatedField(user._OffList);
                    response.State = user._State;
                    response.WalletTipList = RepeatedFieldAndListChangeTool.ListToRepeatedField(user._WalletTipList);

                    response.IsSuccess = true;
                    response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "钱包数据获取成功";
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "钱包数据获取失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
    
    /// <summary>
    /// 修改钱包数据
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateWalletDataHandler : AMRpcHandler<C2G_UpdateWalletData, G2C_UpdateWalletData>
    {
        protected override async void Run(Session session, C2G_UpdateWalletData message, Action<G2C_UpdateWalletData> reply)
        {
            G2C_UpdateWalletData response = new G2C_UpdateWalletData();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<WalletData>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count > 0)
                {
                    WalletData WalletData = acounts[0] as WalletData;

                    if (message.PayPassword != "")
                    {
                        WalletData._PayPassword = message.PayPassword;
                    }
                    if (message.WalletType != 0)
                    {
                        WalletData._WalletType = message.WalletType;
                    }
                    if (message.OffList != 0)
                    {
                        WalletData._OffList.Add(message.OffList);
                    }
                    if (message.State != 0)
                    {
                        WalletData._State = message.State;
                    }
                    if (message.WalletTipList != 0)
                    {
                        WalletData._WalletTipList.Add(message.WalletTipList);
                    }
                    response.IsSuccess = true;

                    await dBProxyComponent.Save(WalletData);
                    await dBProxyComponent.SaveLog(WalletData);
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "钱包数据获取失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
}