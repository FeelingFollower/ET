using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 交易订单
    /// </summary>
    public class DealOrder : Entity
    {
        /// <summary>
        /// 所属id
        /// </summary>
        public long _InvAccountID;

        /// <summary>
        /// 好友id
        /// </summary>
        public long _ByInvAccountID;

        /// <summary>
        /// 产品id
        /// </summary>
        public long _ProductID;

        /// <summary>
        /// 邀请者交易物品列表
        /// </summary>
        public List<long> _InvItemList;

        /// <summary>
        /// 产品积分
        /// </summary>
        public int _InvProductPoint;

        /// <summary>
        /// 产品钻石
        /// </summary>
        public int _InvProductDiamond;

        /// <summary>
        /// 被邀请者交易物品列表
        /// </summary>
        public List<long> _ByInvItemList;

        /// <summary>
        /// 产品积分
        /// </summary>
        public int _ByInvProductPoint;

        /// <summary>
        /// 产品钻石
        /// </summary>
        public int _ByInvProductDiamond;


        /// <summary>
        /// 交易聊天记录
        /// </summary>
        public List<long> _MessageList;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string _CreateDate;

        /// <summary>
        /// 成交时间
        /// </summary>
        public string _DealDate;

        /// <summary>
        /// 邀请者IP
        /// </summary>
        public string _InvIP;

        /// <summary>
        /// 被邀请者IP
        /// </summary>
        public string _ByInvIP;

        /// <summary>
        ///状态
        /// </summary>
        public int _State;
    }
}
