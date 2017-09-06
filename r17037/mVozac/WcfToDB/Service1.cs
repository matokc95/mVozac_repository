using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfToDB
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public Service1()
        {
            ConnectToDB();
        }

        SqlConnection connection;
        SqlCommand command;
        SqlConnectionStringBuilder connStringBuilder;

        void ConnectToDB()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = "31.147.204.119\\PISERVER,1433";
            connStringBuilder.InitialCatalog = "17037_DB";
            connStringBuilder.Password = "rKVuTFPL";
            connStringBuilder.Encrypt = true;
            connStringBuilder.UserID = "17037_User";
            connStringBuilder.TrustServerCertificate = true;
            connStringBuilder.ConnectTimeout = 30;
            connStringBuilder.AsynchronousProcessing = true;
            connStringBuilder.MultipleActiveResultSets = true;
            //connStringBuilder.IntegratedSecurity = true;
            connection = new SqlConnection(connStringBuilder.ToString());
            command = connection.CreateCommand();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }

            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }

            return composite;
        }
        public int InsertKorisnika(Korisnik k)
        {
            try
            {
                command.CommandText = "insert into korisnik(ime,prezime,datum_rodenja,oib,korisnicko_ime,lozinka,email) values(@ime, @prezime, null, @oib, @korisnicko_ime, @lozinka, @email)";
                command.Parameters.AddWithValue("ime", k.Ime);
                command.Parameters.AddWithValue("prezime", k.Prezime);
                //command.Parameters.AddWithValue("datum_rodenja", null);
                command.Parameters.AddWithValue("oib", k.OIB);
                command.Parameters.AddWithValue("korisnicko_ime", k.KorisnickoIme);
                command.Parameters.AddWithValue("lozinka", k.Lozinka);
                command.Parameters.AddWithValue("email", k.Email);
                command.CommandType = CommandType.Text;
                connection.Open();

                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public Korisnik SelectKorisnika(Korisnik k)
        {
            Korisnik kor = new Korisnik();

            try
            {
                command.CommandText = "SELECT korisnicko_ime,lozinka " +
                    "FROM korisnik " +
                    "WHERE korisnicko_ime=@korisnicko_ime " +
                    "AND lozinka=@lozinka";
                command.Parameters.AddWithValue("korisnicko_ime", k.KorisnickoIme);
                command.Parameters.AddWithValue("lozinka", k.Lozinka);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    kor.KorisnickoIme = reader[0].ToString();
                    kor.Lozinka = reader[1].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return kor;
        }
        public Voznja SelectVoznju(string tekst)
        {
            Voznja voznja = new Voznja();

            try
            {
                command.CommandText = "SELECT k.ime,k.prezime,l.naziv_linije,b.broj_sjedala,b.duzina,b.sirina,b.max_brzina " +
                    "FROM voznja v JOIN korisnik k ON k.korisnik_id=v.vozac " +
                    "JOIN bus b ON b.bus_id=v.bus " +
                    "JOIN linija l ON v.linija=l.linija_id " +
                    "WHERE k.korisnicko_ime=@korisnicko_ime";
                command.Parameters.AddWithValue("korisnicko_ime", tekst);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    voznja.ImeVozaca = reader[0].ToString();
                    voznja.PrezimeVozaca = reader[1].ToString();
                    voznja.NazivLinije = reader[2].ToString();
                    voznja.BrojSjedala = int.Parse(reader[3].ToString());
                    voznja.DuzinaBusa = float.Parse(reader[4].ToString());
                    voznja.SirinaBusa = float.Parse(reader[5].ToString());
                    voznja.MaxBrzina = float.Parse(reader[6].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return voznja;
        }

        public Popust SelectPopust(string nazivPopusta)
        {
            Popust popust = new Popust();

            try
            {
                command.CommandText = "SELECT naziv_popusta, kolicina_popusta " +
                    "FROM popust " +
                    "WHERE naziv_popusta = @naziv_popusta";
                command.Parameters.AddWithValue("naziv_popusta", nazivPopusta);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    popust.NazivPopusta = reader[0].ToString();
                    popust.KolicinaPopusta = float.Parse(reader[1].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return popust;
        }

        public float SelectVoznjaCijena(string nazivLinije, string vozac)
        {
            float price = -1;

            try
            {
                command.CommandText = "SELECT v.cijena, v.prihvacena, v.gotova, k.korisnicko_ime " +
                    "FROM voznja v JOIN linija l ON v.linija = l.linija_id " +
                    "JOIN korisnik k ON v.vozac = k.korisnik_id " +
                    "WHERE v.prihvacena = 1 " +
                    "AND v.gotova = 0 " +
                    "AND l.naziv_linije = @naziv_linije " +
                    "AND k.korisnicko_ime = @korisnicko_ime";
                command.Parameters.AddWithValue("naziv_linije", nazivLinije);
                command.Parameters.AddWithValue("korisnicko_ime", vozac);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    price = float.Parse(reader[0].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return price;
        }

        public int GetPopustID(string naziv)
        {
            int id = 0;

            try
            {
                command.CommandText = "SELECT popust_id " +
                    "FROM popust " +
                    "WHERE naziv_popusta = @naziv_popusta";
                command.Parameters.AddWithValue("naziv_popusta", naziv);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = int.Parse(reader[0].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return id;
        }

        public int GetKorisnikID(string ime)
        {
            int id = 0;

            try
            {
                command.CommandText = "SELECT korisnik_id " +
                    "FROM korisnik " +
                    "WHERE korisnicko_ime = @korisnicko_ime";
                command.Parameters.AddWithValue("korisnicko_ime", ime);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = int.Parse(reader[0].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return id;
        }

        public int GetVoznjaID(string linija, string vozac)
        {
            int id = 0;

            try
            {
                command.CommandText = "SELECT v.voznja_id " +
                    "FROM voznja v " +
                    "JOIN linija l ON v.linija = l.linija_id " +
                    "JOIN korisnik k ON v.vozac = k.korisnik_id " +
                    "WHERE v.prihvacena = 1 " +
                    "AND v.gotova = 0 " +
                    "AND l.naziv_linije = @naziv_linije " +
                    "AND k.korisnicko_ime = @korisnicko_ime";
                command.Parameters.AddWithValue("naziv_linije", linija);
                command.Parameters.AddWithValue("korisnicko_ime", vozac);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = int.Parse(reader[0].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return id;
        }

        public int InsertKarta(Karta k)
        {
            try
            {
                command.CommandText = "INSERT INTO karta " +
                    "(popust, vozac, voznja, ponistena) " +
                    "VALUES(@popust, @vozac, @voznja, @ponistena)";
                command.Parameters.AddWithValue("popust", k.Popust);
                command.Parameters.AddWithValue("vozac", k.Vozac);
                command.Parameters.AddWithValue("voznja", k.Voznja);
                command.Parameters.AddWithValue("ponistena", false);
                command.CommandType = CommandType.Text;
                connection.Open();

                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public PonistiKartu SelectKarta()
        {
            PonistiKartu karta = new PonistiKartu();

            try
            {
                command.CommandText = "SELECT ka.karta_id,k.ime,k.prezime,p.kolicina_popusta,v.voznja_id " +
                    "FROM karta ka " +
                    "JOIN popust p ON ka.popust=p.popust_id " +
                    "JOIN korisnik k ON ka.vozac=k.korisnik_id " +
                    "JOIN voznja v ON ka.voznja=v.voznja_id";
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    karta.KartaID = int.Parse(reader[0].ToString());
                    karta.ImeVozaca = reader[1].ToString();
                    karta.PrezimeVozaca = reader[2].ToString();
                    karta.KolPopusta = float.Parse(reader[3].ToString());
                    karta.VoznjaID = int.Parse(reader[4].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return karta;
        }
        public int PotvrdiVoznju(string linija)
        {
            try
            {
                command.CommandText = "UPDATE voznja " +
                    "SET prihvacena = 1 " +
                    "WHERE linija=(SELECT linija_id FROM linija WHERE naziv_linije=@linija)";
                command.Parameters.AddWithValue("linija", linija);
                command.CommandType = CommandType.Text;
                connection.Open();

                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public Linija GetLinijaID(string linija_naziv)
        {
            Linija line = new Linija();

            try
            {
                command.CommandText = "SELECT linija_id,naziv_linije " +
                    "FROM linija " +
                    "WHERE naziv_linije=@linija_naziv";
                command.Parameters.AddWithValue("linija_naziv", linija_naziv);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    line.LinijaID = reader[0].ToString();
                    line.NazivLinije = reader[1].ToString();
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return line;
        }
        public StanicaPocetak SelectStanicaIDPocetak(int idLinije)
        {
            StanicaPocetak stanicaPocetak = new StanicaPocetak();

            try
            {
                command.CommandText = "SELECT s.naziv_stanice,m.pocetak,m.kraj " +
                    "FROM medustanice m " +
                    "JOIN stanica s ON m.stanica=s.stanica_id " +
                    "WHERE m.pocetak=1 " +
                    "AND m.linija=@idLinije";
                command.Parameters.AddWithValue("idLinije", idLinije);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stanicaPocetak.StanicaNaziv = reader[0].ToString();
                    stanicaPocetak.Pocetak = bool.Parse(reader[1].ToString());
                    stanicaPocetak.Kraj = bool.Parse(reader[2].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return stanicaPocetak;
        }
        public StanicaZavrsetak SelectStanicaIDZavrsetak(int idLinije)
        {
            StanicaZavrsetak stanicaZavrsetak = new StanicaZavrsetak();

            try
            {
                command.CommandText = "SELECT s.naziv_stanice,m.pocetak,m.kraj " +
                    "FROM medustanice m " +
                    "JOIN stanica s ON m.stanica=s.stanica_id " +
                    "WHERE m.pocetak=0 " +
                    "AND m.kraj=1 AND m.linija=@idLinije";
                command.Parameters.AddWithValue("idLinije", idLinije);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stanicaZavrsetak.StanicaNaziv = reader[0].ToString();
                    stanicaZavrsetak.Pocetak = bool.Parse(reader[1].ToString());
                    stanicaZavrsetak.Kraj = bool.Parse(reader[2].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return stanicaZavrsetak;
        }

        public Lokacija DohvatiLokaciju(string stanica)
        {
            Lokacija lokacijaPocetak = new Lokacija();

            try
            {
                command.CommandText = "SELECT g.latitude,g.longitude " +
                    "FROM grad g " +
                    "JOIN stanica s ON g.grad_id=(SELECT grad FROM stanica WHERE naziv_stanice=@naziv)";
                command.Parameters.AddWithValue("naziv", stanica);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lokacijaPocetak.Latitude = double.Parse(reader[0].ToString());
                    lokacijaPocetak.Longitude = double.Parse(reader[1].ToString());
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lokacijaPocetak;
        }

        public Karta UkloniKartu(int brojKarte)
        {
            Karta karta = new Karta();
            karta.KartaID = 0;

            try
            {
                command.CommandText = "SELECT karta_id, popust, vozac, voznja, ponistena " +
                    "FROM karta " +
                    "WHERE karta_id = @broj_karte";
                command.Parameters.AddWithValue("broj_karte", brojKarte);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    karta.KartaID = int.Parse(reader[0].ToString());
                    karta.Popust = float.Parse(reader[1].ToString());
                    karta.Vozac = int.Parse(reader[2].ToString());
                    karta.Voznja = int.Parse(reader[3].ToString());
                    karta.Ponistena = bool.Parse(reader[4].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return karta;
        }

        public KartaIspis FindKarta(int brojKarte)
        {
            KartaIspis karta = new KartaIspis();
            karta.KartaID = 0;

            try
            {
                command.CommandText = "SELECT karta.karta_id, popust.naziv_popusta, korisnik.korisnicko_ime, linija.naziv_linije, voznja.cijena, popust.kolicina_popusta, karta.ponistena " +
                    "FROM karta JOIN popust ON karta.popust = popust.popust_id " +
                    "JOIN korisnik ON karta.vozac = korisnik.korisnik_id " +
                    "JOIN voznja ON karta.voznja = voznja.voznja_id " +
                    "JOIN linija ON voznja.linija = linija.linija_id " +
                    "WHERE karta.karta_id = @broj_karte ";
                command.Parameters.AddWithValue("broj_karte", brojKarte);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    karta.KartaID = int.Parse(reader[0].ToString());
                    karta.Popust = reader[1].ToString();
                    karta.Vozac = reader[2].ToString();
                    karta.Linija = reader[3].ToString();
                    karta.CijenaVoznje = float.Parse(reader[4].ToString());
                    karta.KolicinaPopusta = float.Parse(reader[5].ToString());
                    karta.Ponistena = bool.Parse(reader[6].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return karta;
        }

        public void DeleteKarta(int brojKarte)
        {
            try
            {
                command.CommandText = "UPDATE karta " +
                    "SET ponistena = 1 " +
                    "WHERE karta_id = @broj_karte";
                command.Parameters.AddWithValue("broj_karte", brojKarte);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public List<string> ListaPopusta()
        {
            List<string> lista = new List<string>();

            try
            {
                command.CommandText = "SELECT naziv_popusta " +
                    "FROM popust";
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(reader[0].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lista;
        }

        public ObservableCollection<Grad> ListaMedustanica(string kor_ime)
        {
            ObservableCollection<Grad> lista = new ObservableCollection<Grad>();


            try
            {
                command.CommandText = "SELECT g.naziv,g.latitude,g.longitude " +
                    "FROM grad g " +
                    "JOIN stanica s ON g.grad_id = s.grad " +
                    "JOIN medustanice m ON m.stanica = s.stanica_id " +
                    "JOIN linija l ON m.linija = l.linija_id " +
                    "JOIN voznja v ON v.linija = l.linija_id " +
                    "JOIN korisnik k ON k.korisnik_id = v.vozac " +
                    "WHERE m.pocetak = 0 " +
                    "AND m.kraj = 0 " +
                    "AND k.korisnicko_ime = @kor";
                command.Parameters.AddWithValue("kor", kor_ime);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Grad noviGrad = new Grad();
                    noviGrad.NazivGrada = reader[0].ToString();
                    noviGrad.Latitude = double.Parse(reader[1].ToString());
                    noviGrad.Longitude = double.Parse(reader[2].ToString());
                    lista.Add(noviGrad);
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lista;
        }

        public ObservableCollection<string> PopustiCombo()
        {
            ObservableCollection<string> lista = new ObservableCollection<string>();

            try
            {
                command.CommandText = "SELECT naziv_popusta " +
                    "FROM popust";
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(reader[0].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lista;
        }

        public ObservableCollection<string> LinijeCombo(int vozac_id)
        {
            ObservableCollection<string> lista = new ObservableCollection<string>();

            try
            {
                command.CommandText = "SELECT linija.naziv_linije, linija.linija_id " +
                    "FROM linija " +
                    "JOIN voznja " +
                    "ON voznja.linija = linija.linija_id " +
                    "WHERE linija.linija_id IN" +
                    "(SELECT linija FROM voznja) " +
                    "AND voznja.vozac = @vozac_id";
                command.Parameters.AddWithValue("vozac_id", vozac_id);
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(reader[0].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lista;
        }

        public int GetCount()
        {
            int counter = 0;

            try
            {
                command.CommandText = "SELECT COUNT(*) " +
                    "FROM karta";
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    counter = int.Parse(reader[0].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return counter;
        }

        public ObservableCollection<int> PonisteneKarte()
        {
            ObservableCollection<int> lista = new ObservableCollection<int>();

            try
            {
                command.CommandText = "SELECT karta_id " +
                    "FROM karta " +
                    "WHERE ponistena = 1";
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(int.Parse(reader[0].ToString()));
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lista;
        }

        public void AktivirajKartu(int broj_karte)
        {
            try
            {
                command.CommandText = "UPDATE karta " +
                    "SET ponistena = 0 " +
                    "WHERE karta_id = @broj_karte";
                command.Parameters.AddWithValue("broj_karte", broj_karte);
                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
    }
}