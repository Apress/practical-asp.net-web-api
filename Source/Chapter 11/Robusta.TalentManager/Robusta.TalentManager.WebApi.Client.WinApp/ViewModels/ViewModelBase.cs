using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Robusta.TalentManager.WebApi.Client.WinApp.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion)
        {
            string propertyName = GetPropertyName(propertyExpresssion);

            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string GetPropertyName<T>(Expression<Func<T>> propertyExpresssion)
        {
            string propertyName = String.Empty;

            MemberExpression expression = propertyExpresssion.Body as MemberExpression;
            if (expression != null)
            {
                propertyName = expression.Member.Name;
            }

            return propertyName;
        }
    }
}
