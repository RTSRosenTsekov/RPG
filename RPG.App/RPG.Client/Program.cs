namespace RPG.Client
{
    using RPG.Data;
    using RPG.Services;

    public class Program
    {
        static void Main(string[] args)
        {
            

            Utility.InitDb();

            PlayGame.Game();
        }
    }
}
