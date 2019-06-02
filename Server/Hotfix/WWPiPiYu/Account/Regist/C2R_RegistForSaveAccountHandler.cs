using System;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_RegistForSaveAccountHandler : AMRpcHandler<C2R_RegistForSaveAccount, R2C_RegistForSaveAccount>
    {
        protected override async void Run(Session session, C2R_RegistForSaveAccount message, Action<R2C_RegistForSaveAccount> reply)
        {
            R2C_RegistForSaveAccount response = new R2C_RegistForSaveAccount();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();


                var acounts = await dBProxyComponent.Query<MainAccount>("{'_Account' : '" + message.Account + "'}");
                //判断账号是否存在
                if (acounts.Count > 0)
                {
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "此号码已注册，请检查号码");
                    response.IsSuccess = false;
                    response.Message = "此号码已注册，请检查号码";
                }
                else
                {
                    MainAccount mainAccount = ComponentFactory.Create<MainAccount>();
                    mainAccount._Account = message.Account;
                    mainAccount._AccountID = mainAccount.Id;
                    mainAccount._CumulativeTime = 0;
                    mainAccount._EMail = "";
                    mainAccount._InfoID = -1;
                    mainAccount._LastOnlineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    mainAccount._ManagerPassword = "";
                    mainAccount._Password = message.Password;
                    mainAccount._RegistrTime = message.RegistTime;
                    mainAccount._State = 0;

                    await dBProxyComponent.Save(mainAccount);

                    response.UserID = mainAccount.Id;
                    response.IsSuccess = true;
                    response.Message = "注册成功";
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