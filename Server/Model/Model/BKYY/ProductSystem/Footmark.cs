using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 足迹表
    /// </summary>
    public class Footmark : Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long _UserID;
        
        /// <summary>
        ///足迹时间 
        /// </summary>
        public string _FootmarkTime;

        /// <summary>
        /// 足迹产品id
        /// </summary>
        public long _FootmarkID;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
