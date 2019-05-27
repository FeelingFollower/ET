using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 评价产品表
    /// </summary>
    public class EvaluateProduct : Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long _UserID;

        /// <summary>
        /// 评价的产品id
        /// </summary>
        public long _EvaluateProductID;

        /// <summary>
        /// 评价时间
        /// </summary>
        public string _EvaluateTime;

        /// <summary>
        /// 评价内容
        /// </summary>
        public string _EvaluateContent;

        /// <summary>
        /// 评分
        /// </summary>
        public int _Evaluateminute;

        /// <summary>
        /// 是否删除了
        /// </summary>
        public int _State;
    }
}
