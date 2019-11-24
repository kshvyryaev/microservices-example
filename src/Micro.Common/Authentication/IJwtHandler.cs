using System;

namespace Micro.Common.Authentication
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}
