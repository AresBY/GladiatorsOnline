using Gladiators.Business.DTOs;
using Gladiators.Data.Entities;

namespace Gladiators.Business.Mapping
{
    public static class GladiatorMapping
    {
        public static GladiatorDto ToDto(this Fighter entity)
        {
            return new GladiatorDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Damage = entity.Damage,
                CriticalPower = entity.CriticalPower,
                Dodge = entity.Dodge,
                AntiDodge = entity.AntiDodge,
                Critical = entity.Critical,
                AntiCritical = entity.AntiCritical,
                HP = entity.HP,
                Wins = entity.Wins
            };
        }

        public static Fighter ToEntity(this GladiatorDto dto)
        {
            return new Fighter
            {
                Id = dto.Id,
                Name = dto.Name,
                Damage = dto.Damage,
                CriticalPower = dto.CriticalPower,
                Dodge = dto.Dodge,
                AntiDodge = dto.AntiDodge,
                Critical = dto.Critical,
                AntiCritical = dto.AntiCritical,
                HP = dto.HP,
                Wins = dto.Wins
            };
        }

        public static void UpdateFromDto(this Fighter entity, GladiatorDto dto)
        {
            entity.Name = dto.Name;
            entity.Damage = dto.Damage;
            entity.Dodge = dto.Dodge;
            entity.AntiDodge = dto.AntiDodge;
            entity.Critical = dto.Critical;
            entity.CriticalPower = dto.CriticalPower;
            entity.AntiCritical = dto.AntiCritical;
            entity.HP = dto.HP;
            entity.Wins = dto.Wins;
        }
    }
}
