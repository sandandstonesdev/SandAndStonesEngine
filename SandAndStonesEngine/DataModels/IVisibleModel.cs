using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.DataModels
{
    internal interface IVisibleModel
    {
        bool IsVisible(ScrollableViewport scrollableViewport);
    }
}
