using System;
using System.Net;
using System.Text.RegularExpressions;
using ETModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class GuestAccountRegistComponentSystem : AwakeSystem<GuestAccountRegistComponent>
    {
        public override void Awake(GuestAccountRegistComponent self)
        {
            self.Awake();
        }
    }

    public class GuestAccountRegistComponent : Component
    {
        string RandomAccount = "";
        string Email = "";
        string Account = "";
        string EMail = "";
        public void Awake()
        {

        }
        
        /// <summary>
        /// 查询游客数据表
        /// </summary>
        async void QueryGuestAccount()
        {
            try
            {
                G2C_QueryGuestAccount AccountInfo = (G2C_QueryGuestAccount)await SessionComponent.Instance.Session.Call(new C2G_QueryGuestAccount()
                {
                    AccountID = RandomAccount,
                });
            }
            catch (Exception e)
            {
                Debug.Log("GuestAccountRegistComponent QueryGuestAccount" + e.Message);
            }
        }

        /// <summary>
        /// 创建游客数据
        /// </summary>
        async void EstablishGuest()
        {
            try
            {
                G2C_AddGuestAccount AccountInfo = (G2C_AddGuestAccount)await SessionComponent.Instance.Session.Call(new C2G_AddGuestAccount()
                {
                    AccountID = 0,
                    RandomAccount = RandomAccount,
                    Password = "",
                    ManagerPassword = "",
                    EMail = Email,
                    LastOnlineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    RegistrTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CumulativeTime = 0,
                    Account = Account
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("GuestAccountRegistComponent EstablishGuest" + "游客账户创建成功");
                }
                else
                {
                    Debug.Log("GuestAccountRegistComponent EstablishGuest" + "游客账户创建失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("GuestAccountRegistComponent EstablishGuest" + e.Message);
            }
        }

        int State = -1;
        int CumulativeTime = -1;
        /// <summary>
        /// 修改游客数据
        /// </summary>
        async void UpdateGuestAccount()
        {
            try
            {
                G2C_UpdateGuestAccount g2C_UpdateMainAccount = (G2C_UpdateGuestAccount)await SessionComponent.Instance.Session.Call(new C2G_UpdateGuestAccount()
                {
                    AccountID = -1,
                    Password = "",
                    ManagerPassword = "",
                    EMail = EMail,
                    LastOnlineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    RegistrTime = "",
                    CumulativeTime = CumulativeTime,
                    Account = Account,
                    State = State,
                });
                if (g2C_UpdateMainAccount.IsOk)
                {
                    Debug.Log("GuestAccountRegistComponent UpdateGuestAccount" + "游客账户数据修改成功");
                }
                else
                {
                    Debug.Log("GuestAccountRegistComponent UpdateGuestAccount" + "游客账户信息修改失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("GuestAccountRegistComponent UpdateGuestAccount" + e.Message);
            }
        }

        /// <summary>
        /// 游客转正式功能
        /// </summary>
        async void GuestToMainAccount()
        {
            try
            {
                G2C_GuestToMainAccount G2C_GuestToMainAccount = (G2C_GuestToMainAccount)await SessionComponent.Instance.Session.Call(new C2G_GuestToMainAccount()
                {
                    RandomAccount = RandomAccount
                });
                if (G2C_GuestToMainAccount.IsOk)
                {
                    Debug.Log("GuestAccountRegistComponent GuestToMainAccount" + "游客转正式账户成功");
                }
                else
                {
                    Debug.Log("GuestAccountRegistComponent GuestToMainAccount" + "游客转正式账户失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("GuestAccountRegistComponent GuestToMainAccount" + e.Message);
            }
        }
    }
}