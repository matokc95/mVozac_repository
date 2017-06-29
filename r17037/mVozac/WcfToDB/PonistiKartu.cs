using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class PonistiKartu
    {
        //karta:
        [DataMember]
        public int KartaID { get; set; }
        //vozac:
        [DataMember]
        public string ImeVozaca { get; set; }
        [DataMember]
        public string PrezimeVozaca { get; set; }
        //popust:
        [DataMember]
        public float KolPopusta { get; set; }
        [DataMember]
        //voznja:
        public int VoznjaID { get; set; }
    }
}
