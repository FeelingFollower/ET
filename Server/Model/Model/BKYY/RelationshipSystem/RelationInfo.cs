using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 好友列表
    /// </summary>
    public class RelationInfo:Entity
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
        /// 存储好友信息ID列表
        /// </summary>
        public List<long> _FriendIDList;

        /// <summary>
        /// 黑名单
        /// </summary>
        public List<long> _BlackIDList;

        /// <summary>
        /// 储存群组信息列表
        /// </summary>
        public List<long> _GroupList;

        /// <summary>
        /// 聊天室编号列表
        /// </summary>
        public List<long> _ChatRoomList;

    }
}
