using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace mVozac
{
    public class MVozacContext : DbContext
    {
        public DbSet<Bus> Busevi { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Karta> Karte { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Linija> Linije { get; set; }
        public DbSet<Medustanice> Medustanice { get; set; }
        public DbSet<Popust> Popusti { get; set; }
        public DbSet<Stanica> Stanice { get; set; }
        public DbSet<Vozac> Vozaci { get; set; }
        public DbSet<Voznja> Voznje { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=31.147.204.119\\PISERVER,1433;Initial Catalog=17037_DB;User ID=17037_User;Password=***********");
        }
    }
    
    public class Bus
    {
        public int BusId { get; set; }
        public int Duzina { get; set; }
        public int Sirina { get; set; }
        public int BrojSjedala { get; set; }
        public int MaxBrzina { get; set; }
    }

    public class Grad
    {
        public int GradId { get; set; }
        public string Naziv { get; set; }
    }
    public class Karta
    {
        public int KartaId { get; set; }
        public int Popust { get; set; }
        public int Vozac { get; set; }
        public int Voznja { get; set; }
    }
    public class Korisnik
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodenja { get; set; }
        public string OIB { get; set; }
    }
    public class Linija
    {
        public int LinijaId { get; set; }
        public string NazivLinije { get; set; }
    }
    public class Medustanice
    {
        //public int Pocetak { get; set; }
        //public int Zavrsetak { get; set; }
        public Linija Linija { get; set; }
        public Stanica Stanica { get; set; }
    }
    public class Popust
    {
        public int PopustId { get; set; }
        public string NazivPopusta { get; set; }
        public int KolicinaPopusta { get; set; }
    }
    public class Stanica
    {
        public int StanicaId { get; set; }
        public string NazivStanice { get; set; }
        public Grad Grad { get; set; }
    }
    public class Vozac
    {
        public int VozacId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
    public class Voznja
    {
        public int VoznjaId { get; set; }
        public DateTime DatumVoznje { get; set; }
        public Vozac Vozac { get; set; }
        public Bus Bus { get; set; }
        public Linija Linija { get; set; }
    }


}
