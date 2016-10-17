using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace RequestBinding
{
    public class HeaderValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            var request = actionContext.ControllerContext.Request;

            return new HeaderValueProvider(request.Headers);
        }
    }
}