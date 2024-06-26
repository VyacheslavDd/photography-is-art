﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Exceptions.Enums
{
    public enum ExceptionType
    {
        ArgumentNull,
        UserAlreadyExists,
        IncorrectPassword,
        DefaultRolesOverflow,
        RoleAlreadyExists,
        InvalidToken,
        TokenExpired,
        UserDoesNotExist,
        IncorrentArgument,
        NoConnectionsAvailable
    }
}
