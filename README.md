# mVozac_repository
# Faze izrade projekta
Za upravljanje našim projektom smo odabrali vodopadnu metodu. Vodopadna metoda se temelji na tome da se proces razvija slijedom koraka, tj. faza po faza. Svaka faza mora rezultirati dokumentom, a rezultat prethodne faze je polazište u razvijanju sljedeće faze. Razvoj našeg softvera smo podjelili na sljedeće faze:
1. Utvrđivanje zahtjeva
2. Oblikovanje sustava, oblikovanje softvera (dizajn)
3. Implementacija i testiranje dijelova sustava
4. Integriranje modula i testiranje sustava
5. Uvođenje u rad i održavanje sustava
# 1. Faza - Utvrđivanje zahtjeva
Pošto nemamo trenutno zainteresiranog budućeg korisnika, moguće zahtjeve aplikacije smo odredili sami:
1.	Pomoć u aplikaciji
2.	Prijava/Registracija
3.	Prodaja karata
4.	Pretraga karata
5.	Poništavanje karata
6.	Raspored vožnje
7.	Digitalni tahometar
8.	Ponovno aktiviranje karte

Pod osnovnu funkcionalnost aplikacija podrazumijeva se još i pomoć u aplikaciji kroz koju korisnik saznaje detalje vezane uz korištenje aplikacije i njezine funkcionalnosti.

Prijava je obrazac na kojem korisnik unosi korisničko ime i lozinku, te ako korisnik nije registriran, pritiskom na gumb "Registracija", korisnika se vodi na obrazac za registraciju.

Prodaja karata služi vozaču da na jednostavan način proda kartu korisniku prijevoza. Na zaslonu se nalazi opcija za odabir popusta ukoliko je korisnik student, umirovljenik... Također, nakon odabira popusta, vozač odabire za koju relaciju se prodaje karta.

Pretraga karata je funkcionalnost kojom vozač autobusa potvrđuje da prijevoznik ima kartu i da se korisnik može prevoziti u njegovom autobusu. Aktivnost sadrži kameru pomoću koje korisnik skenira QR kod, te mu aplikacija vraća povratnu poruku da li je karta važeća ili ne.

Poništavanje karata je funkcionalnost kojom vozač poništava kartu ukoliko je korisnik odustao od prijevoza ili u nekim sličnim scenarijima.

Raspored vožnje služi vozaču da nakon što mu je dodijeljena relacija, kada uđe u autobus da odabere tu relaciju i potvrdi Na zaslonu je prikazano ime i prezime vozača, kao i podaci o autobusu i prijevoznoj liniji.

Digitalni tahometar je zaslon koji je aktivan tijekom vožnje. Na njemu je prikazana polazišna i odredišna lokacija linije vožnje. Također na mapi je iscrtana ruta od njegove početne pozicije koja je utvrđena GPS-om do polazišta (crvena linija), žuta linija predstavlja rutu od polazišta do oredišta.

Ponovno aktiviranje karte. Ovaj zaslon vozač otvara kada je potrebno ponovno aktivirati kartu koju je deaktivirao ili se dogodio neki sličan scenarij.

U ovoj fazi smo prošli kroz nekoliko koraka: proučavanju korisničkih zahtjeva, pisanju projekta, redovitim sastancima članova tima i kao rezultat svega smo dobili dokument o specifikacijama zahtjeva koju će u nastavku biti detaljno objašnjen.
## Specifikacije zahtjeva korisnika
Ovaj dokumenat daje opis i pregled svega što je kroz nekoliko iteracija napravljeno i prikupljeno u 1. fazi našeg projekta.
### Svrha
Svrha ovog dokumenta je prikazati detalje zahtjeva za mVozac softversko rješenje. Dokument će ilustrirati svrhu i potpunu deklaraciju za razvoj sustava. Također će biti obješnjena softverska ograničenja, sučelje i interakcija softvera sa okolinom.
### Djelokrug
Aplikacija mVozac je UWP aplikacija koja olakšava svakodnevnicu vozača autobusa. Vozač autobusa time može na brz, jednostavan i pregledan način pronaći informacije o svojoj dnevnoj ruti vožnje i voditi evidenciju o putnicima u autobusu. 

