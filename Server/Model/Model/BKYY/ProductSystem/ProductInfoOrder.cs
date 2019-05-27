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
    public class ProductInfoOrder : Entity
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
        /// 支付方式  
        /// </summary>
        public int _PayType;

        /// <summary>
        /// 支付价格
        /// </summary>
        public float _Price;

        /// <summary>
        /// 购买数量
        /// </summary>
        public int _Count;

        /// <summary>
        /// 物流编号
        /// </summary>
        public string _LogisticsNumber;

        /// <summary>
        /// 支付状态 0、待付款，1、付款成功，待发货，2、付款成功，待收货，3、收货成功，待评价，4，退款/售后，5退款成功，6取消订单，7订单完成
        /// </summary>
        public int _PayState;

        /// <summary>
        /// 取消消息
        /// </summary>
        public string _CancelMessage;

        /// <summary>
        /// 退款消息
        /// </summary>
        public string _RecedeMessage;

        /// <summary>
        /// 订单类别
        /// </summary>
        public int _OrderSort;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string _CreationTime;
        
        /// <summary>
        /// 付款时间
        /// </summary>
        public string _PaymentTime;

        /// <summary>
        /// 发货时间
        /// </summary>
        public string _SendProductTime;

        /// <summary>
        /// 收货时间
        /// </summary>
        public string _DeliveryTime;
        
        /// <summary>
        /// 取消时间
        /// </summary>
        public string _CancelTime;

        /// <summary>
        /// 退款时间
        /// </summary>
        public string _RecedeTime;
        
        /// <summary>
        /// 退款成功时间
        /// </summary>
        public string _RecedeOverTime;

        /// <summary>
        /// 服务列表
        /// </summary>
        public List<long> _ServiceList;

        /// <summary>
        /// 订单信息
        /// </summary>
        public string _OrderInfo;

        /// <summary>
        /// 支付平台交易号
        /// </summary>
        public string _PayPlatformNumber;

        /// <summary>
        /// 用户是否评价
        /// </summary>
        public bool _IsAppraise;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
