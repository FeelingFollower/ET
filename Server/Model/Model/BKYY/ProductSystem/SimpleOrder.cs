using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 简易订单(购物车使用)
    /// </summary>
    public class SimpleOrder : Entity
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public long _OrderID;

        /// <summary>
        /// 商品id
        /// </summary>
        public long _ProductInfoID;

        /// <summary>
        /// 用户id
        /// </summary>
        public long _UserID;

        /// <summary>
        /// 购买属性包
        /// </summary>
        public List<string> _AttributeBag;

        /// <summary>
        /// 支付价格
        /// </summary>
        public float _Price;

        /// <summary>
        /// 购买数量
        /// </summary>
        public int _Count;

        /// <summary>
        /// 更新时间
        /// </summary>
        public string _UpdateTime;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
