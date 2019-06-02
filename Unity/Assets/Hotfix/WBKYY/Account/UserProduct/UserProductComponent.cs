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
    public class UserProductInfoComponentSystem : AwakeSystem<UserProductInfoComponent>
    {
        public override void Awake(UserProductInfoComponent self)
        {
            self.Awake();
        }
    }

    public class UserProductInfoComponent : Component
    {
        public void Awake()
        {

        }

        long AccountID = 0;
        long UserPoint = 0;
        List<int> PortraitList = new List<int>();
        /// <summary>
        /// 查询用户产品信息
        /// </summary>
        async void QueryUserProductInfo()
        {
            try
            {
                G2C_QueryUserProductInfo AccountInfo = (G2C_QueryUserProductInfo)await SessionComponent.Instance.Session.Call(new C2G_QueryUserProductInfo()
                {
                    AccountID = AccountID,
                });
                PortraitList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(AccountInfo.ProductList);
                UserPoint = AccountInfo.UserPoint;
            }
            catch (Exception e)
            {
                Debug.Log("UserProductInfoComponent QueryUserProductInfo" + e.Message);
            }
        }

        long InfoID = 0;
        int Product = 0;
        /// <summary>
        /// 添加用户产品信息  积分未使用
        /// </summary>
        async void UserProductInfo()
        {
            try
            {
                G2C_UserProductInfo AccountInfo = (G2C_UserProductInfo)await SessionComponent.Instance.Session.Call(new C2G_UserProductInfo()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    Product = Product,
                    Type = 1,
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("UserProductInfoComponent UserProductInfo" + "添加用户产品信息成功");
                }
                else
                {
                    Debug.Log("UserProductInfoComponent UserProductInfo" + "添加用户产品信息失败");
                }

                G2C_UserProductInfo AccountInfoDetele = (G2C_UserProductInfo)await SessionComponent.Instance.Session.Call(new C2G_UserProductInfo()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    Product = Product,
                    Type = 2,
                });
                if (AccountInfoDetele.IsOk)
                {
                    Debug.Log("UserProductInfoComponent UserProductInfo" + "删除用户产品信息成功");
                }
                else
                {
                    Debug.Log("UserProductInfoComponent UserProductInfo" + "删除用户产品信息失败");
                }

            }
            catch (Exception e)
            {
                Debug.Log("UserProductInfoComponent UserProductInfo" + e.Message);
            }
        }

        /// <summary>
        /// 查询用户产品记录
        /// </summary>
        async void QueryUserProductRecord()
        {
            try
            {
                G2C_QueryUserProductRecord AccountInfo = (G2C_QueryUserProductRecord)await SessionComponent.Instance.Session.Call(new C2G_QueryUserProductRecord()
                {
                    AccountID = AccountID,
                });
                foreach (ProductRecord item in AccountInfo.Userlist)
                {

                }
            }
            catch (Exception e)
            {
                Debug.Log("UserProductInfoComponent QueryUserProductRecord" + e.Message);
            }
        }

        int ProductID = 0;
        int Level = 0;
        string EndDate = "";
        List<int> RightCodeList = new List<int>();
        int BuyType = 0;
        float Price = 0;
        /// <summary>
        /// 添加用户产品记录
        /// </summary>
        async void AddUserProductRecord()
        {
            try
            {
                G2C_AddUserProductRecord AccountInfo = (G2C_AddUserProductRecord)await SessionComponent.Instance.Session.Call(new C2G_AddUserProductRecord()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    ProductID = ProductID,
                    Level = Level,
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndDate = EndDate,
                    RightCodeList = RepeatedFieldAndListChangeTool.ListToRepeatedField( RightCodeList),
                    BuyType = BuyType,
                    Price = Price,
                });

            }
            catch (Exception e)
            {
                Debug.Log("UserProductInfoComponent AddUserProductRecord" + e.Message);
            }
        }
    }
}