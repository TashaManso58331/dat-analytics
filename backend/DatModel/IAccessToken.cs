using System;

namespace Dat.Model
{
    public interface IAccessToken
    {
        DateTime GetExpiry();
    }
}
