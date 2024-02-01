namespace RPG.Data
{
    using RPG.Data.Models;
    using System.Data.Entity;

    public class RpgDBContext : DbContext
    {
        public RpgDBContext() : base("name=RpgEntities")
        {
            
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        
        }
                    
          public DbSet<Monstars> Monstars { get; set; }

          public DbSet<Archer> Archers { get; set; }

          public DbSet<Warrior> Warriors { get; set; }

          public DbSet<Mage> Mages { get; set; }
            
        
    }
}
