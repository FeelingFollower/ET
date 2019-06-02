using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    /// <summary>
    /// 账号信息表
    /// </summary>
    public class AccountInfo : Entity
    {
        /// <summary>
        /// 所属账号id
        /// </summary>
        public long _AccountID;

        /// <summary>
        /// 姓名
        /// </summary>
        public string _Name;
        
        /// <summary>
        /// 年龄
        /// </summary>
        public int _Age;

        /// <summary>
        /// 出生年月
        /// </summary>
        public string _BornDate;

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string _IDCardNumber;

        /// <summary>
        /// 性别 0未设置1男2女
        /// </summary>
        public int _Sex;

        /// <summary>
        /// 是否完成实名认证
        /// </summary>
        public int _IsFinishIdentify;

        /// <summary>
        /// 头像照片名称
        /// </summary>
        public string _HeadImage;

        /// <summary>
        /// 指纹识别码
        /// </summary>
        public string _FingerprintCode;
        
        /// <summary>
        /// 面部识别码
        /// </summary>
        public string _FaceprintCode;

        /// <summary>
        /// 识别类型 0未选择1指纹2面部
        /// </summary>
        public int _PrintType;

        /// <summary>
        /// 用户重要等级
        /// </summary>
        public int _UserImpotentLevel;
        
    }
}
