namespace RPG.Services
{
    using RPG.Data;
    using RPG.Data.Models;
    using RPG.Services.ViewModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using static RPG.Services.Common.GeneralConstants;

    public static class CharacterSelect
    {
        public static void Chois()
        {

            Console.Clear();
            Console.WriteLine("CharacterSelect\n");
            Console.WriteLine(lines);

            string[] options = new string[] { "Warrior", "Archer", "Mage" };

            Console.WriteLine("Options:\n");
            for (int i = 1; i <= options.Length; i++)
            {
                Console.WriteLine($"{i}) {options[i - 1]}\n");
            }

            Console.Write("Your pick: ");
            Stopwatch sw;
            sw = Stopwatch.StartNew();
            // to do if enter string return 0 and fell error 
            int heroInput = InputTryParsToInt(1, "Enter number 1 to 3");

            string hero = options[heroInput - 1];
            Console.WriteLine();

            Console.WriteLine("Would you like to buff up your stats before starting?" + new string(' ', 22) + "(Limit: 3 points total)");
            Console.Write("Response (Y\\N): ");
            string input = Console.ReadLine()!;
            int strenght = 0;
            int agility = 0;
            int intelligence = 0;

            while (input.ToLower() != "y" && input.ToLower() != "n")
            {
                Console.Write("Response (Y\\N): ");
                input = Console.ReadLine()!;
            }

            int point = 3;

            if (input.ToLower() == "n")
            {
                Console.Clear();

            }
            else
            {
                Console.WriteLine($"Remaining Points:{point}");
                Console.Write("Add to Strenght: ");
                int inputBuffUpStrenght;

                inputBuffUpStrenght = InputTryParsToInt(3, "Enter number 0 to 3 ");
                Console.WriteLine();
                int inputBuffUpAgility;
                int inputBuffUpIntelligence;

                switch (inputBuffUpStrenght)
                {
                    case 3:
                        point -= 3;
                        Console.WriteLine($"Remaining Points:{point}");
                        strenght += inputBuffUpStrenght;
                        break;
                    case 2:
                        point -= 2;
                        strenght += inputBuffUpStrenght;
                        Console.WriteLine($"Remaining Points:{point}");

                        Console.Write("Add to Agility: ");

                        int numAgility = InputTryParsToInt(1, "Enter number 0 to 1 ");

                        if (numAgility == 1)
                        {
                            point -= numAgility;
                            agility = numAgility;
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            Console.Write("Add to Intelligence: ");
                            inputBuffUpIntelligence = InputTryParsToInt(1, "Enter number 0 to 1 ");

                            point -= inputBuffUpIntelligence;
                            intelligence = inputBuffUpIntelligence;
                            Console.WriteLine();

                        }
                        break;

                    case 1:
                        point -= 1;
                        Console.WriteLine($"Remaining Points:{point}");
                        strenght += inputBuffUpStrenght;
                        Console.WriteLine();

                        Console.Write("Add to Agility: ");

                        int parsedBuffUpAgility = InputTryParsToInt(2, "Enter number 0 to 2");
                        if (parsedBuffUpAgility == 2)
                        {
                            point -= parsedBuffUpAgility;
                            agility += parsedBuffUpAgility;
                            break;
                        }
                        else if (parsedBuffUpAgility == 1)
                        {
                            point -= parsedBuffUpAgility;
                            agility += parsedBuffUpAgility;
                            Console.WriteLine();
                            Console.Write("Add to Intelligence: ");
                            int parsedBuffUpIntelligence = InputTryParsToInt(1, "Enter number 0 to 1");
                            point -= parsedBuffUpAgility;
                            intelligence += parsedBuffUpIntelligence;
                            Console.WriteLine();
                        }
                        break;
                    case 0:
                        Console.WriteLine($"Remaining Points:{point}");
                        strenght += inputBuffUpStrenght;
                        Console.WriteLine();

                        Console.Write("Add to Agility: ");
                        inputBuffUpAgility = InputTryParsToInt(3, "Enter number 0 to 3 ");
                        switch (inputBuffUpAgility)
                        {
                            case 3:
                                point -= inputBuffUpAgility;
                                agility += inputBuffUpAgility;
                                break;
                            case 2:
                                point -= inputBuffUpAgility;
                                agility += inputBuffUpAgility;
                                Console.WriteLine();

                                inputBuffUpIntelligence = InputTryParsToInt(1, "Enter number 0 to 1 ");
                                point -= inputBuffUpIntelligence;
                                intelligence += inputBuffUpIntelligence;
                                break;
                            case 1:
                                point -= inputBuffUpAgility;
                                agility += inputBuffUpAgility;

                                Console.WriteLine();

                                inputBuffUpIntelligence = InputTryParsToInt(2, "Enter number 0 to 1 ");
                                point -= inputBuffUpIntelligence;
                                intelligence += inputBuffUpIntelligence;
                                break;

                            case 0:
                                point -= inputBuffUpAgility;
                                Console.WriteLine($"Remaining Points:{point}");

                                Console.WriteLine("Add to Intelligence: ");
                                inputBuffUpIntelligence = InputTryParsToInt(3, "Enter number 0 to 3 ");
                                point -= inputBuffUpIntelligence;
                                intelligence += inputBuffUpIntelligence;
                                break;
                        }

                        break;
                }

                sw.Stop();
                var resultHero=SaveHero(hero, strenght, agility, intelligence, sw);

                InGame.Play(int.Parse(resultHero[0]),resultHero[1],int.Parse(resultHero[2]));


            }

        }

