using System;
using System.Collections.Generic;
using System.Text;


namespace JKTech.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(Guid userId);
    }
}
