using System.Data.Entity.Migrations;
using AbcYazilim.OgrenciTakip.Data.Contexts;

namespace AbcYazilim.OgrenciTakip.Data.OgrenciTakipMigration
{
    public class Configuration : DbMigrationsConfiguration<OgrenciTakipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}