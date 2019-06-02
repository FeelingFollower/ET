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
    public class GoodsInfo : Entity
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public long _GoodsInfoID;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string _GoodsInfoName;

        /// <summary>
        /// 商品类别
        /// </summary>
        public int _GoodsSort;

        /// <summary>
        /// 物品id
        /// </summary>
        public long _GoodsID;

        /// <summary>
        /// 价格
        /// </summary>
        public float _Price;

        /// <summary>
        /// 商品图片
        /// </summary>
        public string _GoodsInfoImage;

        /// <summary>
        /// 商品描述
        /// </summary>
        public List<string> _Intrduce;

        /// <summary>
        /// 留言消息列表
        /// </summary>
        public List<string> _PublicTime;
    }
}
