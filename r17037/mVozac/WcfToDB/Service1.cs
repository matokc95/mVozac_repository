using System;
using System.Collections.Generic;
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
                //       command.Parameters.AddWithValue("datum_rodenja", null);
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
                command.CommandText = "select korisnicko_ime,lozinka from korisnik where korisnicko_ime=@korisnicko_ime and lozinka=@lozinka";
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
                return kor;
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
        public Voznja SelectVoznju(string tekst)
        {
            Voznja voznja = new Voznja();
            try
            {
                command.CommandText = "select k.ime,k.prezime,l.naziv_linije,b.broj_sjedala,b.duzina,b.sirina,b.max_brzina from voznja v join korisnik k on k.korisnik_id=v.vozac join bus b on b.bus_id=v.bus join linija l on v.linija=l.linija_id where k.korisnicko_ime=@korisnicko_ime";
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
                return voznja;
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

        public Popust SelectPopust()
        {
            Popust popust = new Popust();
            try
            {
                command.CommandText = "select naziv_popusta, kolicina_popusta from popust";

                command.CommandType = CommandType.Text;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    popust.NazivPopusta = reader[0].ToString();
                    popust.KolicinaPopusta = float.Parse(reader[1].ToString());
                }
                reader.Close();
                return popust;
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
