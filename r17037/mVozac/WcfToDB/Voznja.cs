using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class Voznja
    {
        [DataMember]
        public DateTime DatumVoznje { get; set; }
        [DataMember]
        public string ImeVozaca { get; set; }
        [DataMember]
        public string PrezimeVozaca { get; set; }
        [DataMember]
        public int BrojSjedala { get; set; }
        [DataMember]
        public string NazivLinije { get; set; }
    }
}
