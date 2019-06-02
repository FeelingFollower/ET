using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 群组信息
    /// </summary>
    public class GroupInfo : Entity
    {
        /// <summary>
        /// 所属id
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 群组密码
        /// </summary>
        public string _GroupPassword;

        /// <summary>
        /// 颜色代码
        /// </summary>
        public string _ColorCode;

        /// <summary>
        /// 群组名称
        /// </summary>
        public string _Name;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string _CreateDate;

        /// <summary>
        /// 群组编号
        /// </summary>
        public long _GroupNumber;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _UseState;
    }
}
