using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaperID
{
    public static class Helpers
    {
        public static int? MockDataScenario (this HttpRequest httpRequest)
        {
            var referrer = httpRequest.Headers["Referer"].ToString();
            if (referrer.Contains("mockdata=1"))
            {
                return 1;
            } else if (referrer.Contains("mockdata=2"))
            {
                return 2;
            }

            return null;
        }
    }
}
