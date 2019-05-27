using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 用户产品信息
    /// </summary>
    public class UserProductInfo : Entity
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
        /// 用户当前产品列表
        /// </summary>
        public List<int> _ProductList;

        /// <summary>
        /// 用户积分
        /// </summary>
        public int _UserPoint;

    }
}
