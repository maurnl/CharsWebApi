﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services.Abstractions
{
    public interface IUserService
    {
        string GetCurrentUserGuid();
    }
}
