using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 商品订单
    /// </summary>
    public class GoodsInfoOrder : Entity
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public long _OrderID;

        /// <summary>
        /// 商品id
        /// </summary>
        public long _GoodsInfoID;

        /// <summary>
        /// 数量
        /// </summary>
        public float _Count;

        /// <summary>
        /// 价格
        /// </summary>
        public float _Price;

        /// <summary>
        /// 账户id
        /// </summary>
        public long _AccountID;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string _CreateDate;

        /// <summary>
        /// 成交时间
        /// </summary>
        public string _DealDate;

        /// <summary>
        /// 支付方式
        /// </summary>
        public int _PayType;

        /// <summary>
        /// 支付状态 0、未执行，1、付款成功，2、付款失败，3、退款
        /// </summary>
        public int _PayState;

        /// <summary>
        /// 平台订单
        /// </summary>
        public string _PlatformOrder;
    }
}
