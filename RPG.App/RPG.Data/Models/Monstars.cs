namespace RPG.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Monstars
    {

        public Monstars(int strenght, int agility, int intelligence)
        {
            this.Strenght = strenght;
            this.Agility = agility;
            this.Intelligence = intelligence;
        }

        [Key]
        public int Id { get; set; }

        public int Helth { get; set; }

        public int Mana { get; set; }

        public int Damage { get; set; }

        public int Strenght { get; set; }

        public int Agility { get; set; }

        public int Intelligence { get; set; }

        public int Range { get; set; } = 1;


        public void Setup()
        {

            this.Helth = this.Strenght * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;

        }
    }
}
