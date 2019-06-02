using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_FindPasswordHandler : AMRpcHandler<C2R_FindPassword, R2C_FindPassword>
    {
        protected override async void Run(Session session, C2R_FindPassword message, Action<R2C_FindPassword> reply)
        {
            R2C_FindPassword response = new R2C_FindPassword();
            try
            {

                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                //查询数据是否存在
                var acounts = await dBProxyComponent.Query<MainAccount>("{'PhoneAccount' : '" + message.Account + "'}");

                if (acounts.Count == 1)
                {
                    foreach (MainAccount item in acounts)
                    {
                        item._Password = message.Password;

                        Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "zhaohuid mima   :" + message.Password);
                        await dBProxyComponent.Save(item);
                        response.Message = "密码重置成功，请返回登陆。";
                        response.IsSuccess = true;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "数据库异常";
                }
                reply(response);

            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "数据操作失败，请稍后再试。";
                response.IsSuccess = false;

                ReplyError(response, e, reply);
            }
        }
    }
}