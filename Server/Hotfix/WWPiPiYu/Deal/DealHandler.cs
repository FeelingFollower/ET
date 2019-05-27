using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.WWPiPiYu.Deal
{

    #region DealOrder 交易订单 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddDealOrderHandler : AMRpcHandler<C2G_AddDealOrder, G2C_AddDealOrder>
    {
        protected override async void Run(Session session, C2G_AddDealOrder message, Action<G2C_AddDealOrder> reply)
        {
            G2C_AddDealOrder response = new G2C_AddDealOrder();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                DealOrder DealOrder = ComponentFactory.Create<DealOrder>();
                DealOrder._InvAccountID = message.InvAccountID;
                DealOrder._ByInvAccountID = message.ByInvAccountID;
                DealOrder._ProductID = message.ProductID;
                DealOrder._InvItemList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.InvItemList);
                DealOrder._InvProductPoint = message.InvProductPoint;
                DealOrder._InvProductDiamond = message.InvProductDiamond;
                DealOrder._InvItemList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ByInvItemList);
                DealOrder._ByInvProductPoint = message.ByInvProductPoint;
                DealOrder._ByInvProductDiamond = message.ByInvProductDiamond;
                DealOrder._InvItemList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.MessageList);
                DealOrder._CreateDate = message.CreateDate;
                DealOrder._DealDate = message.DealDate;
                DealOrder._InvIP = message.InvIP;
                DealOrder._ByInvIP = message.ByInvIP;
                DealOrder._State = message.State;

                await dBProxyComponent.Save(DealOrder);
                await dBProxyComponent.SaveLog(DealOrder);
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
    public class C2G_UpdateDealOrderHandler : AMRpcHandler<C2G_UpdateDealOrder, G2C_UpdateDealOrder>
    {
        protected override async void Run(Session session, C2G_UpdateDealOrder message, Action<G2C_UpdateDealOrder> reply)
        {
            G2C_UpdateDealOrder response = new G2C_UpdateDealOrder();
            DealOrder DealOrder = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<DealOrder>("{ '_AccountID': " + message.ByInvAccountID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                }
                else
                {
                    DealOrder = acounts[0] as DealOrder;

                    if (message.ByInvAccountID != -1)
                    {
                        DealOrder._ByInvAccountID = message.ByInvAccountID;
                    }
                    if (message.ProductID != -1)
                    {
                        DealOrder._ProductID = message.ProductID;
                    }
                    if (message.InvItemList.Count != 0)
                    {
                        DealOrder._InvItemList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.InvItemList);
                    }
                    if (message.InvProductPoint != -1)
                    {
                        DealOrder._InvProductPoint = message.InvProductPoint;
                    }
                    if (message.InvProductDiamond != -1)
                    {
                        DealOrder._InvProductDiamond = message.InvProductDiamond;
                    }
                    if (message.ByInvItemList.count != 0)
                    {
                        DealOrder._InvItemList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ByInvItemList);
                    }
                    if (message.ByInvProductPoint != -1)
                    {
                        DealOrder._ByInvProductPoint = message.ByInvProductPoint;
                    }
                    if (message.ByInvProductDiamond != -1)
                    {
                        DealOrder._ByInvProductDiamond = message.ByInvProductDiamond;
                    }
                    if (message.MessageList.count != 0)
                    {
                        DealOrder._InvItemList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.MessageList);
                    }
                    if (message.CreateDate != "")
                    {
                        DealOrder._CreateDate = message.CreateDate;
                    }
                    if (message.DealDate != "")
                    {
                        DealOrder._DealDate = message.DealDate;
                    }
                    if (message.InvIP != "")
                    {
                        DealOrder._InvIP = message.InvIP;
                    }
                    if (message.ByInvIP != "")
                    {
                        DealOrder._ByInvIP = message.ByInvIP;
                    }
                    if (message.State != -1)
                    {
                        DealOrder._State = message.State;
                    }
                }

                await dBProxyComponent.Save(DealOrder);
                await dBProxyComponent.SaveLog(DealOrder);
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
    public class C2G_QueryDealOrderHandler : AMRpcHandler<C2G_QueryDealOrder, G2C_QueryDealOrder>
    {
        protected override async void Run(Session session, C2G_QueryDealOrder message, Action<G2C_QueryDealOrder> reply)
        {
            G2C_QueryDealOrder response = new G2C_QueryDealOrder();
            DealOrder DealOrder = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<DealOrder>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    //查询不到不创建
                }
                else
                {
                    for (int i = 0; i < acounts.Count; i++)
                    {
                        DealOrder = acounts[i] as DealOrder;

                        Dealorder record = new Dealorder();
                        record.ByInvAccountID = DealOrder._ByInvAccountID;
                        record.ProductID = DealOrder._ProductID;
                        record.InvItemList = RepeatedFieldAndListChangeTool.ListToRepeatedField( DealOrder._InvItemList);
                        record.InvProductPoint = DealOrder._InvProductPoint;
                        record.InvProductDiamond = DealOrder._InvProductDiamond;
                        record.ByInvItemList = RepeatedFieldAndListChangeTool.ListToRepeatedField(DealOrder._ByInvItemList);
                        record.ByInvProductPoint = DealOrder._ByInvProductPoint;
                        record.ByInvProductDiamond = DealOrder._ByInvProductDiamond;
                        record.MessageList = RepeatedFieldAndListChangeTool.ListToRepeatedField(DealOrder._MessageList);
                        record.CreateDate = DealOrder._CreateDate;
                        record.DealDate = DealOrder._DealDate;
                        record.InvIP = DealOrder._InvIP;
                        record.ByInvIP = DealOrder._ByInvIP;
                        record.State = DealOrder._State;

                        response.Userlist.Add(record);
                    }
                }

                await dBProxyComponent.Save(DealOrder);
                await dBProxyComponent.SaveLog(DealOrder);
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
