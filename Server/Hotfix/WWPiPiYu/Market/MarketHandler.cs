using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.WWPiPiYu.Market
{
    #region GoodsOrder 商品预售订单 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddGoodsOrderHandler : AMRpcHandler<C2G_AddGoodsOrder, G2C_AddGoodsOrder>
    {
        protected override async void Run(Session session, C2G_AddGoodsOrder message, Action<G2C_AddGoodsOrder> reply)
        {
            G2C_AddGoodsOrder response = new G2C_AddGoodsOrder();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                GoodsOrder GoodsOrder = ComponentFactory.Create<GoodsOrder>();
                GoodsOrder._InvAccountID = message.InvAccountID;
                GoodsOrder._GoodsID = message.GoodsID;
                GoodsOrder._GoodsDataID = message.GoodsDataID;
                GoodsOrder._Price = message.Price;
                GoodsOrder._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                GoodsOrder._PublicTime = message.PublicTime;

                await dBProxyComponent.Save(GoodsOrder);
                await dBProxyComponent.SaveLog(GoodsOrder);
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
    public class C2G_UpdateGoodsOrderHandler : AMRpcHandler<C2G_UpdateGoodsOrder, G2C_UpdateGoodsOrder>
    {
        protected override async void Run(Session session, C2G_UpdateGoodsOrder message, Action<G2C_UpdateGoodsOrder> reply)
        {
            G2C_UpdateGoodsOrder response = new G2C_UpdateGoodsOrder();
            GoodsOrder GoodsOrder = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<GoodsOrder>("{ '_AccountID': " + message.InvAccountID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                }
                else
                {
                    GoodsOrder = acounts[0] as GoodsOrder;

                    GoodsOrder._InvAccountID = message.InvAccountID;
                    GoodsOrder._GoodsID = message.GoodsID;
                    GoodsOrder._GoodsDataID = message.GoodsDataID;
                    GoodsOrder._Price = message.Price;
                    GoodsOrder._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                    GoodsOrder._PublicTime = message.PublicTime;
                }

                await dBProxyComponent.Save(GoodsOrder);
                await dBProxyComponent.SaveLog(GoodsOrder);
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
    public class C2G_QueryGoodsOrderHandler : AMRpcHandler<C2G_QueryGoodsOrder, G2C_QueryGoodsOrder>
    {
        protected override async void Run(Session session, C2G_QueryGoodsOrder message, Action<G2C_QueryGoodsOrder> reply)
        {
            G2C_QueryGoodsOrder response = new G2C_QueryGoodsOrder();
            GoodsOrder GoodsOrder = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<GoodsOrder>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    GoodsOrder Info = ComponentFactory.Create<GoodsOrder>();
                    
                    Info._InvAccountID = message.AccountID;
                    Info._GoodsID = 0;
                    Info._GoodsDataID = 0;
                    Info._Price = 0;
                    Info._Intrduce = new List<string>();
                    Info._PublicTime = "";


                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    GoodsOrder = acounts[0] as GoodsOrder;

                    response.GoodsID = GoodsOrder._GoodsID;
                    response.GoodsDataID = GoodsOrder._GoodsDataID;
                    response.Price = GoodsOrder._Price;
                    response.Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(GoodsOrder._Intrduce);
                    response.PublicTime = GoodsOrder._PublicTime;

                }

                await dBProxyComponent.Save(GoodsOrder);
                await dBProxyComponent.SaveLog(GoodsOrder);
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
