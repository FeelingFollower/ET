using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 钱包数据
    /// </summary>
    public class WalletData : Entity
    {
        /// <summary>
        /// 所属用户id
        /// </summary>
        public long _AccountID;

        /// <summary>
        /// 支付密码
        /// </summary>
        public string _PayPassword;

        /// <summary>
        /// 钻石数量
        /// </summary>
        public float _Diamond;

        /// <summary>
        /// 积分
        /// </summary>
        public string _Point;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string _CreateDate;

        /// <summary>
        /// 钱包类型 金主等级
        /// </summary>
        public int _WalletType;

        /// <summary>
        /// 优惠列表
        /// </summary>
        public List<int> _OffList;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;

        /// <summary>
        /// 钱包标签
        /// </summary>
        public List<int> _WalletTipList;

    }
}
