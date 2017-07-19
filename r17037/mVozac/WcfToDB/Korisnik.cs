using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class Korisnik
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public DateTime DatumRodenja { get; set; }
        [DataMember]
        public string OIB { get; set; }
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public string Lozinka { get; set; }
        [DataMember]
        public string Email { get; set; }

    }
}