Prilikom dolaska putnika u autobus, korisnik aplikacije skenira QR kod kodji je vidljiv na karti putnika. Na temelju QR koda pretražuje se baza podataka sa kupljenim kartama i evidentira se da je karta važeća. U slučaju da putnik nema kartu ili je ona nevažeća, vozač ima mogućnost prodaje karte tom putniku.

Vozaču je također na raspolaganju i digitalni tahometar koji, uz standardne funkcionalnosti, nudi i praćenje busa uz pomoć GPS lokatora. Vozač ima mogućnost provjere i prodaje karata. 

Na kraju dana vozač bilježi svoju dnevnu statistiku kako bi autobusna kompanija dobila feedback o broju putnika koji se prevoze i eventualno poduzela neke promjene (npr. vozač vozi autobus za 30 putnika, a u autobusu je bilo 10 putnika...promjena bi bila da se uvede manji bus za tu relaciju, da ne dolazi do prevelikih troškova). 
### Funkcionalnosti proizvoda
1. Prijava
2. Registracija
3. Pretraga karata
4. Poništavanje karata
5. Raspored vožnje
6. Prodaja karte
7. Digitalni tahometar
8. Ponovno aktiviranje karte

Pod osnovnu funkcionalnost aplikacija podrazumijeva se još i pomoć u aplikaciji kroz koju korisnik saznaje detalje vezane uz korištenje aplikacije i njezine funkcionalnosti. 

Prijava je obrazac na kojem korisnik unosi korisničko ime i lozinku, te ako korisnik nije registriran, pritiskom na gumb "Registracija", korisnika se vodi na obrazac za registraciju. 

Prodaja karata služi vozaču da na jednostavan način proda kartu korisniku prijevoza. Na zaslonu se nalazi trenutna stanica, koja je dohvaćena preko servera, odredišta koja su povezana sa tom stanicom, cijena karte , a također postoji i opcija za ostvarivanje popusta ukoliko je korisnik student ili umirovljenik (što predoči sa identifikacijskom oznakom, x-ica ili slično).

Pretraga karata služi kao opcija ukoliko je korisnik prijevoza zaboravio kartu ili je karta rezervirana. Vozač upiše id karte koji je dodijeljen putniku prilikom kupnje. Na temelju id karte vozač pretražuje popis karata i pronalazi kartu sa traženim id-om. Kada identificira kartu, vozač check-ira da je putnik u autobusu čime se povećava vrijednost koliko je putnika u autobusu na tahometru.

Poništavanje karata je funkcionalnost kojom vozač poništava kartu ako je krivo upisao podatke prilikom prodaje u
busu ili ako se dogodio neki drugi scenarij.

Raspored vožnje služi vozaču da nakon što mu je dodijeljena relacija, kada uđe u autobus da odabere tu relaciju i potvrdi (čime se ažurira vrijeme polaska busa, kašnjenje busa ukoliko postoji…) Na zaslonu je prikazan kolodvor, popis relacija, sa kojeg perona kreće autobus i kada je vrijeme polaska.

Digitalni tahometar je zaslon koji mora biti aktivan za vrijeme vožnje. Ovaj zaslon prikazuje trenutnu stanicu, sljedeću lokaciju (putem odabira relacije), udaljenost između određenih stanica, koje je trenutno vrijeme te broj putnika u autobusu. Vozač na svakoj stanici odabere potvrdi lokaciju, kao potvrda da je prošao tu stanicu (ako je prazna ili ako je stao). Funkcionalnost je realizirana pomoću gps-a.

Statistika. Ovaj zaslon vozač otvara na kraju svoje smjene da sve podatke vezane uz njegovu smjenu pošalje na server. To je potrebno kako bi se lakše utvrdile specifikacije prijevoza (koliko putnika dnevno putuje, može li se koristiti manji autobus, veći autobus, troškovi prijevoza i slično...).

