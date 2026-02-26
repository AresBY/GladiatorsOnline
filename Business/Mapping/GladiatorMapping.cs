using Gladiators.Business.DTOs;
using Gladiators.Data.Entities;

namespace Gladiators.Business.Mapping
{
    public static class GladiatorMapping
    {
        public static GladiatorDto ToDto(this Gladiator entity)
        {
            return new GladiatorDto
            {
                Id = entity.Id,
                Name = entity.Name,
                PortraitID = entity.PortraitID,
                Strength = entity.Strength,
                Dexterity = entity.Dexterity,
                Intuition = entity.Intuition,
                Stamina = entity.Stamina,
                Damage = entity.Damage,
                Dodge = entity.Dodge,
                AntiDodge = entity.AntiDodge,
                Critical = entity.Critical,
                AntiCritical = entity.AntiCritical,
                HP = entity.HP,
                Wins = entity.Wins
            };
        }

        public static Gladiator ToEntity(this GladiatorDto dto)
        {
            return new Gladiator
            {
                Id = dto.Id,
                Name = dto.Name,
                PortraitID = dto.PortraitID,
                Strength = dto.Strength,
                Dexterity = dto.Dexterity,
                Intuition = dto.Intuition,
                Stamina = dto.Stamina,
                Damage = dto.Damage,
                Dodge = dto.Dodge,
                AntiDodge = dto.AntiDodge,
                Critical = dto.Critical,
                AntiCritical = dto.AntiCritical,
                HP = dto.HP,
                Wins = dto.Wins
            };
        }

        public static void UpdateFromDto(this Gladiator entity, GladiatorDto dto)
        {
            entity.Name = dto.Name;
            entity.PortraitID = dto.PortraitID;
            entity.Strength = dto.Strength;
            entity.Dexterity = dto.Dexterity;
            entity.Intuition = dto.Intuition;
            entity.Stamina = dto.Stamina;
            entity.Damage = dto.Damage;
            entity.Dodge = dto.Dodge;
            entity.AntiDodge = dto.AntiDodge;
            entity.Critical = dto.Critical;
            entity.AntiCritical = dto.AntiCritical;
            entity.HP = dto.HP;
            entity.Wins = dto.Wins;
        }
    }
}
