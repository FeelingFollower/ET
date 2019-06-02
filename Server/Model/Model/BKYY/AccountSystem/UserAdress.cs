using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 用户地址表
    /// </summary>
    public class UserAdress : Entity
    {
        /// <summary>
        /// 所属账号id
        /// </summary>
        public long _AccountID;

        /// <summary>
        /// 所属信息id
        /// </summary>
        public long _InfoID;

        /// <summary>
        /// 详细地址
        /// </summary>
        public List<string> _AdressList;

    }
}
