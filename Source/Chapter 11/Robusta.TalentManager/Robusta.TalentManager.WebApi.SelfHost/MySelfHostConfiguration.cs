using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;

namespace Robusta.TalentManager.WebApi.SelfHost
{
    public class MySelfHostConfiguration : HttpSelfHostConfiguration
    {
        public MySelfHostConfiguration(string baseAddress) : base(baseAddress) { }

        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Mode = HttpBindingSecurityMode.Transport;

            return base.OnConfigureBinding(httpBinding);
        }
    }
}
