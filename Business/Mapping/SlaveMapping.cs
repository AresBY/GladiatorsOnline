using Gladiators.Business.DTOs;
using Gladiators.Data.Entities;

namespace Gladiators.Business.Mapping
{
    public static class SlaveMapping
    {
        // Entity → DTO
        public static TDto ToDto<TDto>(this BaseSlave entity)
        where TDto : BaseSlaveDto, new()
        {
            var dto = new TDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Strength = entity.Strength,
                Dexterity = entity.Dexterity,
                Intuition = entity.Intuition,
                Stamina = entity.Stamina

            };

            switch (entity)
            {
                case PlayersSlave ps when dto is PlayersSlaveDto psDto:
                    psDto.OwnerId = ps.OwnerId;
                    break;
                case MarketSlave ms when dto is MarketSlaveDto msDto:
                    msDto.PlayerId = ms.PlayerId;
                    msDto.Price = ms.Price;
                    break;
            }

            return dto;
        }

        // DTO → Entity
        public static TEntity ToEntity<TEntity>(this BaseSlaveDto dto)
        where TEntity : BaseSlave, new()
        {
            var entity = new TEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                Strength = dto.Strength,
                Dexterity = dto.Dexterity,
                Intuition = dto.Intuition,
                Stamina = dto.Stamina
            };

            switch (entity)
            {
                case PlayersSlave ps when dto is PlayersSlaveDto psDto:
                    ps.OwnerId = psDto.OwnerId;
                    break;
                case MarketSlave ms when dto is MarketSlaveDto msDto:
                    ms.PlayerId = msDto.PlayerId;
                    ms.Price = msDto.Price;
                    break;
            }

            return entity;
        }
    }
}