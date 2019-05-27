using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    public class ShopInfoData : Entity
    {
        /// <summary>
        /// 店铺id
        /// </summary>
        public long _ShopInfoID;

        /// <summary>
        /// 店主id
        /// </summary>
        public long _UserID;

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string _ShopName;

        /// <summary>
        /// 店铺发布地
        /// </summary>
        public string _ShopPublishGround;

        /// <summary>
        /// 商铺描述
        /// </summary>
        public List<string> _Intrduce;

        /// <summary>
        /// 店铺类型
        /// </summary>
        public int _ShopType;

        /// <summary>
        /// 店铺头图
        /// </summary>
        public string _ShopInfoHeadImage;

        /// <summary>
        /// 店铺图片集
        /// </summary>
        public List<string> _ShopInfoImages;

        /// <summary>
        /// 认证
        /// </summary>
        public bool _IsAuthentication;

        /// <summary>
        /// 商铺等级
        /// </summary>
        public int _ShopLevel;

        /// <summary>
        /// 商铺保证金
        /// </summary>
        public float _ShopBailMoney;

        /// <summary>
        /// 需支付的商铺保证金
        /// </summary>
        public float _PayShopBailMoney;

        /// <summary>
        /// 店铺内部类别
        /// </summary>
        public List<string> _ShopSorts;
        
        /// <summary>
        /// 商铺会员列表id
        /// </summary>
        public List<long> _ShopUserVIP;

        /// <summary>
        /// 商铺活动列表
        /// </summary>
        public List<long> _ShopActivity;

        /// <summary>
        /// 商铺成立时间
        /// </summary>
        public string _ShopTime;

        /// <summary>
        /// 审核状态 0审核中，1通过，2拒绝，3待支付保证金
        /// </summary>
        public int _AuditState;

        /// <summary>
        /// 审核消息
        /// </summary>
        public string _AuditMessage;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
