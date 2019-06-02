using System;
using System.Net;
using System.Reflection;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
	{
		protected override void Run(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		private async ETVoid RunAsync(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			R2C_Login response = new R2C_Login();
			try
			{
                //if (message.LoginType == 1)
                //{
                //if (message.Account != "abcdef" || message.Password != "111111")
                //{
                //	response.Error = ErrorCode.ERR_AccountOrPasswordError;
                //	reply(response);
                //	return;
                //}

                // 随机分配一个Gate
                StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                //Log.Debug("C2R_LoginHandler1" + $"gate address: {MongoHelper.ToJson(config)}");
                IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
                Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

                // 向gate请求一个key,客户端可以拿着这个key连接gate
                G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() { Account = message.Account });

                string outerAddress = config.GetComponent<OuterConfig>().Address2;

                response.Address = outerAddress;
                response.Key = g2RGetLoginKey.Key;
                response.Message = MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name;
                //}
                //else if (message.LoginType == 2)
                //{
                //    //if (message.Account != "abcdef" || message.Password != "")
                //    //{
                //    //	response.Error = ErrorCode.ERR_AccountOrPasswordError;
                //    //	reply(response);
                //    //	return;
                //    //}
                //    // 随机分配一个Gate
                //    StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                //    //Log.Debug("C2R_LoginHandler2" + $"gate address: {MongoHelper.ToJson(config)}");
                //    IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
                //    Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

                //    // 向gate请求一个key,客户端可以拿着这个key连接gate
                //    G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() { Account = message.Account });

                //    string outerAddress = config.GetComponent<OuterConfig>().Address2;

                //    response.Address = outerAddress;
                //    response.Key = g2RGetLoginKey.Key;
                //}
                reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}