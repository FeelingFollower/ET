﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 主账号表
    /// </summary>
    public class MainAccount:Entity
    {
        /// <summary>
        /// 账号id
        /// </summary>
        public long _AccountID;

        /// <summary>
        /// 信息id
        /// </summary>
        public long _InfoID;

        /// <summary>
        /// 账号（手机号码）
        /// </summary>
        public string _Account;

        /// <summary>
        /// 密码
        /// </summary>
        public string _Password;

        /// <summary>
        /// 管理密码
        /// </summary>
        public string _ManagerPassword;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string _EMail;

        /// <summary>
        /// 最后一次在线时间
        /// </summary>
        public string _LastOnlineTime;

        /// <summary>
        /// 注册时间
        /// </summary>
        public string _RegistrTime;

        /// <summary>
        /// 在线累积时间
        /// </summary>
        public int _CumulativeTime;
        
        /// <summary>
        /// 是否删除了(1删除，0未操作)
        /// </summary>
        public int _State;
    }
}
