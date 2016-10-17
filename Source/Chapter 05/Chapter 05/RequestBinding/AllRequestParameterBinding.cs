using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using RequestBinding.Models;

namespace RequestBinding
{
    public class AllRequestParameterBinding : HttpParameterBinding
    {
        private HttpParameterBinding modelBinding = null;
        private HttpParameterBinding formatterBinding = null;

        public AllRequestParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {
            // GetBinding returns ModelBinderParameterBinding
            modelBinding = new ModelBinderAttribute().GetBinding(descriptor);

            // GetBinding returns FormatterParameterBinding
            formatterBinding = new FromBodyAttribute().GetBinding(descriptor);
        }

        public override async Task ExecuteBindingAsync(
                                                     ModelMetadataProvider metadataProvider,
                                                                HttpActionContext context,
                                                                         CancellationToken cancellationToken)
        {

            // Perform formatter binding
            await formatterBinding.ExecuteBindingAsync(metadataProvider, context, cancellationToken);

            // and store the resulting model
            var employee = GetValue(context) as Employee;

            // Perform model binding
            await modelBinding.ExecuteBindingAsync(metadataProvider, context, cancellationToken);

            // and store the resulting model
            var employeeFromUri = GetValue(context) as Employee;

            // Apply the delta on top of the employee object resulting from formatter binding
            employee = Merge(employee, employeeFromUri);

            // Set the merged model in the context
            SetValue(context, employee);
        }

        private Employee Merge(Employee @base, Employee @new)
        {
            Type employeeType = typeof(Employee);

            foreach (var property in employeeType.GetProperties(
                                            BindingFlags.Instance | BindingFlags.Public))
            {
                object baseValue = property.GetValue(@base, null);
                object newValue = property.GetValue(@new, null);

                object defaultValue = property.PropertyType.IsValueType ?
                                            Activator.CreateInstance(property.PropertyType) :
                                                null;

                if (baseValue == null || baseValue.Equals(defaultValue))
                    property.SetValue(@base, newValue);
            }

            return @base;
        }

    }

}