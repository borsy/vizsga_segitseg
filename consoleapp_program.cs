namespace ConsoleApp
{
    class Program
    {

        static List<Epulet> epuletek = new List<Epulet>();

        static void Main(string[] args)
        {
            beolvasas();
            feladat1();
            feladat2();
            feladat3();
            feladat4();

            Console.WriteLine("Program vége!");
            Console.ReadLine();


        }

        private static void feladat4() //Olvassatok be a billenytyűzetről egy évszámot. Összesen milyen magasak azok az épületek, amelyek ez előtt az évszám előtt épültek?
        {
            Console.WriteLine("4. feladat:");
            Console.WriteLine("Kérek egy évszámot:");

            int evszam = int.Parse(Console.ReadLine());
            double ossz_magassag = 0;

            foreach (var item in epuletek)
            {
                if (item.epites_eve < evszam)
                {
                    ossz_magassag = item.magassag_ft + ossz_magassag;
                }
            }

            Console.WriteLine("Összmagasság: " + ossz_magassag);
        }

        private static void feladat3() //Készítsetek egy új metódust Tutiepulet() néven az Epulet osztályban, amely egy logiakai értékkel tér vissza attól függően, hogy az épület 500m-nél magasabb és 100 emelettel többel rendelkezik-e?
        {

        }

        private static void feladat2() //Menny olyan épület van, amelyik neve nem tartalmazza a "Tower" szót?
        {
            int db = 0;
            for (int i = 0; i < epuletek.Count; i++)
            {
                if (!epuletek[i].nev.Contains("Tower"))
                {
                    db++;
                }

            }
            Console.WriteLine("2. feladat:");
            Console.WriteLine($"\t{db} ");
        }

        private static void feladat1() //Menny épület adata lett beolvasva
        {
            Console.WriteLine("1. feladat:");
            Console.WriteLine($"\tAz épületek száma: {epuletek.Count} db");
        }

        private static void beolvasas()
        {
            using (StreamReader olvaso = new StreamReader("100_tallest_javitott4.csv"))
            {
                while (!olvaso.EndOfStream)
                {
                    string[] sor = olvaso.ReadLine().Split(';');
                    Epulet epulet = new Epulet(int.Parse(sor[0]), sor[1], sor[2], double.Parse(sor[3]), double.Parse(sor[4]), int.Parse(sor[5]), int.Parse(sor[6]));
                    epuletek.Add(epulet);
                }
            }
        }
    }
}