using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 点赞产品表
    /// </summary>
    public class ThumbProduct : Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long _UserID;

        /// <summary>
        /// 点赞产品id
        /// </summary>
        public long _ThumbProductID;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
