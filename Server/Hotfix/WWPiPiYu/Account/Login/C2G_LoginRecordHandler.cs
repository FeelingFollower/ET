using System;
using System.Reflection;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_LoginRecordHandler : AMRpcHandler<C2G_LoginRecord, G2C_LoginRecord>
    {
        protected override async void Run(Session session, C2G_LoginRecord message, Action<G2C_LoginRecord> reply)
        {
            G2C_LoginRecord response = new G2C_LoginRecord();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                if (message.DoState == 1)
                {
                    var acounts = await dBProxyComponent.Query<UserLoginRecord>("{'_AccountID' : " + message.AccountID + "}");
                    if (acounts.Count == 1)
                    {
                        foreach (UserLoginRecord item in acounts)
                        {
                            response.AccountID = item._AccountID;
                            response.InfoID = item._InfoID;
                            response.LoginTimes = item._LoginTimes;
                            response.IP = item._IP;
                            response.LoginLocInfo = item._LoginLocInfo;
                            response.AbnormalStateCode = item._AbnormalStateCode;
                            response.LoginDate = item._LoginDate;
                        }
                        response.IsSuccess = true;
                        response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "拿取数据成功";
                    }
                    else if (acounts.Count == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "无数据";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "登录记录数据错误";
                    }
                }
                else
                {
                    //保存数据表
                    UserLoginRecord userLoginRecord = ComponentFactory.Create<UserLoginRecord>();
                    userLoginRecord._AccountID = message.AccountID;
                    userLoginRecord._InfoID = message.InfoID;
                    userLoginRecord._AbnormalStateCode = message.AbnormalStateCode;
                    userLoginRecord._IP = message.IP;
                    userLoginRecord._LoginDate = message.LoginDate;
                    userLoginRecord._LoginLocInfo = message.LoginLocInfo;
                    userLoginRecord._LoginTimes = message.LoginTimes;
                    await dBProxyComponent.Save(userLoginRecord);
                    response.IsSuccess = true;
                    response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "登录记录地址成功";
                }
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