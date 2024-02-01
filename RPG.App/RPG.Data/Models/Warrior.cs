namespace RPG.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Warrior
    {
        public Warrior()
        {
                
        }
        public Warrior(int strenght, int agility, int intelligence)
        {
            this.Strenght += strenght;
            this.Agility += agility;
            this.Intelligence += intelligence;

        }

        [Key]
        public int Id { get; set; }

        public int Helth { get; set; }

        public int Mana { get; set; }

        public int Damage { get; set; }

        public int Strenght { get; set; } = 3;

        public int Agility { get; set; } = 3;

        public int Intelligence { get; set; } = 0;

        public int Range { get; set; } = 1;

        public void Setup()
        {

            this.Helth = this.Strenght * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;

        }

    }
}
