using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Common
{
    public class BaseView<TModel>: System.Web.Mvc.WebViewPage<TModel>
    {
        public UserIdentity Identity { get; private set; }
        public BaseView()
        {
            Identity = Thread.CurrentPrincipal.Identity as UserIdentity;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}