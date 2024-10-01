﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAccountingServer.Domain.ValueObjects
{
    public sealed record Database(string Server, string DatabaseName, string UserId, string Password);
}