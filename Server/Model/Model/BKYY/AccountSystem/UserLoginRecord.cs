using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 用户登录记录
    /// </summary>
    public class UserLoginRecord : Entity
    {
        /// <summary>
        /// 所属账号id
        /// </summary>
        public long _AccountID;

        /// <summary>
        /// 所属信息id
        /// </summary>
        public long _InfoID;

        /// <summary>
        /// 用户当前登录次数
        /// </summary>
        public int _LoginTimes;

        /// <summary>
        /// 本次登录IP
        /// </summary>
        public string _IP;

        /// <summary>
        /// 登录地点文字描述
        /// </summary>
        public string _LoginLocInfo;

        /// <summary>
        /// 异常状态码
        /// </summary>
        public int _AbnormalStateCode;

        /// <summary>
        /// 登录时间
        /// </summary>
        public string _LoginDate;
        

    }
}
