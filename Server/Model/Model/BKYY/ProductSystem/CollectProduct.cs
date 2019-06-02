using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 收藏产品表
    /// </summary>
    public class CollectProduct : Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long _UserID;

        /// <summary>
        /// 收藏产品id
        /// </summary>
        public long _CollectProductID;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
