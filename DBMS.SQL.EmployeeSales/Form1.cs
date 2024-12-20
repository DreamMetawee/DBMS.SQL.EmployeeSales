
using Microsoft.Data.SqlClient;
using System.Data;

namespace DBMS.SQL.EmployeeSales
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void ConnectDB()
        {
            string server = @".\SQLEXPRESS";
            string db = "northwind";
            string strCon = string.Format(@"Data Source={0};Initial Catalog={1};"
                            + "Integrated Security=True;Encrypt=False", server, db);

            conn = new SqlConnection(strCon);
            conn.Open();
        }

        private void DisconnectDB()
        {
            conn.Close();
        }

        private void showdata(string sql, DataGridView dgv)
        {
            da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            dgvEmployees.DataSource = ds.Tables[0];

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectDB();

            string sqlQuery = "select * from EmployeeList";
            showdata(sqlQuery, dgvEmployees);
        }




        private void dgvEmployees_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int id = Convert.ToInt32(dgvEmployees.CurrentRow.Cells[0].Value);
                string sqlQuery = "select * from OrderList2 where EmployeeID = @id";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvOrderList.DataSource = ds.Tables[0];

            }
        }
    }
}
