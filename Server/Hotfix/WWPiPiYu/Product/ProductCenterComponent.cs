using ETModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ETHotfix.WWPaoFu.Activity
{

    [ObjectSystem]
    public class ProductCenterComponentAwakeSystem : AwakeSystem<ProductCenterComponent>
    {
        public override void Awake(ProductCenterComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class ProductCenterComponentUpdateSystem : UpdateSystem<ProductCenterComponent>
    {
        public override void Update(ProductCenterComponent self)
        {
            self.Update();
        }
    }

    /// <summary>
    /// 日期大小
    /// </summary>
    public enum ComparType
    {
        uncompar,
        big,
        equ,
        smo,
    }

    /// <summary>
    /// 商品中心组件
    /// 功能：
    /// 管理所有商品
    /// </summary>
    public class ProductCenterComponent : Component
    {

        static ProductCenterComponent instance;
        public static ProductCenterComponent Instance { get; private set; }

        /// <summary>
        /// 商品总表
        /// </summary>
        public Dictionary<long, ProductInfoData> ProductInfolist = new Dictionary<long, ProductInfoData>();

        /// <summary>
        /// 店铺总表
        /// </summary>
        public Dictionary<long, ShopInfoData> ShopInfolist = new Dictionary<long, ShopInfoData>();

        /// <summary>
        /// 商铺商品列表
        /// </summary>
        public Dictionary<long, List<ProductInfoData>> ShopProductlist = new Dictionary<long, List<ProductInfoData>>();

        /// <summary>
        /// 个人商品订单列表
        /// </summary>
        public Dictionary<long, List<ProductInfoOrder>> UserProductOrderlist = new Dictionary<long, List<ProductInfoOrder>>();

        /// <summary>
        /// 个人购物车列表
        /// </summary>
        public Dictionary<long, List<SimpleOrder>> UserSimpleOrderlist = new Dictionary<long, List<SimpleOrder>>();

        /// <summary>
        /// 店铺的活动列表
        /// </summary>
        public Dictionary<long, List<ShopActivityInfo>> ShopActivitylist = new Dictionary<long, List<ShopActivityInfo>>();

        /// <summary>
        /// 用户收藏商品列表
        /// </summary>
        public Dictionary<long, List<CollectProduct>> UserCollectProductlist = new Dictionary<long, List<CollectProduct>>();

        /// <summary>
        /// 商品评价列表
        /// </summary>
        public Dictionary<long, List<EvaluateProduct>> ProductEvaluatelist = new Dictionary<long, List<EvaluateProduct>>();

        /// <summary>
        /// 用户收藏商铺列表
        /// </summary>
        public Dictionary<long, List<CollectShopInfo>> UserCollecShoptlist = new Dictionary<long, List<CollectShopInfo>>();

        /// <summary>
        /// 商品点赞列表
        /// </summary>
        public Dictionary<long, List<ThumbProduct>> ThumbUserlist = new Dictionary<long, List<ThumbProduct>>();

        /// <summary>
        /// 个人足迹列表
        /// </summary>
        public Dictionary<long, List<Footmark>> UserFootmarklist = new Dictionary<long, List<Footmark>>();

        /// <summary>
        /// 个人优惠券列表
        /// </summary>
        public Dictionary<long, List<Discount>> UserDiscountlist = new Dictionary<long, List<Discount>>();

        /// <summary>
        /// 服务id列表
        /// </summary>
        public List<long> ServiceInfos = new List<long>();

        public void Awake()
        {
            instance = this;
            try
            {
                Log.Debug("商品中心初始化赋值");
                //ceshi();
                GetDayDataFromDB();
                Task.Run(() =>
                {
                    Log.Debug("异步调用");
                    CheckActivityTime2();
                });
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
            }
        }

        public async void ceshi()
        {
            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
            Log.Debug("测试");
            var acounts1count = await dBProxyComponent.QueryCount<ProductInfoData>("{ }");
            Log.Debug("测试acounts1count" + acounts1count);

        }

        int skip = 0;
        int limit = 100;//拿几条数据

        public async void GetDayDataFromDB()
        {
            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

            try
            {
                var acountscount = await dBProxyComponent.QueryCount<ProductInfoData>("{ }");
                while (skip < acountscount)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts = await dBProxyComponent.QueryLss<ProductInfoData>(LSS);
                    foreach (var dataDate in acounts)
                    {
                        ProductInfoData Infodata = dataDate as ProductInfoData;
                        if (ProductInfolist.TryAdd(Infodata._ProductInfoID, Infodata)) { };
                        long ShopInfoID = Infodata._ShopInfoID;
                        lock (ShopProductlist)
                        {
                            List<ProductInfoData> datalist = new List<ProductInfoData>();
                            if (ShopProductlist.ContainsKey(ShopInfoID))
                            {
                                //有这个商铺直接往这个商铺的list里添加
                                ShopProductlist[ShopInfoID].Add(Infodata);
                            }
                            else
                            {
                                //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                if (ShopProductlist.TryAdd(ShopInfoID, datalist) == true) { };
                                ShopProductlist[ShopInfoID].Add(Infodata);
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts1count = await dBProxyComponent.QueryCount<ProductInfoOrder>("{ }");
                while (skip < acounts1count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{_CreationTime:-1 }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<ProductInfoOrder>(LSS);
                    foreach (ProductInfoOrder item in acounts1)
                    {
                        ProductInfoOrder Orderdata = item as ProductInfoOrder;
                        long UserID = Orderdata._UserID;
                        lock (UserProductOrderlist)
                        {
                            List<ProductInfoOrder> datalist = new List<ProductInfoOrder>();
                            if (UserProductOrderlist.ContainsKey(UserID))
                            {
                                //有这个商铺直接往这个商铺的list里添加
                                UserProductOrderlist[UserID].Add(Orderdata);
                            }
                            else
                            {
                                //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                if (UserProductOrderlist.TryAdd(UserID, datalist) == true) { };
                                UserProductOrderlist[UserID].Add(Orderdata);
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts2count = await dBProxyComponent.QueryCount<SimpleOrder>("{ }");
                while (skip < acounts2count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{_id:-1 }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<SimpleOrder>(LSS);
                    foreach (SimpleOrder item in acounts1)
                    {
                        SimpleOrder Orderdata = item as SimpleOrder;
                        if (Orderdata._State == 0)
                        {
                            long UserID = Orderdata._UserID;
                            lock (UserSimpleOrderlist)
                            {
                                List<SimpleOrder> datalist = new List<SimpleOrder>();
                                if (UserSimpleOrderlist.ContainsKey(UserID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    UserSimpleOrderlist[UserID].Add(Orderdata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (UserSimpleOrderlist.TryAdd(UserID, datalist) == true) { };
                                    UserSimpleOrderlist[UserID].Add(Orderdata);
                                }
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acount1s = await dBProxyComponent.Query<ServiceInfoData>("{ }");
                foreach (ServiceInfoData item in acount1s)
                {
                    if (!ServiceInfos.Contains(item._ServiceInfoID))
                    {
                        ServiceInfos.Add(item._ServiceInfoID);
                    }
                }

                var acounts3count = await dBProxyComponent.QueryCount<ShopActivityInfo>("{ }");
                while (skip < acounts3count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<ShopActivityInfo>(LSS);
                    foreach (ShopActivityInfo item in acounts1)
                    {
                        ShopActivityInfo Infodata = item as ShopActivityInfo;
                        long ShopInfoID = Infodata._ShopInfoID;
                        lock (UserSimpleOrderlist)
                        {
                            List<ShopActivityInfo> datalist = new List<ShopActivityInfo>();
                            if (ShopActivitylist.ContainsKey(ShopInfoID))
                            {
                                //有这个商铺直接往这个商铺的list里添加
                                ShopActivitylist[ShopInfoID].Add(Infodata);
                            }
                            else
                            {
                                //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                if (ShopActivitylist.TryAdd(ShopInfoID, datalist) == true) { };
                                ShopActivitylist[ShopInfoID].Add(Infodata);
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts4count = await dBProxyComponent.QueryCount<ShopInfoData>("{ }");
                while (skip < acounts4count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts = await dBProxyComponent.QueryLss<ShopInfoData>(LSS);
                    foreach (var dataDate in acounts)
                    {
                        ShopInfoData Infodata = dataDate as ShopInfoData;
                        if (ShopInfolist.TryAdd(Infodata._ShopInfoID, Infodata)) { };
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts5count = await dBProxyComponent.QueryCount<CollectProduct>("{ }");
                while (skip < acounts5count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<CollectProduct>(LSS);
                    foreach (CollectProduct item in acounts1)
                    {
                        CollectProduct Orderdata = item as CollectProduct;
                        if (Orderdata._State == 0)
                        {
                            long UserID = Orderdata._UserID;
                            lock (UserCollectProductlist)
                            {
                                List<CollectProduct> datalist = new List<CollectProduct>();
                                if (UserCollectProductlist.ContainsKey(UserID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    UserCollectProductlist[UserID].Add(Orderdata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (UserCollectProductlist.TryAdd(UserID, datalist) == true) { };
                                    UserCollectProductlist[UserID].Add(Orderdata);
                                }
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts6count = await dBProxyComponent.QueryCount<CollectShopInfo>("{ }");
                while (skip < acounts6count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<CollectShopInfo>(LSS);
                    foreach (CollectShopInfo item in acounts1)
                    {
                        CollectShopInfo Orderdata = item as CollectShopInfo;
                        if (Orderdata._State == 0)
                        {
                            long UserID = Orderdata._UserID;
                            lock (UserCollecShoptlist)
                            {
                                List<CollectShopInfo> datalist = new List<CollectShopInfo>();
                                if (UserCollecShoptlist.ContainsKey(UserID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    UserCollecShoptlist[UserID].Add(Orderdata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (UserCollecShoptlist.TryAdd(UserID, datalist) == true) { };
                                    UserCollecShoptlist[UserID].Add(Orderdata);
                                }
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts7count = await dBProxyComponent.QueryCount<Discount>("{ }");
                while (skip < acounts7count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<Discount>(LSS);
                    foreach (Discount item in acounts1)
                    {
                        Discount Orderdata = item as Discount;
                        if (Orderdata._State == 0)
                        {
                            long UserID = Orderdata._UserID;
                            lock (UserDiscountlist)
                            {
                                List<Discount> datalist = new List<Discount>();
                                if (UserDiscountlist.ContainsKey(UserID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    UserDiscountlist[UserID].Add(Orderdata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (UserDiscountlist.TryAdd(UserID, datalist) == true) { };
                                    UserDiscountlist[UserID].Add(Orderdata);
                                }
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts8count = await dBProxyComponent.QueryCount<Footmark>("{ }");
                while (skip < acounts8count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<Footmark>(LSS);
                    foreach (Footmark item in acounts1)
                    {
                        Footmark Orderdata = item as Footmark;
                        if (Orderdata._State == 0)
                        {
                            long UserID = Orderdata._UserID;
                            lock (UserFootmarklist)
                            {
                                List<Footmark> datalist = new List<Footmark>();
                                if (UserFootmarklist.ContainsKey(UserID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    UserFootmarklist[UserID].Add(Orderdata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (UserFootmarklist.TryAdd(UserID, datalist) == true) { };
                                    UserFootmarklist[UserID].Add(Orderdata);
                                }
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts9count = await dBProxyComponent.QueryCount<ThumbProduct>("{ }");
                while (skip < acounts9count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<ThumbProduct>(LSS);
                    foreach (ThumbProduct item in acounts1)
                    {
                        ThumbProduct Orderdata = item as ThumbProduct;
                        if (Orderdata._State == 0)
                        {
                            long ThumbProductID = Orderdata._ThumbProductID;
                            lock (ThumbUserlist)
                            {
                                List<ThumbProduct> datalist = new List<ThumbProduct>();
                                if (ThumbUserlist.ContainsKey(ThumbProductID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    ThumbUserlist[ThumbProductID].Add(Orderdata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (ThumbUserlist.TryAdd(ThumbProductID, datalist) == true) { };
                                    ThumbUserlist[ThumbProductID].Add(Orderdata);
                                }
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;

                var acounts10count = await dBProxyComponent.QueryCount<EvaluateProduct>("{ }");
                while (skip < acounts10count)
                {
                    string Json = "{ }";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                    string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                    string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                    var acounts1 = await dBProxyComponent.QueryLss<EvaluateProduct>(LSS);
                    foreach (EvaluateProduct item in acounts1)
                    {
                        EvaluateProduct Orderdata = item as EvaluateProduct;
                        if (Orderdata._State == 0)
                        {
                            long EvaluateProductID = Orderdata._EvaluateProductID;
                            lock (ProductEvaluatelist)
                            {
                                List<EvaluateProduct> datalist = new List<EvaluateProduct>();
                                if (ProductEvaluatelist.ContainsKey(EvaluateProductID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    ProductEvaluatelist[EvaluateProductID].Add(Orderdata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (ProductEvaluatelist.TryAdd(EvaluateProductID, datalist) == true) { };
                                    ProductEvaluatelist[EvaluateProductID].Add(Orderdata);
                                }
                            }
                        }
                    }
                    skip += limit;
                }
                skip = 0;
            }
            catch (Exception e)
            {
                Log.Debug("ProductCenterComponent:GetDayDataFromDB:" + e);
            }
        }

        public void Update()
        {

        }

        long iindex = 0;
        int FootDay = 15; 
        int OrderDay = 15; 
        bool isOpen = true;
        //long ii = 0;
        public async void CheckActivityTime2()
        {
        A:
            DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
            //个人商品订单个人id列表
            List<long> UserOrderList = new List<long>();
            lock (UserProductOrderlist)
            {
                foreach (long item in UserProductOrderlist.Keys)
                {
                    UserOrderList.Add(item);
                }
            }
            foreach (long id in UserOrderList)
            {
                iindex = id;
                List<ProductInfoOrder> Foots = UserProductOrderlist[id];
                if (isOpen == true)
                {
                    foreach (ProductInfoOrder item in Foots)
                    {
                        //查看这个订单是否没有确认收获，如果没有就判断是否过去了OrderDay，如果超过了就自动确认收货
                        if (item._DeliveryTime == "")
                        {
                            if (item._SendProductTime != "")
                            {
                                int mm = CompanyDatemm(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"), item._SendProductTime);
                                int day = mm / 60 / 24;
                                if (day > OrderDay)
                                {
                                    //自动确认收货
                                    item._PayState = 3;
                                    item._DeliveryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    await dBProxyComponent.Save(item);
                                    await dBProxyComponent.SaveLog(item);
                                }

                                isOpen = true;
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                //还没发货
                            }
                        }
                    }
                }
                else
                {
                    iindex = 0;
                    isOpen = true;
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
            }
            //个人足迹个人id列表
            List<long> UserFootList = new List<long>();
            lock (UserFootmarklist)
            {
                foreach (long item in UserFootmarklist.Keys)
                {
                    UserFootList.Add(item);
                }
            }
            foreach (long id in UserFootList)
            {
                iindex = id;
                List<Footmark> Foots = UserFootmarklist[id];
                if (isOpen == true)
                {
                    foreach (Footmark item in Foots)
                    {
                        //查找这个足迹是否过了FootDay，如果超过了就删除
                        int mm = CompanyDatemm(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"), item._FootmarkTime);
                        int day = mm / 60 / 24;
                        if (day > FootDay)
                        {
                            //删除足迹表
                            item._State = 1;

                            await dBProxyComponent.Save(item);
                            await dBProxyComponent.SaveLog(item);
                        }

                        isOpen = true;
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    iindex = 0;
                    isOpen = true;
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
            }
            //个人优惠卷个人id列表
            List<long> UserDisList = new List<long>();
            lock (UserDiscountlist)
            {
                foreach (long item in UserDiscountlist.Keys)
                {
                    UserDisList.Add(item);
                }
            }
            foreach (long id in UserDisList)
            {
                iindex = id;
                List<Discount> DisIDs = UserDiscountlist[id];
                if (isOpen == true)
                {
                    foreach (Discount item in DisIDs)
                    {
                        //TODO 查找这个优惠券，判断是否过期了

                        isOpen = true;
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    iindex = 0;
                    isOpen = true;
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
            }
            Thread.Sleep(1000);
            goto A;
            CheckActivityTime2();
        }



        bool isCountDayFinish = true;

        /// <summary>
        /// 计算日期时间差(分单位)
        /// </summary>
        /// <param name="dateStr1">日期1</param>
        /// <param name="dateStr2">日期2</param>
        /// <param name="msg">返回信息</param>
        public static int CompanyDatemm(string now, string target)
        {
            //Log.Info(now);
            //Log.Info(target);
            //将日期字符串转换为日期对象
            DateTime t1 = Convert.ToDateTime(now);
            DateTime t2 = Convert.ToDateTime(target);
            //通过DateTIme.Compare()进行比较（）
            return (int)((t1 - t2).Ticks / 10000000 / 60);
        }

        /// <summary>
        /// 比较两个日期大小
        /// </summary>
        /// <param name="dateStr1">日期1</param>
        /// <param name="dateStr2">日期2</param>
        /// <param name="msg">返回信息</param>
        public ComparType CompanyDate(string now, string target)
        {
            //Log.Info(now);
            //Log.Info(target);
            //将日期字符串转换为日期对象
            DateTime t1 = Convert.ToDateTime(now);
            DateTime t2 = Convert.ToDateTime(target);
            //通过DateTIme.Compare()进行比较（）
            int compNum = DateTime.Compare(t1, t2.AddHours(1));
            //int compNum = DateTime.Compare(t1, t2);

            //t1> t2
            if (compNum > 0)
            {
                return ComparType.big;
            }
            //t1= t2
            if (compNum == 0)
            {
                return ComparType.equ;
            }
            //t1< t2
            if (compNum < 0)
            {
                return ComparType.smo;
            }
            return ComparType.uncompar;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        private void Mail()
        {

        }
    }
}
