//mysql használata a xampphoz
using MySqlConnector;

namespace WINFORM
{
    public partial class Form1 : Form
    {

        MySqlConnection kapcsolat;

        public Form1()
        {
            InitializeComponent();


            //Adatbázis Kapcsolat és adatbetöltés
            //Beállítjuk az adatbázis kapcsolatot, és lekérdezzük az adatokat, amelyeket egy DataGridView komponensben jelenítünk meg.
            string szerver;
            string sqlParancs;

            szerver = "server=localhost;userid=root;password=;database=tallest_buildings";
            kapcsolat = new MySqlConnection(szerver);

            sqlParancs = "SELECT rank, building_name, floors, city FROM buildings WHERE 1; ";
            kapcsolat.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlParancs, kapcsolat);


            //DataSet: Egy adatstruktúra, amely adatokat tartalmazó táblák gyűjteményét tárolja.
            //Ez egy memóriában lévő adatbázis, amely több táblát és azok közötti kapcsolatokat is képes tárolni.
            DataSet adattarolas = new DataSet();
            adapter.Fill(adattarolas);

            //Adatok Kötése a DataGridView-hez:
            //A BindingSource segítségével az adatokat hozzárendeljük a DataGridView-hez.
            BindingSource adatbetoltes = new BindingSource();
            adatbetoltes.DataSource = adattarolas.Tables[0];
            dataGridView_adatmegjelenites.DataSource = adatbetoltes;

            //nem kell lezárni
            //connection.Close();
        }

        private void button_orszag_Click(object sender, EventArgs e)
        {
            string adat = dataGridView_adatmegjelenites.SelectedRows[0].Cells[1].Value.ToString();
            string sqlParancs = "SELECT city FROM buildings WHERE building_name ='" + adat + "';";
            MySqlCommand sqlCommand = new MySqlCommand(sqlParancs, kapcsolat);

            //Lekérdezés végrehajtása: ExecuteScalar() segítségével végrehajtjuk a lekérdezést és az eredményt megjelenítjük egy címkén (label1).
            string varos = sqlCommand.ExecuteScalar().ToString();
            label_orszag.Text = "Ország: " + varos;
        }

        private void button_osszemeletek_Click(object sender, EventArgs e)
        {
            string adat = dataGridView_adatmegjelenites.SelectedRows[0].Cells[2].Value.ToString();
            string sqlParancs = "SELECT COUNT(building_name) FROM buildings WHERE height_m > 400 ;";
            MySqlCommand sqlCommand = new MySqlCommand(sqlParancs, kapcsolat);
            string varos = sqlCommand.ExecuteScalar().ToString();
            label_osszemelet.Text = "A 400 m-nél magasabb épületek száma: " + varos;
        }
    }
}