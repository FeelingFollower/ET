using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 商品预售订单
    /// </summary>
    public class GoodsOrder : Entity
    {
        /// <summary>
        /// 所属id
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 物品id
        /// </summary>
        public long _GoodsID;

        /// <summary>
        /// 物品数据id
        /// </summary>
        public long _GoodsDataID;

        /// <summary>
        /// 价格
        /// </summary>
        public float _Price;

        /// <summary>
        /// 介绍信息
        /// </summary>
        public List<string> _Intrduce;

        /// <summary>
        /// 留言消息列表
        /// </summary>
        public string _PublicTime;
    }
}
