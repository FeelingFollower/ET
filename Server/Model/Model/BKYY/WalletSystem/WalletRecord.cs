using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 钱包交易记录
    /// </summary>
    public class WalletRecord : Entity
    {
        /// <summary>
        /// 所属钱包id
        /// </summary>
        public long _WalletID;

        /// <summary>
        /// 购买数额
        /// </summary>
        public float _Amount;

        /// <summary>
        /// 交易信息
        /// </summary>
        public string _Info;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string _CreateDate;

        /// <summary>
        /// 成交时间
        /// </summary>
        public string _DealDate;

        /// <summary>
        /// 特殊状态
        /// </summary>
        public string _SpeiaclInfo;

        /// <summary>
        /// 类型
        /// </summary>
        public int _Type;

        /// <summary>
        /// 交易状态
        /// </summary>
        public int _State;
    }
}
