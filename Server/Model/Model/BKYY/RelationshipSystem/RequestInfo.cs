using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 好友申请
    /// </summary>
    public class RequestInfo:Entity
    {
        /// <summary>
        /// 所属账号id
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 被邀请ID
        /// </summary>
        public long _ByInvAccountID;

        /// <summary>
        /// 申请信息
        /// </summary>
        public string _RequestMessage;

        /// <summary>
        /// 申请的备注
        /// </summary>
        public string _Note;

        /// <summary>
        /// 状态
        /// </summary>
        public int _StateCode;

        /// <summary>
        /// 申请时间
        /// </summary>
        public string _RequestDate;

        /// <summary>
        /// 处理时间
        /// </summary>
        public string _ProcessDate;
    }
}
