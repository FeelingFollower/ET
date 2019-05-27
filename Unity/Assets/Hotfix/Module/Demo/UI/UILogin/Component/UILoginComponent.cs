using System;
using System.Collections.Generic;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UiLoginComponentSystem : AwakeSystem<UILoginComponent>
	{
		public override void Awake(UILoginComponent self)
		{
			self.Awake();
		}
	}
	
	public class UILoginComponent: Component
	{
		private GameObject account;
		private GameObject loginBtn;
		//private GameObject GusetLoginBtn;
		private GameObject RegisterBtn;
		private GameObject FindPasswordBtn;

		public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			loginBtn = rc.Get<GameObject>("LoginBtn");
            //GusetLoginBtn = rc.Get<GameObject>("GusetLoginBtn");
            RegisterBtn = rc.Get<GameObject>("RegisterBtn");
            FindPasswordBtn = rc.Get<GameObject>("FindPasswordBtn");
			loginBtn.GetComponent<Button>().onClick.Add(OnLogin);
            //GusetLoginBtn.GetComponent<Button>().onClick.Add(OnGuestLogin);
            RegisterBtn.GetComponent<Button>().onClick.Add(OnRegister);
            FindPasswordBtn.GetComponent<Button>().onClick.Add(OnFindPassword);
			this.account = rc.Get<GameObject>("Account");
		}

        List<string> st = new List<string>();
        List<long> lo = new List<long>();
        public async void OnFindPassword()
        {
            Debug.Log("UILoginComponent OnFindPassword" + "你是谁");

            // 创建一个ETModel层的Session
            ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

            // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
            Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
            //Debug.Log("属性包");

            //st.Add("属性包");
            //G2C_AddProductInfoData g2C_RequestActivityIndexCount = (G2C_AddProductInfoData)await realmSession.Call(new C2G_AddProductInfoData()
            //{
            //    AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Count = 50,
            //    DiscountsTags = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    PorductInfoDis = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    PorductInfoTags = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Price = 50,
            //    ProductID = 0,
            //    ProductInfoHeadImage = "我是头图",
            //    ProductInfoImages = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    ProductInfoName = "我也不知道",
            //    ProductInfoSort = 1,
            //    ProductPublishGround = "长沙",
            //    ProductShopSort = 2,
            //    ServiceList = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //    ShopInfoID = 123,
            //});
            //Debug.Log(g2C_RequestActivityIndexCount.IsSuccess);
            //Debug.Log(g2C_RequestActivityIndexCount.Message);

            G2C_QueryProductInfoDataCount r2CLogin = (G2C_QueryProductInfoDataCount)await realmSession.Call(new C2G_QueryProductInfoDataCount()
            {
                Count = 2,
                PriceMax = 100,
                PriceMin = 0,
                ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
                QueryConntent = "我",
                QueryType = 2,
                ShopInfoID = 123,
                StateType = 1,
            });
            Debug.Log(r2CLogin.IsSuccess);
            Debug.Log(r2CLogin.Message);
            if (r2CLogin.IsSuccess)
            {
                foreach (var item in r2CLogin.ProductInfos)
                {
                    Debug.Log(item);
                }
            }

        }

        public void OnRegister()
        {
            Debug.Log("UILoginComponent OnRegister" + "打开主账户注册页面");
        }

        /// <summary>
        /// 游客登录按钮
        /// </summary>
        //public void OnGuestLogin()
        //{
        //    string GusetID = "";
        //    //获取游客手机上id

        //    //如果有文件
        //    if (true)
        //    {
        //        GuestLoginHelper.OnGuestLoginAsync(GusetID).Coroutine();
        //    }
        //    else
        //    {
        //        //打开游客注册页面
        //    }
        //}

        public void OnLogin()
		{
			LoginHelper.OnLoginAsync(this.account.GetComponent<InputField>().text).Coroutine();
		}
	}
}
