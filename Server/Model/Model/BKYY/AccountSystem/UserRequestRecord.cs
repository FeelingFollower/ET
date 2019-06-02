using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 用户申请记录
    /// </summary>
    public class UserRequestRecord : Entity
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
        /// 用户当前申请次数
        /// </summary>
        public int _RequestTimes;

        /// <summary>
        /// 本次申请IP
        /// </summary>
        public string _IP;

        /// <summary>
        /// 申请描述
        /// </summary>
        public string _RequestInfo;

        /// <summary>
        /// 申请类型码
        /// </summary>
        public int _RequestTypeCode;

        /// <summary>
        /// 申请状态
        /// </summary>
        public string _RequestResultCode;
    }
}
