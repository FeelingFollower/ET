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
    public class MainAccountComponentSystem : AwakeSystem<MainAccountComponent>
    {
        public override void Awake(MainAccountComponent self)
        {
            self.Awake();
        }
    }

    public class MainAccountComponent : Component
    {
        string Account = "";
        string Password = "";
        string ManagerPassword = "";
        string EMail = "";
        string LastOnlineTime = "";
        string RegistrTime = "";
        int CumulativeTime = -1;
        int State = -1;
        
        long InfoID = -1;

        long AccountID = -1;
        string Name = "";
        string BornDate = "";
        string IDCardNumber = "";
        int Sex = -1;
        string HeadImage = "";
        int IsFinishIdentify = -1;
        string FingerprintCode = "";
        string FaceprintCode = "";
        int UserImpotentLevel = -1;
        int PrintType = -1;
        public void Awake()
        {

        }

        /// <summary>
        /// 查询主账户表
        /// </summary>
        async void QueryMainAccount()
        {
            try
            {
                G2C_QueryMainAccount AccountInfo = (G2C_QueryMainAccount)await SessionComponent.Instance.Session.Call(new C2G_QueryMainAccount()
                {
                    AccountID = AccountID,
                });
            }
            catch (Exception e)
            {
                Debug.Log("QueryMainAccount" + e.Message);
            }
        }

        /// <summary>
        /// 创建主账户数据（主账户注册）
        /// </summary>
        async void EstablishMain()
        {
            try
            {
                G2C_AddMainAccount AccountInfo = (G2C_AddMainAccount)await SessionComponent.Instance.Session.Call(new C2G_AddMainAccount()
                {
                    AccountID = 0,
                    InfoID = 0,
                    Account = Account,
                    Password = "123456",
                    ManagerPassword = "",
                    EMail = "",
                    LastOnlineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    RegistrTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CumulativeTime = 0,
                });
            }
            catch (Exception e)
            {
                Debug.Log("EstablishMain" + e.Message);
            }
        }

        /// <summary>
        /// 修改主账户数据
        /// </summary>
        async void UpdateMainAccount()
        {
            try
            {
                G2C_UpdateMainAccount g2C_UpdateMainAccount = (G2C_UpdateMainAccount)await SessionComponent.Instance.Session.Call(new C2G_UpdateMainAccount()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    Account = Account,
                    Password = Password,
                    ManagerPassword = ManagerPassword,
                    EMail = EMail,
                    LastOnlineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    RegistrTime = "",
                    CumulativeTime = CumulativeTime,
                    State = State,
                });
                if (g2C_UpdateMainAccount.IsOk)
                {
                    Debug.Log("UpdateMainAccount" + "主账户数据修改成功");
                }
                else
                {
                    Debug.Log("UpdateMainAccount" + "主账户信息修改失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("UpdateMainAccount" + e.Message);
            }
        }

        /// <summary>
        /// 创建账户信息表
        /// </summary>
        async void PerfectAccountInfo()
        {
            try
            {
                G2C_AddAccountInfo AccountInfo = (G2C_AddAccountInfo)await SessionComponent.Instance.Session.Call(new C2G_AddAccountInfo()
                {
                    AccountID = AccountID,
                    Name = Name,
                    BornDate = BornDate,
                    IDCardNumber = IDCardNumber,
                    Sex = Sex,
                    IsFinishIdentify = IsFinishIdentify,
                    HeadImage = HeadImage,
                    FingerprintCode = "",
                    UserImpotentLevel = 0,
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("PerfectAccountInfo" + "完善账户信息表");
                    InfoID = AccountInfo.InfoID;
                }
                else
                {
                    Debug.Log("PerfectAccountInfo" + "账户信息表完善失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("PerfectAccountInfo" + e.Message);
            }
        }

        /// <summary>
        /// 修改账户信息表（完善客户信息）
        /// </summary>
        async void UpdateAccountInfo()
        {
            try
            {
                G2C_UpdateAccountInfo AccountInfo = (G2C_UpdateAccountInfo)await SessionComponent.Instance.Session.Call(new C2G_UpdateAccountInfo()
                {
                    AccountID = AccountID,
                    Name = Name,
                    BornDate = BornDate,
                    IDCardNumber = IDCardNumber,
                    Sex = Sex,
                    IsFinishIdentify = IsFinishIdentify,
                    HeadImage = HeadImage,
                    FingerprintCode = FingerprintCode,
                    UserImpotentLevel = UserImpotentLevel,
                    FaceprintCode = FaceprintCode,
                    PrintType = PrintType,
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("UpdateAccountInfo" + "修改账户信息成功");
                }
                else
                {
                    Debug.Log("UpdateAccountInfo" + "修改账户信息失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("UpdateAccountInfo" + e.Message);
            }
        }

        /// <summary>
        /// 查找账户信息表
        /// </summary>
        async void QueryAccountInfo()
        {
            try
            {
                G2C_QueryAccountInfo AccountInfo = (G2C_QueryAccountInfo)await SessionComponent.Instance.Session.Call(new C2G_QueryAccountInfo()
                {
                    AccountID = AccountID,
                });
            }
            catch (Exception e)
            {
                Debug.Log("QueryAccountInfo" + e.Message);
            }
        }
    }
}