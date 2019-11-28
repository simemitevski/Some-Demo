using SecurityDemo.Domain.Entities;
using System;

namespace SecurityDemo.Domain.Repositories.Interfaces
{
    public interface IUserActivationRepository
    {
        UserActivation GetByActivationCode(Guid code);
    }
}
