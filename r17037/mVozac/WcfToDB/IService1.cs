using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfToDB
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        int InsertKorisnika(Korisnik k);
        [OperationContract]
        Korisnik SelectKorisnika(Korisnik k);
        [OperationContract]
        Voznja SelectVoznju(string tekst);
        [OperationContract]
        Popust SelectPopust(string nazivPopusta);
        [OperationContract]
        float SelectVoznjaCijena(string nazivLinije, string vozac);
        [OperationContract]
        int GetPopustID(string naziv);
        [OperationContract]
        int GetKorisnikID(string ime);
        [OperationContract]
        int GetVoznjaID(string linija, string vozac);
        [OperationContract]
        int InsertKarta(Karta k);
        [OperationContract]
        int PotvrdiVoznju(string linija);
        [OperationContract]
        Linija GetLinijaID(string linija_naziv);
        [OperationContract]
        StanicaPocetak SelectStanicaIDPocetak(int idLinije);
        [OperationContract]
        StanicaZavrsetak SelectStanicaIDZavrsetak(int idLinije);
        [OperationContract]
        Lokacija DohvatiLokaciju(string stanica);
        [OperationContract]
        Karta UkloniKartu(int brojKarte);
        [OperationContract]
        void DeleteKarta(int brojKarte);
        [OperationContract]
        KartaIspis FindKarta(int brojKarte);
        [OperationContract]
        List<string> ListaPopusta();
        [OperationContract]
        List<Grad> ListaMedustanica(string kor_ime);
        [OperationContract]
        ObservableCollection<string> PopustiCombo();
        [OperationContract]
        ObservableCollection<string> LinijeCombo(int vozac_id);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfToDB.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}