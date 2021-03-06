﻿using System;

namespace PixBlocks_Addition.Infrastructure.Extentions
{
    public static class Extentions
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = dateTime.Ticks - epoch.Ticks;

            return time / TimeSpan.TicksPerSecond;
        }
    }
}