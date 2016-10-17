using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using RequestBinding.Models;

namespace RequestBinding
{
    public class TalentScoutModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext context)
        {
            var scoutCriteria = (TalentScout)context.Model ?? new TalentScout();

            var result = context.ValueProvider.GetValue("dept");
            if (result != null)
                scoutCriteria.Departments = result.AttemptedValue.Split(',').Select(d => d.Trim()).ToList();

            result = context.ValueProvider.GetValue("xctcbased");
            if (result != null)
            {
                int basedOn;
                if (Int32.TryParse(result.AttemptedValue, out basedOn))
                {
                    scoutCriteria.IsCtcBased = (basedOn > 0);
                }
            }

            result = context.ValueProvider.GetValue("doj");
            if (result != null)
            {
                DateTime doj;
                if (DateTime.TryParse(result.AttemptedValue, out doj))
                {
                    scoutCriteria.Doj = doj;
                }
            }

            context.Model = scoutCriteria;

            return true;
        }
    }
}