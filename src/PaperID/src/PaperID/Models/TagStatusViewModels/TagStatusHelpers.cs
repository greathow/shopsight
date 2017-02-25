using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaperID.Models.TagStatusViewModels
{
    public static class TagStatusHelpers
    {
        public static TagLocation ToTagLocation(long value)
        {
            switch (value)
            {
                case 1:
                    return TagLocation.OnShelf;
                case 2:
                    return TagLocation.InFittingRoom;
                default:
                    return TagLocation.Unknown;
            }
        }

        public static TagInterest ToTagInterest(long value)
        {
            switch (value)
            {
                case 1:
                    return TagInterest.Browse;
                case 2:
                    return TagInterest.Interested;
                default:
                    return TagInterest.None;
            }
        }

        public static TagProximity ToTagProximity(long value)
        {
            switch (value)
            {
                case 1:
                    return TagProximity.NearShopper;
                case 2:
                    return TagProximity.NotNearShopper;
                default:
                    return TagProximity.Unknown;
            }
        }
    }
}
