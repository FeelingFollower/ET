using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using ETModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class DealOrderComponentSystem : AwakeSystem<DealOrderComponent>
    {
        public override void Awake(DealOrderComponent self)
        {
            self.Awake();
        }
    }

    public class DealOrderComponent : Component
    {
        public void Awake()
        {

        }

        long AccountID = 0;
        long UserPoint = 0;
        List<int> PortraitList = new List<int>();
        /// <summary>
        /// 查询交易订单信息
        /// </summary>
        async void QueryDealOrder()
        {
            try
            {
                G2C_QueryDealOrder AccountInfo = (G2C_QueryDealOrder)await SessionComponent.Instance.Session.Call(new C2G_QueryDealOrder()
                {
                    AccountID = AccountID,
                });
                foreach (Dealorder item in AccountInfo.Userlist)
                {

                }
            }
            catch (Exception e)
            {
                Debug.Log("DealOrderComponent QueryDealOrder" + e.Message);
            }
        }

        long ByInvAccountID = 0;
        long ProductID = 0;
        List<long> InvItemList = new List<long>();
        int InvProductPoint = 0;
        int InvProductDiamond = 0;
        List<long> ByInvItemList = new List<long>();
        int ByInvProductPoint = 0;
        int ByInvProductDiamond = 0;
        List<long> MessageList = new List<long>();
        string CreateDate = "";
        string DealDate = "";
        string InvIP = "";
        string ByInvIP = "";
        int State = 0;
        /// <summary>
        /// 添加交易订单信息
        /// </summary>
        async void DealOrder()
        {
            try
            {
                G2C_AddDealOrder AccountInfo = (G2C_AddDealOrder)await SessionComponent.Instance.Session.Call(new C2G_AddDealOrder()
                {
                    InvAccountID = AccountID,
                    ByInvAccountID = ByInvAccountID,
                    ProductID = ProductID,
                    InvItemList = RepeatedFieldAndListChangeTool.ListToRepeatedField(InvItemList),
                    InvProductPoint = InvProductPoint,
                    InvProductDiamond = InvProductDiamond,
                    ByInvItemList = RepeatedFieldAndListChangeTool.ListToRepeatedField(ByInvItemList),
                    ByInvProductPoint = ByInvProductPoint,
                    ByInvProductDiamond = ByInvProductDiamond,
                    MessageList = RepeatedFieldAndListChangeTool.ListToRepeatedField(MessageList),
                    CreateDate = CreateDate,
                    DealDate = DealDate,
                    InvIP = InvIP,
                    ByInvIP = ByInvIP,
                    State = State,
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("DealOrderComponent DealOrder" + "添加交易订单信息成功");
                }
                else
                {
                    Debug.Log("DealOrderComponent DealOrder" + "添加交易订单信息失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("DealOrderComponent DealOrder" + e.Message);
            }
        }

        /// <summary>
        /// 修改主账户数据
        /// </summary>
        async void UpdateMainAccount()
        {
            try
            {
                G2C_UpdateDealOrder g2C_UpdateMainAccount = (G2C_UpdateDealOrder)await SessionComponent.Instance.Session.Call(new C2G_UpdateDealOrder()
                {
                    ByInvAccountID = ByInvAccountID,
                    ProductID = ProductID,
                    InvItemList = RepeatedFieldAndListChangeTool.ListToRepeatedField(InvItemList),
                    InvProductPoint = InvProductPoint,
                    InvProductDiamond = InvProductDiamond,
                    ByInvItemList = RepeatedFieldAndListChangeTool.ListToRepeatedField(ByInvItemList),
                    ByInvProductPoint = ByInvProductPoint,
                    ByInvProductDiamond = ByInvProductDiamond,
                    MessageList = RepeatedFieldAndListChangeTool.ListToRepeatedField(MessageList),
                    CreateDate = CreateDate,
                    DealDate = DealDate,
                    InvIP = InvIP,
                    ByInvIP = ByInvIP,
                    State = State,
                });
                if (g2C_UpdateMainAccount.IsOk)
                {
                    Debug.Log("DealOrderComponent UpdateMainAccount" + "交易订单修改成功");
                }
                else
                {
                    Debug.Log("DealOrderComponent UpdateMainAccount" + "交易订单修改失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("DealOrderComponent UpdateMainAccount" + e.Message);
            }
        }
    }
}