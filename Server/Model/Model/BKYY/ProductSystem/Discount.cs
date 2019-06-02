using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 优惠卷表
    /// </summary>
    public class Discount : Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long _UserID;

        /// <summary>
        /// 优惠卷id
        /// </summary>
        public long _DiscountID;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
