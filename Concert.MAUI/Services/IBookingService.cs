﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concert.MAUI.Services
{
    public interface IBookingService
    {
        Task<bool> BookPerformanceAsync(string userid, string concertId);
    }
}
