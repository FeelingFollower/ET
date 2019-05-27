using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 好友信息
    /// </summary>
    public class FriendInfo : Entity
    {
        /// <summary>
        /// 所属id
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 好友id
        /// </summary>
        public long _ByInvAccountID;

        /// <summary>
        /// 设置的好友昵称
        /// </summary>
        public string _NickName;

        /// <summary>
        /// 聊天日期数据ID列表  格式2019-05-02 19:21:00|消息id
        /// </summary>
        public List<string> _DateMessageIDList;

        /// <summary>
        /// 上一次聊天时间
        /// </summary>
        public string _LastDate;

        /// <summary>
        /// 留言消息列表  格式2019-05-02 19:21:00|消息id
        /// </summary>
        public List<string> _LiveMassageList;

        /// <summary>
        /// 群组编号
        /// </summary>
        public long _GroupNumber;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;

    }
}
