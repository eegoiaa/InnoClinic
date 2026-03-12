using InnoClinic.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Auth.Domain.Exceptions;

public class IdentityException : BaseBusinessException
{
    public override int StatusCode => 400;

    public IdentityException(IEnumerable<IdentityError> errors)
        : base(string.Join(";", errors.Select(e => e.Description)))
    {   
    }

    public IdentityException(string message)
        :base(message)
    {
    }
}
