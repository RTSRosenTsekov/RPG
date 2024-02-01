namespace RPG.Services
{
    using RPG.Data;
    using RPG.Data.Models;
    using System.Text;

    public class InGame
    {
        public static void Play( int heroId , string heroType , int heroRange)
        {
            Console.Clear();
            
            int rows = 10;
            int cols = 10;
            string matrixSymbol = "0";
            string[][] matrix = new string[rows][];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                matrix[rowIndex] = new string[cols];

                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    matrix[rowIndex][colIndex] = matrixSymbol;

                }

            }

            string MageSymbol = "*";
            string WarrSymbol = "@";
            string ArcherSymbol = "#";
            string MonsterSymbol = "1";  //◙

            matrix[1][1] = WarrSymbol;
            int heroCordinateRow = 1;
            int heroCordinateCol = 1;
            for (int rowIndex = 0; rowIndex < matrix.Length; rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix[rowIndex].Length; colIndex++)
                {
                    Console.Write(matrix[rowIndex][colIndex]);
                }
                Console.WriteLine();

                if (rowIndex == 9)
                {
                    Console.WriteLine("Choose action");
                    Console.WriteLine("1) Attack");
                    Console.WriteLine("2) Move\n");

                    Dictionary<int, List<int>> monstarsIdAndCordinate = new Dictionary<int, List<int>>();

                    string chois = Console.ReadLine().ToLower();
                    while (chois != "exit")
                    {


                        int choisAction = InputTryParsToInt(chois, "Numbur must be between 1 or 2 ");


                        if (choisAction == 2)
                        {
                            string inputMove = Console.ReadLine().ToLower();
                            string move = CheckInputMove(inputMove);
                            List<int> newCordinate = new List<int>();
                            switch (move)
                            {
                                case "w":
                                    newCordinate = MoveUp(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                case "s":
                                    newCordinate = MoveDown(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                case "d":
                                    newCordinate = MoveRight(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                case "a":
                                    newCordinate = MoveLeft(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                case "e":
                                    newCordinate = MoveDiagonallyUpAndRight(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                case "x":
                                    newCordinate = MoveDiagonallyDownAndRight(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                case "q":
                                    newCordinate = MoveDiagonallyUpAndLeft(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                case "z":
                                    newCordinate = MoveDiagonallyDownAndLeft(heroRange, matrix, heroCordinateRow, heroCordinateCol, matrixSymbol, monstarsIdAndCordinate, heroId, heroType);
                                    heroCordinateRow = newCordinate[0];
                                    heroCordinateCol = newCordinate[1];
                                    monstarsIdAndCordinate.Add(newCordinate[2], new List<int> { newCordinate[3], newCordinate[4] });

                                    break;
                                default:
                                    break;
                            }


                        }
                        else if (choisAction == 1)
                        {
                            AttackHero(heroId, heroType, monstarsIdAndCordinate, heroCordinateRow, heroCordinateCol);

                        }

                        Console.WriteLine("Choose action");
                        Console.WriteLine("1) Attack");
                        Console.WriteLine("2) Move\n");
                        chois = Console.ReadLine();
                    }

                }
            }
        }
        private static void AttackHero(int heroId, string heroType, Dictionary<int, List<int>> monstarsIdAndCordinate, int heroCordinateRow, int heroCordinateCol)
        {
            using (var context = new RpgDBContext())
            {
                switch (heroType)
                {
                    case "Warrior":
                        var heroW = context.Warriors.Where(c => c.Id == heroId).FirstOrDefault();
                        var heroDamag = heroW.Damage;
                        var heroRange = heroW.Range;
                        CheckWhereIsMonstarAndAttack(monstarsIdAndCordinate, heroCordinateRow, heroCordinateCol, heroDamag, heroRange, heroId);
                        break;
                    case "Mage":
                        var heroM = context.Mages.Where(c => c.Id == heroId).FirstOrDefault();
                        var heroMDamage = heroM.Damage;
                        var heroMRange = heroM.Range;
                        CheckWhereIsMonstarAndAttack(monstarsIdAndCordinate, heroCordinateRow, heroCordinateCol, heroMDamage, heroMRange, heroId);
                        break;
                    case "Archer":
                        var heroA = context.Archers.Where(c => c.Id == heroId).FirstOrDefault();
                        var heroADamage = heroA.Damage;
                        var heroADRange = heroA.Range;
                        CheckWhereIsMonstarAndAttack(monstarsIdAndCordinate, heroCordinateRow, heroCordinateCol, heroADamage, heroADRange, heroId);
                        break;
                    default:
                        break;
                }

            }
        }

        private static void CheckWhereIsMonstarAndAttack(Dictionary<int, List<int>> monstarsIdAndCordinate, int heroCordinateRow, int heroCordinateCol, int heroDamag, int heroRange, int heroId)
        {
            int countMonster = 0;
            List<int> listMonsterId = new List<int>();
            foreach (var kvp in monstarsIdAndCordinate)
            {
                int monstarId = kvp.Key;
                List<int> cordinaties = kvp.Value;
                int monstarCordinateRow = cordinaties[0];
                int monstarCordinateCol = cordinaties[1];

                // check up
                if (heroCordinateRow - heroRange == monstarCordinateRow & heroCordinateCol == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);
                }
                // check up+right 
                if (heroCordinateRow - heroRange == monstarCordinateRow & heroCordinateCol + heroRange == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);

                }
                // up+left
                if (heroCordinateRow - heroRange == monstarCordinateRow & heroCordinateCol - heroRange == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);

                }
                //right
                if (heroCordinateRow == monstarCordinateRow & heroCordinateCol + heroRange == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);

                }
                // left
                if (heroCordinateRow == monstarCordinateRow & heroCordinateCol - heroRange == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);

                }
                //down
                if (heroCordinateRow + heroRange == monstarCordinateRow & heroCordinateCol == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);

                }
                //down+right
                if (heroCordinateRow + heroRange == monstarCordinateRow & heroCordinateCol + heroRange == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);

                }
                //down+ left
                if (heroCordinateRow + heroRange == monstarCordinateRow & heroCordinateCol - heroRange == monstarCordinateCol)
                {
                    countMonster++;
                    listMonsterId.Add(kvp.Key);

                }

            }

            if (countMonster > 1)
            {
                Console.WriteLine($"Target with remaining blood {countMonster}. Which one to attack:");
            }
            else
            {
                using (var context = new RpgDBContext())
                {
                    var monstar = context.Monstars.Where(c => c.Id == listMonsterId[0]).FirstOrDefault();
                    monstar.Helth -= heroDamag;

                    context.SaveChanges();
                }

                Console.WriteLine($"Monstar helth is less with {heroDamag}");
            }
        }
        private static int InputTryParsToInt(string input, string errMessage)
        {
            int inputNum;

            while (!int.TryParse(input, out inputNum) && inputNum > 2 || inputNum < 1)
            {
                Console.WriteLine(errMessage);
                input = Console.ReadLine();
            }

            return inputNum;
        }

        private static string CheckInputMove(string move)
        {
            while (move != "w" & move != "s" & move != "d" & move != "a" & move != "e" & move != "x" & move != "q" & move != "z")
            {

                move = Console.ReadLine().ToLower();
            }

            return move;
        }

        private static List<int> MoveUp(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];
            if (rowIndex == 0 && rowIndex - range < 0)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }

            int newRowIndex = rowIndex - range;
            matrix[newRowIndex][colIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(newRowIndex);
            newCordinate.Add(colIndex);
            matrix = MoveMonsters(matrix, dictMonster, newRowIndex, colIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";
            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);
            PrintMatrix(matrix);

            return newCordinate;


        }

        private static List<int> MoveDown(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];

            if (rowIndex == 9 && rowIndex + range > 9)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }

            int newRowIndex = rowIndex + range;
            matrix[newRowIndex][colIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(newRowIndex);
            newCordinate.Add(colIndex);
            matrix = MoveMonsters(matrix, dictMonster, newRowIndex, colIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";
            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);

            PrintMatrix(matrix);
            return newCordinate;

        }

        private static List<int> MoveRight(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];
            if (colIndex == 9 && colIndex + range > 9)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }

            int newColIndex = colIndex + range;
            matrix[rowIndex][newColIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(rowIndex);
            newCordinate.Add(newColIndex);
            matrix = MoveMonsters(matrix, dictMonster, rowIndex, newColIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";

            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);

            PrintMatrix(matrix);
            return newCordinate;
        }

        private static List<int> MoveLeft(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];
            if (colIndex == 0 && colIndex - range < 0)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }

            int newColIndex = colIndex - range;
            matrix[rowIndex][newColIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(rowIndex);
            newCordinate.Add(newColIndex);
            matrix = MoveMonsters(matrix, dictMonster, rowIndex, newColIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";

            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);

            PrintMatrix(matrix);
            return newCordinate;


        }

        private static List<int> MoveDiagonallyUpAndRight(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];
            if (rowIndex == 0 && rowIndex - range < 0 && colIndex == 9 && colIndex + range > 9)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }

            int newRowIndex = rowIndex - range;
            int newColIndex = colIndex + range;
            matrix[newRowIndex][newColIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(newRowIndex);
            newCordinate.Add(newColIndex);
            matrix = MoveMonsters(matrix, dictMonster, newRowIndex, newColIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";

            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);

            PrintMatrix(matrix);

            return newCordinate;
        }

        private static List<int> MoveDiagonallyDownAndRight(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];
            if (rowIndex == 9 && rowIndex + range > 9 && colIndex == 9 && colIndex + range > 9)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }
            int newRowIndex = rowIndex + range;
            int newColIndex = colIndex + range;
            matrix[newRowIndex][newColIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(newRowIndex);
            newCordinate.Add(newColIndex);
            matrix = MoveMonsters(matrix, dictMonster, newRowIndex, newColIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";

            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);

            PrintMatrix(matrix);

            return newCordinate;
        }

        private static List<int> MoveDiagonallyUpAndLeft(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];
            if (rowIndex == 0 && rowIndex - range < 0 && colIndex == 0 && colIndex - range < 0)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }

            int newRowIndex = rowIndex - range;
            int newColIndex = colIndex - range;
            matrix[newRowIndex][newColIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(newRowIndex);
            newCordinate.Add(newColIndex);
            matrix = MoveMonsters(matrix, dictMonster, newRowIndex, newColIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";

            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);

            PrintMatrix(matrix);
            return newCordinate;
        }

        private static List<int> MoveDiagonallyDownAndLeft(int range, string[][] matrix, int rowIndex, int colIndex, string matrixSymbol, Dictionary<int, List<int>> dictMonster, int heroId, string heroType)
        {
            List<int> newCordinate = new List<int>();
            string heroSymbol = matrix[rowIndex][colIndex];
            if (rowIndex == 9 && rowIndex + range > 9 && colIndex == 0 && colIndex - range < 0)
            {
                newCordinate.Add(rowIndex);
                newCordinate.Add(colIndex);
                Console.WriteLine("You dont muvo more for this way");
                PrintMatrix(matrix);
                return newCordinate;
            }
            int newRowIndex = rowIndex + range;
            int newColIndex = colIndex - range;
            matrix[newRowIndex][newColIndex] = heroSymbol;
            matrix[rowIndex][colIndex] = matrixSymbol;
            newCordinate.Add(newRowIndex);
            newCordinate.Add(newColIndex);
            matrix = MoveMonsters(matrix, dictMonster, newRowIndex, newColIndex, heroId, heroType);

            var monstarCordinaties = CreateMonster();
            matrix[monstarCordinaties[1]][monstarCordinaties[2]] = "1";

            // Id and cordinates of new monstar
            newCordinate.Add(monstarCordinaties[0]);
            newCordinate.Add(monstarCordinaties[1]);
            newCordinate.Add(monstarCordinaties[2]);

            PrintMatrix(matrix);
            return newCordinate;
        }

        private static string[][] MoveMonsters(string[][] matrix, Dictionary<int, List<int>> dictMonster, int RowIndex, int colIndex, int heroId, string heroType)
        {
            int monstarRange = 1;

            foreach (var kvp in dictMonster)
            {
                int monstarId = kvp.Key;
                List<int> cordinatiMons = kvp.Value;
                int cordRowMons = cordinatiMons[0];
                int cordColMons = cordinatiMons[1];

                if (cordRowMons < RowIndex)
                {
                    cordRowMons += monstarRange;

                }
                if (cordRowMons > RowIndex)
                {
                    cordRowMons -= monstarRange;
                }
                if (cordColMons < colIndex)
                {
                    cordColMons += monstarRange;
                }
                if (cordColMons > colIndex)
                {
                    cordColMons -= monstarRange;
                }
                if (cordRowMons == RowIndex - 1 || cordRowMons == RowIndex + 1 || cordColMons == colIndex - 1 || cordColMons == colIndex + 1)
                {
                    // ataka
                    AttackMonstar(monstarId, heroId, heroType);
                }

                dictMonster[monstarId] = new List<int> { cordRowMons, cordColMons };
                matrix[cordRowMons][cordColMons] = "1";
                matrix[cordinatiMons[0]][cordinatiMons[1]] = "0";

            }


            return matrix;
        }

        public static void AttackMonstar(int monstarId, int heroId, string heroType)
        {
            using (var context = new RpgDBContext())
            {
                var monstar = context.Monstars.Where(c => c.Id == monstarId).FirstOrDefault();
                switch (heroType)
                {
                    case "Warrior":
                        var heroW = context.Warriors.Where(c => c.Id == heroId).FirstOrDefault();

                        heroW.Helth -= monstar.Damage;

                        break;

                    case "Mage":
                        var heroM = context.Mages.Where(c => c.Id == heroId).FirstOrDefault();

                        heroM.Helth -= monstar.Damage;

                        break;
                    case "Archer":
                        var heroA = context.Archers.Where(c => c.Id == heroId).FirstOrDefault();

                        heroA.Helth -= monstar.Damage;


                        break;
                    default:
                        break;
                }

                context.SaveChanges();
            }
        }

        private static void PrintMatrix(string[][] matrix)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix.Length; col++)
                {
                    Console.Write(matrix[row][col]);
                }
                Console.WriteLine();
            }


        }

        private static List<int> CreateMonster()
        {
            var random = new Random();
            var result = new List<int>();
            using (var context = new RpgDBContext())
            {
                int strenght = random.Next(1, 3);
                int agility = random.Next(1, 3);
                int intelligence = random.Next(1, 3);
                var monster = new Monstars(strenght, agility, intelligence);
                monster.Setup();
                context.Monstars.Add(monster);
                context.SaveChanges();
                result.Add(monster.Id);

            }


            var indexRowMonstar = random.Next(0, 9);
            var indecColMonstar = random.Next(0, 9);
            result.Add(indexRowMonstar);
            result.Add(indecColMonstar);
            // to do check cordinate on hero 
            return result;
        }
    }
}