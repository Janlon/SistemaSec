using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sec
{
    public enum DbAction
    {
        Insert,
        Update,
        Remove,
        Reactivate,
        Deactivate,
        List,
        Filter,
        FirstOrDefault
    }

}
