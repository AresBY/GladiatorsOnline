namespace Gladiators.Data.Entities
{
    public class BaseSlave
    {
        public Guid Id { get; set; }                 // Уникальный идентификатор
        public string Name { get; set; } = null!;   // Имя раба
        public int Strength { get; set; }           // Сила
        public int Dexterity { get; set; }          // Ловкость
        public int Stamina { get; set; }            // Выносливость
        public int Price { get; set; }              // Цена в игровой валюте
    }
}
