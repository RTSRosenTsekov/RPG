namespace RPG.Data
{
    public static class Utility
    {
        public static void InitDb()
        {
            var context = new RpgDBContext();
            context.Database.Initialize(true);

        }
    }
}
