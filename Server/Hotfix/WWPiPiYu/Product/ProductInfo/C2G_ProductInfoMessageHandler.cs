using System;
using System.Collections.Generic;
using System.Reflection;
using ETHotfix.WWPaoFu.Activity;
using ETModel;

namespace ETHotfix
{
    #region 商品
    /// <summary>
    /// 查询自己收藏的商品id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryMyProductInfoDataCountHandler : AMRpcHandler<C2G_QueryMyProductInfoDataCount, G2C_QueryMyProductInfoDataCount>
    {
        protected override async void Run(Session session, C2G_QueryMyProductInfoDataCount message, Action<G2C_QueryMyProductInfoDataCount> reply)
        {
            G2C_QueryMyProductInfoDataCount response = new G2C_QueryMyProductInfoDataCount();
            List<long> ProductInfoids = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ProductInfos);
            List<CollectProduct> acountsInfo = new List<CollectProduct>();
            response.IsSuccess = false;

            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist.ContainsKey(message.UserID))
                    {
                        acountsInfo = Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist[message.UserID];
                    }
                }

                if (acountsInfo.Count > 0)
                {
                    foreach (CollectProduct item in acountsInfo)
                    {
                        if (ProductInfoids.Contains(item._CollectProductID))
                        {
                            continue;
                        }
                        if (ProductInfoids.Count > message.Count)
                        {
                            break;
                        }
                        ProductInfoids.Add(item._CollectProductID);
                    }
                    response.IsSuccess = true;
                    response.ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(ProductInfoids);
                    response.Message = "商品id列表获取成功";
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

    /// <summary>
    /// 查询商品id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryProductInfoDataCountHandler : AMRpcHandler<C2G_QueryProductInfoDataCount, G2C_QueryProductInfoDataCount>
    {
        protected override async void Run(Session session, C2G_QueryProductInfoDataCount message, Action<G2C_QueryProductInfoDataCount> reply)
        {
            G2C_QueryProductInfoDataCount response = new G2C_QueryProductInfoDataCount();
            List<long> ProductInfoids = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ProductInfos);
            List<ProductInfoData> acountsInfo = new List<ProductInfoData>();
            response.IsSuccess = false;

            int skip = 0;
            int limit = 20;//拿几条数据
            int state = 0; //拿上架还是下架的
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                if (message.StateType == 1)
                {
                    state = 0;
                }
                else if (message.StateType == 2)
                {
                    state = 1;
                }
                if (message.QueryType == 1)
                {
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                        {
                            acountsInfo = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                        }
                    }
                }
                else if (message.QueryType == 2)
                {
                    var acountscount = await dBProxyComponent.QueryCount<ProductInfoData>("{_ProductInfoName:/" + message.QueryConntent + "/}");
                    while (skip < acountscount)
                    {
                        string Json = "{_ProductInfoName:/" + message.QueryConntent + "/}";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                        string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                        string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                        var acounts = await dBProxyComponent.QueryLss<ProductInfoData>(LSS);
                        foreach (ProductInfoData item in acounts)
                        {
                            if (item._State == state)
                            {
                                if (ProductInfoids.Contains(item._ProductInfoID))
                                {
                                    continue;
                                }
                                if (ProductInfoids.Count >= message.Count)
                                {
                                    break;
                                }
                                ProductInfoids.Add(item._ProductInfoID);
                            }
                        }
                        if (ProductInfoids.Count >= message.Count)
                        {
                            break;
                        }
                        skip += limit;
                    }
                    skip = 0;
                }

                if (acountsInfo.Count > 0)
                {
                    foreach (ProductInfoData item in acountsInfo)
                    {
                        if (item._State == state)
                        {
                            if (ProductInfoids.Contains(item._ProductInfoID))
                            {
                                continue;
                            }
                            if (ProductInfoids.Count > message.Count)
                            {
                                break;
                            }
                            ProductInfoids.Add(item._ProductInfoID);
                        }
                    }
                }

                response.IsSuccess = true;
                response.ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(ProductInfoids);
                response.Message = "商品id列表获取成功";

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

    /// <summary>
    /// 查询商品
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryProductInfoDataHandler : AMRpcHandler<C2G_QueryProductInfoData, G2C_QueryProductInfoData>
    {
        protected override async void Run(Session session, C2G_QueryProductInfoData message, Action<G2C_QueryProductInfoData> reply)
        {
            G2C_QueryProductInfoData response = new G2C_QueryProductInfoData();
            ProductInfoData InfoData = null;
            response.IsSuccess = false;
            List<ProductInfoData> productInfoDatas = new List<ProductInfoData>();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                    {
                        productInfoDatas = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                    }
                }
                if (productInfoDatas.Count > 0)
                {
                    foreach (ProductInfoData item in productInfoDatas)
                    {
                        if (item._ProductInfoID == message.ProductInfoID)
                        {
                            InfoData = item;
                            break;
                        }
                    }
                }
                else
                {
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist.ContainsKey(message.ProductInfoID))
                        {
                            InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist[message.ProductInfoID];
                        }
                    }
                }

                if (InfoData!=null)
                {
                    response.ShopInfoID = InfoData._ShopInfoID;
                    response.ProductID = InfoData._ProductID;
                    response.ProductInfoName = InfoData._ProductInfoName;
                    response.ProductPublishGround = InfoData._ProductPublishGround;
                    response.ProductInfoSort = InfoData._ProductInfoSort;
                    response.Price = InfoData._Price;
                    response.Count = InfoData._Count;
                    response.Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._Intrduce);
                    response.ProductInfoHeadImage = InfoData._ProductInfoHeadImage;
                    response.ProductInfoImages = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._ProductInfoImages);
                    response.PorductInfoTags = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._PorductInfoTags);
                    response.DiscountsTags = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._DiscountsTags);
                    response.PorductInfoDis = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._PorductInfoDis);
                    response.ServiceList = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._ServiceList);
                    response.AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._AttributeBag);
                    response.ProductShopSort = InfoData._ProductShopSort;
                    response.BayCounts = InfoData._BayCounts;
                    response.ViewCounts = InfoData._ViewCounts;
                    response.Thumbs = InfoData._Thumbs;
                    response.Collects = InfoData._Collects;
                    response.PublicMessage = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._PublicMessage);
                    response.PublishTime = InfoData._PublishTime;
                    response.UpdateTime = InfoData._UpdateTime;
                    response.AuditState = InfoData._AuditState;
                    response.AuditMessage = InfoData._AuditMessage;
                    response.State = InfoData._State;

                    response.IsSuccess = true;
                    response.Message = "商品查询成功！";
                }
                else
                {
                    response.Message = "商品数据获取失败！";
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

    /// <summary>
    /// 添加商品
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddProductInfoDataHandler : AMRpcHandler<C2G_AddProductInfoData, G2C_AddProductInfoData>
    {
        protected override async void Run(Session session, C2G_AddProductInfoData message, Action<G2C_AddProductInfoData> reply)
        {
            G2C_AddProductInfoData response = new G2C_AddProductInfoData();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                ProductInfoData infodata = ComponentFactory.Create<ProductInfoData>();

                infodata._AuditState = 0;
                infodata._AuditMessage = "";
                infodata._AttributeBag = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.AttributeBag);
                infodata._BayCounts = 0;
                infodata._Count = message.Count;
                infodata._DiscountsTags = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.DiscountsTags);
                infodata._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                infodata._PorductInfoDis = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.PorductInfoDis);
                infodata._PorductInfoTags = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.PorductInfoTags);
                infodata._Price = message.Price;
                infodata._ProductID = infodata.Id;
                infodata._ProductInfoHeadImage = message.ProductInfoHeadImage;
                infodata._ProductInfoID = infodata.Id;
                infodata._ProductInfoImages = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ProductInfoImages);
                infodata._ProductInfoName = message.ProductInfoName;
                infodata._ProductInfoSort = message.ProductInfoSort;
                infodata._ProductShopSort = message.ProductShopSort;
                infodata._ProductPublishGround = message.ProductPublishGround;
                infodata._PublicMessage = new List<string>();
                infodata._PublishTime = "";
                infodata._UpdateTime = "";
                infodata._ServiceList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ServiceList);
                infodata._ShopInfoID = message.ShopInfoID;
                infodata._State = 0;
                infodata._Collects = 0;
                infodata._Thumbs = 0;
                infodata._ViewCounts = 0;

                response.IsSuccess = true;
                response.Message = "发布商品成功，等待审核！";

                await dBProxyComponent.Save(infodata);
                dBProxyComponent.SaveLog(infodata).Coroutine();

                //添加缓存商铺商品缓存
                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                {
                    List<ProductInfoData> datalist = new List<ProductInfoData>();
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(infodata._ShopInfoID))
                    {
                        //有这个商铺直接往这个商铺的list里添加
                        Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[infodata._ShopInfoID].Add(infodata);
                    }
                    else
                    {
                        //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.TryAdd(infodata._ShopInfoID, datalist) == true) { };
                        Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[infodata._ShopInfoID].Add(infodata);
                    }
                }
                //添加商品列表
                lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist.TryAdd(infodata._ShopInfoID, infodata)) { };
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

    /// <summary>
    /// 删除商品
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_DelProductInfoDataHandler : AMRpcHandler<C2G_DelProductInfoData, G2C_DelProductInfoData>
    {
        protected override async void Run(Session session, C2G_DelProductInfoData message, Action<G2C_DelProductInfoData> reply)
        {
            G2C_DelProductInfoData response = new G2C_DelProductInfoData();
            ProductInfoData InfoData = null;
            response.IsSuccess = false;
            List<ProductInfoData> productInfoDatas = new List<ProductInfoData>();

            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                    {
                        productInfoDatas = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                    }
                }
                if (productInfoDatas.Count > 0)
                {
                    foreach (ProductInfoData item in productInfoDatas)
                    {
                        if (item._ProductInfoID == message.ProductInfoID)
                        {
                            InfoData = item;
                            break;
                        }
                    }
                }
                else
                {
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist.ContainsKey(message.ProductInfoID))
                        {
                            InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist[message.ProductInfoID];
                        }
                    }
                }

                if (InfoData != null)
                {
                    InfoData._State = 1;

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "商品删除成功！";
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

    /// <summary>
    /// 商家修改商品信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateProductInfoDataHandler : AMRpcHandler<C2G_UpdateProductInfoData, G2C_UpdateProductInfoData>
    {
        protected override async void Run(Session session, C2G_UpdateProductInfoData message, Action<G2C_UpdateProductInfoData> reply)
        {
            G2C_UpdateProductInfoData response = new G2C_UpdateProductInfoData();
            ProductInfoData InfoData = null;
            response.IsSuccess = false;
            List<ProductInfoData> productInfoDatas = new List<ProductInfoData>();

            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                    {
                        productInfoDatas = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                    }
                }
                if (productInfoDatas.Count > 0)
                {
                    foreach (ProductInfoData item in productInfoDatas)
                    {
                        if (item._ProductInfoID == message.ProductInfoID)
                        {
                            InfoData = item;
                            break;
                        }
                    }
                }
                else
                {
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist.ContainsKey(message.ProductInfoID))
                        {
                            InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist[message.ProductInfoID];
                        }
                    }
                }

                if (InfoData != null)
                {
                    if (message.ProductInfoName != "")
                    {
                        InfoData._ProductInfoName = message.ProductInfoName;
                    }
                    if (message.ProductPublishGround != "")
                    {
                        InfoData._ProductPublishGround = message.ProductPublishGround;
                    }
                    if (message.ProductInfoSort != 0)
                    {
                        InfoData._ProductInfoSort = message.ProductInfoSort;
                    }
                    if (message.ProductShopSort != 0)
                    {
                        InfoData._ProductShopSort = message.ProductShopSort;
                    }
                    if (message.Price != 0)
                    {
                        InfoData._Price = message.Price;
                    }
                    if (message.Count != 0)
                    {
                        InfoData._Count = message.Count;
                    }
                    if (message.Intrduce.Count != 0)
                    {
                        InfoData._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                    }
                    if (message.ProductInfoHeadImage != "")
                    {
                        InfoData._ProductInfoHeadImage = message.ProductInfoHeadImage;
                    }
                    if (message.ProductInfoImages.Count != 0)
                    {
                        InfoData._ProductInfoImages = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ProductInfoImages);
                    }
                    if (message.PorductInfoTags.Count != 0)
                    {
                        InfoData._PorductInfoTags = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.PorductInfoTags);
                    }
                    if (message.DiscountsTags.Count != 0)
                    {
                        InfoData._DiscountsTags = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.DiscountsTags);
                    }
                    if (message.PorductInfoDis.Count != 0)
                    {
                        InfoData._PorductInfoDis = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.PorductInfoDis);
                    }
                    if (message.ServiceList.Count != 0)
                    {
                        InfoData._ServiceList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ServiceList);
                    }
                    if (message.AttributeBag.Count != 0)
                    {
                        InfoData._AttributeBag = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.AttributeBag);
                    }
                    InfoData._UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "商品修改成功！";
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

    /// <summary>
    /// 添加浏览或点赞或收藏
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_ViewProductInfoDataHandler : AMRpcHandler<C2G_ViewProductInfoData, G2C_ViewProductInfoData>
    {
        protected override async void Run(Session session, C2G_ViewProductInfoData message, Action<G2C_ViewProductInfoData> reply)
        {
            G2C_ViewProductInfoData response = new G2C_ViewProductInfoData();
            ProductInfoData InfoData = null;
            response.IsSuccess = false;
            List<ProductInfoData> productInfoDatas = new List<ProductInfoData>();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                    {
                        productInfoDatas = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                    }
                }
                if (productInfoDatas.Count > 0)
                {
                    foreach (ProductInfoData item in productInfoDatas)
                    {
                        if (item._ProductInfoID == message.ProductInfoID)
                        {
                            InfoData = item;
                            break;
                        }
                    }
                }
                else
                {
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist.ContainsKey(message.ProductInfoID))
                        {
                            InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist[message.ProductInfoID];
                        }
                    }
                }

                if (InfoData != null)
                {
                    if (message.Type == 1)
                    {
                        InfoData._ViewCounts++;

                        Footmark footmark = null;
                        lock (Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist)
                        {
                            if (Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist.ContainsKey(message.Userid))
                            {
                                List<Footmark> datalist = new List<Footmark>();
                                datalist = Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist[message.Userid];
                                foreach (Footmark item in datalist)
                                {
                                    if (item._FootmarkID == InfoData._ProductInfoID)
                                    {
                                        footmark = item;
                                        break;
                                    }
                                }
                            }
                        }
                        //footmark如果不为空说明有数据修改时间，为空说明没数据创建数据
                        if (footmark!=null)
                        {
                            footmark._FootmarkTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                            await dBProxyComponent.Save(footmark);
                            dBProxyComponent.SaveLog(footmark).Coroutine();
                        }
                        else
                        {
                            //添加用户足迹
                            Footmark infodata = ComponentFactory.Create<Footmark>();
                            infodata._UserID = message.Userid;
                            infodata._FootmarkTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                            infodata._FootmarkID = InfoData._ProductInfoID;
                            infodata._State = 0;

                            await dBProxyComponent.Save(infodata);
                            dBProxyComponent.SaveLog(infodata).Coroutine();

                            //添加缓存
                            lock (Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist)
                            {
                                List<Footmark> datalist = new List<Footmark>();
                                if (Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist.ContainsKey(message.Userid))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist[message.Userid].Add(infodata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist.TryAdd(message.Userid, datalist) == true) { };
                                    Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist[message.Userid].Add(infodata);
                                }
                            }
                        }

                        response.IsSuccess = true;
                        response.Message = "添加浏览次数成功！";
                    }
                    else if (message.Type == 2)
                    {
                        List<ThumbProduct> ProductCollectUserid = new List<ThumbProduct>();
                        ThumbProduct thumbProduct = null;

                        lock (Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist)
                        {
                            if (Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist.ContainsKey(message.ProductInfoID))
                            {
                                ProductCollectUserid = Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist[message.ProductInfoID];
                            }
                            if (ProductCollectUserid.Count > 0)
                            {
                                foreach (ThumbProduct item in ProductCollectUserid)
                                {
                                    if (item._UserID == message.Userid)
                                    {
                                        thumbProduct = item;
                                        break;
                                    }
                                }
                            }
                        }
                        if (thumbProduct == null)
                        {
                            InfoData._Thumbs++;

                            ThumbProduct infodata = ComponentFactory.Create<ThumbProduct>();
                            infodata._UserID = message.Userid;
                            infodata._ThumbProductID = InfoData._ProductInfoID;
                            infodata._State = 0;

                            await dBProxyComponent.Save(infodata);
                            dBProxyComponent.SaveLog(infodata).Coroutine();

                            //添加缓存
                            lock (Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist)
                            {
                                List<ThumbProduct> datalist = new List<ThumbProduct>();
                                if (Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist.ContainsKey(InfoData._ProductInfoID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist[InfoData._ProductInfoID].Add(infodata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist.TryAdd(InfoData._ProductInfoID, datalist) == true) { };
                                    Game.Scene.GetComponent<ProductCenterComponent>().ThumbUserlist[InfoData._ProductInfoID].Add(infodata);
                                }
                            }

                            response.IsSuccess = true;
                            response.Message = "点赞成功！";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "点赞失败！，该用户已点赞过！";
                        }
                    }
                    else if (message.Type == 3)
                    {
                        InfoData._Collects++;

                        CollectProduct infodata = ComponentFactory.Create<CollectProduct>();
                        infodata._UserID = message.Userid;
                        infodata._CollectProductID = InfoData._ProductInfoID;
                        infodata._State = 0;

                        await dBProxyComponent.Save(infodata);
                        dBProxyComponent.SaveLog(infodata).Coroutine();

                        //添加缓存
                        lock (Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist)
                        {
                            List<CollectProduct> datalist = new List<CollectProduct>();
                            if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist.ContainsKey(message.Userid))
                            {
                                //有这个商铺直接往这个商铺的list里添加
                                Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist[message.Userid].Add(infodata);
                            }
                            else
                            {
                                //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist.TryAdd(message.Userid, datalist) == true) { };
                                Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist[message.Userid].Add(infodata);
                            }
                        }

                        response.IsSuccess = true;
                        response.Message = "收藏成功！";
                    }
                    else if (message.Type == 4)
                    {
                        List<CollectProduct> ProductCollectUserid = new List<CollectProduct>();
                        CollectProduct collectProduct = null;

                        lock (Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist)
                        {
                            if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist.ContainsKey(message.Userid))
                            {
                                ProductCollectUserid = Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist[message.Userid];
                            }
                            if (ProductCollectUserid.Count > 0)
                            {
                                foreach (CollectProduct item in ProductCollectUserid)
                                {
                                    if (item._CollectProductID == message.ProductInfoID)
                                    {
                                        collectProduct = item;
                                        break;
                                    }
                                }
                            }
                        }
                        if (collectProduct != null)
                        {
                            InfoData._Collects--;
                            collectProduct._State = 1;

                            await dBProxyComponent.Save(collectProduct);
                            dBProxyComponent.SaveLog(collectProduct).Coroutine();
                            Game.Scene.GetComponent<ProductCenterComponent>().UserCollectProductlist[message.Userid].Remove(collectProduct);

                            response.IsSuccess = true;
                            response.Message = "取消收藏成功！";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "取消收藏失败！，该用户并未收藏该商品！";
                        }
                    }
                    else if (message.Type == 5)
                    {
                        List<Footmark> ProductCollectUserid = new List<Footmark>();
                        Footmark footmark = null;

                        lock (Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist)
                        {
                            if (Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist.ContainsKey(message.Userid))
                            {
                                ProductCollectUserid = Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist[message.Userid];
                            }
                            if (ProductCollectUserid.Count > 0)
                            {
                                foreach (Footmark item in ProductCollectUserid)
                                {
                                    if (item._FootmarkID == message.ProductInfoID)
                                    {
                                        footmark = item;
                                        break;
                                    }
                                }
                            }
                        }
                        if (footmark != null)
                        {
                            footmark._State = 1;

                            await dBProxyComponent.Save(footmark);
                            dBProxyComponent.SaveLog(footmark).Coroutine();
                            Game.Scene.GetComponent<ProductCenterComponent>().UserFootmarklist[message.Userid].Remove(footmark);

                            response.IsSuccess = true;
                            response.Message = "删除足迹成功！";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "删除足迹失败！，该用户并没有这个足迹！";
                        }
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

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

    /// <summary>
    /// 留言或者评价和取消评价  TODO通知卖家
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_ProductInfoDataMessageHandler : AMRpcHandler<C2G_ProductInfoDataMessage, G2C_ProductInfoDataMessage>
    {
        protected override async void Run(Session session, C2G_ProductInfoDataMessage message, Action<G2C_ProductInfoDataMessage> reply)
        {
            G2C_ProductInfoDataMessage response = new G2C_ProductInfoDataMessage();
            ProductInfoData InfoData = null;
            ProductInfoOrder OrderData = null;
            response.IsSuccess = false;
            List<ProductInfoData> productInfoDatas = new List<ProductInfoData>();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                    {
                        productInfoDatas = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                    }
                }
                if (productInfoDatas.Count > 0)
                {
                    foreach (ProductInfoData item in productInfoDatas)
                    {
                        if (item._ProductInfoID == message.ProductInfoID)
                        {
                            InfoData = item;
                            break;
                        }
                    }
                }
                else
                {
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist.ContainsKey(message.ProductInfoID))
                        {
                            InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist[message.ProductInfoID];
                        }
                    }
                }

                if (InfoData != null)
                {
                    if (message.Type == 1)
                    {
                        //留言
                        InfoData._PublicMessage.Insert(0, message.PublicMessage);

                        //TODO 通知卖家某某商品的有人留言


                        response.IsSuccess = true;
                        response.Message = "留言成功！";
                    }
                    else if (message.Type == 2)
                    {
                        EvaluateProduct evaluateProduct = null;
                        lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist)
                        {
                            List<EvaluateProduct> datalist = new List<EvaluateProduct>();
                            if (Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist.ContainsKey(InfoData._ProductInfoID))
                            {
                                datalist = Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist[InfoData._ProductInfoID];
                                foreach (EvaluateProduct item in datalist)
                                {
                                    if (item._UserID == message.UserID)
                                    {
                                        evaluateProduct = item;
                                        break;
                                    }
                                }
                            }
                        }
                        if (evaluateProduct == null)
                        {
                            //添加评价过的商品
                            EvaluateProduct infodata = ComponentFactory.Create<EvaluateProduct>();
                            infodata._UserID = message.UserID;
                            infodata._EvaluateProductID = InfoData._ProductInfoID;
                            infodata._EvaluateContent = message.Evaluate;
                            infodata._Evaluateminute = message.Score;
                            infodata._EvaluateTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                            infodata._State = 0;

                            await dBProxyComponent.Save(infodata);
                            dBProxyComponent.SaveLog(infodata).Coroutine();

                            //添加缓存
                            lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist)
                            {
                                List<EvaluateProduct> datalist = new List<EvaluateProduct>();
                                if (Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist.ContainsKey(InfoData._ProductInfoID))
                                {
                                    //有这个商铺直接往这个商铺的list里添加
                                    Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist[InfoData._ProductInfoID].Add(infodata);
                                }
                                else
                                {
                                    //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                                    if (Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist.TryAdd(InfoData._ProductInfoID, datalist) == true) { };
                                    Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist[InfoData._ProductInfoID].Add(infodata);
                                }
                            }
                            //修改订单用户是否评价了
                            List<ProductInfoOrder> infolist = new List<ProductInfoOrder>();
                            lock (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist)
                            {
                                if (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist.ContainsKey(message.UserID))
                                {
                                    infolist = Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist[message.UserID];
                                }
                            }
                            foreach (ProductInfoOrder item in infolist)
                            {
                                if (item._ProductInfoID == message.ProductInfoID)
                                {
                                    OrderData = item;
                                    break;
                                }
                            }
                            if (OrderData != null)
                            {
                                OrderData._IsAppraise = true;

                                await dBProxyComponent.Save(OrderData);
                                dBProxyComponent.SaveLog(OrderData).Coroutine();
                            }

                            //TODO 通知卖家某某订单的买家已经评价了

                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "评价失败，该用户已评价过了！";
                            //后续加追评，点赞，评价
                        }

                        response.IsSuccess = true;
                        response.Message = "评价成功！";
                    }
                    //else if (message.Type == 3)
                    //{
                    //    //清理缓存
                    //    lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist)
                    //    {
                    //        EvaluateProduct simpleOrder = null;
                    //        List<EvaluateProduct> datalist = new List<EvaluateProduct>();
                    //        if (Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist.ContainsKey(InfoData._ProductInfoID))
                    //        {
                    //            //有这个商铺直接往这个商铺的list里添加
                    //            datalist = Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist[InfoData._ProductInfoID];
                    //        }
                    //        else
                    //        {
                    //            response.IsSuccess = true;
                    //            response.Message = "清理个人购物车列表缓存出错！";
                    //        }
                    //        foreach (EvaluateProduct item in datalist)
                    //        {
                    //            if (item._EvaluateProductID == InfoData._ProductInfoID)
                    //            {
                    //                simpleOrder = item;
                    //                break;
                    //            }
                    //        }
                    //        if (simpleOrder != null)
                    //        {
                    //            Game.Scene.GetComponent<ProductCenterComponent>().ProductEvaluatelist[InfoData._ProductInfoID].Remove(simpleOrder);
                    //        }
                    //    }
                    //}

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

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

    /// <summary>
    /// 商品审核 
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AuditProductInfoDataHandler : AMRpcHandler<C2G_AuditProductInfoData, G2C_AuditProductInfoData>
    {
        protected override async void Run(Session session, C2G_AuditProductInfoData message, Action<G2C_AuditProductInfoData> reply)
        {
            G2C_AuditProductInfoData response = new G2C_AuditProductInfoData();
            ProductInfoData InfoData = null;
            response.IsSuccess = false;
            List<ProductInfoData> productInfoDatas = new List<ProductInfoData>();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                    {
                        productInfoDatas = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                    }
                }
                if (productInfoDatas.Count > 0)
                {
                    foreach (ProductInfoData item in productInfoDatas)
                    {
                        if (item._ProductInfoID == message.ProductInfoID)
                        {
                            InfoData = item;
                            break;
                        }
                    }
                }
                else
                {
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist.ContainsKey(message.ProductInfoID))
                        {
                            InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ProductInfolist[message.ProductInfoID];
                        }
                    }
                }

                if (InfoData != null)
                {
                    if (message.Type == 1)
                    {
                        InfoData._AuditState = 1;
                        InfoData._AuditMessage = message.AuditMessage;
                        InfoData._PublishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        InfoData._UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        response.IsSuccess = true;
                        response.Message = "审核修改成功，修改状态通过！";
                    }
                    else if (message.Type == 2)
                    {
                        InfoData._AuditState = 2;
                        InfoData._AuditMessage = message.AuditMessage;

                        response.IsSuccess = true;
                        response.Message = "审核修改成功，修改状态未通过！";
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "审核修改失败，未找到表！";
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
    #endregion

    #region 商品订单
    /// <summary>
    /// 查询自己商品订单id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryProductInfoOrderCountHandler : AMRpcHandler<C2G_QueryProductInfoOrderCount, G2C_QueryProductInfoOrderCount>
    {
        protected override async void Run(Session session, C2G_QueryProductInfoOrderCount message, Action<G2C_QueryProductInfoOrderCount> reply)
        {
            G2C_QueryProductInfoOrderCount response = new G2C_QueryProductInfoOrderCount();
            List<long> ProducOrderids = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ProductInfos);
            List<ProductInfoOrder> acountsInfo = new List<ProductInfoOrder>();
            response.IsSuccess = false;

            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist.ContainsKey(message.Userid))
                    {
                        acountsInfo = Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist[message.Userid];
                    }
                }

                if (acountsInfo.Count > 0)
                {
                    foreach (ProductInfoOrder item in acountsInfo)
                    {
                        if (ProducOrderids.Contains(item._OrderID))
                        {
                            continue;
                        }
                        if (ProducOrderids.Count >= message.Count)
                        {
                            break;
                        }
                        ProducOrderids.Add(item._OrderID);
                    }
                    response.IsSuccess = true;
                    response.ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(ProducOrderids);
                    response.Message = "商品订单id列表获取成功";
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

    /// <summary>
    /// 查询订单
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryProductInfoOrderHandler : AMRpcHandler<C2G_QueryProductInfoOrder, G2C_QueryProductInfoOrder>
    {
        protected override async void Run(Session session, C2G_QueryProductInfoOrder message, Action<G2C_QueryProductInfoOrder> reply)
        {
            G2C_QueryProductInfoOrder response = new G2C_QueryProductInfoOrder();
            List<ProductInfoOrder> infoOrders = new List<ProductInfoOrder>();
            ProductInfoOrder OrderData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist.ContainsKey(message.UserID))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist[message.UserID];
                    }
                }
                foreach (ProductInfoOrder item in infoOrders)
                {
                    if (item._OrderID == message.ProductOrderID)
                    {
                        OrderData = item;
                        break;
                    }
                }


                if (OrderData != null)
                {
                    response.ProductInfoID = OrderData._ProductInfoID;
                    response.UserID = OrderData._UserID;
                    response.AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(OrderData._AttributeBag);
                    response.PayType = OrderData._PayType;
                    response.Price = OrderData._Price;
                    response.Count = OrderData._Count;
                    response.LogisticsNumber = OrderData._LogisticsNumber;
                    response.PayState = OrderData._PayState;
                    response.CancelMessage = OrderData._CancelMessage;
                    response.RecedeMessage = OrderData._RecedeMessage;
                    response.OrderSort = OrderData._OrderSort;
                    response.CreationTime = OrderData._CreationTime;
                    response.PaymentTime = OrderData._PaymentTime;
                    response.SendProductTime = OrderData._SendProductTime;
                    response.DeliveryTime = OrderData._DeliveryTime;
                    response.CancelTime = OrderData._CancelTime;
                    response.RecedeTime = OrderData._RecedeTime;
                    response.RecedeOverTime = OrderData._RecedeOverTime;
                    response.ServiceList = RepeatedFieldAndListChangeTool.ListToRepeatedField(OrderData._ServiceList);
                    response.OrderInfo = OrderData._OrderInfo;
                    response.PayPlatformNumber = OrderData._PayPlatformNumber;
                    response.IsAppraise = OrderData._IsAppraise;
                    response.State = OrderData._State;

                    response.IsSuccess = true;
                    response.Message = "商品订单查询成功！";
                }
                else
                {
                    response.Message = "商品订单数据获取失败！";
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

    /// <summary>
    /// 购买商品 生成订单
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddProductInfoOrderHandler : AMRpcHandler<C2G_AddProductOrderData, G2C_AddProductOrderData>
    {
        protected override async void Run(Session session, C2G_AddProductOrderData message, Action<G2C_AddProductOrderData> reply)
        {
            G2C_AddProductOrderData response = new G2C_AddProductOrderData();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                ProductInfoOrder Orderdata = ComponentFactory.Create<ProductInfoOrder>();

                Orderdata._AttributeBag = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.AttributeBag);
                Orderdata._Count = message.Count;
                Orderdata._CreationTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Orderdata._DeliveryTime = "";
                Orderdata._IsAppraise = false;
                Orderdata._LogisticsNumber = "";
                Orderdata._OrderID = Orderdata.Id;
                Orderdata._OrderInfo = message.OrderInfo;
                Orderdata._OrderSort = message.OrderSort;
                Orderdata._PaymentTime = "";
                Orderdata._PayPlatformNumber = "";
                Orderdata._RecedeMessage = "";
                Orderdata._CancelMessage = "";
                Orderdata._PayState = 0;
                Orderdata._PayType = 0;
                Orderdata._Price = message.Price;
                Orderdata._ProductInfoID = message.ProductInfoID;
                Orderdata._SendProductTime = "";
                Orderdata._RecedeTime = "";
                Orderdata._RecedeOverTime = "";
                Orderdata._CancelTime = "";
                Orderdata._ServiceList = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ServiceList);
                Orderdata._UserID = message.UserID;
                Orderdata._State = 0;

                response.IsSuccess = true;
                response.Message = "购买商品，生成订单成功！";

                await dBProxyComponent.Save(Orderdata);
                dBProxyComponent.SaveLog(Orderdata).Coroutine();

                //添加缓存个人商品订单缓存
                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist)
                {
                    List<ProductInfoOrder> datalist = new List<ProductInfoOrder>();
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist.ContainsKey(Orderdata._UserID))
                    {
                        //有这个商铺直接往这个商铺的list里添加
                        Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist[Orderdata._UserID].Add(Orderdata);
                    }
                    else
                    {
                        //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                        if (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist.TryAdd(Orderdata._UserID, datalist) == true) { };
                        Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist[Orderdata._UserID].Add(Orderdata);
                    }
                }

                if (message.SimpleOrderID != 0)
                {
                    //删除购物车数据
                    SimpleOrder OrderData = null;
                    List<SimpleOrder> infoOrders = new List<SimpleOrder>();
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.ContainsKey(message.UserID))
                        {
                            infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[message.UserID];
                        }
                    }
                    foreach (SimpleOrder item in infoOrders)
                    {
                        if (item._OrderID == message.SimpleOrderID)
                        {
                            OrderData = item;
                            break;
                        }
                    }
                    if (OrderData != null)
                    {
                        OrderData._State = 1;

                        await dBProxyComponent.Save(OrderData);
                        dBProxyComponent.SaveLog(OrderData).Coroutine();
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

    /// <summary>
    /// 商品订单状态管理
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_ProductInfoOrderMessageHandler : AMRpcHandler<C2G_AuditProductOrderMessage, G2C_AuditProductOrderMessage>
    {
        protected override async void Run(Session session, C2G_AuditProductOrderMessage message, Action<G2C_AuditProductOrderMessage> reply)
        {
            G2C_AuditProductOrderMessage response = new G2C_AuditProductOrderMessage();
            List<ProductInfoOrder> infoOrders = new List<ProductInfoOrder>();
            ProductInfoOrder OrderData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist.ContainsKey(message.Userid))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist[message.Userid];
                    }
                }
                foreach (ProductInfoOrder item in infoOrders)
                {
                    if (item._OrderID == message.ProductOrderID)
                    {
                        OrderData = item;
                        break;
                    }
                }

                if (OrderData != null)
                {
                    if (message.Type == 1)
                    {
                        OrderData._PayState = 1;
                        OrderData._PayType = message.PayType;
                        OrderData._PayPlatformNumber = message.PayPlatformNumber;
                        OrderData._PaymentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        response.IsSuccess = true;
                        response.Message = "修改付款成功状态！";
                    }
                    else if (message.Type == 2)
                    {
                        OrderData._PayState = 2;
                        OrderData._LogisticsNumber = message.LogisticsNumber;
                        OrderData._SendProductTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        response.IsSuccess = true;
                        response.Message = "修改发货成功状态！";
                    }
                    else if (message.Type == 3)
                    {
                        OrderData._PayState = 3;
                        OrderData._DeliveryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        response.IsSuccess = true;
                        response.Message = "修改收货成功状态";
                    }
                    else if (message.Type == 4)
                    {
                        OrderData._PayState = 4;
                        OrderData._RecedeMessage = message.RecedeMessage;
                        OrderData._RecedeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        response.IsSuccess = true;
                        response.Message = "修改退款/售后状态";
                    }
                    else if (message.Type == 5)
                    {
                        OrderData._PayState = 5;
                        OrderData._RecedeOverTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        response.IsSuccess = true;
                        response.Message = "修改退款成功状态";
                    }
                    else if (message.Type == 6)
                    {
                        OrderData._PayState = 6;
                        OrderData._CancelMessage = message.CancelMessage;
                        OrderData._CancelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        response.IsSuccess = true;
                        response.Message = "修改取消订单状态";
                    }

                    await dBProxyComponent.Save(OrderData);
                    dBProxyComponent.SaveLog(OrderData).Coroutine();

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

    /// <summary>
    /// 删除订单
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_DelProductInfoOrderHandler : AMRpcHandler<C2G_DelProductInfoOrder, G2C_DelProductInfoOrder>
    {
        protected override async void Run(Session session, C2G_DelProductInfoOrder message, Action<G2C_DelProductInfoOrder> reply)
        {
            G2C_DelProductInfoOrder response = new G2C_DelProductInfoOrder();
            List<ProductInfoOrder> infoOrders = new List<ProductInfoOrder>();
            ProductInfoOrder OrderData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist.ContainsKey(message.Userid))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserProductOrderlist[message.Userid];
                    }
                }
                foreach (ProductInfoOrder item in infoOrders)
                {
                    if (item._OrderID == message.ProductOrderID)
                    {
                        OrderData = item;
                        break;
                    }
                }

                if (OrderData != null)
                {
                    OrderData._State = 1;

                    await dBProxyComponent.Save(OrderData);
                    dBProxyComponent.SaveLog(OrderData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "订单删除成功！";
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
    #endregion

    #region 购物车
    /// <summary>
    /// 查询自己购物车id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QuerySimpleOrderCountHandler : AMRpcHandler<C2G_QuerySimpleOrderCount, G2C_QuerySimpleOrderCount>
    {
        protected override async void Run(Session session, C2G_QuerySimpleOrderCount message, Action<G2C_QuerySimpleOrderCount> reply)
        {
            G2C_QuerySimpleOrderCount response = new G2C_QuerySimpleOrderCount();
            List<long> ProducOrderids = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ProductInfos);
            List<SimpleOrder> acountsInfo = new List<SimpleOrder>();
            response.IsSuccess = false;

            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.ContainsKey(message.Userid))
                    {
                        acountsInfo = Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[message.Userid];
                    }
                }

                if (acountsInfo.Count > 0)
                {
                    foreach (SimpleOrder item in acountsInfo)
                    {
                        if (ProducOrderids.Contains(item._OrderID))
                        {
                            continue;
                        }
                        if (ProducOrderids.Count >= message.Count)
                        {
                            break;
                        }
                        ProducOrderids.Add(item._OrderID);
                    }
                    response.IsSuccess = true;
                    response.ProductInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(ProducOrderids);
                    response.Message = "购物车id列表获取成功";
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

    /// <summary>
    /// 查询购物车
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QuerySimpleOrderHandler : AMRpcHandler<C2G_QuerySimpleOrder, G2C_QuerySimpleOrder>
    {
        protected override async void Run(Session session, C2G_QuerySimpleOrder message, Action<G2C_QuerySimpleOrder> reply)
        {
            G2C_QuerySimpleOrder response = new G2C_QuerySimpleOrder();
            List<SimpleOrder> infoOrders = new List<SimpleOrder>();
            SimpleOrder OrderData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.ContainsKey(message.Userid))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[message.Userid];
                    }
                }
                foreach (SimpleOrder item in infoOrders)
                {
                    if (item._OrderID == message.SimpleOrderID)
                    {
                        OrderData = item;
                        break;
                    }
                }

                if (OrderData != null)
                {
                    response.ProductInfoID = OrderData._ProductInfoID;
                    response.UserID = OrderData._UserID;
                    response.AttributeBag = RepeatedFieldAndListChangeTool.ListToRepeatedField(OrderData._AttributeBag);
                    response.Price = OrderData._Price;
                    response.Count = OrderData._Count;
                    response.UpdateTime = OrderData._UpdateTime;

                    response.IsSuccess = true;
                    response.Message = "购物车查询成功！";
                }
                else
                {
                    response.Message = "购物车数据获取失败！";
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

    /// <summary>
    /// 添加购物车
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddSimpleOrderHandler : AMRpcHandler<C2G_AddSimpleOrder, G2C_AddSimpleOrder>
    {
        protected override async void Run(Session session, C2G_AddSimpleOrder message, Action<G2C_AddSimpleOrder> reply)
        {
            G2C_AddSimpleOrder response = new G2C_AddSimpleOrder();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                SimpleOrder Orderdata = ComponentFactory.Create<SimpleOrder>();

                Orderdata._AttributeBag = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.AttributeBag);
                Orderdata._Count = message.Count;
                Orderdata._Price = message.Price;
                Orderdata._OrderID = Orderdata.Id;
                Orderdata._ProductInfoID = message.ProductInfoID;
                Orderdata._UserID = message.UserID;
                Orderdata._UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Orderdata._State = 0;

                response.IsSuccess = true;
                response.Message = "添加购物车成功！";

                await dBProxyComponent.Save(Orderdata);
                dBProxyComponent.SaveLog(Orderdata).Coroutine();

                //添加缓存个人购物车缓存
                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist)
                {
                    List<SimpleOrder> datalist = new List<SimpleOrder>();
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.ContainsKey(Orderdata._UserID))
                    {
                        //有这个商铺直接往这个商铺的list里添加
                        Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[Orderdata._UserID].Add(Orderdata);
                    }
                    else
                    {
                        //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                        if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.TryAdd(Orderdata._UserID, datalist) == true) { };
                        Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[Orderdata._UserID].Add(Orderdata);
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

    /// <summary>
    /// 用户修改购物车信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateSimpleOrderHandler : AMRpcHandler<C2G_UpdateSimpleOrder, G2C_UpdateSimpleOrder>
    {
        protected override async void Run(Session session, C2G_UpdateSimpleOrder message, Action<G2C_UpdateSimpleOrder> reply)
        {
            G2C_UpdateSimpleOrder response = new G2C_UpdateSimpleOrder();
            List<SimpleOrder> infoOrders = new List<SimpleOrder>();
            SimpleOrder OrderData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.ContainsKey(message.UserID))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[message.UserID];
                    }
                }
                foreach (SimpleOrder item in infoOrders)
                {
                    if (item._OrderID == message.OrderID)
                    {
                        OrderData = item;
                        break;
                    }
                }

                if (OrderData != null)
                {
                    if (message.Price != 0)
                    {
                        OrderData._Price = message.Price;
                    }
                    if (message.Count != 0)
                    {
                        OrderData._Count = message.Count;
                    }
                    if (message.AttributeBag.Count != 0)
                    {
                        OrderData._AttributeBag = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.AttributeBag);
                    }
                    OrderData._UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    await dBProxyComponent.Save(OrderData);
                    dBProxyComponent.SaveLog(OrderData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "购物车订单修改成功！";
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

    /// <summary>
    /// 删除购物车订单
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_DelSimpleOrderHandler : AMRpcHandler<C2G_DelSimpleOrder, G2C_DelSimpleOrder>
    {
        protected override async void Run(Session session, C2G_DelSimpleOrder message, Action<G2C_DelSimpleOrder> reply)
        {
            G2C_DelSimpleOrder response = new G2C_DelSimpleOrder();
            List<SimpleOrder> infoOrders = new List<SimpleOrder>();
            SimpleOrder OrderData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.ContainsKey(message.UserID))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[message.UserID];
                    }
                }
                foreach (SimpleOrder item in infoOrders)
                {
                    if (item._OrderID == message.OrderID)
                    {
                        OrderData = item;
                        break;
                    }
                }

                if (OrderData != null)
                {
                    //删除购物车数据
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist.ContainsKey(message.UserID))
                        {
                            infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().UserSimpleOrderlist[message.UserID];
                        }
                    }
                    foreach (SimpleOrder item in infoOrders)
                    {
                        if (item._OrderID == message.OrderID)
                        {
                            OrderData = item;
                            break;
                        }
                    }
                    if (OrderData != null)
                    {
                        OrderData._State = 1;

                        await dBProxyComponent.Save(OrderData);
                        dBProxyComponent.SaveLog(OrderData).Coroutine();

                        response.IsSuccess = true;
                        response.Message = "购物车订单删除成功！";
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
    #endregion

    #region 服务
    /// <summary>
    /// 查询服务id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryServiceInfoCountHandler : AMRpcHandler<C2G_QueryServiceInfoCount, G2C_QueryServiceInfoCount>
    {
        protected override void Run(Session session, C2G_QueryServiceInfoCount message, Action<G2C_QueryServiceInfoCount> reply)
        {
            G2C_QueryServiceInfoCount response = new G2C_QueryServiceInfoCount();
            response.IsSuccess = false;

            try
            {
                if (Game.Scene.GetComponent<ProductCenterComponent>().ServiceInfos.Count != 0)
                {
                    response.IsSuccess = true;
                    response.Message = "服务id列表获取成功";
                    response.ServiceInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(Game.Scene.GetComponent<ProductCenterComponent>().ServiceInfos);
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

    /// <summary>
    /// 查询服务
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryServiceInfoHandler : AMRpcHandler<C2G_QueryServiceInfo, G2C_QueryServiceInfo>
    {
        protected override async void Run(Session session, C2G_QueryServiceInfo message, Action<G2C_QueryServiceInfo> reply)
        {
            G2C_QueryServiceInfo response = new G2C_QueryServiceInfo();
            ServiceInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<ServiceInfoData>("{'_ServiceInfoID' : " + message.ServiceInfoID + "}");

                if (acounts.Count > 0)
                {
                    InfoData = acounts[0] as ServiceInfoData;

                    response.ServiceInfoName = InfoData._ServiceInfoName;
                    response.ServiceInfoContent = InfoData._ServiceInfoContent;
                    response.State = InfoData._State;

                    response.IsSuccess = true;
                    response.Message = "服务查询成功！";
                }
                else
                {
                    response.Message = "服务数据获取失败！";
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

    /// <summary>
    /// 添加服务
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddServiceInfoHandler : AMRpcHandler<C2G_AddServiceInfo, G2C_AddServiceInfo>
    {
        protected override async void Run(Session session, C2G_AddServiceInfo message, Action<G2C_AddServiceInfo> reply)
        {
            G2C_AddServiceInfo response = new G2C_AddServiceInfo();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                ServiceInfoData Infodata = ComponentFactory.Create<ServiceInfoData>();

                Infodata._ServiceInfoContent = message.ServiceInfoContent;
                Infodata._ServiceInfoID = Infodata.Id;
                Infodata._ServiceInfoName = message.ServiceInfoName;
                Infodata._State = 0;

                response.IsSuccess = true;
                response.Message = "添加服务成功！";

                await dBProxyComponent.Save(Infodata);
                dBProxyComponent.SaveLog(Infodata).Coroutine();


                //添加缓存
                lock (Game.Scene.GetComponent<ProductCenterComponent>().ServiceInfos)
                {
                    if (!Game.Scene.GetComponent<ProductCenterComponent>().ServiceInfos.Contains(Infodata.Id))
                    {
                        Game.Scene.GetComponent<ProductCenterComponent>().ServiceInfos.Add(Infodata.Id);
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

    /// <summary>
    /// 后台修改服务信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateServiceInfoHandler : AMRpcHandler<C2G_UpdateServiceInfo, G2C_UpdateServiceInfo>
    {
        protected override async void Run(Session session, C2G_UpdateServiceInfo message, Action<G2C_UpdateServiceInfo> reply)
        {
            G2C_UpdateServiceInfo response = new G2C_UpdateServiceInfo();
            ServiceInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<ServiceInfoData>("{'_ServiceInfoID' : " + message.ServiceInfoID + "}");

                if (acounts.Count > 0)
                {
                    InfoData = acounts[0] as ServiceInfoData;
                    if (message.ServiceInfoContent != "")
                    {
                        InfoData._ServiceInfoContent = message.ServiceInfoContent;
                    }
                    if (message.ServiceInfoName != "")
                    {
                        InfoData._ServiceInfoName = message.ServiceInfoName;
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "服务信息修改成功！";
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

    /// <summary>
    /// 删除服务信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_DelServiceInfoHandler : AMRpcHandler<C2G_DelServiceInfo, G2C_DelServiceInfo>
    {
        protected override async void Run(Session session, C2G_DelServiceInfo message, Action<G2C_DelServiceInfo> reply)
        {
            G2C_DelServiceInfo response = new G2C_DelServiceInfo();
            ServiceInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                var acounts = await dBProxyComponent.Query<ServiceInfoData>("{'_ServiceInfoID' : " + message.ServiceInfoID + "}");

                if (acounts.Count > 0)
                {
                    InfoData = acounts[0] as ServiceInfoData;
                    InfoData._State = 1;

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "服务信息删除成功！";
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
    #endregion

    #region 商铺
    /// <summary>
    /// 查询自己收藏的店铺id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryMyShopInfoDataCountHandler : AMRpcHandler<C2G_QueryMyShopInfoDataCount, G2C_QueryMyShopInfoDataCount>
    {
        protected override async void Run(Session session, C2G_QueryMyShopInfoDataCount message, Action<G2C_QueryMyShopInfoDataCount> reply)
        {
            G2C_QueryMyShopInfoDataCount response = new G2C_QueryMyShopInfoDataCount();
            List<long> ProductInfoids = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ShopInfos);
            List<CollectShopInfo> acountsInfo = new List<CollectShopInfo>();
            response.IsSuccess = false;

            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                lock (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist.ContainsKey(message.UserID))
                    {
                        acountsInfo = Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist[message.UserID];
                    }
                }

                if (acountsInfo.Count > 0)
                {
                    foreach (CollectShopInfo item in acountsInfo)
                    {
                        if (item._State == 0)
                        {
                            if (ProductInfoids.Contains(item._CollectShopInfoID))
                            {
                                continue;
                            }
                            if (ProductInfoids.Count > message.Count)
                            {
                                break;
                            }
                            ProductInfoids.Add(item._CollectShopInfoID);
                        }
                    }
                }

                response.IsSuccess = true;
                response.ShopInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(ProductInfoids);
                response.Message = "商铺id列表获取成功";

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

    /// <summary>
    /// 查询店铺id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryShopInfoDataCountHandler : AMRpcHandler<C2G_QueryShopInfoDataCount, G2C_QueryShopInfoDataCount>
    {
        protected override async void Run(Session session, C2G_QueryShopInfoDataCount message, Action<G2C_QueryShopInfoDataCount> reply)
        {
            G2C_QueryShopInfoDataCount response = new G2C_QueryShopInfoDataCount();
            List<long> ProductInfoids = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ShopInfos);
            List<ShopInfoData> acountsInfo = new List<ShopInfoData>();
            response.IsSuccess = false;

            int skip = 0;
            int limit = 20;//拿几条数据
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                if (message.QueryType == 1)
                {
                    var acountscount = await dBProxyComponent.QueryCount<ShopInfoData>("{_ShopName:/" + message.QueryConntent + "/}");
                    Log.Debug(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + acountscount.ToString());
                    while (skip < acountscount)
                    {
                        string Json = "{_ShopName:/" + message.QueryConntent + "/}";//查找类型里面可以添加斜杠来做匹配查找，例如：{userNowArea:/^湖南省/}
                        string sort = "{ }";//排序方式，必须要有参数和排序方式，例如：{userID:1}
                        string LSS = Json + "|" + skip + "|" + limit + "|" + sort;
                        var acounts = await dBProxyComponent.QueryLss<ShopInfoData>(LSS);
                        foreach (ShopInfoData item in acounts)
                        {
                            if (ProductInfoids.Contains(item._ShopInfoID))
                            {
                                continue;
                            }
                            if (ProductInfoids.Count > message.Count)
                            {
                                break;
                            }
                            ProductInfoids.Add(item._ShopInfoID);
                        }
                        if (ProductInfoids.Count > message.Count)
                        {
                            break;
                        }
                        skip += limit;
                    }
                    skip = 0;
                }

                response.IsSuccess = true;
                response.ShopInfos = RepeatedFieldAndListChangeTool.ListToRepeatedField(ProductInfoids);
                response.Message = "商铺id列表获取成功";

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

    /// <summary>
    /// 查询商铺
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryShopInfoDataHandler : AMRpcHandler<C2G_QueryShopInfoData, G2C_QueryShopInfoData>
    {
        protected override async void Run(Session session, C2G_QueryShopInfoData message, Action<G2C_QueryShopInfoData> reply)
        {
            G2C_QueryShopInfoData response = new G2C_QueryShopInfoData();
            ShopInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.ContainsKey(message.ShopInfoID))
                    {
                        InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist[message.ShopInfoID];
                    }
                }

                if (InfoData != null)
                {
                    response.UserID = InfoData._UserID;
                    response.ShopName = InfoData._ShopName;
                    response.ShopPublishGround = InfoData._ShopPublishGround;
                    response.Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._Intrduce);
                    response.ShopType = InfoData._ShopType;
                    response.ShopInfoHeadImage = InfoData._ShopInfoHeadImage;
                    response.ShopInfoImages = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._ShopInfoImages);
                    response.IsAuthentication = InfoData._IsAuthentication;
                    response.ShopLevel = InfoData._ShopLevel;
                    response.ShopBailMoney = InfoData._ShopBailMoney;
                    response.PayShopBailMoney = InfoData._PayShopBailMoney;
                    response.ShopSorts = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._ShopSorts);
                    response.ShopUserVIP = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._ShopUserVIP);
                    response.ShopActivity = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._ShopActivity);
                    response.ShopTime = InfoData._ShopTime;
                    response.AuditState = InfoData._AuditState;
                    response.AuditMessage = InfoData._AuditMessage;
                    response.State = InfoData._State;

                    response.IsSuccess = true;
                    response.Message = "商铺查询成功！";
                }
                else
                {
                    response.Message = "商铺数据获取失败！";
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

    /// <summary>
    /// 添加店铺  //TODO 商铺名称不能一样，否则创建失败    或者设置一个查询商铺名称的方法，返回true或者false
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddShopInfoDataHandler : AMRpcHandler<C2G_AddShopInfoData, G2C_AddShopInfoData>
    {
        protected override async void Run(Session session, C2G_AddShopInfoData message, Action<G2C_AddShopInfoData> reply)
        {
            G2C_AddShopInfoData response = new G2C_AddShopInfoData();
            ShopInfoData shopInfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                if (message.ShopInfoID != 0)
                {
                    //如果有商铺数据就数据制空，如果没有就创建新的
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.ContainsKey(message.ShopInfoID))
                        {
                            shopInfoData = Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist[message.ShopInfoID];
                        }
                    }

                    shopInfoData._AuditState = 0;
                    shopInfoData._AuditMessage = "";
                    shopInfoData._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                    shopInfoData._IsAuthentication = message.IsAuthentication;
                    shopInfoData._PayShopBailMoney = 0;
                    shopInfoData._ShopActivity = new List<long>();
                    shopInfoData._ShopBailMoney = 0;
                    shopInfoData._ShopInfoHeadImage = message.ShopInfoHeadImage;
                    shopInfoData._ShopInfoImages = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ShopInfoImages);
                    shopInfoData._ShopLevel = 0;
                    shopInfoData._ShopTime = "";
                    shopInfoData._ShopUserVIP = new List<long>();
                    shopInfoData._ShopSorts = new List<string>();
                    shopInfoData._ShopInfoID = shopInfoData.Id;
                    shopInfoData._UserID = message.UserID;
                    shopInfoData._ShopName = message.ShopName;
                    shopInfoData._ShopPublishGround = message.ShopPublishGround;
                    shopInfoData._State = 0;

                    response.IsSuccess = true;
                    response.Message = "重新申请店铺成功，等待审核！";

                    await dBProxyComponent.Save(shopInfoData);
                    dBProxyComponent.SaveLog(shopInfoData).Coroutine();
                }
                else
                {
                    ShopInfoData infodata = ComponentFactory.Create<ShopInfoData>();

                    infodata._AuditState = 0;
                    infodata._AuditMessage = "";
                    infodata._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                    infodata._IsAuthentication = message.IsAuthentication;
                    infodata._PayShopBailMoney = 0;
                    infodata._ShopActivity = new List<long>();
                    infodata._ShopBailMoney = 0;
                    infodata._ShopInfoHeadImage = message.ShopInfoHeadImage;
                    infodata._ShopInfoImages = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.ShopInfoImages);
                    infodata._ShopLevel = 0;
                    infodata._ShopTime = "";
                    infodata._ShopUserVIP = new List<long>();
                    infodata._ShopSorts = new List<string>();
                    infodata._ShopInfoID = infodata.Id;
                    infodata._UserID = message.UserID;
                    infodata._ShopName = message.ShopName;
                    infodata._ShopPublishGround = message.ShopPublishGround;
                    infodata._State = 0;

                    response.IsSuccess = true;
                    response.Message = "申请店铺成功，等待审核！";

                    await dBProxyComponent.Save(infodata);
                    dBProxyComponent.SaveLog(infodata).Coroutine();

                    //添加缓存
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.TryAdd(infodata._ShopInfoID, infodata)) { };
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

    /// <summary>
    /// 删除店铺  TODO 店铺删除必须所有的订单都已经结束且离最后一件订单要距离7天时间
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_DelShopInfoDataHandler : AMRpcHandler<C2G_DelShopInfoData, G2C_DelShopInfoData>
    {
        protected override async void Run(Session session, C2G_DelShopInfoData message, Action<G2C_DelShopInfoData> reply)
        {
            G2C_DelShopInfoData response = new G2C_DelShopInfoData();
            ShopInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.ContainsKey(message.ShopInfoID))
                    {
                        InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist[message.ShopInfoID];
                    }
                }

                if (InfoData != null)
                {
                    InfoData._State = 1;

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "店铺删除成功！";

                    //清空店铺总表
                    Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.Remove(message.ShopInfoID);

                    //清空店铺商品下架
                    List<ProductInfoData> productlist = new List<ProductInfoData>();
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist.ContainsKey(message.ShopInfoID))
                        {
                            productlist = Game.Scene.GetComponent<ProductCenterComponent>().ShopProductlist[message.ShopInfoID];
                        }
                    }
                    foreach (ProductInfoData item in productlist)
                    {
                        item._State = 1;

                        await dBProxyComponent.Save(item);
                        dBProxyComponent.SaveLog(item).Coroutine();
                    }

                    //清空店铺活动
                    List<ShopActivityInfo> activitylist = new List<ShopActivityInfo>();
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.ContainsKey(message.ShopInfoID))
                        {
                            activitylist = Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[message.ShopInfoID];
                        }
                    }
                    foreach (ShopActivityInfo item in activitylist)
                    {
                        item._State = 1;

                        await dBProxyComponent.Save(item);
                        dBProxyComponent.SaveLog(item).Coroutine();
                    }

                    //清空店铺收藏
                    List<CollectShopInfo> collectlist = new List<CollectShopInfo>();
                    lock (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist.ContainsKey(message.UserID))
                        {
                            collectlist = Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist[message.UserID];
                        }
                    }
                    foreach (CollectShopInfo item in collectlist)
                    {
                        item._State = 1;

                        await dBProxyComponent.Save(item);
                        dBProxyComponent.SaveLog(item).Coroutine();
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

    /// <summary>
    /// 商家修改店铺信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateShopInfoDataHandler : AMRpcHandler<C2G_UpdateShopInfoData, G2C_UpdateShopInfoData>
    {
        protected override async void Run(Session session, C2G_UpdateShopInfoData message, Action<G2C_UpdateShopInfoData> reply)
        {
            G2C_UpdateShopInfoData response = new G2C_UpdateShopInfoData();
            ShopInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.ContainsKey(message.ShopInfoID))
                    {
                        InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist[message.ShopInfoID];
                    }
                }

                if (InfoData != null)
                {
                    if (message.ShopName != "")
                    {
                        InfoData._ShopName = message.ShopName;
                    }
                    if (message.ShopPublishGround != "")
                    {
                        InfoData._ShopPublishGround = message.ShopPublishGround;
                    }
                    if (message.ShopType != 0)
                    {
                        InfoData._ShopType = message.ShopType;
                    }
                    if (message.ShopInfoHeadImage != "")
                    {
                        InfoData._ShopInfoHeadImage = message.ShopInfoHeadImage;
                    }
                    if (message.ShopImageType != 0)
                    {
                        if (message.ShopImageType == 1)
                        {
                            InfoData._ShopInfoImages.Add(message.ShopInfoImage);
                        }
                        else if (message.ShopImageType == 2)
                        {
                            InfoData._ShopInfoImages.Remove(message.ShopInfoImage);
                        }
                    }
                    if (message.ShopSortType != 0)
                    {
                        if (message.ShopSortType == 1)
                        {
                            InfoData._ShopSorts.Add(message.ShopSort);
                        }
                        else if (message.ShopSortType == 2)
                        {
                            InfoData._ShopSorts.Remove(message.ShopSort);
                        }
                    }
                    if (message.Intrduce.Count != 0)
                    {
                        InfoData._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "店铺修改成功！";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "店铺修改失败，没有找到店铺表！";
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

    /// <summary>
    /// 客服人员修改店铺信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_SUpdateShopInfoDataHandler : AMRpcHandler<C2G_SUpdateShopInfoData, G2C_SUpdateShopInfoData>
    {
        protected override async void Run(Session session, C2G_SUpdateShopInfoData message, Action<G2C_SUpdateShopInfoData> reply)
        {
            G2C_SUpdateShopInfoData response = new G2C_SUpdateShopInfoData();
            ShopInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.ContainsKey(message.ShopInfoID))
                    {
                        InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist[message.ShopInfoID];
                    }
                }

                if (InfoData != null)
                {
                    if (message.ShopLevel != 0)
                    {
                        InfoData._ShopLevel = message.ShopLevel;
                    }
                    if (message.PayShopBailMoney != 0)
                    {
                        InfoData._PayShopBailMoney = message.PayShopBailMoney;
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "客服修改店铺信息修改成功！";
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

    /// <summary>
    /// 商铺审核
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AuditShopInfoDataHandler : AMRpcHandler<C2G_AuditShopInfoData, G2C_AuditShopInfoData>
    {
        protected override async void Run(Session session, C2G_AuditShopInfoData message, Action<G2C_AuditShopInfoData> reply)
        {
            G2C_AuditShopInfoData response = new G2C_AuditShopInfoData();
            ShopInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.ContainsKey(message.ShopInfoID))
                    {
                        InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist[message.ShopInfoID];
                    }
                }

                if (InfoData != null)
                {
                    if (message.Type == 1)
                    {
                        InfoData._AuditState = 1;
                        InfoData._AuditMessage = message.AuditMessage;
                        if (InfoData._ShopTime == "")
                        {
                            InfoData._ShopTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        InfoData._PayShopBailMoney = message.PayShopBailMoney;

                        response.IsSuccess = true;
                        response.Message = "审核修改成功，修改状态通过！";

                    }
                    else if (message.Type == 2)
                    {
                        InfoData._AuditState = 2;
                        InfoData._AuditMessage = message.AuditMessage;

                        response.IsSuccess = true;
                        response.Message = "审核修改成功，修改状态未通过！";
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

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

    /// <summary>
    /// 添加收藏或取消收藏
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_ViewShopInfoDataHandler : AMRpcHandler<C2G_ViewShopInfoData, G2C_ViewShopInfoData>
    {
        protected override async void Run(Session session, C2G_ViewShopInfoData message, Action<G2C_ViewShopInfoData> reply)
        {
            G2C_ViewShopInfoData response = new G2C_ViewShopInfoData();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                if (message.Type == 1)
                {
                    //收藏
                    CollectShopInfo infodata = ComponentFactory.Create<CollectShopInfo>();
                    infodata._UserID = message.Userid;
                    infodata._CollectShopInfoID = message.ShopInfoID;
                    infodata._State = 0;

                    await dBProxyComponent.Save(infodata);
                    dBProxyComponent.SaveLog(infodata).Coroutine();

                    if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist.ContainsKey(message.Userid))
                    {
                        //有这个商铺直接往这个商铺的list里添加
                        Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist[message.Userid].Add(infodata);
                    }
                    else
                    {
                        List<CollectShopInfo> datalist = new List<CollectShopInfo>();
                        //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                        if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist.TryAdd(message.Userid, datalist) == true) { };
                        Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist[message.Userid].Add(infodata);
                    }

                    response.IsSuccess = true;
                    response.Message = "收藏成功！";
                }
                else if (message.Type == 2)
                {
                    //取消收藏

                    List<CollectShopInfo> ProductCollectUserid = new List<CollectShopInfo>();
                    CollectShopInfo collectShopInfo = null;

                    lock (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist)
                    {
                        if (Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist.ContainsKey(message.Userid))
                        {
                            ProductCollectUserid = Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist[message.Userid];
                        }
                        if (ProductCollectUserid.Count > 0)
                        {
                            foreach (CollectShopInfo item in ProductCollectUserid)
                            {
                                if (item._CollectShopInfoID == message.ShopInfoID)
                                {
                                    collectShopInfo = item;
                                    break;
                                }
                            }
                        }
                    }
                    if (collectShopInfo != null)
                    {
                        collectShopInfo._State = 1;

                        await dBProxyComponent.Save(collectShopInfo);
                        dBProxyComponent.SaveLog(collectShopInfo).Coroutine();
                        Game.Scene.GetComponent<ProductCenterComponent>().UserCollecShoptlist[message.Userid].Remove(collectShopInfo);

                        response.IsSuccess = true;
                        response.Message = "取消收藏成功！";
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

    /// <summary>
    /// 添加店铺会员 （暂时不使用）
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddShopUserVIPHandler : AMRpcHandler<C2G_AddShopUserVIP, G2C_AddShopUserVIP>
    {
        protected override async void Run(Session session, C2G_AddShopUserVIP message, Action<G2C_AddShopUserVIP> reply)
        {
            G2C_AddShopUserVIP response = new G2C_AddShopUserVIP();
            ShopInfoData InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist.ContainsKey(message.ShopInfoID))
                    {
                        InfoData = Game.Scene.GetComponent<ProductCenterComponent>().ShopInfolist[message.ShopInfoID];
                    }
                }

                if (InfoData != null)
                {
                    if (message.Type == 1)
                    {
                        InfoData._ShopUserVIP.Add(message.UserID);

                        response.IsSuccess = true;
                        response.Message = "添加会员成功！";

                    }
                    else if (message.Type == 2)
                    {
                        InfoData._ShopUserVIP.Remove(message.UserID);

                        response.IsSuccess = true;
                        response.Message = "删除会员成功！";
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

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
    #endregion

    #region 店铺活动
    /// <summary>
    /// 查询店铺活动id列
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryShopActivityInfoCountHandler : AMRpcHandler<C2G_QueryShopActivityInfoCount, G2C_QueryShopActivityInfoCount>
    {
        protected override async void Run(Session session, C2G_QueryShopActivityInfoCount message, Action<G2C_QueryShopActivityInfoCount> reply)
        {
            G2C_QueryShopActivityInfoCount response = new G2C_QueryShopActivityInfoCount();
            List<long> ProductInfoids = new List<long>();
            List<ShopActivityInfo> acountsInfo = new List<ShopActivityInfo>();
            response.IsSuccess = false;

            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.ContainsKey(message.ShopInfoID))
                    {
                        acountsInfo = Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[message.ShopInfoID];
                    }
                }

                if (acountsInfo.Count > 0)
                {
                    foreach (ShopActivityInfo item in acountsInfo)
                    {
                        if (ProductInfoids.Contains(item._ShopActivityID))
                        {
                            continue;
                        }
                        ProductInfoids.Add(item._ShopActivityID);
                    }
                    response.IsSuccess = true;
                    response.ShopActivitys = RepeatedFieldAndListChangeTool.ListToRepeatedField(ProductInfoids);
                    response.Message = "商铺活动id列表获取成功";
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

    /// <summary>
    /// 查询商铺活动
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_QueryShopActivityInfoHandler : AMRpcHandler<C2G_QueryShopActivityInfo, G2C_QueryShopActivityInfo>
    {
        protected override async void Run(Session session, C2G_QueryShopActivityInfo message, Action<G2C_QueryShopActivityInfo> reply)
        {
            G2C_QueryShopActivityInfo response = new G2C_QueryShopActivityInfo();
            List<ShopActivityInfo> infoOrders = new List<ShopActivityInfo>();
            ShopActivityInfo InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.ContainsKey(message.ShopInfoID))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[message.ShopInfoID];
                    }
                }
                foreach (ShopActivityInfo item in infoOrders)
                {
                    if (item._ShopActivityID == message.ShopActivityID)
                    {
                        InfoData = item;
                        break;
                    }
                }

                if (InfoData != null)
                {
                    response.ShopInfoID = InfoData._ShopInfoID;
                    response.DiscountsSort = InfoData._DiscountsSort;
                    response.DiscountsSortFields = InfoData._DiscountsSortFields;
                    response.Intrduce = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._Intrduce);
                    response.DisProducts = RepeatedFieldAndListChangeTool.ListToRepeatedField(InfoData._DisProducts);
                    response.Count = InfoData._Count;
                    response.StartTime = InfoData._StartTime;
                    response.EndTime = InfoData._EndTime;
                    response.DiscountsAlert = InfoData._DiscountsAlert;
                    response.AuditState = InfoData._AuditState;
                    response.AuditMessage = InfoData._AuditMessage;
                    response.State = InfoData._State;

                    response.IsSuccess = true;
                    response.Message = "商铺查询活动成功！";
                }
                else
                {
                    response.Message = "商铺活动数据获取失败！";
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

    /// <summary>
    /// 添加店铺活动
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AddShopActivityInfoHandler : AMRpcHandler<C2G_AddShopActivityInfo, G2C_AddShopActivityInfo>
    {
        protected override async void Run(Session session, C2G_AddShopActivityInfo message, Action<G2C_AddShopActivityInfo> reply)
        {
            G2C_AddShopActivityInfo response = new G2C_AddShopActivityInfo();
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                ShopActivityInfo Infodata = ComponentFactory.Create<ShopActivityInfo>();

                Infodata._AuditMessage = "";
                Infodata._AuditState = 0;
                Infodata._Count = message.Count;
                Infodata._DiscountsAlert = message.DiscountsAlert;
                Infodata._DiscountsSort = message.DiscountsSort;
                Infodata._DiscountsSortFields = message.DiscountsSortFields;
                Infodata._EndTime = message.EndTime;
                Infodata._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                Infodata._DisProducts = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.DisProducts);
                Infodata._ShopActivityID = Infodata.Id;
                Infodata._ShopInfoID = message.ShopInfoID;
                Infodata._StartTime = message.StartTime;
                Infodata._IsOver = 0;
                Infodata._State = 0;

                response.IsSuccess = true;
                response.Message = "添加店铺活动成功！,等待审核！";

                await dBProxyComponent.Save(Infodata);
                dBProxyComponent.SaveLog(Infodata).Coroutine();

                //添加缓存商铺活动缓存
                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist)
                {
                    List<ShopActivityInfo> datalist = new List<ShopActivityInfo>();
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.ContainsKey(Infodata._ShopInfoID))
                    {
                        //有这个商铺直接往这个商铺的list里添加
                        Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[Infodata._ShopInfoID].Add(Infodata);
                    }
                    else
                    {
                        //没有这个商铺，添加这个商铺，在往商铺里面的list里添加
                        if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.TryAdd(Infodata._ShopInfoID, datalist) == true) { };
                        Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[Infodata._ShopInfoID].Add(Infodata);
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

    /// <summary>
    /// 店主修改店铺活动信息
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_UpdateShopActivityInfoHandler : AMRpcHandler<C2G_UpdateShopActivityInfo, G2C_UpdateShopActivityInfo>
    {
        protected override async void Run(Session session, C2G_UpdateShopActivityInfo message, Action<G2C_UpdateShopActivityInfo> reply)
        {
            G2C_UpdateShopActivityInfo response = new G2C_UpdateShopActivityInfo();
            List<ShopActivityInfo> infoOrders = new List<ShopActivityInfo>();
            ShopActivityInfo InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.ContainsKey(message.ShopInfoID))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[message.ShopInfoID];
                    }
                }
                foreach (ShopActivityInfo item in infoOrders)
                {
                    if (item._ShopActivityID == message.ShopActivityID)
                    {
                        InfoData = item;
                        break;
                    }
                }

                if (InfoData != null)
                {
                    if (message.DisProductType != 0)
                    {
                        if (message.DisProductType == 1)
                        {
                            InfoData._DisProducts.Add(message.DisProduct);
                        }
                        else if (message.DisProductType == 2)
                        {
                            InfoData._DisProducts.Remove(message.DisProduct);
                        }
                    }
                    if (message.DiscountsSort != 0)
                    {
                        InfoData._DiscountsSort = message.DiscountsSort;
                    }
                    if (message.DiscountsSortFields != "")
                    {
                        InfoData._DiscountsSortFields = message.DiscountsSortFields;
                    }
                    if (message.Intrduce.Count != 0)
                    {
                        InfoData._Intrduce = RepeatedFieldAndListChangeTool.RepeatedFieldToList(message.Intrduce);
                    }
                    if (message.Count != 0)
                    {
                        InfoData._Count = message.Count;
                    }
                    if (message.StartTime != "")
                    {
                        InfoData._StartTime = message.StartTime;
                    }
                    if (message.EndTime != "")
                    {
                        InfoData._EndTime = message.EndTime;
                    }
                    if (message.DiscountsAlert != "")
                    {
                        InfoData._DiscountsAlert = message.DiscountsAlert;
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "店铺活动信息修改成功！";
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

    /// <summary>
    /// 删除店铺活动
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_DelShopActivityInfoHandler : AMRpcHandler<C2G_DelShopActivityInfo, G2C_DelShopActivityInfo>
    {
        protected override async void Run(Session session, C2G_DelShopActivityInfo message, Action<G2C_DelShopActivityInfo> reply)
        {
            G2C_DelShopActivityInfo response = new G2C_DelShopActivityInfo();
            List<ShopActivityInfo> infoOrders = new List<ShopActivityInfo>();
            ShopActivityInfo InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.ContainsKey(message.ShopInfoID))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[message.ShopInfoID];
                    }
                }
                foreach (ShopActivityInfo item in infoOrders)
                {
                    if (item._ShopActivityID == message.ShopActivityID)
                    {
                        InfoData = item;
                        break;
                    }
                }

                if (InfoData != null)
                {
                    InfoData._State = 1;

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

                    response.IsSuccess = true;
                    response.Message = "店铺活动信息删除成功！";
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

    /// <summary>
    /// 商铺活动审核
    /// </summary>
    [MessageHandler(AppType.Gate)]
    public class C2G_AuditShopActivityInfoHandler : AMRpcHandler<C2G_AuditShopActivityInfo, G2C_AuditShopActivityInfo>
    {
        protected override async void Run(Session session, C2G_AuditShopActivityInfo message, Action<G2C_AuditShopActivityInfo> reply)
        {
            G2C_AuditShopActivityInfo response = new G2C_AuditShopActivityInfo();
            List<ShopActivityInfo> infoOrders = new List<ShopActivityInfo>();
            ShopActivityInfo InfoData = null;
            response.IsSuccess = false;
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();

                lock (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist)
                {
                    if (Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist.ContainsKey(message.ShopInfoID))
                    {
                        infoOrders = Game.Scene.GetComponent<ProductCenterComponent>().ShopActivitylist[message.ShopInfoID];
                    }
                }
                foreach (ShopActivityInfo item in infoOrders)
                {
                    if (item._ShopActivityID == message.ShopActivityID)
                    {
                        InfoData = item;
                        break;
                    }
                }

                if (InfoData != null)
                {
                    if (message.Type == 1)
                    {
                        InfoData._AuditState = 1;
                        InfoData._AuditMessage = message.AuditMessage;

                        response.IsSuccess = true;
                        response.Message = "审核修改成功，修改状态通过！";

                    }
                    else if (message.Type == 2)
                    {
                        InfoData._AuditState = 2;
                        InfoData._AuditMessage = message.AuditMessage;

                        response.IsSuccess = true;
                        response.Message = "审核修改成功，修改状态未通过！";
                    }

                    await dBProxyComponent.Save(InfoData);
                    dBProxyComponent.SaveLog(InfoData).Coroutine();

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
    #endregion
}