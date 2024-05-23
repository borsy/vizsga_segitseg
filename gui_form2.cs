namespace GUI20240502
{
    public partial class Form1 : Form
    {
        MySqlConnection adatbazis_kapcsolat = Adatbazis_kapcsolat.adatbazis_kapcsolat;
        List<string> orszagok;
        public Form1()
        {
            InitializeComponent();
            orszagok = new List<string>();
        }

        void button_orszag_Click(object sender, EventArgs e)
        {
            dataGridView_adatmegjelenites.Rows.Clear();
            dataGridView_adatmegjelenites.Columns.Clear();
            dataGridView_adatmegjelenites.Columns.Add(new DataGridViewColumn()
            {
                Name = "Országok",
                CellTemplate = new DataGridViewTextBoxCell() { }
            });
            foreach (string orszag in orszagok)
            {
                dataGridView_adatmegjelenites.Rows.Add(orszag);
            }
        }

        void button_osszemelet_Click(object sender, EventArgs e)
        {
            if (dataGridView_adatmegjelenites.SelectedCells[0].Value == null) return;

            string city = "";
            int osszEmelet = 0;
            city = dataGridView_adatmegjelenites.SelectedCells[0].Value.ToString();
            MySqlCommand parancs = adatbazis_kapcsolat.CreateCommand();
            parancs.CommandText = $"SELECT city, SUM(floors) AS osszemelet FROM buildings WHERE city LIKE '%{city}%' GROUP BY CITY";
            adatbazis_kapcsolat.Open();
            MySqlDataReader olvaso = parancs.ExecuteReader();
            while (olvaso.Read())
            {
                city = olvaso.GetString("city");
                osszEmelet = olvaso.GetInt32("osszemelet");
            }
            adatbazis_kapcsolat.Close();

            dataGridView_adatmegjelenites.Rows.Clear();
            dataGridView_adatmegjelenites.Columns.Clear();

            dataGridView_adatmegjelenites.Columns.Add(new DataGridViewColumn()
            {
                Name = city,
                CellTemplate = new DataGridViewTextBoxCell() { }
            });
            dataGridView_adatmegjelenites.Rows.Add(osszEmelet.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> varosok = new List<string>();
            MySqlCommand parancs = adatbazis_kapcsolat.CreateCommand();
            parancs.CommandText = "SELECT DISTINCT(city) as varos FROM buildings";
            adatbazis_kapcsolat.Open();
            MySqlDataReader olvaso = parancs.ExecuteReader();
            while (olvaso.Read())
            {
                varosok.Add(olvaso.GetString("varos"));
            }
            adatbazis_kapcsolat.Close();
            for (int i = 0; i < varosok.Count; i++)
            {
                string orszag = varosok[i].Split('(')[1].Split(')')[0];
                if (!orszagok.Contains(orszag))
                    orszagok.Add(orszag);
            }
        }
    }
}