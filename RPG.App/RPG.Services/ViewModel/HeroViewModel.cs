namespace RPG.Services.ViewModel
{
    public class HeroViewModel
    {
        public int Id { get; set; }

        public int Damage { get; set; } 

        public int Helth { get; set; } 

        public int Mana { get; set; }

        public int Range { get; set; }

        public string TimeToCreate { get; set; }= null!;
    }
}
