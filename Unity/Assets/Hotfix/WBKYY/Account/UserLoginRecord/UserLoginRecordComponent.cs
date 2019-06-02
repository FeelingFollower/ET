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
    public class UserLoginRecordComponentSystem : AwakeSystem<UserLoginRecordComponent>
    {
        public override void Awake(UserLoginRecordComponent self)
        {
            self.Awake();
        }
    }

    public class UserLoginRecordComponent : Component
    {
        public void Awake()
        {

        }

        long AccountID = 0;
        /// <summary>
        /// 查询用户登录记录表
        /// </summary>
        async void QueryUserLoginRecord()
        {
            try
            {
                G2C_QueryUserLoginRecord AccountInfo = (G2C_QueryUserLoginRecord)await SessionComponent.Instance.Session.Call(new C2G_QueryUserLoginRecord()
                {
                    AccountID = AccountID,
                });
                foreach (LoginRecord item in AccountInfo.Userlist)
                {

                }
            }
            catch (Exception e)
            {
                Debug.Log("UserLoginRecordComponent QueryUserLoginRecord" + e.Message);
            }
        }

        long InfoID = 0;
        string IP = "";
        string LoginLocInfo = "";
        int AbnormalStateCode = 0;
        string LoginDate = "";
        /// <summary>
        /// 添加用户登录记录表
        /// </summary>
        async void AddUserLoginRecord()
        {
            try
            {
                G2C_AddUserLoginRecord AccountInfo = (G2C_AddUserLoginRecord)await SessionComponent.Instance.Session.Call(new C2G_AddUserLoginRecord()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    LoginTimes = 1,
                    IP = IP,
                    LoginLocInfo = LoginLocInfo,
                    AbnormalStateCode = AbnormalStateCode,
                    LoginDate = LoginDate,
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("UserLoginRecordComponent AddUserLoginRecord" + "添加用户登录记录成功");
                }
                else
                {
                    Debug.Log("UserLoginRecordComponent AddUserLoginRecord" + "添加用户登录记录失败");
                }

            }
            catch (Exception e)
            {
                Debug.Log("UserLoginRecordComponent AddUserLoginRecord" + e.Message);
            }
        }
    }
}