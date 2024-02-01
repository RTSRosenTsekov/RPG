namespace RPG.Services
{
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using static RPG.Services.Common.GeneralConstants;
    public class PlayGame
    {

        public static void Game()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine(lines);
            Console.WriteLine("Welcom");
            Console.WriteLine("Press any key to play.");

            var input = Console.ReadLine();
            if (input != null)
            {
                CharacterSelect.Chois();
               
            }
        }

    }
}
