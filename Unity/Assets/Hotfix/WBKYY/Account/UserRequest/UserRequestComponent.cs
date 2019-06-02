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
    public class UserRequestComponentSystem : AwakeSystem<UserRequestComponent>
    {
        public override void Awake(UserRequestComponent self)
        {
            self.Awake();
        }
    }

    public class UserRequestComponent : Component
    {
        public void Awake()
        {

        }

        long AccountID = 0;
        /// <summary>
        /// 查询用户申请记录
        /// </summary>
        async void QueryUserRequestRecord()
        {
            try
            {
                G2C_QueryUserRequestRecord AccountInfo = (G2C_QueryUserRequestRecord)await SessionComponent.Instance.Session.Call(new C2G_QueryUserRequestRecord()
                {
                    AccountID = AccountID,
                });
                foreach (RequestRecord item in AccountInfo.Userlist)
                {

                }
            }
            catch (Exception e)
            {
                Debug.Log("UserRequestComponent QueryUserRequestRecord" + e.Message);
            }
        }

        long InfoID = 0;
        int RequestTimes = 0;
        string IP = "";
        string RequestInfo = "";
        int RequestTypeCode = 0;
        string RequestResultCode = "";
        /// <summary>
        /// 添加用户申请记录
        /// </summary>
        async void AddUserRequestRecord()
        {
            try
            {
                G2C_AddUserRequestRecord AccountInfo = (G2C_AddUserRequestRecord)await SessionComponent.Instance.Session.Call(new C2G_AddUserRequestRecord()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    RequestTimes = RequestTimes,
                    IP = IP,
                    RequestInfo = RequestInfo,
                    RequestTypeCode = RequestTypeCode,
                    RequestResultCode = RequestResultCode,
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("UserRequestComponent AddUserRequestRecord" + "添加用户申请记录成功");
                }
                else
                {
                    Debug.Log("UserRequestComponent AddUserRequestRecord" + "添加用户申请记录失败");
                }
            }
            catch (Exception e)
            {
                Debug.Log("UserRequestComponent AddUserRequestRecord" + e.Message);
            }
        }
    }
}