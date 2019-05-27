using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 好友消息内容
    /// </summary>
    public class UserMessage : Entity
    {
        /// <summary>
        /// 发送人ID
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 接收人ID
        /// </summary>
        public long _ByInvAccountID;

        /// <summary>
        /// 发送时间
        /// </summary>
        public string _SendDate;

        /// <summary>
        /// 内容
        /// </summary>
        public List<string> _Message;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
