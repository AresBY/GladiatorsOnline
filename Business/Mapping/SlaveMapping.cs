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
                PortraitID = entity.PortraitID,
                Name = entity.Name,
                Strength = entity.Strength,
                Dexterity = entity.Dexterity,
                Intuition = entity.Intuition,
                Stamina = entity.Stamina,
                Weight = entity.Weight,
                Wins = entity.Wins
            };

            switch (entity)
            {
                case PlayersSlave ps when dto is PlayersSlaveDto psDto:
                    psDto.OwnerId = ps.OwnerId;
                    psDto.Achievements = ps.Achievements
                        .Select(a => new AchievementDto
                        {
                            Id = a.Id,
                            Type = a.Type,
                            Level = a.Level,
                            // Автоматически подставляем Description из ресурсов
                            Description = AchievementTextHelper.GetTooltipText(a)
                        }).ToList();
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
                PortraitID = dto.PortraitID,
                Name = dto.Name,
                Strength = dto.Strength,
                Dexterity = dto.Dexterity,
                Intuition = dto.Intuition,
                Stamina = dto.Stamina,
                Weight = dto.Weight,
                Wins = dto.Wins
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

        public static FighterDetailDto ToDetailDto(this Fighter fighter, PlayersSlave slave)
        {
            return new FighterDetailDto
            {
                Id = slave.Id,
                PortraitID = slave.PortraitID,
                Name = slave.Name,
                Strength = slave.Strength,
                Dexterity = slave.Dexterity,
                Intuition = slave.Intuition,
                Stamina = slave.Stamina,
                Weight = slave.Weight,
                Wins = slave.Wins,

                Achievements = slave.Achievements
                    .Select(a => new AchievementDto
                    {
                        Id = a.Id,
                        Type = a.Type,
                        Level = a.Level,
                        Description = AchievementTextHelper.GetTooltipText(a)
                    })
                    .ToList(),

                Damage = fighter.Damage,
                Dodge = fighter.Dodge,
                AntiDodge = fighter.AntiDodge,
                Critical = fighter.Critical,
                CriticalPower = fighter.CriticalPower,
                AntiCritical = fighter.AntiCritical,
                HP = fighter.HP,
                HPMax = fighter.HPMax
            };
        }
    }
}