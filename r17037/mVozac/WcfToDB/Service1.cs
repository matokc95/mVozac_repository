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
                if(connection != null)
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
    }
}