        private static int InputTryParsToInt(int comparisonNum, string errMessage)
        {
            int inputNum;
            while (int.TryParse(Console.ReadLine(), out inputNum) && inputNum > comparisonNum)
            {
                Console.WriteLine(errMessage);
            }

            return inputNum;
        }

        private static List<string> SaveHero(string heroName, int strenght, int agility, int intelligence,Stopwatch sw)
        {
            List<string> result = new List<string>();
            using (var context = new RpgDBContext())
            {
                switch (heroName)
                {
                    case "Warrior":
                        var newHeroWarrior = new Warrior(strenght, agility, intelligence);
                        newHeroWarrior.Setup();
                        context.Warriors.Add(newHeroWarrior);
                        context.SaveChanges();

                        ShowHero("Warrior", newHeroWarrior.Id, sw);
                        result.AddRange(new List<string> { $"{newHeroWarrior.Id}", "Warrior", $"{newHeroWarrior.Range}" });
                        break;

                    case "Mage":
                        var newHeroMage = new Mage(strenght, agility, intelligence);
                        context.Mages.Add(newHeroMage);
                        newHeroMage.Setup();
                        context.SaveChanges();
                        ShowHero("Mage", newHeroMage.Id, sw);
                        result.AddRange(new List<string>{ $"{newHeroMage.Id}", "Mage", $"{newHeroMage.Range}" });
                        break;
                    case "Archer":
                        var newHeroArcher = new Archer(strenght, agility, intelligence);
                        newHeroArcher.Setup();
                        context.Archers.Add(newHeroArcher);
                        context.SaveChanges();
                        ShowHero("Archer", newHeroArcher.Id, sw);
                        result.AddRange(new List<string>{ $"{newHeroArcher.Id}", "Archer", $"{newHeroArcher.Range}" });
                        break;
                       

                }

            }
            return result;
        }

        private static void ShowHero(string hero, int id, Stopwatch sw)
        {
            using (var context = new RpgDBContext())
            {
                switch (hero)
                {
                    case "Warrior":
                        var resultHero = context.Warriors.Where(c => c.Id == id).FirstOrDefault();
                        var heroViewModel = new HeroViewModel()
                        {
                            Id = resultHero!.Id,
                            Damage = resultHero.Damage,
                            Helth = resultHero.Helth,
                            Mana = resultHero.Mana,
                            Range = resultHero.Range,
                            TimeToCreate = sw.ToString()!
                        };

                        showStrBuilder("Warrior", heroViewModel.Damage, heroViewModel.Helth, heroViewModel.Mana);
                        break;
                    case "Mage":
                        var resulMage = context.Mages.Where(c => c.Id == id).FirstOrDefault();
                        var mageViewModel = new HeroViewModel()
                        {
                            Id = resulMage!.Id,
                            Damage = resulMage.Damage,
                            Helth = resulMage.Helth,
                            Mana = resulMage.Mana,
                            Range = resulMage.Range,
                            TimeToCreate = sw.ToString()!
                        };

                        showStrBuilder("Mage", mageViewModel.Damage, mageViewModel.Helth, mageViewModel.Mana);
                        break;

                    case "Archer":
                        var resulArcher = context.Archers.FirstOrDefault(c => c.Id == id);
                        var archerViewModel = new HeroViewModel()
                        {
                            Id = resulArcher!.Id,
                            Damage = resulArcher.Damage,
                            Helth = resulArcher.Helth,
                            Mana = resulArcher.Mana,
                            Range = resulArcher.Range,
                            TimeToCreate = sw.ToString()!
                        };

                        showStrBuilder("Archer", archerViewModel.Damage, archerViewModel.Helth, archerViewModel.Mana);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void showStrBuilder(string name, int damage, int helth, int mana)
        {
            var str = new StringBuilder();
            str.AppendLine($"Hero:{name} ");
            str.AppendLine($"Damage: {damage}");
            str.AppendLine($"Health: {helth}");
            str.AppendLine($"Mana: {mana}");
            Console.WriteLine(str);
        }
    }
}
