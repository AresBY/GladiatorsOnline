using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IBattleService
    {
        Task<Battle> ExecuteBattle(Guid firstSlaveId, Guid secondSlaveId);
    }
}
