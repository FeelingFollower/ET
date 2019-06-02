using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class ProductInfoData : Entity
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public long _ProductInfoID;

        /// <summary>
        /// 商铺id
        /// </summary>
        public long _ShopInfoID;

        /// <summary>
        /// 物品id
        /// </summary>
        public long _ProductID;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string _ProductInfoName;

        /// <summary>
        /// 商品发布地
        /// </summary>
        public string _ProductPublishGround;

        /// <summary>
        /// 商品类别
        /// </summary>
        public int _ProductInfoSort;

        /// <summary>
        /// 商品商铺类别
        /// </summary>
        public int _ProductShopSort;

        /// <summary>
        /// 价格
        /// </summary>
        public float _Price;

        /// <summary>
        /// 数量（库存）
        /// </summary>
        public int _Count;

        /// <summary>
        /// 商品描述
        /// </summary>
        public List<string> _Intrduce;

        /// <summary>
        /// 商品头图
        /// </summary>
        public string _ProductInfoHeadImage;

        /// <summary>
        /// 商品图片集
        /// </summary>
        public List<string> _ProductInfoImages;
        
        /// <summary>
        /// 购买次数
        /// </summary>
        public int _BayCounts;

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int _ViewCounts;
        
        /// <summary>
        /// 商品属性标签
        /// </summary>
        public List<string> _PorductInfoTags;

        /// <summary>
        /// 优惠标签
        /// </summary>
        public List<string> _DiscountsTags;

        /// <summary>
        /// 商品优惠
        /// </summary>
        public List<string> _PorductInfoDis;

        /// <summary>
        /// 点赞数
        /// </summary>
        public int _Thumbs;

        /// <summary>
        /// 收藏数
        /// </summary>
        public int _Collects;

        /// <summary>
        /// 服务列表
        /// </summary>
        public List<long> _ServiceList;

        /// <summary>
        /// 属性包
        /// </summary>
        public List<string> _AttributeBag;

        /// <summary>
        /// 留言消息列表
        /// </summary>
        public List<string> _PublicMessage;

        /// <summary>
        /// 上架时间
        /// </summary>
        public string _PublishTime;

        /// <summary>
        /// 修改时间
        /// </summary>
        public string _UpdateTime;

        /// <summary>
        /// 审核状态 0审核中，1通过，2拒绝
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
