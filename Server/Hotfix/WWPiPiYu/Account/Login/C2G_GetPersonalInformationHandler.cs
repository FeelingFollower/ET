using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_GetPersonalInformationHandler : AMRpcHandler<C2G_GetPersonalInformation, G2C_GetPersonalInformation>
    {
        protected override async void Run(Session session, C2G_GetPersonalInformation message, Action<G2C_GetPersonalInformation> reply)
        {
            G2C_GetPersonalInformation response = new G2C_GetPersonalInformation();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + message.UserID.ToString());
                //有资料，返回资料数据
                var acounts = await dBProxyComponent.Query<AccountInfo>("{'UserID' : " + message.UserID + "}");
                if (acounts.Count == 1)
                {
                    foreach (AccountInfo infoItem in acounts)
                    {
                        response.Nickname = infoItem._Name;
                        response.Age = infoItem._Age;
                        response.HeadPhoto = infoItem._HeadImage;
                        response.Sex = infoItem._Sex;
                        response.UserImpotentLevel = infoItem._UserImpotentLevel;
                        response.FaceprintCode = infoItem._FaceprintCode;
                        response.FingerprintCode = infoItem._FingerprintCode;
                        response.IsFinishIdentify = infoItem._IsFinishIdentify;
                        response.IDCardNumber = infoItem._IDCardNumber;
                        response.BornDate = infoItem._BornDate;

                        response.IsSuccess = true;
                        response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "获取信息成功";
                        Log.Debug(response.Message);

                    }
                }

                else if (acounts.Count == 0)
                {
                    response.IsSuccess = false;
                    response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "资料未填写完成,请先补充资料";
                }

                else
                {
                    response.IsSuccess = false;
                    response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "资料数据异常。";
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + response.Message);

                }

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
