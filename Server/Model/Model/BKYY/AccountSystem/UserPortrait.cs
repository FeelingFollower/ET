using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 用户画像表
    /// </summary>
    public class UserPortrait : Entity
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
        /// 用户习性标签表（标签编号）
        /// </summary>
        public List<int> _PortraitList;

    }
}
