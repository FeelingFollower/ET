using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 收藏店铺表
    /// </summary>
    public class CollectShopInfo : Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long _UserID;

        /// <summary>
        /// 收藏店铺id
        /// </summary>
        public long _CollectShopInfoID;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
