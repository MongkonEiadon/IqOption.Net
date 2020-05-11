﻿using System.Collections.Generic;
using System.Linq;
using IqOptionApi.Models;

namespace IqOptionApi.extensions
{
    public static class InfoDataExtensions
    {
        public static IEnumerable<InfoData> OnlyOpenedInfoData(this IEnumerable<InfoData> This)
        {
            return This.Where(x => x.Win.Equals("equal"));
        }
    }
}