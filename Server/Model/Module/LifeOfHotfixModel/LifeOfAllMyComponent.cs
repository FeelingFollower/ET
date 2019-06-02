using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    //所有HotFix扩展组件的核心组件
     public class LifeOfAllMyComponent:Component
    {

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();
        }
    }
}
