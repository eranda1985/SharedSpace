using System;
using System.Collections.Generic;
using System.Text;

namespace Example.SharedSpace.Common.Features
{
    public class MediatorPageModel: FreshMvvm.FreshBasePageModel
    {
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            CoreMethods.SwitchOutRootNavigation("Welcome");
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
