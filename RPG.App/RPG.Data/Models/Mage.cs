namespace RPG.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Mage
    {
        public Mage()
        {
                
        }

        public Mage(int strenght, int agility, int intelligence)
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

        public int Strenght { get; set; } = 2;


        public int Agility { get; set; } = 1;


        public int Intelligence { get; set; } = 3;


        public int Range { get; set; } = 3;


        public void Setup()
        {

            this.Helth = this.Strenght * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;

        }


    }
}
