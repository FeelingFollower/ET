using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2R_ForSaveInformationHandler : AMRpcHandler<C2R_ForSaveInformation, R2C_ForSaveInformation>
    {
        protected override async void Run(Session session, C2R_ForSaveInformation message, Action<R2C_ForSaveInformation> reply)
        {
            R2C_ForSaveInformation response = new R2C_ForSaveInformation();
            try
            {

                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                //新建保存数据
                if (message.State == 1)
                {
                    //查询数据是否存在
                    var acounts = await dBProxyComponent.Query<AccountInfo>("{'_AccountID' : " + message.AccountID + "}");
                    if (acounts.Count > 0)
                    {
                        response.Message = "账号已存在";
                        response.IsSuccess = false;
                    }
                    else
                    {
                        Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "创建用户表和用户购物表！");
                        AccountInfo accountInfo = ComponentFactory.Create<AccountInfo>();

                        accountInfo._AccountID = accountInfo.Id;
                        accountInfo._Name = message.Nickname;
                        accountInfo._BornDate = message.BornDate;
                        accountInfo._IDCardNumber = message.IDCardNumber;
                        accountInfo._Sex = message.Sex;
                        accountInfo._IsFinishIdentify = message.IsFinishIdentify;
                        accountInfo._HeadImage = message.HeadPhoto;
                        accountInfo._FingerprintCode = "";
                        accountInfo._UserImpotentLevel = message.UserImpotentLevel;
                        accountInfo._FaceprintCode = "";
                        accountInfo._PrintType = 0;

                        await dBProxyComponent.Save(accountInfo);
                        await dBProxyComponent.SaveLog(accountInfo);
                    }
                }
                //修改数据
                else if (message.State == 2)
                {
                    //查询数据是否存在
                    var acounts = await dBProxyComponent.Query<AccountInfo>("{'_AccountID' : " + message.AccountID + "}");
                    Log.Debug(message.AccountID.ToString());
                    if (acounts.Count != 1)
                    {
                        if (acounts.Count > 1)
                        {
                            //存在多条数据
                            Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "账号数据库异常，存在多条相同的账号。");
                            response.Message = "数据异常， 1003";
                            response.IsSuccess = false;
                        }
                        else
                        {
                            //数据不存在
                            Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "账号数据库异常，账号信息不存在。");

                            response.Message = "数据异常 ， 1002";
                            response.IsSuccess = false;
                        }
                    }
                    else
                    {
                        //找到数据，修改数据
                        foreach (var item in acounts)
                        {
                            AccountInfo accountInfo = item as AccountInfo;

                            if (message.HeadPhoto.Length > 1)
                            {
                                accountInfo._HeadImage = message.HeadPhoto;
                            }

                            accountInfo._Name = message.Nickname;
                            accountInfo._Age = message.Age;
                            accountInfo._IsFinishIdentify = message.IsFinishIdentify;
                            accountInfo._FingerprintCode = message.FingerprintCode;
                            accountInfo._FaceprintCode = message.FaceprintCode;
                            accountInfo._PrintType = message.PrintType;
                            accountInfo._UserImpotentLevel = message.UserImpotentLevel;

                            await dBProxyComponent.Save(accountInfo);
                            await dBProxyComponent.SaveLog(accountInfo);
                        }

                        response.IsSuccess = true;
                        response.Message = "资料修改成功";
                        Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "修改资料完成");
                    }
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