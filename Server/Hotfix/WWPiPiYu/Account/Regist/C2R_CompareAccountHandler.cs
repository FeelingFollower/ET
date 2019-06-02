using System;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_CompareAccountHandler : AMRpcHandler<C2R_CompareAccount, R2C_CompareAccount>
    {
        protected override async void Run(Session session, C2R_CompareAccount message, Action<R2C_CompareAccount> reply)
        {
            R2C_CompareAccount response = new R2C_CompareAccount();
            try
            {

                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                //查询数据是否存在
                var acounts = await dBProxyComponent.Query<MainAccount>("{'PhoneAccount' : '" + message.PhoneNumber + "'}");

                if (message.State == 1)
                {
                    //注册
                    if (acounts.Count > 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "此号码已注册，请检查号码";

                    }
                    else
                    {
                        //此号码可以使用
                        response.IsSuccess = true;
                        response.Message = "此号码可以使用，请进行手机号码验证";
                    }
                }
                else
                {
                    //找回
                    if (acounts.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "等待短信。";
                    }
                    else
                    {
                        //此号码可以使用
                        response.IsSuccess = false;
                        response.Message = "账号不存在，请检查。";
                    }
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "注册失败，服务器维护中。";

                ReplyError(response, e, reply);
            }
        }
    }
}