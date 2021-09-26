using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboard.Services
{
    public class TimeZoneService
    {
        private readonly IJSRuntime jSRuntime;
        private TimeSpan? _userOffset;

        public TimeZoneService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async ValueTask<DateTimeOffset> GetLocalDateTime(DateTimeOffset dateTime)
        {
            if (_userOffset == null)
            {
                int offsetInMinutes = await jSRuntime.InvokeAsync<int>("blazorGetTimezoneOffset");
                _userOffset = TimeSpan.FromMinutes(-offsetInMinutes);
            }

            return dateTime.ToOffset(_userOffset.Value);
        }
    }
}
