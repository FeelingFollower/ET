using System;
using System.Collections.Generic;
using System.Reflection;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_ProductInfoMessageHandler : AMRpcHandler<C2G_AddressManager, G2C_AddressManager>
    {
        protected override async void Run(Session session, C2G_AddressManager message, Action<G2C_AddressManager> reply)
        {
            G2C_AddressManager response = new G2C_AddressManager();
            try
            {
                Log.Debug("地址管理");
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                //读取数据表
                if (message.DoState == 1)
                {
                    var acounts = await dBProxyComponent.Query<UserAdress>("{'_AccountID' : " + message.AccountID + "}");
                    if (acounts.Count == 1)
                    {
                        foreach (UserAdress item in acounts)
                        {
                            response.InfoID = item._InfoID;
                            response.AdressList = RepeatedFieldAndListChangeTool.ListToRepeatedField(item._AdressList);
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
                        response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "地址数据错误";
                    }

                }
                //操作数据表
                else if(message.DoState == 2)
                {
                    Log.Debug("操作数据表");
                    var acounts = await dBProxyComponent.Query<UserAdress>("{'_AccountID' : " + message.AccountID + "}");
                    //修改
                    if (acounts.Count == 1)
                    {
                        foreach (UserAdress item in acounts)
                        {
                            if (message.AddressType == 1)
                            {
                                item._AdressList.Insert(0, message.DetailsAddress);
                                await dBProxyComponent.Save(item);
                                response.IsSuccess = true;
                                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "修改地址成功";
                            }
                            if (message.AddressType == 2)
                            {
                                item._AdressList.Remove(message.DetailsAddress);
                                await dBProxyComponent.Save(item);
                                response.IsSuccess = true;
                                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "修改地址成功";
                            }
                        }
                    }
                    //保存数据表
                    else if (acounts.Count == 0)
                    {
                        UserAdress adressData = ComponentFactory.Create<UserAdress>();

                        adressData._AccountID = message.AccountID;
                        adressData._InfoID = adressData.Id;
                        adressData._AdressList = new List<string>();

                        await dBProxyComponent.Save(adressData);
                        Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "添加地址成功");

                        response.IsSuccess = true;
                        response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "添加地址成功";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + "地址数据错误";
                    }

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