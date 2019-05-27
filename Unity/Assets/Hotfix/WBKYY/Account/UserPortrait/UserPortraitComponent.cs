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
    public class UserPortraitComponentSystem : AwakeSystem<UserPortraitComponent>
    {
        public override void Awake(UserPortraitComponent self)
        {
            self.Awake();
        }
    }

    public class UserPortraitComponent : Component
    {
        public void Awake()
        {

        }

        long AccountID = 0;
        List<int> PortraitList = new List<int>();
        /// <summary>
        /// 查询用户画像表
        /// </summary>
        async void QueryUserPortrait()
        {
            try
            {
                G2C_QueryUserPortrait AccountInfo = (G2C_QueryUserPortrait)await SessionComponent.Instance.Session.Call(new C2G_QueryUserPortrait()
                {
                    AccountID = AccountID,
                });
                PortraitList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(AccountInfo.PortraitList);
            }
            catch (Exception e)
            {
                Debug.Log("UserPortraitComponent QueryUserPortrait" + e.Message);
            }
        }

        long InfoID = 0;
        int Portrait = 0;
        /// <summary>
        /// 添加用户画像表
        /// </summary>
        async void AddUserPortrait()
        {
            try
            {
                G2C_AddUserPortrait AccountInfo = (G2C_AddUserPortrait)await SessionComponent.Instance.Session.Call(new C2G_AddUserPortrait()
                {
                    AccountID = AccountID,
                    InfoID = InfoID,
                    Portrait = Portrait,
                });
                if (AccountInfo.IsOk)
                {
                    Debug.Log("UserPortraitComponent AddUserPortrait" + "添加用户画像成功");
                }
                else
                {
                    Debug.Log("UserPortraitComponent AddUserPortrait" + "添加用户画像失败");
                }

            }
            catch (Exception e)
            {
                Debug.Log("UserPortraitComponent AddUserPortrait" + e.Message);
            }
        }
    }
}