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
            #region 创建商品
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
            //    ShopInfoID = 456,
            //});
            //Debug.Log(g2C_RequestActivityIndexCount.IsSuccess);
            //Debug.Log(g2C_RequestActivityIndexCount.Message);
            #endregion

            #region 查询商品id列表 查询商品 删除商品

            //G2C_QueryProductInfoDataCount r2CLogin = (G2C_QueryProductInfoDataCount)await realmSession.Call(new C2G_QueryProductInfoDataCount()
            //{
            //    Count = 1,
            //    PriceMax = 100,
            //    PriceMin = 0,
            //    ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //    QueryConntent = "我",
            //    QueryType = 2,
            //    ShopInfoID = 456,
            //    StateType = 1,
            //});
            //Debug.Log(r2CLogin.IsSuccess);
            //Debug.Log(r2CLogin.Message);
            //if (r2CLogin.IsSuccess)
            //{
            //    foreach (var item in r2CLogin.ProductInfos)
            //    {
            //        Debug.Log(item);
            //        //删除商品
            //        //G2C_DelProductInfoData g2C_DelProductInfoData = (G2C_DelProductInfoData)await realmSession.Call(new C2G_DelProductInfoData()
            //        //{
            //        //    ShopInfoID = 0,
            //        //    ProductInfoID = item,
            //        //});
            //        //Debug.Log(g2C_DelProductInfoData.IsSuccess);
            //        //Debug.Log(g2C_DelProductInfoData.Message);

            //        //查询商品
            //        ////G2C_QueryProductInfoData g2C_QueryProductInfoData = (G2C_QueryProductInfoData)await realmSession.Call(new C2G_QueryProductInfoData()
            //        ////{
            //        ////    ShopInfoID = 0,
            //        ////    ProductInfoID = item,
            //        ////});
            //        ////Debug.Log(g2C_QueryProductInfoData.IsSuccess);
            //        ////Debug.Log(g2C_QueryProductInfoData.Message);
            //        ////if (g2C_QueryProductInfoData.IsSuccess)
            //        ////{
            //        ////    Debug.Log("ShopInfoID" + g2C_QueryProductInfoData.ShopInfoID);
            //        ////}
            //    }
            //}
            #endregion

            #region 查询自己收藏的商品id列表
            //G2C_QueryMyProductInfoDataCount g2C_QueryMyProductInfoDataCount = (G2C_QueryMyProductInfoDataCount)await realmSession.Call(new C2G_QueryMyProductInfoDataCount()
            //{
            //    Count = 50,
            //    UserID = 888888888888888,
            //    ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //});
            //Debug.Log(g2C_QueryMyProductInfoDataCount.IsSuccess);
            //Debug.Log(g2C_QueryMyProductInfoDataCount.Message);
            //foreach (var item in g2C_QueryMyProductInfoDataCount.ProductInfos)
            //{
            //    Debug.Log("收藏的商品id：" + item);
            //}
            #endregion

            #region 修改商品
            //st.Add("修改商品");

            //G2C_UpdateProductInfoData g2C_UpdateProductInfoData = (G2C_UpdateProductInfoData)await realmSession.Call(new C2G_UpdateProductInfoData()
            //{
            //    AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Count = 50,
            //    DiscountsTags = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    PorductInfoDis = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    PorductInfoTags = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Price = 50,
            //    ProductInfoHeadImage = "修改头图",
            //    ProductInfoImages = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    ProductInfoName = "修改名字",
            //    ProductInfoSort = 1,
            //    ProductPublishGround = "修改地址",
            //    ProductShopSort = 2,
            //    ServiceList = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //    ShopInfoID = 456,
            //    ProductInfoID = 281474976710777,
            //});
            //Debug.Log(g2C_UpdateProductInfoData.IsSuccess);
            //Debug.Log(g2C_UpdateProductInfoData.Message);
            #endregion

            #region 浏览点赞收藏
            //G2C_ViewProductInfoData g2C_ViewProductInfoData = (G2C_ViewProductInfoData)await realmSession.Call(new C2G_ViewProductInfoData()
            //{
            //    ShopInfoID = 456,
            //    ProductInfoID = 281474976710777,
            //    Userid = 888888888888888,
            //    Type = 5,
            //});
            //Debug.Log(g2C_ViewProductInfoData.IsSuccess);
            //Debug.Log(g2C_ViewProductInfoData.Message);
            #endregion

            #region 评价 TODO
            //G2C_ProductInfoDataMessage g2C_ProductInfoDataMessage = (G2C_ProductInfoDataMessage)await realmSession.Call(new C2G_ProductInfoDataMessage()
            //{
            //    ShopInfoID = 456,
            //    ProductInfoID = 281474976710777,
            //    UserID = 888888888888888,
            //    Type = 2,
            //    Evaluate = "你是猪",
            //    ProductOrderID = 281474976710758,
            //    Score = 5,
            //    PublicMessage = "我有留言吗",
            //});
            //Debug.Log(g2C_ProductInfoDataMessage.IsSuccess);
            //Debug.Log(g2C_ProductInfoDataMessage.Message);
            #endregion

            #region 审核 
            //G2C_AuditProductInfoData g2C_AuditProductInfoData = (G2C_AuditProductInfoData)await realmSession.Call(new C2G_AuditProductInfoData()
            //{
            //    ShopInfoID = 456,
            //    ProductInfoID = 281474976710777,
            //    AuditMessage = "你是爸爸",
            //    Type = 1,
            //});
            //Debug.Log(g2C_AuditProductInfoData.IsSuccess);
            //Debug.Log(g2C_AuditProductInfoData.Message);
            #endregion

            #region 创建订单
            //st.Add("属性包");
            //G2C_AddProductOrderData g2C_AddProductOrderData = (G2C_AddProductOrderData)await realmSession.Call(new C2G_AddProductOrderData()
            //{
            //    AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Count = 50,
            //    Price = 50,
            //    ServiceList = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //    OrderInfo = "这是备注吗",
            //    OrderSort = 1,
            //    ProductInfoID = 281474976710777,
            //    SimpleOrderID = 0,
            //    UserID = 888888888888888,
            //});
            //Debug.Log(g2C_AddProductOrderData.IsSuccess);
            //Debug.Log(g2C_AddProductOrderData.Message);

            #endregion

            #region 查询自己的商品订单id列表  查询商品订单
            //G2C_QueryProductInfoOrderCount g2C_QueryProductInfoOrderCount = (G2C_QueryProductInfoOrderCount)await realmSession.Call(new C2G_QueryProductInfoOrderCount()
            //{
            //    Count = 50,
            //    Userid = 888888888888888,
            //    ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //});
            //Debug.Log(g2C_QueryProductInfoOrderCount.IsSuccess);
            //Debug.Log(g2C_QueryProductInfoOrderCount.Message);
            //foreach (long item in g2C_QueryProductInfoOrderCount.ProductInfos)
            //{
            //    Debug.Log("商品订单id：" + item);
            //    G2C_QueryProductInfoOrder G2C_QueryProductInfoOrder = (G2C_QueryProductInfoOrder)await realmSession.Call(new C2G_QueryProductInfoOrder()
            //    {
            //        UserID = 888888888888888,
            //        ProductOrderID = item,
            //    });
            //    Debug.Log(G2C_QueryProductInfoOrder.IsSuccess);
            //    Debug.Log(G2C_QueryProductInfoOrder.Message);
            //    if (G2C_QueryProductInfoOrder.IsSuccess)
            //    {
            //        Debug.Log("G2C_QueryProductInfoOrder" + G2C_QueryProductInfoOrder.ProductInfoID);
            //    }
            //}
            #endregion

            #region 商品订单状态管理
            //G2C_AuditProductOrderMessage g2C_AuditProductOrderMessage = (G2C_AuditProductOrderMessage)await realmSession.Call(new C2G_AuditProductOrderMessage()
            //{
            //    CancelMessage="我不想要你了，哦耶！",
            //    LogisticsNumber = "3838438oy",
            //    PayPlatformNumber = "我有交易吗",
            //    PayType = 1,
            //    ProductOrderID = 281474976710758,
            //    RecedeMessage = "我有退款吗",
            //    Type = 6,
            //    Userid = 888888888888888,
            //});
            //Debug.Log(g2C_AuditProductOrderMessage.IsSuccess);
            //Debug.Log(g2C_AuditProductOrderMessage.Message);
            #endregion

            #region 删除订单
            //G2C_DelProductInfoOrder g2C_DelProductInfoOrder = (G2C_DelProductInfoOrder)await realmSession.Call(new C2G_DelProductInfoOrder()
            //{
            //    ProductOrderID = 281474976710758,
            //    Userid = 888888888888888,
            //});
            //Debug.Log(g2C_DelProductInfoOrder.IsSuccess);
            //Debug.Log(g2C_DelProductInfoOrder.Message);
            #endregion

            #region 添加购物车
            //st.Add("购物车");
            //G2C_AddSimpleOrder g2C_AddSimpleOrder = (G2C_AddSimpleOrder)await realmSession.Call(new C2G_AddSimpleOrder()
            //{
            //    AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Count = 50,
            //    Price = 50,
            //    ProductInfoID = 281474976710777,
            //    UserID = 888888888888888,
            //});
            //Debug.Log(g2C_AddSimpleOrder.IsSuccess);
            //Debug.Log(g2C_AddSimpleOrder.Message);
            #endregion

            #region 查询自己的购物车id列表  查询购物车
            //G2C_QuerySimpleOrderCount g2C_QuerySimpleOrderCount = (G2C_QuerySimpleOrderCount)await realmSession.Call(new C2G_QuerySimpleOrderCount()
            //{
            //    Count = 50,
            //    Userid = 888888888888888,
            //    ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //});
            //Debug.Log(g2C_QuerySimpleOrderCount.IsSuccess);
            //Debug.Log(g2C_QuerySimpleOrderCount.Message);
            //foreach (long item in g2C_QuerySimpleOrderCount.ProductInfos)
            //{
            //    Debug.Log("商品订单id：" + item);
            //    G2C_QuerySimpleOrder g2C_QuerySimpleOrder = (G2C_QuerySimpleOrder)await realmSession.Call(new C2G_QuerySimpleOrder()
            //    {
            //        Userid = 888888888888888,
            //        SimpleOrderID = item,
            //    });
            //    Debug.Log(g2C_QuerySimpleOrder.IsSuccess);
            //    Debug.Log(g2C_QuerySimpleOrder.Message);
            //    if (g2C_QuerySimpleOrder.IsSuccess)
            //    {
            //        Debug.Log("G2C_QueryProductInfoOrder" + g2C_QuerySimpleOrder.ProductInfoID);
            //    }
            //}
            #endregion

            #region 购物车修改
            //st.Add("购物车修改");
            //G2C_UpdateSimpleOrder g2C_AddSimpleOrder = (G2C_UpdateSimpleOrder)await realmSession.Call(new C2G_UpdateSimpleOrder()
            //{
            //    AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    Count = 500,
            //    Price = 100,
            //    OrderID = 281474976710764,
            //    UserID = 888888888888888,
            //});
            //Debug.Log(g2C_AddSimpleOrder.IsSuccess);
            //Debug.Log(g2C_AddSimpleOrder.Message);
            #endregion

            #region 购物车删除
            //G2C_DelSimpleOrder g2C_DelSimpleOrder = (G2C_DelSimpleOrder)await realmSession.Call(new C2G_DelSimpleOrder()
            //{
            //    OrderID = 281474976710764,
            //    UserID = 888888888888888,
            //});
            //Debug.Log(g2C_DelSimpleOrder.IsSuccess);
            //Debug.Log(g2C_DelSimpleOrder.Message);
            #endregion

            #region 添加服务
            //G2C_AddServiceInfo g2C_AddServiceInfo = (G2C_AddServiceInfo)await realmSession.Call(new C2G_AddServiceInfo()
            //{
            //    ServiceInfoContent = "打你不需要理由",
            //    ServiceInfoName = "服务险",
            //});
            //Debug.Log(g2C_AddServiceInfo.IsSuccess);
            //Debug.Log(g2C_AddServiceInfo.Message);
            #endregion

            #region 查询服务id列表  查询服务
            //G2C_QueryServiceInfoCount g2C_QueryServiceInfoCount = (G2C_QueryServiceInfoCount)await realmSession.Call(new C2G_QueryServiceInfoCount()
            //{

            //});
            //Debug.Log(g2C_QueryServiceInfoCount.IsSuccess);
            //Debug.Log(g2C_QueryServiceInfoCount.Message);
            //foreach (long item in g2C_QueryServiceInfoCount.ServiceInfos)
            //{
            //    Debug.Log("商品订单id：" + item);
            //    G2C_QueryServiceInfo g2C_QueryServiceInfo = (G2C_QueryServiceInfo)await realmSession.Call(new C2G_QueryServiceInfo()
            //    {
            //        ServiceInfoID = item,
            //    });
            //    Debug.Log(g2C_QueryServiceInfo.IsSuccess);
            //    Debug.Log(g2C_QueryServiceInfo.Message);
            //    if (g2C_QueryServiceInfo.IsSuccess)
            //    {
            //        Debug.Log("G2C_QueryProductInfoOrder" + g2C_QueryServiceInfo.ServiceInfoName);
            //    }
            //}
            #endregion

            #region 服务修改
            //G2C_UpdateServiceInfo g2C_UpdateServiceInfo = (G2C_UpdateServiceInfo)await realmSession.Call(new C2G_UpdateServiceInfo()
            //{
            //    ServiceInfoContent = "爱你不需要理由",
            //    ServiceInfoName = "爱情险",
            //    ServiceInfoID = 281474976710785,
            //});
            //Debug.Log(g2C_UpdateServiceInfo.IsSuccess);
            //Debug.Log(g2C_UpdateServiceInfo.Message);
            #endregion

            #region 服务删除
            //G2C_DelServiceInfo g2C_DelServiceInfo = (G2C_DelServiceInfo)await realmSession.Call(new C2G_DelServiceInfo()
            //{
            //    ServiceInfoID = 281474976710785,
            //});
            //Debug.Log(g2C_DelServiceInfo.IsSuccess);
            //Debug.Log(g2C_DelServiceInfo.Message);
            #endregion

            #region 创建商铺
            //st.Add("我是商铺吗");
            //G2C_AddShopInfoData g2C_AddShopInfoData = (G2C_AddShopInfoData)await realmSession.Call(new C2G_AddShopInfoData()
            //{
            //    Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    ShopInfoID = 0,
            //    IsAuthentication = false,
            //    ShopInfoHeadImage = "我是商铺头图",
            //    ShopInfoImages = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    ShopName = "麻花店",
            //    ShopPublishGround = "长沙",
            //    UserID = 888888888888888,
            //});
            //Debug.Log(g2C_AddShopInfoData.IsSuccess);
            //Debug.Log(g2C_AddShopInfoData.Message);
            #endregion

            #region 查询自己的收藏的商铺id列表  查询商铺信息
            //G2C_QueryMyShopInfoDataCount g2C_QueryMyShopInfoDataCount = (G2C_QueryMyShopInfoDataCount)await realmSession.Call(new C2G_QueryMyShopInfoDataCount()
            //{
            //    Count = 50,
            //    ShopInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //    UserID = 888888888888888,
            //});
            //Debug.Log(g2C_QueryMyShopInfoDataCount.IsSuccess);
            //Debug.Log(g2C_QueryMyShopInfoDataCount.Message);
            //G2C_QueryShopInfoDataCount g2C_QueryShopInfoDataCount = (G2C_QueryShopInfoDataCount)await realmSession.Call(new C2G_QueryShopInfoDataCount()
            //{
            //    Count = 50,
            //    ShopInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //    QueryType = 1,
            //    QueryConntent = "麻",
            //});
            //Debug.Log(g2C_QueryShopInfoDataCount.IsSuccess);
            //Debug.Log(g2C_QueryShopInfoDataCount.Message);
            //foreach (long item in g2C_QueryShopInfoDataCount.ShopInfos)
            //{
            //    Debug.Log("商品订单id：" + item);
            //    G2C_QueryShopInfoData g2C_QueryShopInfoData = (G2C_QueryShopInfoData)await realmSession.Call(new C2G_QueryShopInfoData()
            //    {
            //        ShopInfoID = item,
            //    });
            //    Debug.Log(g2C_QueryShopInfoData.IsSuccess);
            //    Debug.Log(g2C_QueryShopInfoData.Message);
            //    if (g2C_QueryShopInfoData.IsSuccess)
            //    {
            //        Debug.Log("G2C_QueryProductInfoOrder" + g2C_QueryShopInfoData.ShopName);
            //    }
            //}
            #endregion

            #region 修改商铺
            //st.Add("修改商铺");

            //G2C_UpdateShopInfoData g2C_UpdateShopInfoData = (G2C_UpdateShopInfoData)await realmSession.Call(new C2G_UpdateShopInfoData()
            //{
            //    Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //    ShopInfoID = 281474976710810,
            //    ShopImageType = 2,
            //    ShopInfoHeadImage = "我是修改后的商铺头图",
            //    ShopInfoImage = "我是新的图片",
            //    ShopName = "修改后的麻花店",
            //    ShopPublishGround = "修改后的长沙",
            //    ShopSort = "我是新的店铺内部类别",
            //    ShopSortType = 2,
            //    ShopType = 5,
            //});
            //Debug.Log(g2C_UpdateShopInfoData.IsSuccess);
            //Debug.Log(g2C_UpdateShopInfoData.Message);

            //G2C_SUpdateShopInfoData g2C_SUpdateShopInfoData = (G2C_SUpdateShopInfoData)await realmSession.Call(new C2G_SUpdateShopInfoData()
            //{
            //    ShopInfoID = 281474976710810,
            //    PayShopBailMoney = 500,
            //    ShopLevel = 10,
            //});
            //Debug.Log(g2C_SUpdateShopInfoData.IsSuccess);
            //Debug.Log(g2C_SUpdateShopInfoData.Message);
            #endregion

            #region 商铺审核
            //G2C_AuditShopInfoData g2C_AuditShopInfoData = (G2C_AuditShopInfoData)await realmSession.Call(new C2G_AuditShopInfoData()
            //{
            //    ShopInfoID = 281474976710810,
            //    PayShopBailMoney = 500,
            //    Type = 1,
            //    AuditMessage = "还是给你通过把",
            //});
            //Debug.Log(g2C_AuditShopInfoData.IsSuccess);
            //Debug.Log(g2C_AuditShopInfoData.Message);
            #endregion

            #region 收藏店铺
            //G2C_ViewShopInfoData g2C_ViewShopInfoData = (G2C_ViewShopInfoData)await realmSession.Call(new C2G_ViewShopInfoData()
            //{
            //    ShopInfoID = 281474976710810,
            //    Type = 2,
            //    Userid = 888888888888888,
            //});
            //Debug.Log(g2C_ViewShopInfoData.IsSuccess);
            //Debug.Log(g2C_ViewShopInfoData.Message);
            #endregion

            #region 添加店铺活动
            //st.Add("我是优惠描述");
            //G2C_AddShopActivityInfo g2C_AddShopActivityInfo = (G2C_AddShopActivityInfo)await realmSession.Call(new C2G_AddShopActivityInfo()
            //{
            //    ShopInfoID = 281474976710810,
            //    Count = 10,
            //    DiscountsAlert = "我不发货的哦",
            //    DiscountsSort  = 1,
            //    DiscountsSortFields = "优惠说明",
            //    DisProducts = RepeatedFieldAndListChangeTool.ListToRepeatedField(lo),
            //    EndTime = "2020-5-14 20:00:00",
            //    StartTime = "2019-5-14 20:00:00",
            //    Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //});
            //Debug.Log(g2C_AddShopActivityInfo.IsSuccess);
            //Debug.Log(g2C_AddShopActivityInfo.Message);
            #endregion

            #region 查询商铺的活动id列表  查询商铺活动信息
            //G2C_QueryShopActivityInfoCount g2C_QueryShopActivityInfoCount = (G2C_QueryShopActivityInfoCount)await realmSession.Call(new C2G_QueryShopActivityInfoCount()
            //{
            //    ShopInfoID = 281474976710810,
            //});
            //Debug.Log(g2C_QueryShopActivityInfoCount.IsSuccess);
            //Debug.Log(g2C_QueryShopActivityInfoCount.Message);
            //foreach (long item in g2C_QueryShopActivityInfoCount.ShopActivitys)
            //{
            //    Debug.Log("商铺的活动id：" + item);
            //    G2C_QueryShopActivityInfo g2C_QueryShopActivityInfo = (G2C_QueryShopActivityInfo)await realmSession.Call(new C2G_QueryShopActivityInfo()
            //    {
            //        ShopInfoID = 281474976710810,
            //        ShopActivityID = item,
            //    });
            //    Debug.Log(g2C_QueryShopActivityInfo.IsSuccess);
            //    Debug.Log(g2C_QueryShopActivityInfo.Message);
            //    if (g2C_QueryShopActivityInfo.IsSuccess)
            //    {
            //        Debug.Log("g2C_QueryShopActivityInfo" + g2C_QueryShopActivityInfo.DiscountsAlert);
            //    }
            //}
            #endregion

            #region 修改店铺活动信息
            //G2C_UpdateShopActivityInfo g2C_UpdateShopActivityInfo = (G2C_UpdateShopActivityInfo)await realmSession.Call(new C2G_UpdateShopActivityInfo()
            //{
            //    ShopInfoID = 281474976710810,
            //    Count = 10,
            //    DiscountsAlert = "我不发货的哦",
            //    DiscountsSort = 1,
            //    DiscountsSortFields = "优惠说明",
            //    ShopActivityID = 281474976710774,
            //    DisProductType = 2,
            //    DisProduct = 123456789,
            //    EndTime = "2020-5-14 20:00:00",
            //    StartTime = "2019-5-14 20:00:00",
            //    Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(st),
            //});
            //Debug.Log(g2C_UpdateShopActivityInfo.IsSuccess);
            //Debug.Log(g2C_UpdateShopActivityInfo.Message);
            #endregion

            #region 删除店铺活动
            //G2C_DelShopActivityInfo g2C_DelShopActivityInfo = (G2C_DelShopActivityInfo)await realmSession.Call(new C2G_DelShopActivityInfo()
            //{
            //    ShopInfoID = 281474976710810,
            //    ShopActivityID = 281474976710774,
            //});
            //Debug.Log(g2C_DelShopActivityInfo.IsSuccess);
            //Debug.Log(g2C_DelShopActivityInfo.Message);

            #endregion

            #region 店铺活动审核
            //G2C_AuditShopActivityInfo g2C_AuditShopActivityInfo = (G2C_AuditShopActivityInfo)await realmSession.Call(new C2G_AuditShopActivityInfo()
            //{
            //    ShopInfoID = 281474976710810,
            //    ShopActivityID = 281474976710774,
            //    AuditMessage = "通过吗",
            //    Type = 1,
            //});
            //Debug.Log(g2C_AuditShopActivityInfo.IsSuccess);
            //Debug.Log(g2C_AuditShopActivityInfo.Message);

            #endregion
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
