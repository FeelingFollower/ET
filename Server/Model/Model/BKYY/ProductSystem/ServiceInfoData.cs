using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 服务信息
    /// </summary>
    public class ServiceInfoData : Entity
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public long _ServiceInfoID;

        /// <summary>
        /// 服务名称
        /// </summary>
        public string _ServiceInfoName;
        
        /// <summary>
        /// 服务内容
        /// </summary>
        public string _ServiceInfoContent;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
