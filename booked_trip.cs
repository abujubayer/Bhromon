using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Poth_Dekho
{
    public partial class booked_trip : Form
    {
        string cs = "Data Source=NAZIB;Initial Catalog=Bhromon;Integrated Security=True";
        //string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        int travellerId;
        public booked_trip(int travellerId)
        {
            InitializeComponent();
            this.travellerId = travellerId;
            BindGridView();
            
            
        }


        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd =new SqlCommand("select * from package_register_TBL where travellerId= @travellerId",con);
            cmd.Parameters.AddWithValue("@travellerId", travellerId.ToString());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

 
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            //DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            //dgv = (DataGridViewImageColumn)dataGridView1.Columns[4];
            //dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowTemplate.Height = 40;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tripNo.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void cancelTrip_Click(object sender, EventArgs e)
        {
            try
            {


                SqlConnection con = new SqlConnection(cs);
                string query = "delete from package_register_TBL where travellerId =@travellerId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@travellerId", int.Parse(tripNo.Text));

                con.Open();
                cmd.ExecuteNonQuery();

            }
                /*
                Console.WriteLine(a);
                if (a > 0)
                {
                    MessageBox.Show("Data Deleted Successfully");
                    BindGridView();
                    tripNo.Text = "0";
                }
                else
                {
                    MessageBox.Show("Data Not Deleted");
                }
                */
                catch(Exception ex)
            {
                MessageBox.Show("data not deleted");
            }
            
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            traveller_dashboard f = new traveller_dashboard(travellerId); // travellerId need to insert
            f.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
