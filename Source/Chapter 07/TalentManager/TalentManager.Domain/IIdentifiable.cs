using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManager.Domain
{
    public interface IIdentifiable
    {
        int Id { get; }
    }
}
