using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Robusta.TalentManager.WebApi.Core.Filters
{
    public class AuthorizeByTimeSlot : AuthorizeAttribute
    {
        public int SlotStartHour { get; set; }
        public int SlotEndHour { get; set; }

        protected override bool IsAuthorized(HttpActionContext context)
        {
            if (DateTime.Now.Hour >= this.SlotStartHour &&
                        DateTime.Now.Hour <= this.SlotEndHour &&
                            base.IsAuthorized(context))
                return true;

            return false;
        }
    }
}
