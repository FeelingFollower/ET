using System;
using System.Reflection;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_ChangePasswordHandler : AMRpcHandler<C2G_ChangePassword, G2C_ChangePassword>
    {
        protected override async void Run(Session session, C2G_ChangePassword message, Action<G2C_ChangePassword> reply)
        {
            G2C_ChangePassword response = new G2C_ChangePassword();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                //查询数据是否存在
                var acounts = await dBProxyComponent.Query<MainAccount>("{'_Account' : '" + message.Account + "'}");

                if (acounts.Count == 1)
                {
                    switch (message.State)
                    {
                        case 1:
                            //登陆密码重置
                            foreach (MainAccount item in acounts)
                            {
                                item._Password = message.NewPassword;
                                Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "xinmimashi    " + message.Password);
                                await dBProxyComponent.Save(item);
                                response.Message = "密码重置成功，请返回登陆。";
                                response.IsSuccess = true;
                            }

                            break;
                        case 2:
                            //修改登陆密码
                            foreach (MainAccount item in acounts)
                            {
                                if (item._Password == message.Password)
                                {
                                    item._Password = message.NewPassword;

                                    await dBProxyComponent.Save(item);
                                    response.Message = "修改密码成功，请返回。";
                                    response.IsSuccess = true;
                                }
                                else
                                {
                                    response.IsSuccess = false;
                                    response.Message = "原登陆密码输入错误。";
                                }
                            }
                            break;

                        case 3:
                            //密码校验
                            foreach (MainAccount item in acounts)
                            {
                                if (item._Password != message.Password)
                                {
                                    response.IsSuccess = false;
                                    response.Message = "原支付密码输入错误。";
                                }
                                else
                                {
                                    response.IsSuccess = true;
                                }
                            }
                            break;

                        default:
                            response.Message = "数据异常。";
                            response.IsSuccess = false;
                            break;
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
                response.IsSuccess = false;
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
}