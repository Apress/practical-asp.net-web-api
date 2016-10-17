using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robusta.TalentManager.Domain
{
    public interface IIdentifiable
    {
        int Id { get; }
    }
}
