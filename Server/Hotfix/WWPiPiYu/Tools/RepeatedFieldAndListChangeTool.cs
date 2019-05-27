using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    /// <summary>
    /// 将list与RepeatedField互相转换的工具类
    /// </summary>
    public class RepeatedFieldAndListChangeTool
    {

        /// <summary>
        /// 将list转成RepeatedField集合
        /// </summary>
        /// <typeparam name="T">转换的集合中的数据类型</typeparam>
        /// <param name="list">要转换的集合</param>
        /// <returns></returns>
        public static Google.Protobuf.Collections.RepeatedField<T> ListToRepeatedField<T>(List<T> list) {

            Google.Protobuf.Collections.RepeatedField<T> repeatedfieldList = new Google.Protobuf.Collections.RepeatedField<T>();
            foreach (var item in list)
            {
                repeatedfieldList.Add(item);
            }      

            return repeatedfieldList;
        }

        /// <summary>
        /// 将RepeatedField转成list集合
        /// </summary>
        /// <typeparam name="T">转换的集合中的数据类型</typeparam>
        /// <param name="list">要转换的集合</param>
        /// <returns></returns>
        public static List<T> RepeatedFieldToList<T>( Google.Protobuf.Collections.RepeatedField<T> list)
        {

            List<T> repeatedfieldList = new List<T>();
            foreach (var item in list)
            {
                repeatedfieldList.Add(item);
            }

            return repeatedfieldList;
        }
    }
}
