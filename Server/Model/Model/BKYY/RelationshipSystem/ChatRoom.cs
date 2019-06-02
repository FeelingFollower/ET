using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 聊天室
    /// </summary>
    public class ChatRoom : Entity
    {
        /// <summary>
        /// 所属用户id
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 管理密码
        /// </summary>
        public string _ManagePassword;

        /// <summary>
        /// 颜色代码
        /// </summary>
        public string _PublicBorad;

        /// <summary>
        /// 聊天室名称
        /// </summary>
        public string _Name;

        /// <summary>
        /// 聊天室成员列表
        /// </summary>
        public List<long> _UserList;
        
        /// <summary>
        /// 聊天日期数据ID列表  格式2019-05-02 19:21:00|消息id
        /// </summary>
        public List<string> _DateMessageIDList;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string _CreateDate;

        /// <summary>
        /// 群组类型
        /// </summary>
        public int _GroupType;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
