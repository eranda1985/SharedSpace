using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Example.SharedSpace.Common.Features
{
    public class WelcomePageModel: FreshMvvm.FreshBasePageModel
    {
        public Command MoveOnCommand
        {
            get => new Command(async ()=> 
            {
                await CoreMethods.PushPageModel<MainPageModel>(true);
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

    }
}
