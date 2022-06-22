using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz2.Helpers
{
    public interface IPlaneMapper
    {
        double MapLatitudeToPlaneY(double latitude);

        double MapLongitudeToPlaneX(double longitude);
    }
}
