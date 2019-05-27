using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 用户产品记录
    /// </summary>
    public class UserProductRecord : Entity
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
        /// 用户当前产品id
        /// </summary>
        public int _ProductID;

        /// <summary>
        /// 用户等级
        /// </summary>
        public int _Level;

        /// <summary>
        /// 开始时间日期
        /// </summary>
        public string _StartDate;

        /// <summary>
        /// 结束时间日期
        /// </summary>
        public string _EndDate;

        /// <summary>
        /// 权益id列表
        /// </summary>
        public List<int> _RightCodeList;

        /// <summary>
        /// 如何购买的（渠道、方式）
        /// </summary>
        public int _BuyType;

        /// <summary>
        /// 购买的金额
        /// </summary>
        public float _Price;

    }
}
