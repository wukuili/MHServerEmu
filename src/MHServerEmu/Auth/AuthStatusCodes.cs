﻿namespace MHServerEmu.Auth
{
    public enum AuthStatusCode
    {
        Success = 200,
        IncorrectUsernameOrPassword401 = 401,
        AccountBanned = 402,
        IncorrectUsernameOrPassword403 = 403,
        CouldNotReachAuthServer = 404,
        EmailNotVerified = 405,
        UnableToConnect406 = 406,
        NeedToAcceptLegal = 407,
        PatchRequired = 409,
        AccountArchived = 411,
        PasswordExpired = 412,
        UnableToConnect413 = 413,
        UnableToConnect414 = 414,
        UnableToConnect415 = 415,
        UnableToConnect416 = 416,
        AgeRestricted = 417,
        UnableToConnect418 = 418,
        TemporarilyUnavailable = 503
    }
}
