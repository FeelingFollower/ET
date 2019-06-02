using System;
using System.Collections.Generic;
using System.Text;
using ETHotfix.WWPaoFu.Activity;
using ETModel;
namespace ETHotfix
{
    [ObjectSystem]
    public class ActivityExFromLifeOfAllComponentAwakeSystem : AwakeSystem<LifeOfAllMyComponent>
    {
        public override void Awake(LifeOfAllMyComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class ActivityExFromLifeOfAllComponentStartSystem : StartSystem<LifeOfAllMyComponent>
    {
        public override void Start(LifeOfAllMyComponent self)
        {
            self.Start();
        }
    }

    [ObjectSystem]
    public class ActivityExFromLifeOfAllComponentUpdateSystem : UpdateSystem<LifeOfAllMyComponent>
    {
        public override void Update(LifeOfAllMyComponent self)
        {
            self.Update();
        }
    }

    [ObjectSystem]
    public class ActivityExFromLifeOfAllComponentLateUpdateSystem : LateUpdateSystem<LifeOfAllMyComponent>
    {
        public override void LateUpdate(LifeOfAllMyComponent self)
        {
            self.LateUpdate();
        }
    }


    /// <summary>
    /// 扩展ET的对应组件功能 
    /// </summary>
    public static class ActivityExFromLifeOfAllComponent
    {
        public static void Awake(this LifeOfAllMyComponent self)
        {

            try
            {
                Log.Debug("扩展ET的对应组件功能");
                ///插入HotFix组件
                Game.Scene.AddComponent<ProductCenterComponent>();
            }
            catch (Exception e)
            {

                Log.Debug("ActivityExFromLifeOfAllComponent:Awake:"+e);
            }

          
        
        }

        public static void Start(this LifeOfAllMyComponent self)
        {

        }

        public static void Update(this LifeOfAllMyComponent self)
        {

        }

        public static void LateUpdate(this LifeOfAllMyComponent self)
        {

        }

      
    }
}
