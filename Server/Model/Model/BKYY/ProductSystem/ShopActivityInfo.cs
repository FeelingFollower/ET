using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 店铺活动
    /// </summary>
    public class ShopActivityInfo : Entity
    {
        /// <summary>
        /// 商铺活动id
        /// </summary>
        public long _ShopActivityID;

        /// <summary>
        /// 店铺id
        /// </summary>
        public long _ShopInfoID;

        /// <summary>
        /// 优惠类别
        /// </summary>
        public int _DiscountsSort;

        /// <summary>
        /// 优惠字段
        /// </summary>
        public string _DiscountsSortFields;

        /// <summary>
        /// 优惠描述
        /// </summary>
        public List<string> _Intrduce;

        /// <summary>
        /// 优惠商品
        /// </summary>
        public List<long> _DisProducts;

        /// <summary>
        /// 优惠数量
        /// </summary>
        public int _Count;

        /// <summary>
        /// 优惠开始时间
        /// </summary>
        public string _StartTime;

        /// <summary>
        /// 优惠结束时间
        /// </summary>
        public string _EndTime;

        /// <summary>
        /// 优惠提醒(例如多久发货啊等等)
        /// </summary>
        public string _DiscountsAlert;
        
        /// <summary>
        /// 审核状态 0审核中，1通过，2拒绝
        /// </summary>
        public int _AuditState;

        /// <summary>
        /// 审核消息
        /// </summary>
        public string _AuditMessage;

        /// <summary>
        /// 是否结束了
        /// </summary>
        public int _IsOver;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
