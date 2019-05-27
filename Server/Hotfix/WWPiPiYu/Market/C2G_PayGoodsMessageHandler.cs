using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;


namespace ETHotfix
{
    /// <summary>
    /// 购买商品
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_PayGoodsMessageHandler : AMRpcHandler<C2G_PayGoods, G2C_PayGoods>
    {
        protected override async void Run(Session session, C2G_PayGoods message, Action<G2C_PayGoods> reply)
        {
            G2C_PayGoods response = new G2C_PayGoods();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                //1从缓存中查询商品信息，先暂时用查数据库的方式查询
                var acounts = await dBProxyComponent.Query<GoodsInfo>("{ '_GoodsInfoID': " + message.GoodsID + "}");

                GoodsInfo goodsInfo = acounts[0] as GoodsInfo;

                //2生成订单
                GoodsInfoOrder relationInfo = ComponentFactory.Create<GoodsInfoOrder>();
                relationInfo._AccountID = message.AccountID;
                relationInfo._Count = message.Count;
                relationInfo._CreateDate = DateTime.Now.ToString("yyyy-hh-dd HH:mm:ss");
                relationInfo._DealDate = "";
                relationInfo._GoodsInfoID = message.GoodsID;
                relationInfo._OrderID = relationInfo.Id;
                relationInfo._PayState = 0;
                relationInfo._PayType = message.PayType;
                relationInfo._Price = message.Count * goodsInfo._Price;

                //3信息合并发送给对应的支付平台

                //4接受交易平台的交易代码

                relationInfo._PlatformOrder = "";
                string PayCode = "";
                //5回执交易代码
                response.PayCode = PayCode;

                await dBProxyComponent.Save(relationInfo);

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "购买商品失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 接收平台http协议
    /// </summary>
    [MessageHandler(AppType.Realm)]
    public class C2G_PayHttpMessageHandler : AMRpcHandler<C2G_PayHttpMessage, G2C_PayHttpMessage>
    {
        protected override async void Run(Session session, C2G_PayHttpMessage message, Action<G2C_PayHttpMessage> reply)
        {
            G2C_PayHttpMessage response = new G2C_PayHttpMessage();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                //解密平台回执信息，获取用户是否支付成功
                string htto = message.PayHttp;
                bool IsSuccess = false;
                var acounts = await dBProxyComponent.Query<GoodsInfoOrder>("{ '_OrderID': " + message.GoodsInfoOrderid + "}");

                if (acounts.Count > 0)
                {
                    GoodsInfoOrder order = acounts[0] as GoodsInfoOrder;
                    if (IsSuccess)
                    {
                        //支付成功
                        order._DealDate = DateTime.Now.ToString("yyyy-hh-dd HH:mm:ss");
                        order._PayState = 1;

                        //给用户发送商品  TODO
                    }
                    else
                    {
                        //支付失败或退款
                        if (order._PayState == 1)
                        {
                            //退款
                            order._DealDate = DateTime.Now.ToString("yyyy-hh-dd HH:mm:ss");
                            order._PayState = 3;

                            //回收用户商品 并发送退款信息给对应平台进行退款  TODO
                        }
                        else if (order._PayState == 0)
                        {
                            //支付失败
                            order._DealDate = DateTime.Now.ToString("yyyy-hh-dd HH:mm:ss");
                            order._PayState = 2;

                            //回执用户支付失败的提醒  TODO
                        }
                    }
                    await dBProxyComponent.Save(order);
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "接收平台http协议失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
}