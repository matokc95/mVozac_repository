using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class KartaIspis
    {
        [DataMember]
        public int KartaID;
        [DataMember]
        public string Popust { get; set; }
        [DataMember]
        public string Vozac { get; set; }
        [DataMember]
        public string Linija { get; set; }
        [DataMember]
        public float CijenaVoznje { get; set; }
        [DataMember]
        public float KolicinaPopusta { get; set; }
        [DataMember]
        public int Ponistena { get; set; }
    }
}