### Vrste korisnika
Postoje 3 vrste korisnika aplikacije: vozač autobusa, moderator i administrator. Vozač autobusa je osoba koja ima svrhu aplikacije. Moderator je osoba nadležna vozaču koja za njega kreira korisnički račun za aplikaciju. Administratore predstavljaju osobe koje imaju najveće ovlasti nad aplikacijom, u ovom slučaju to je naš tim.

### Ograničenja u radu softvera
Aplikaciju ograničava sučelje sustava sa GPS navigacijskim sustavom. Budući da postoji više sustava i više proizvođaća GPS-a, sučelje vrlo vjerojatno neće biti isto za svakoga od njih. Također, mogu postojati i neke razlike između onoga što navigacijske značajke pružaju.

Veza interneta je potrebna jer aplikacija preuzima podatke iz baze podataka preko poslužitelja, pa je veza posebno bitna za potrebnu funkcionalnost.

GPS signal je također ograničenje aplikacije, jer bez toga lokator ne bi radio.
### Pretpostavke i ovisnosti softvera
Jedna je pretpostavka o proizvodu je da se može koristiti na raznim platformama sa Windows OS-om. Ako se aplikacija koristi na mobilnim uređajima potrebno je dovoljno hardverskih resursa jer ako ih korisnik dodjeli nekim drugim aplikacijama, postoji mogućnost da aplikacija ne funkcionira na način koji bi trebala, ili da uopće ne funkcionira.

Još jedna pretpostavka je da GPS komponente u svim telefonima rade na isti način. Ako uređaji imaju različita sučelja s GPS-om, aplikacija mora biti posebno prilagođena svakom sučelju.

