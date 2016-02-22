using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Importar_Datos_de_excel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            // importing from excel file

            string connection = "Provider = Microsoft.Jet.OleDb.4.0;Data Source = C:/Users/Oleg/Documents/Visual Studio 2015/Projects/Importar Datos de excel/Data.xlsx;Extended Properties = \"Excel 8.0;HDR = Yes\"";

            OleDbConnection connector = default(OleDbConnection);
            connector = new OleDbConnection(connection);
            connector.Open();

            OleDbCommand consult = default(OleDbCommand);
            consult = new OleDbCommand("select * from [Sheet1$]", connector);

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = consult;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            connector.Close();

            // exporting to database

            SqlConnection mySqlConnection = new SqlConnection("server = localhost user id = root  Password = blank  database = censuses");
            mySqlConnection.Open();

            SqlBulkCopy export = default(SqlBulkCopy);
            export = new SqlBulkCopy(mySqlConnection);
            export.DestinationTableName = "censuses";
            export.WriteToServer(ds.Tables[0]);

            mySqlConnection.Close();

            MessageBox.Show("Exportation Complete");
        }
    }
}
