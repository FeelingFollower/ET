using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 聊天室消息内容
    /// </summary>
    public class ChatRoomMessage : Entity
    {
        /// <summary>
        /// 发送人ID
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 发送时间
        /// </summary>
        public string _SendDate;

        /// <summary>
        /// 内容
        /// </summary>
        public List<string> _Message;

        /// <summary>
        /// 所属聊天室ID
        /// </summary>
        public long _ChatRoomID;

    }
}
