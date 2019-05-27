using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{


    #region AccountInfo 账户信息表 增 改 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddAccountInfoHandler : AMRpcHandler<C2G_AddAccountInfo, G2C_AddAccountInfo>
    {
        protected override async void Run(Session session, C2G_AddAccountInfo message, Action<G2C_AddAccountInfo> reply)
        {
            G2C_AddAccountInfo response = new G2C_AddAccountInfo();
            response.IsOk = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                AccountInfo accountInfo = ComponentFactory.Create<AccountInfo>();
                accountInfo._AccountID = message.AccountID;
                accountInfo._Name = message.Name;
                accountInfo._BornDate = message.BornDate;
                accountInfo._IDCardNumber = message.IDCardNumber;
                accountInfo._Sex = message.Sex;
                accountInfo._IsFinishIdentify = message.IsFinishIdentify;
                accountInfo._HeadImage = message.HeadImage;
                accountInfo._FingerprintCode = message.FingerprintCode;
                accountInfo._UserImpotentLevel = message.UserImpotentLevel;
                accountInfo._FaceprintCode = "";
                accountInfo._PrintType = 0;

                response.IsOk = true;
                response.InfoID = accountInfo.Id;

                await dBProxyComponent.Save(accountInfo);
                await dBProxyComponent.SaveLog(accountInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 修改账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateAccountInfoHandler : AMRpcHandler<C2G_UpdateAccountInfo, G2C_UpdateAccountInfo>
    {
        protected override async void Run(Session session, C2G_UpdateAccountInfo message, Action<G2C_UpdateAccountInfo> reply)
        {
            G2C_UpdateAccountInfo response = new G2C_UpdateAccountInfo();
            AccountInfo accountInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<AccountInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    //修改的时候查询不到说明数据库有问题
                    //AccountInfo Info = ComponentFactory.Create<AccountInfo>();
                    //Info._AccountID = message.AccountID;
                    //Info._Name = message.Name;
                    //Info._BornDate = message.BornDate;
                    //Info._IDCardNumber = message.IDCardNumber;
                    //Info._Sex = message.Sex;
                    //Info._IsFinishIdentify = message.IsFinishIdentify;
                    //Info._HeadImage = message.HeadImage;
                    //Info._FingerprintCode = message.FingerprintCode;
                    //Info._UserImpotentLevel = message.UserImpotentLevel;

                    //await dBProxyComponent.Save(Info);
                    //await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    accountInfo = acounts[0] as AccountInfo;

                    if (message.Name != "")
                    {
                        accountInfo._Name = message.Name;
                    }
                    if (message.BornDate != "")
                    {
                        accountInfo._BornDate = message.BornDate;
                    }
                    if (message.IDCardNumber != "")
                    {
                        accountInfo._IDCardNumber = message.IDCardNumber;
                    }
                    if (message.Sex != -1)
                    {
                        accountInfo._Sex = message.Sex;
                    }
                    if (message.IsFinishIdentify != -1)
                    {
                        accountInfo._IsFinishIdentify = message.IsFinishIdentify;
                    }
                    if (message.HeadImage != "")
                    {
                        accountInfo._HeadImage = message.HeadImage;
                    }
                    if (message.FingerprintCode != "")
                    {
                        accountInfo._FingerprintCode = message.FingerprintCode;
                    }
                    if (message.UserImpotentLevel != -1)
                    {
                        accountInfo._UserImpotentLevel = message.UserImpotentLevel;
                    }
                    if (message.FaceprintCode != "")
                    {
                        accountInfo._FaceprintCode = message.FaceprintCode;
                    }
                    if (message.PrintType != -1)
                    {
                        accountInfo._PrintType = message.PrintType;
                    }
                }

                await dBProxyComponent.Save(accountInfo);
                await dBProxyComponent.SaveLog(accountInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    
    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryAccountInfoHandler : AMRpcHandler<C2G_QueryAccountInfo, G2C_QueryAccountInfo>
    {
        protected override async void Run(Session session, C2G_QueryAccountInfo message, Action<G2C_QueryAccountInfo> reply)
        {
            G2C_QueryAccountInfo response = new G2C_QueryAccountInfo();
            AccountInfo accountInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<AccountInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    AccountInfo Info = ComponentFactory.Create<AccountInfo>();
                    Info._AccountID = message.AccountID;
                    Info._Name = "皮皮鱼";
                    Info._BornDate = DateTime.Now.ToString("yyyy-MM-dd");
                    Info._IDCardNumber = "";
                    Info._Sex = 0;
                    Info._IsFinishIdentify = 0;
                    Info._HeadImage = "";
                    Info._FingerprintCode = "";
                    Info._UserImpotentLevel = 0;
                    
                    response.Name = Info._Name;
                    response.BornDate = Info._BornDate;
                    response.IDCardNumber = Info._IDCardNumber;
                    response.Sex = Info._Sex;
                    response.IsFinishIdentify = Info._IsFinishIdentify;
                    response.HeadImage = Info._HeadImage;
                    response.FingerprintCode = Info._FingerprintCode;
                    response.UserImpotentLevel = Info._UserImpotentLevel;
                    response.FaceprintCode = Info._FaceprintCode;
                    response.PrintType = Info._PrintType;
                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    accountInfo = acounts[0] as AccountInfo;

                    response.Name = accountInfo._Name;
                    response.BornDate = accountInfo._BornDate;
                    response.IDCardNumber = accountInfo._IDCardNumber;
                    response.Sex = accountInfo._Sex;
                    response.IsFinishIdentify = accountInfo._IsFinishIdentify;
                    response.HeadImage = accountInfo._HeadImage;
                    response.FingerprintCode = accountInfo._FingerprintCode;
                    response.UserImpotentLevel = accountInfo._UserImpotentLevel;
                    response.FaceprintCode = accountInfo._FaceprintCode;
                    response.PrintType = accountInfo._PrintType;
                    await dBProxyComponent.Save(accountInfo);
                    await dBProxyComponent.SaveLog(accountInfo);
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion

    #region GuestAccount 游客信息表 增 改 查
    ///// <summary>
    ///// 创建账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_AddGuestAccountHandler : AMRpcHandler<C2G_AddGuestAccount, G2C_AddGuestAccount>
    //{
    //    protected override async void Run(Session session, C2G_AddGuestAccount message, Action<G2C_AddGuestAccount> reply)
    //    {
    //        G2C_AddGuestAccount response = new G2C_AddGuestAccount();
    //        response.IsOk = false;
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            GuestAccount guestAccount = ComponentFactory.Create<GuestAccount>();
    //            guestAccount._AccountID = guestAccount.Id;
    //            guestAccount._RandomAccount = message.RandomAccount;
    //            guestAccount._Password = message.Password;
    //            guestAccount._ManagerPassword = message.ManagerPassword;    
    //            guestAccount._EMail = message.EMail;
    //            guestAccount._LastOnlineTime = message.LastOnlineTime;
    //            guestAccount._RegistrTime = message.RegistrTime;
    //            guestAccount._CumulativeTime = message.CumulativeTime;
    //            guestAccount._Account = message.Account;
    //            response.IsOk = true;

    //            await dBProxyComponent.Save(guestAccount);
    //            await dBProxyComponent.SaveLog(guestAccount);
    //            reply(response);
    //        }
    //        catch (Exception e)
    //        {
    //            response.IsOk = false;
    //            response.Message = "数据库异常";
    //            ReplyError(response, e, reply);
    //        }
    //    }
    //}

    ///// <summary>
    ///// 修改账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_UpdateGuestAccountHandler : AMRpcHandler<C2G_UpdateGuestAccount, G2C_UpdateGuestAccount>
    //{
    //    protected override async void Run(Session session, C2G_UpdateGuestAccount message, Action<G2C_UpdateGuestAccount> reply)
    //    {
    //        G2C_UpdateGuestAccount response = new G2C_UpdateGuestAccount();
    //        response.IsOk = false;
    //        GuestAccount guestAccount = null;
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            var acounts = await dBProxyComponent.Query<GuestAccount>("{ '_RandomAccount': " + message.RandomAccount + "}");

    //            if (acounts.Count <= 0)
    //            {

    //            }
    //            else
    //            {
    //                guestAccount = acounts[0] as GuestAccount;

    //                if (message.RandomAccount != "")
    //                {
    //                    guestAccount._RandomAccount = message.RandomAccount;
    //                }
    //                if (message.Password != "")
    //                {
    //                    guestAccount._Password = message.Password;
    //                }
    //                if (message.ManagerPassword != "")
    //                {
    //                    guestAccount._ManagerPassword = message.ManagerPassword;
    //                }
    //                if (message.EMail != "")
    //                {
    //                    guestAccount._EMail = message.EMail;
    //                }
    //                if (message.LastOnlineTime != "")
    //                {
    //                    guestAccount._LastOnlineTime = message.LastOnlineTime;
    //                }
    //                if (message.RegistrTime != "")
    //                {
    //                    guestAccount._RegistrTime = message.RegistrTime;
    //                }
    //                if (message.CumulativeTime != -1)
    //                {
    //                    guestAccount._CumulativeTime += message.CumulativeTime;
    //                }
    //                if (message.Account != "")
    //                {
    //                    guestAccount._Account = message.Account;
    //                }
    //                if (message.State != -1)
    //                {
    //                    guestAccount._State = message.State;
    //                }
    //                response.IsOk = true;

    //                await dBProxyComponent.Save(guestAccount);
    //                await dBProxyComponent.SaveLog(guestAccount);
    //            }
    //            reply(response);
    //        }
    //        catch (Exception e)
    //        {
    //            response.IsOk = false;
    //            response.Message = "数据库异常";
    //            ReplyError(response, e, reply);
    //        }
    //    }
    //}
    
    ///// <summary>
    ///// 查询账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_QueryGuestAccountHandler : AMRpcHandler<C2G_QueryGuestAccount, G2C_QueryGuestAccount>
    //{
    //    protected override async void Run(Session session, C2G_QueryGuestAccount message, Action<G2C_QueryGuestAccount> reply)
    //    {
    //        G2C_QueryGuestAccount response = new G2C_QueryGuestAccount();
    //        response.IsOk = false;
    //        GuestAccount guestAccount = null;
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            var acounts = await dBProxyComponent.Query<GuestAccount>("{ '_RandomAccount': " + message.AccountID + "}");

    //            if (acounts.Count <= 0)
    //            {
    //                //游客账户查询不到不进行初始创建
    //            }
    //            else
    //            {
    //                guestAccount = acounts[0] as GuestAccount;

    //                response.RandomAccount = guestAccount._RandomAccount;
    //                response.Password = guestAccount._Password;
    //                response.ManagerPassword = guestAccount._ManagerPassword;
    //                response.EMail = guestAccount._EMail;
    //                response.LastOnlineTime = guestAccount._LastOnlineTime;
    //                response.RegistrTime = guestAccount._RegistrTime;
    //                response.CumulativeTime = guestAccount._CumulativeTime;
    //                response.Account = guestAccount._Account;
    //                response.State = guestAccount._State;
    //                response.IsOk = true;
    //            }

    //            await dBProxyComponent.Save(guestAccount);
    //            await dBProxyComponent.SaveLog(guestAccount);
    //            reply(response);
    //        }
    //        catch (Exception e)
    //        {
    //            response.Message = "数据库异常";
    //            ReplyError(response, e, reply);
    //        }
    //    }
    //}
    #endregion

    #region MainAccount 主账户信息表 增 改 查
    ///// <summary>
    ///// 游客成为主账户信息
    ///// </summary>
    //[MessageHandler(AppType.Gate)]
    //public class C2G_GuestToMainHandler : AMRpcHandler<C2G_GuestToMainAccount, G2C_GuestToMainAccount>
    //{
    //    protected override async void Run(Session session, C2G_GuestToMainAccount message, Action<G2C_GuestToMainAccount> reply)
    //    {
    //        G2C_GuestToMainAccount response = new G2C_GuestToMainAccount();
    //        response.IsOk = false;
    //        try
    //        {
    //            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

    //            var acounts = await dBProxyComponent.Query<GuestAccount>("{ '_RandomAccount': " + message.RandomAccount + "}");

    //            MainAccount mainAccount = ComponentFactory.Create<MainAccount>();
    //            if (acounts.Count > 0)
    //            {
    //                GuestAccount GuestAccount = acounts[0] as GuestAccount;
    //                mainAccount._AccountID = GuestAccount._AccountID;
    //                mainAccount._InfoID = 0;
    //                mainAccount._Account = GuestAccount._Account;
    //                mainAccount._Password = GuestAccount._Password;
    //                mainAccount._ManagerPassword = GuestAccount._ManagerPassword;
    //                mainAccount._EMail = GuestAccount._EMail;
    //                mainAccount._LastOnlineTime = GuestAccount._LastOnlineTime;
    //                mainAccount._RegistrTime = GuestAccount._RegistrTime;
    //                mainAccount._CumulativeTime = GuestAccount._CumulativeTime;
    //                mainAccount._State = GuestAccount._State;
    //                response.IsOk = true;

    //                await dBProxyComponent.Save(mainAccount);
    //                await dBProxyComponent.SaveLog(mainAccount);
    //            }


    //            reply(response);
    //        }
    //        catch (Exception e)
    //        {
    //            response.IsOk = false;
    //            response.Message = "数据库异常";
    //            ReplyError(response, e, reply);
    //        }
    //    }
    //}

    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddMainAccountHandler : AMRpcHandler<C2G_AddMainAccount, G2C_AddMainAccount>
    {
        protected override async void Run(Session session, C2G_AddMainAccount message, Action<G2C_AddMainAccount> reply)
        {
            G2C_AddMainAccount response = new G2C_AddMainAccount();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                MainAccount mainAccount = ComponentFactory.Create<MainAccount>();
                mainAccount._AccountID = message.AccountID;
                mainAccount._InfoID = message.InfoID;
                mainAccount._Account = message.Account;
                mainAccount._Password = message.Password;
                mainAccount._ManagerPassword = message.ManagerPassword;
                mainAccount._EMail = message.EMail;
                mainAccount._LastOnlineTime = message.LastOnlineTime;
                mainAccount._RegistrTime = message.RegistrTime;
                mainAccount._CumulativeTime = message.CumulativeTime;
                mainAccount._State = 0;

                await dBProxyComponent.Save(mainAccount);
                await dBProxyComponent.SaveLog(mainAccount);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 修改账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateMainAccountHandler : AMRpcHandler<C2G_UpdateMainAccount, G2C_UpdateMainAccount>
    {
        protected override async void Run(Session session, C2G_UpdateMainAccount message, Action<G2C_UpdateMainAccount> reply)
        {
            G2C_UpdateMainAccount response = new G2C_UpdateMainAccount();
            MainAccount mainAccount = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<MainAccount>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                }
                else
                {
                    mainAccount = acounts[0] as MainAccount;

                    if (message.InfoID != -1)
                    {
                        mainAccount._InfoID = message.InfoID;
                    }
                    if (message.Account != "")
                    {
                        mainAccount._Account = message.Account;
                    }
                    if (message.Password != "")
                    {
                        mainAccount._Password = message.Password;
                    }
                    if (message.EMail != "")
                    {
                        mainAccount._EMail = message.EMail;
                    }
                    if (message.LastOnlineTime != "")
                    {
                        mainAccount._LastOnlineTime = message.LastOnlineTime;
                    }
                    if (message.CumulativeTime != -1)
                    {
                        mainAccount._CumulativeTime += message.CumulativeTime;
                    }
                    if (message.State != -1)
                    {
                        mainAccount._State = message.State;
                    }
                }

                await dBProxyComponent.Save(mainAccount);
                await dBProxyComponent.SaveLog(mainAccount);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    
    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryMainAccountHandler : AMRpcHandler<C2G_QueryMainAccount, G2C_QueryMainAccount>
    {
        protected override async void Run(Session session, C2G_QueryMainAccount message, Action<G2C_QueryMainAccount> reply)
        {
            G2C_QueryMainAccount response = new G2C_QueryMainAccount();
            MainAccount mainAccount = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<MainAccount>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    //主账户查询不到不去创建
                }
                else
                {
                    mainAccount = acounts[0] as MainAccount;

                    response.InfoID = mainAccount._InfoID;
                    response.Account = mainAccount._Account;
                    response.Password = mainAccount._Password;
                    response.ManagerPassword = mainAccount._ManagerPassword;
                    response.EMail = mainAccount._EMail;
                    response.LastOnlineTime = mainAccount._LastOnlineTime;
                    response.RegistrTime = mainAccount._RegistrTime;
                    response.CumulativeTime = mainAccount._CumulativeTime;
                    response.State = mainAccount._State;
                    await dBProxyComponent.Save(mainAccount);
                    await dBProxyComponent.SaveLog(mainAccount);
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion

    #region UserAdress 用户地址表 增 删 改 查
    /// <summary>
    /// 增加删除修改用户地址表
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UserAdressHandler : AMRpcHandler<C2G_UserAdress, G2C_UserAdress>
    {
        protected override async void Run(Session session, C2G_UserAdress message, Action<G2C_UserAdress> reply)
        {
            G2C_UserAdress response = new G2C_UserAdress();
            UserAdress UserAdress = null;
            response.IsOk = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserAdress>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    UserAdress userAdress = ComponentFactory.Create<UserAdress>();
                    userAdress._AccountID = message.AccountID;
                    userAdress._InfoID = message.InfoID;
                    userAdress._AdressList = new List<string>();
                    UserAdress = userAdress;
                }
                if (acounts.Count > 0 || UserAdress != null)
                {
                    if (UserAdress == null)
                    {
                        UserAdress = acounts[0] as UserAdress;
                    }
                    if (message.Type == 1)
                    {
                        UserAdress._AdressList.Insert(0, message.NewAddress);
                        response.IsOk = true;
                    }
                    else if (message.Type == 2)
                    {
                        UserAdress._AdressList.Remove(message.OldAdress);
                        response.IsOk = true;
                    }
                    else if (message.Type == 3)
                    {
                        UserAdress._AdressList.Remove(message.OldAdress);
                        UserAdress._AdressList.Insert(0, message.NewAddress);
                        response.IsOk = true;
                    }
                }
                await dBProxyComponent.Save(UserAdress);
                await dBProxyComponent.SaveLog(UserAdress);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    
    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryUserAdressHandler : AMRpcHandler<C2G_QueryUserAdress, G2C_QueryUserAdress>
    {
        protected override async void Run(Session session, C2G_QueryUserAdress message, Action<G2C_QueryUserAdress> reply)
        {
            G2C_QueryUserAdress response = new G2C_QueryUserAdress();
            UserAdress userAdress = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserAdress>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    UserAdress Info = ComponentFactory.Create<UserAdress>();

                    Info._AccountID = message.AccountID;
                    Info._InfoID = message.InfoID;
                    Info._AdressList = new List<string>();

                    response.AdressList = RepeatedFieldAndListChangeTool.ListToRepeatedField(Info._AdressList);
                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    userAdress = acounts[0] as UserAdress;
                    
                    response.AdressList = RepeatedFieldAndListChangeTool.ListToRepeatedField(userAdress._AdressList);
                    await dBProxyComponent.Save(userAdress);
                    await dBProxyComponent.SaveLog(userAdress);
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion

    #region UserLoginRecord 用户登录记录 增 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddUserLoginRecordHandler : AMRpcHandler<C2G_AddUserLoginRecord, G2C_AddUserLoginRecord>
    {
        protected override async void Run(Session session, C2G_AddUserLoginRecord message, Action<G2C_AddUserLoginRecord> reply)
        {
            G2C_AddUserLoginRecord response = new G2C_AddUserLoginRecord();
            response.IsOk = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                UserLoginRecord UserLoginRecord = ComponentFactory.Create<UserLoginRecord>();

                UserLoginRecord._AccountID = message.AccountID;
                UserLoginRecord._InfoID = message.InfoID;
                UserLoginRecord._LoginTimes = message.LoginTimes;
                UserLoginRecord._IP = message.IP;
                UserLoginRecord._LoginLocInfo = message.LoginLocInfo;
                UserLoginRecord._AbnormalStateCode = message.AbnormalStateCode;
                UserLoginRecord._LoginDate = message.LoginDate;

                await dBProxyComponent.Save(UserLoginRecord);
                await dBProxyComponent.SaveLog(UserLoginRecord);

                response.IsOk = true;
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    
    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryUserLoginRecordHandler : AMRpcHandler<C2G_QueryUserLoginRecord, G2C_QueryUserLoginRecord>
    {
        protected override async void Run(Session session, C2G_QueryUserLoginRecord message, Action<G2C_QueryUserLoginRecord> reply)
        {
            G2C_QueryUserLoginRecord response = new G2C_QueryUserLoginRecord();
            UserLoginRecord UserLoginRecord = null;
            response.IsOk = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserLoginRecord>("{ '_AccountID': " + message.AccountID + "}");
                
                if (acounts.Count > 0)
                {
                    for (int i = 0; i < acounts.Count; i++)
                    {
                        UserLoginRecord = acounts[i] as UserLoginRecord;

                        LoginRecord record = new LoginRecord();
                        record.AccountID = UserLoginRecord._AccountID;
                        record.InfoID = UserLoginRecord._InfoID;
                        record.LoginTimes = UserLoginRecord._LoginTimes;
                        record.IP = UserLoginRecord._IP;
                        record.LoginLocInfo = UserLoginRecord._LoginLocInfo;
                        record.AbnormalStateCode = UserLoginRecord._AbnormalStateCode;
                        record.LoginDate = UserLoginRecord._LoginDate;
                        response.Userlist.Add(record);
                    }
                }
                
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion

    #region UserPortrait 用户画像表 增 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddUserPortraitHandler : AMRpcHandler<C2G_AddUserPortrait, G2C_AddUserPortrait>
    {
        protected override async void Run(Session session, C2G_AddUserPortrait message, Action<G2C_AddUserPortrait> reply)
        {
            G2C_AddUserPortrait response = new G2C_AddUserPortrait();
            UserPortrait UserPortrait = null;
            response.IsOk = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                var acounts = await dBProxyComponent.Query<UserPortrait>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    UserPortrait userPortrait = ComponentFactory.Create<UserPortrait>();
                    userPortrait._AccountID = message.AccountID;
                    userPortrait._InfoID = message.InfoID;
                    userPortrait._PortraitList = new List<int>();
                    UserPortrait = userPortrait;
                }
                if (acounts.Count > 0 || UserPortrait != null)
                {
                    if (UserPortrait == null)
                    {
                        UserPortrait = acounts[0] as UserPortrait;
                    }
                    UserPortrait._PortraitList.Insert(0, message.Portrait);
                    response.IsOk = true;
                }

                await dBProxyComponent.Save(UserPortrait);
                await dBProxyComponent.SaveLog(UserPortrait);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryUserPortraitHandler : AMRpcHandler<C2G_QueryUserPortrait, G2C_QueryUserPortrait>
    {
        protected override async void Run(Session session, C2G_QueryUserPortrait message, Action<G2C_QueryUserPortrait> reply)
        {
            G2C_QueryUserPortrait response = new G2C_QueryUserPortrait();
            UserPortrait UserPortrait = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserPortrait>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    UserPortrait Info = ComponentFactory.Create<UserPortrait>();

                    Info._AccountID = message.AccountID;
                    Info._InfoID = message.InfoID;
                    Info._PortraitList = new List<int>();

                    response.PortraitList = RepeatedFieldAndListChangeTool.ListToRepeatedField(Info._PortraitList);
                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    UserPortrait = acounts[0] as UserPortrait;
                    
                    response.PortraitList = RepeatedFieldAndListChangeTool.ListToRepeatedField(UserPortrait._PortraitList);
                    await dBProxyComponent.Save(UserPortrait);
                    await dBProxyComponent.SaveLog(UserPortrait);
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion

    #region UserProductInfo 用户产品信息 增 删 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UserProductInfoHandler : AMRpcHandler<C2G_UserProductInfo, G2C_UserProductInfo>
    {
        protected override async void Run(Session session, C2G_UserProductInfo message, Action<G2C_UserProductInfo> reply)
        {
            G2C_UserProductInfo response = new G2C_UserProductInfo();
            UserProductInfo UserProductInfo = null;
            response.IsOk = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                var acounts = await dBProxyComponent.Query<UserProductInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    UserProductInfo userProductInfo = ComponentFactory.Create<UserProductInfo>();
                    userProductInfo._AccountID = message.AccountID;
                    userProductInfo._InfoID = message.InfoID;
                    userProductInfo._ProductList = new List<int>();
                    userProductInfo._UserPoint = 0;

                    UserProductInfo = userProductInfo;
                }
                if (acounts.Count > 0 || UserProductInfo != null)
                {
                    if (UserProductInfo == null)
                    {
                        UserProductInfo = acounts[0] as UserProductInfo;
                    }
                    if (message.Type == 1)
                    {
                        UserProductInfo._ProductList.Insert(0, message.Product);
                        response.IsOk = true;
                    }
                    if (message.Type == 2)
                    {
                        UserProductInfo._ProductList.Remove(message.Product);
                        response.IsOk = true;
                    }
                }

                await dBProxyComponent.Save(UserProductInfo);
                await dBProxyComponent.SaveLog(UserProductInfo);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryUserProductInfoHandler : AMRpcHandler<C2G_QueryUserProductInfo, G2C_QueryUserProductInfo>
    {
        protected override async void Run(Session session, C2G_QueryUserProductInfo message, Action<G2C_QueryUserProductInfo> reply)
        {
            G2C_QueryUserProductInfo response = new G2C_QueryUserProductInfo();
            UserProductInfo UserProductInfo = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserProductInfo>("{ '_AccountID': " + message.AccountID + "}");

                if (acounts.Count <= 0)
                {
                    UserProductInfo Info = ComponentFactory.Create<UserProductInfo>();

                    Info._AccountID = message.AccountID;
                    Info._InfoID = message.InfoID;
                    Info._ProductList = new List<int>();
                    Info._UserPoint = 0;

                    response.ProductList = RepeatedFieldAndListChangeTool.ListToRepeatedField(Info._ProductList);
                    response.UserPoint = Info._UserPoint;
                    await dBProxyComponent.Save(Info);
                    await dBProxyComponent.SaveLog(Info);
                }
                else
                {
                    UserProductInfo = acounts[0] as UserProductInfo;
                    
                    response.ProductList = RepeatedFieldAndListChangeTool.ListToRepeatedField(UserProductInfo._ProductList);
                    response.UserPoint = UserProductInfo._UserPoint;

                    await dBProxyComponent.Save(UserProductInfo);
                    await dBProxyComponent.SaveLog(UserProductInfo);
                }

                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion
    
    #region UserProductRecord 用户产品记录 增 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddUserProductRecordHandler : AMRpcHandler<C2G_AddUserProductRecord, G2C_AddUserProductRecord>
    {
        protected override async void Run(Session session, C2G_AddUserProductRecord message, Action<G2C_AddUserProductRecord> reply)
        {
            G2C_AddUserProductRecord response = new G2C_AddUserProductRecord();
            response.IsOk = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                UserProductRecord UserProductRecord = ComponentFactory.Create<UserProductRecord>();

                UserProductRecord._AccountID = message.AccountID;
                UserProductRecord._InfoID = message.InfoID;
                UserProductRecord._ProductID = message.ProductID;
                UserProductRecord._Level = message.Level;
                UserProductRecord._StartDate = message.StartDate;
                UserProductRecord._EndDate = message.EndDate;
                UserProductRecord._RightCodeList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.RightCodeList);
                UserProductRecord._BuyType = message.BuyType;
                UserProductRecord._Price = message.Price;
                response.IsOk = true;

                await dBProxyComponent.Save(UserProductRecord);
                await dBProxyComponent.SaveLog(UserProductRecord);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryUserProductRecordHandler : AMRpcHandler<C2G_QueryUserProductRecord, G2C_QueryUserProductRecord>
    {
        protected override async void Run(Session session, C2G_QueryUserProductRecord message, Action<G2C_QueryUserProductRecord> reply)
        {
            G2C_QueryUserProductRecord response = new G2C_QueryUserProductRecord();
            UserProductRecord UserProductRecord = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserProductRecord>("{ '_AccountID': " + message.AccountID + "}");
                
                if (acounts.Count > 0)
                {
                    for (int i = 0; i < acounts.Count; i++)
                    {
                        UserProductRecord = acounts[i] as UserProductRecord;

                        ProductRecord record = new ProductRecord();
                        record.AccountID = UserProductRecord._AccountID;
                        record.InfoID = UserProductRecord._InfoID;
                        record.ProductID = UserProductRecord._ProductID;
                        record.Level = UserProductRecord._Level;
                        record.StartDate = UserProductRecord._StartDate;
                        record.EndDate = UserProductRecord._EndDate;
                        record.RightCodeList = RepeatedFieldAndListChangeTool.ListToRepeatedField(UserProductRecord._RightCodeList);
                        record.BuyType = UserProductRecord._BuyType;
                        record.Price = UserProductRecord._Price;
                        response.Userlist.Add(record);
                    }
                }

                await dBProxyComponent.Save(UserProductRecord);
                await dBProxyComponent.SaveLog(UserProductRecord);
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion
    
    #region UserRequestRecord 用户申请记录 增 查
    /// <summary>
    /// 创建账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddUserRequestRecordHandler : AMRpcHandler<C2G_AddUserRequestRecord, G2C_AddUserRequestRecord>
    {
        protected override async void Run(Session session, C2G_AddUserRequestRecord message, Action<G2C_AddUserRequestRecord> reply)
        {
            G2C_AddUserRequestRecord response = new G2C_AddUserRequestRecord();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                UserRequestRecord UserRequestRecord = ComponentFactory.Create<UserRequestRecord>();
                UserRequestRecord._AccountID = message.AccountID;
                UserRequestRecord._InfoID = message.InfoID;
                UserRequestRecord._RequestTimes = message.RequestTimes;
                UserRequestRecord._IP = message.IP;
                UserRequestRecord._RequestInfo = message.RequestInfo;
                UserRequestRecord._RequestTypeCode = message.RequestTypeCode;
                UserRequestRecord._RequestResultCode = message.RequestResultCode;

                await dBProxyComponent.Save(UserRequestRecord);
                await dBProxyComponent.SaveLog(UserRequestRecord);
                reply(response);
            }
            catch (Exception e)
            {
                response.IsOk = false;
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }

    /// <summary>
    /// 查询账户信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryUserRequestRecordHandler : AMRpcHandler<C2G_QueryUserRequestRecord, G2C_QueryUserRequestRecord>
    {
        protected override async void Run(Session session, C2G_QueryUserRequestRecord message, Action<G2C_QueryUserRequestRecord> reply)
        {
            G2C_QueryUserRequestRecord response = new G2C_QueryUserRequestRecord();
            UserRequestRecord UserRequestRecord = null;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<UserRequestRecord>("{ '_AccountID': " + message.AccountID + "}");
                
                if (acounts.Count > 0)
                {
                    for (int i = 0; i < acounts.Count; i++)
                    {
                        UserRequestRecord = acounts[i] as UserRequestRecord;

                        RequestRecord record = new RequestRecord();
                        record.AccountID = UserRequestRecord._AccountID;
                        record.InfoID = UserRequestRecord._InfoID;
                        record.RequestTimes = UserRequestRecord._RequestTimes;
                        record.IP = UserRequestRecord._IP;
                        record.RequestInfo = UserRequestRecord._RequestInfo;
                        record.RequestTypeCode = UserRequestRecord._RequestTypeCode;
                        record.RequestResultCode = UserRequestRecord._RequestResultCode;
                        response.Userlist.Add(record);
                    }
                }

                await dBProxyComponent.Save(UserRequestRecord);
                await dBProxyComponent.SaveLog(UserRequestRecord);
                reply(response);
            }
            catch (Exception e)
            {
                response.Message = "数据库异常";
                ReplyError(response, e, reply);
            }
        }
    }
    #endregion

}