### Zahtjevi prema sučelju
Ovo poglavlje opisuje sve ipute i outpute u/iz sustava, daje opis hardvera, softvera i komunikacijskog sučelja.
1. _Korisničko sučelje:_
Kada korisnik otvori aplikaciju, prikaže se forma za prijavu. Ako je korisnik ranije registriran od strane moderatora, unosi korisničko ime i lozinku, te se prijavljuje u aplikaciju. Ako korisnik nije registriran, na formi za prijavu pritisne na dugme za registraciju i ispuni obrazac za registraciju. Kada se korisnik prijavi, prikaže mu se početna stranica aplikacije na kojoj su vidljive sve mogućnosti rada sustava, kao i dugmad za odjavu i pomoć u radu. 
2. _Softversko sučelje:_
Aplikacija komunicira sa GPS sustavom kako bi dobio geografsku informaciju o položaju autobusa. Također, aplikacija komunicira i sa bazom podataka koja pamti registrirane korisnike, relacije, kupljene karte...
3. _Komunikacijsko sučelje:_
Komunikacija između različitih djelova sustava je važna jer ti djelovi ovise jedni o drugima. Naime, kako se ostvaruje ta komunikacija, to sustava ne zanima. Za to brine OS uređaja.
# 2. Faza - Oblikovanje sustava, arhitektura softvera (dizajn)
## Arhitektura softvera
* autor: Matija Benotić
![Arhitektura](https://github.com/foivz/r17037/blob/master/arhitektura.png)

GPS uređaj daje trenutnu lokaciju korisnika (koordinate širine i dužine). U osnovnom načinu, položaj korisnika se koristi za prikaz rute na kontroli mape.

Baza podataka je središnji repozitorij za podatke o gradovima, njihovim lokacijama, podaci o rutama....

Kamera je uređaj koji služi za čitanje QR koda koji je dodjeljen prilikom prodaje karte za određenu vožnju.

S obzirom na položaj na Zemlji, kontrola mape će izračunati i iscrtati pripadajuću rutu na mapi.

## Dijagram klasa
* autor: Petar Bračko

![class diagram1](https://github.com/foivz/r17037/blob/master/Class%20Diagram1.jpg)
U dijagramu klasa nalazi se ukupno 23 klasa svaka sa svojim atributima, objektima, referencama, metodama i  vezama. Klase IService1 i klasa Service1 služe za interakciju aplikacije s bazom podataka. Svi upiti na bazu podtaka vrše se preko tih dviju klasa. Klasa Service1 sarži sve operacije, odnosno metode za izvršavanje upita, dok klasa IService1 omogućuje samo konekciju prema bazi podataka. Klasa CompostieType služi klasi IService1 kao pomoć kod ostvarivanja konekcije prema bazi podataka. Sve ostale klase koje smo mi kreirali komuniciraju s klasom Service1 kako bi dohvatili tražene podatke iz baze podataka te ih prikazali u aplikaciji pomoći generiranih klasa pageova aplikacije. Klase Bus, Karta, KartaIspis, Korisnik, Voznja, Linija, Lokacija, PonistiKartu, Popust, StanicaPocetak te StanicaZavrsetak služe samo za manipulaciju podacima s kojima aplikacija radi, dok se sve metode za prikaz i obradu tih podataka nalaze u klasama pageova, odnosno aktivnosti aplikacije.
## Dijagram slučajeva korištenja
* autor: Matija Benotić
![Use case](https://github.com/foivz/r17037/blob/master/dijagram%20slucajeva/mvozac%20use%20case.png)

Na temelju zahtjeva korisnika i analize provedene nad istima, izradili smo dijagram slučajeva korištenja po kojem će se programeri orijentirati prilikom realizacije programskog rješenja.

Dijagram se sastoji od 8 slučajeva korištenja:
* Registracija (moderator)
* Prijava
* Provjera i potvrda rasporeda vožnje
* Prodaja karata
* Pretraga karata
* Poništavanje karata
* Aktiviranje karata
* GPS navigacija

Moderator registrira nove vozače u sutav. Nakon što je vozač registriran i dodjeljeno mu je korisničko ime i lozinka, prijavljuje se u aplikaciju. Nakon prijave, provjerava svoj dnevni raspored vožnje i potvrđuje ga. Postoji mogućnost prodaje karte tako da unosi potrebne podatke i kreira kartu sa pripadajućim QR kodom. Prilikom ulaska putnika, provjerava ispravnost karte pomoću njenog pretraživanja u bazi koje se vrši skeniranjem QR kod-a koji je putniku dodjeljen prilikom prodaje. Ukoliko putnik odustane od putovanja, karta se poništi također pomoću skeniranja QR kod-a. Moguće je i ponovno aktiviranje karte koje je realizirano tako da se odabere određena karta iz padajućeg izbornika. Prilikom putovanja, vozaču stoji na raspolaganju GPS navigacija koja mu pokazuje dnevnu rutu.
## Funkcionalnosti i pripadajući dijagrami aktivnosti
### 1. Prijava u sustav
* autor: Matija Benotić

![Log In screen](https://github.com/foivz/r17037/blob/master/skice%20ekrana/login.png)

Dijagram aktivnosti za prijavu korisnika se sastoji od 3 učesnika: Korisnik, Aplikacija i Baza podataka. Aktivnost započinje pokretanjem aplikacije. 

Nakon što se aplikacija pokrenula, instancira se forma za prijavu. Korisnik unosi u formu za prijavu tražene podatke (korisničko ime i lozinku).

Vrši se provjera podataka unutar baze podataka: dohvaćaju se podaci iz baze i vrši se provjera. Ako je provjera neuspješno završena, ispisuje se poruka o grešci i vraća se korisnika na formu za prijavu. Ako je provjera uspješno završena, dealocira se forma za prijavu i preusmjerava se korisnika na glavni izbornik aplikacije.

![](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/login.jpg)

### 2. Registracija
* autor: Matija Benotić

![Register screen](https://github.com/foivz/r17037/blob/master/skice%20ekrana/register.png)

Aktivnost registracije korisnika započinje ukoliko korisnik na formi za prijavu pritisne gumb "Registracija". Nakon pritiska na gumb "Registracija", forma se instancira i prikazuje. 

Korisnik unosi u formu za registraciju tražene podatke (ime,prezime,datum rođenja,oib,korisničko ime,lozinku,email). Nakon unosa traženih podataka, zapis se spremi u bazu i ispisuje se poruka o uspješnoj registraciji. Nakon toga se dealocira forma za registraciju i prosljeđuje se korisnika na formu za prijavu.

![](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/registracija.jpg)

### 3. Pretraži karte
* autor: Bračko Petar

![2 pretrazi karte](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/Pretraga%20karte.jpg)

Ovaj dijagram sadrži pet učesnika, a to su korisnik, aplikacija, baza podataka, web servis koji služi za komuniciranje s bazom podataka te element vezan na kameru uređaja za čitanje njezinog sadržaja. Proces započinje u trenutku kada korisnik odabire gumb Pretraži karte. Nakon što korisnik to odabere, aplikacija instancira formu za pretragu karata, nakon toga je inicijalizira te je na kraju prikaže. 

Aplikacija instancira MediaCapture element koji čita sadržaj kamere. Kada je forma prikazana, aplikacija čeka da korisnik skenira QR kod pomoću ugrađenog čitača QR kodova. Prilikom kreiranja karte, svaka dobiva QR kod u kojem je zapisan ID broj karte po kojem se mogu vršiti pretrage te izvršavati poništavanja i aktiviranja karte. 

Nakon što aplikacija uspješno pročita QR kod, šalje pročitani ID broj web servisu po kojem se pretražuje popis karata u bazi podataka. Ukoliko karta ne postoji, aplikacija javlja odgovarajuću poruku o nepostojanju karte. Ako karta postoji, ona može biti poništena ili valjana. 

U oba slučaja aplikacija ispisuje informacije o karti, te dodatno za poništene karte prikazuje dodatno tekstualno polje koje označava da je karta poništena.

![Skica ekrana pretrage karata](https://github.com/foivz/r17037/blob/master/skice%20ekrana/Pretra%C5%BEivanje%20karte.png)

### 4. Poništi karte
* autor: Bračko Petar

![3 ponisti karte](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/Poni%C5%A1tavanje%20karte.jpg)

Ovaj dijagram sadrži pet učesnika. Bazu podataka, korisnika, aplikaciju, web servis koji služi za komuniciranje s bazom podatka te element koji služi za čitanje sadržaja kamere odnosno QR koda.  

Proces započinje u trenutku kada korisnik odabire gumb Poništi karte. Kada korisnik to odabere, aplikacija instancira formu za pretragu karata, nakon toga je inicijalizira te je na kraju prikaže. Kada je forma prikazana, aplikacija instancira element MediaCapture koji u beskonačnoj petlji čita sadržaj kamere.. 

Forma sadrži čitač QR koda koji čita QR kod s karte u kojem je zapisan ID broj karte. Aplikacija skenira tako dugo dok ne pročita odgovarajući zapis, nakon kojeg provjerava što je zapisano. Nakon što skener pročita QR kod, u bazi podataka provjerava da li u bazi podataka postoji karta s pročitanim ID brojem. 

Ukoliko karta ne postoji, aplikacija javlja odgovarajuću poruku kako karta s određenim ID brojem ne postoji. Ako karta postoji, tada se provjerava da li je karta poništena ili nije. Ako je karta već poništena, aplikacija javi poruku da je karta već poništena, a ukoliko karta nije poništena, aplikacija javlja povratnu poruku o uspješnom poništavanju karte.

![Skica ekrana za poništavanje karata](https://github.com/foivz/r17037/blob/master/skice%20ekrana/Poni%C5%A1tavanje%20karte.png)

### 5. Raspored vožnje
* autor: Matija Benotić 

![Raspored vožnje screen](https://github.com/foivz/r17037/blob/master/skice%20ekrana/raspored%20voznje.png)

Ovaj dijagram sadrži tri učesnika :  Korisnik, Aplikacija i Baza podataka. 

Proces započinje u trenutku kada korisnik odabire gumb "Raspored vožnje". Aplikacija inicijalizira formu, ali prije njenog prikaza dohvaća podatke o vozaču i njegovoj vožnji iz baze podataka. 

Nakon što su dohvaćeni i učitani podaci o vozaču, forma se prikazuje korisniku. Klikom na gumb "Povratak" korisnika se vraća na glavni izbornik aplikacije. Klikom na gumb "Potvrdi vožnju", korisniku se dodjeljuje pravo pristupa vožnji u bazi podataka i prikazuje se poruka o prihvaćanju vožnje.

![](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/raspored_voznje.jpg)

### 6. Prodaj kartu
* autor: Bračko Petar

![5 prodaj kartu](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/Kreiranje%20karte.jpg)
Ovaj dijagram sadrži četiri učesnika : Korisnik, aplikacija, web servis te baza podataka.  

Proces započinje u trenutku kada korisnik odabire gumb Prodaj kartu. Kada korisnik to odabere, aplikacija instancira formu za pretragu karata, nakon toga je inicijalizira te je na kraju prikaže. 

Aplikacija dohvaća pomoću web servisa iz baze podataka sve vrste popusta te vožnje korisnika. Tim sadržajom puni dva padajuća izbornika iz kojih korisnik može birati podatke. Nakon toga korisnik odabire vožnju te određeni popust za kartu te odabire gumb Izdaj kartu. 
![](https://github.com/foivz/r17037/blob/master/skice%20ekrana/Prodaja.PNG)

Aplikacija tada izrađuje kartu na temelju određenih informacija poput popusta, ukupne cijene na koju utječe popust, linija vožnje te QR kod kojem je sadržaj ID broj nove karte. Baza podataka nakon toga radi novi zapis karte. 

Ukoliko je došlo do greške kod upisivanja nove karte, aplikacija ispisuje poruku o neuspjelom kreiranju karte. Ako je karta uspješno kreirana, tada aplikacija instancira novu formu za prikaz informacija o novo kreiranoj karti. Na novoj formi prikazuje sve podatke o karti te QR kod.

![](https://github.com/foivz/r17037/blob/master/skice%20ekrana/Prikaz%20karte.PNG)

### 7. Tahometar
* autor: Matija Benotić

![Tahometar screen](https://github.com/foivz/r17037/blob/master/skice%20ekrana/tahometar.png)

Ovaj dijagram sadrži također tri učesnika : Korisnik, Aplikacija, Baza podataka. 

Proces započinje u trenutku kada korisnik odabire gumb "Tahometar".  Sve što u ovom procesu korisnik treba je prikazana forma tahometra sa svim njezinim podacima. Kada korisnik odabere gumb tahometar, aplikacija instancira formu za tahometar, nakon toga je inicijalizira te je na kraju prikaže. 

U formi se nalaze podaci koji su potrebni vozaču kako bi znao svoj trenutni raspored vožnje, a to su trenutna lokacija (praćenje lokacije putem GPS-a), polazišna i odredišna lokacija i ispis rute na karti. 

Trenutna lokacija dohvaćena je putem GPS-a. Pritiskom na gumb "Započni rutu", korisniku se na karti ispisuje njegova trenutna ruta (crvena i žuta linija). Crvena linija predstavlja relaciju od vozačeve trenutne lokacije do polazišne lokacije zadane rute koja je vidljiva na formi iznad karte. Žuta linija predstavlja relaciju od polazišne do odredišne lokacije zadane rute koja je također vidljiva iznad karte. 

Ruta se dohvaća iz baze podataka. 

Klikom na gumb "Povratak", korisnika se vraća na glavni izbornik aplikacije.

![](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/tahometar.jpg)

### 8. Ponovno aktiviranje karte
* autor: Bračko Petar

![](https://github.com/foivz/r17037/blob/master/slike%20dijagram%20aktivnosti/Aktiviranje%20poni%C5%A1tene%20karte.jpg)
Kod funkcionalnosti za aktiviranje poništenih karata, korisnik bira karte iz padajućeg izbornika koje su poništene. 

Nakon što korisnik odabere jednu od njih, odabire gumb Aktiviraj kartu, te baza podataka mijenja atribute zapisa kojem odgovara ID broj kojem pripada odabrana karta. 

Nakon aktiviranja karte, aplikacija javlja povratnu poruku korisniku da je karta uspješno aktivirana.
![](https://github.com/foivz/r17037/blob/master/skice%20ekrana/Akitivranje.PNG)

## Dizajn podataka
Baza sadržava 10 početnih tablica. 

U tablicu „korisnik“ zapisuju se svi podatci o korisnicima aplikacije, odnosno vozačima, kao zaposlenicima autobusne kompanije. Tako se tablica „vozac“ veže na tablicu „korisnik“ putem vanjskog ključa _korisnik_. Veza između tablica „korisnik“ i „vozač“ je 1:1 zato jer svakom zapisu u tablici „vozac“ pripada samo jedan zapis iz tablice „korisnik“. 

Uz tablicu „vozac“ koja je potrebna za kreiranje vožnji, postoje tablica „bus““, „karta“ i „linija“. Tablica „bus“ sadrži podatke o vozilima, „karta“ sadrži vanjske ključeve na tablice „vozac“, odnosno na vozača koji izdaje kartu, vrsta popusta popust, ukoliko je isti ostvaren. 

Tablica „popust“ sadržava sve moguće vrste popusta (studenti, učenici, umirovljenici) koji određuje koliko popusta se ostvaruje na vožnju. 

Za linije su potrebne stanice – svaka stanica nalazi se u jednom gradu, dok se u svakom gradu mogu nalaziti više stanica. Zato tablica „stanica“ ima vanjski ključ _grad _na tablicu „grad“. 

Svaka linije iz tablice „linija“ ima svoj ID te svaka stanica iz tablice „stanica“ ima također svoj ID. Ta dva ID polja služe kao dvokomponentni primarni ključ. 

U tablici „međustanice“ također postoje dva polja _start _i _end_. Ukoliko je neka stanica početna na nekoj liniji, tada ta stanica ima atribut _start _postavljen TRUE (vrsta atributa start i end je BOOL). Isto vrijedi ukoliko je stanica završna postaja linije, tada atribut _end _ima vrijednost TRUE. Sve međustanice imaju _start _i _end _atribute postavljene na FALSE. Jedna stanica može biti međustanica na više linije, te jedna linija može imati više međustanica, i zato je potrebna veza M:N.

![ERA model](https://github.com/foivz/r17037/blob/master/ERA.png)

## Opis podataka
Svi podatci iz stvarnog života  koriste se i u našoj bazi podataka, kao što su podatci o vozačima, vozilima, linijama, stanica i slično. Svi entiteti zapisuju se u bazu podataka setom atributa koji ih opisuju.

Generirana [SQL skripta](https://github.com/foivz/r17037/blob/master/skripta.sql).

# 3. Faza - Implementacija i testiranje dijelova sustava
Naša implementacija se temeljila na kreiranju objekata koji predstavljaju entitet uz kojeg je vezan skup atributa i operacija, dakle, korištena je objektno orijentirana paradigma!
## Oblikovanje korisničkog sučelja
U većini slučajeva dizajn korisničkog sučelja se definira već u fazi specifikacije. Sučelje može poslužiti kao način da se opiše što sustav treba raditi, a ujedno to sučelje može biti odabrano od korisnika, u skladu sa navikama i standardima. Sučelje je važno svojstvo po kojem će cijeli sustav biti ocijenjen, pa mu treba posvetiti veliku pažnju. U našoj aplikaciji, zbog nedostatka vremena, nismo se posvetili dizajnu sučelja koliko je bilo potrebno! Kod oblikovanja korisničkog sučelja potrebno je uzeti u obzir fizičke i mentalne mogućnosti ljudi koji će koristiti softver.
## Interakcija korisnika sa sustavom
Prvi važan aspekt dizajna korisničkog sučelja je interakcija korisnika sa sustavom.
* Direktna manipulacija -> korisnik "rukuje" objektima na zaslonu. Primjer: da bi pobrisao datoteku, korisnik mišem odvlači njenu ikonu u “koš za smeće”. Prednosti su brza i intuitivna interakcija, te lagano učenje. Ovakav tip interakcije u našem softveru nije prisutan!
* Izbor s izbornika -> Korisnik bira naredbu s izbornika, često u kombinaciji sa selekcijom objekta na zaslonu. Na primjer, da bi pobrisao datoteku, korisnik selektira odgovarajuću ikonu, te zatim na izborniku bira naredbu “Delete”. Ovaj tip interakcije je najviše korišten u našem softveru!
* Ispunjavanje formulara -> Korisnik ispunjava polja na formularu te zatim pritisne gumb za akciju. Pogodan za manipulaciju sa mnogo podataka. Prisutan tip interakcije u našem softveru!
* Komandni jezik -> Korisnik na “komandnu liniju” upisuje naredbu i njene opcije. Prednosti: snažan i fleksibilan način, mogućnost kombiniranja naredbi. Interakcija koja nije korištena u našem softveru!
## Prikazivanje informacija
Drugi važan aspekt dizajna korisničkog sučelja je prikaz informacija. Za informaciju koja se često koristi tijekom rada aplikacije pogodan je tekstualan oblik informacije. Promjenjive podatke ili podatke gdje su važni relativni odnosi
treba prikazivati grafički. U slučajevima kad treba prikazati jako velike količine informacija, smišljaju se posebne vizualizacije: karte, tablice, spremnici, stabla, 2D/3D slike...
## Vođenje korisnika
Treći važan aspekt dizajna korisničkog sučelja. Predstavlja način na koji sustav prati rad korisnika i pomaže mu. Vođenje korisnika se odvija na tri razine: poruke, upute, korisnička dokumentacija
# 4. Faza - Integriranje modula i testiranje sustava
Integriranju modula pristupili smo bottom up metodom. Prvo smo razvili sve module koje smo smatrali da su nam potrebni za ostvarivanje funkcionalnosti, te smo nakon implementiranja svakog od njih, integrirali sve manje cjeline u jednu smislenu cjelinu koja tvori funkcionalnost aplikacije.

Svi dijelovi sustava su testirani te provjereni da li rade na način kako smo očekivali da rade. Provjeru smo vršili pogledom u bazu podataka nakon sve akcije koja je u interakciji sa bazom podataka. Također, testirani su scenariji koji ne bi smjeli proći kod rada s aplikacijom, odnosno da li takvi scenariji ne ruše aplikaciju.

## Test case: Win aplikacija
![Test 1](https://github.com/foivz/r17037/blob/master/testni%20slu%C4%8Dajevi/test1.png)
![Test 2](https://github.com/foivz/r17037/blob/master/testni%20slu%C4%8Dajevi/test2.png)
![Test 3](https://github.com/foivz/r17037/blob/master/testni%20slu%C4%8Dajevi/test3.png)
![Test 4](https://github.com/foivz/r17037/blob/master/testni%20slu%C4%8Dajevi/test4.png)

# 5. Faza - Uvođenje u rad i održavanje sustava
Aplikacija omogućava rad funkcionalnosti za vozače. Korisnici (vozači) koriste aplikaciju za svoje svrhe, ali aplikacija ne omogućuje nikakvu dodjelu poslova samim korisnicima. Aplikacija je osmišljena da ju koriste vozači, dok im poslove (vozni red i sl.) dodjeljuju mogući administratori ili moderatori putem svoje aplikacije.
