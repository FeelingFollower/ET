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
    public class UserAddressComponentSystem : AwakeSystem<UserAddressComponent>
    {
        public override void Awake(UserAddressComponent self)
        {
            self.Awake();
        }
    }

    public class UserAddressComponent : Component
    {
        public void Awake()
        {

        }

        long AccountID = 0;
        long InfoID = 0;
        List<string> AddressList = new List<string>();

        /// <summary>
        /// 查询用户地址表
        /// </summary>
        async void QueryMainAccount()
        {
            try
            {
                G2C_QueryUserAdress AccountInfo = (G2C_QueryUserAdress)await SessionComponent.Instance.Session.Call(new C2G_QueryUserAdress()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                });
                AddressList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(AccountInfo.AdressList);
            }
            catch (Exception e)
            {
                Debug.Log("UserAddressComponent QueryMainAccount" + e.Message);
            }
        }

        string NewAddress = "";
        string OldAddress = "";
        /// <summary>
        /// 添加删除修改用户地址表
        /// </summary>
        async void MainAccount()
        {
            try
            {
                G2C_UserAdress AccountInfoAdd = (G2C_UserAdress)await SessionComponent.Instance.Session.Call(new C2G_UserAdress()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    NewAddress = NewAddress,
                    OldAdress = "",
                    Type = 1
                });
                if (AccountInfoAdd.IsOk)
                {
                    Debug.Log("UserAddressComponent MainAccount" + "添加地址成功");
                }
                else
                {
                    Debug.Log("UserAddressComponent MainAccount" + "添加地址失败");
                }

                G2C_UserAdress AccountInfoDelete = (G2C_UserAdress)await SessionComponent.Instance.Session.Call(new C2G_UserAdress()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    NewAddress = "",
                    OldAdress = OldAddress,
                    Type = 2
                });
                if (AccountInfoDelete.IsOk)
                {
                    Debug.Log("UserAddressComponent MainAccount" + "删除地址成功");
                }
                else
                {
                    Debug.Log("UserAddressComponent MainAccount" + "删除地址失败");
                }

                G2C_UserAdress AccountInfoUpdate = (G2C_UserAdress)await SessionComponent.Instance.Session.Call(new C2G_UserAdress()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    NewAddress = NewAddress,
                    OldAdress = OldAddress,
                    Type = 3
                });
                if (AccountInfoUpdate.IsOk)
                {
                    Debug.Log("UserAddressComponent MainAccount" + "更新地址成功");
                }
                else
                {
                    Debug.Log("UserAddressComponent MainAccount" + "更新地址失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("UserAddressComponent MainAccount" + e.Message);
            }
        }
    }
}