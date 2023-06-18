﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Poth_Dekho
{
    public partial class admin_guide_information : Form
    {
        //string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        string cs = "Data Source=NAZIB;Initial Catalog=Bhromon;Integrated Security=True";
        string username = "";

        public admin_guide_information(string username)
        {
            InitializeComponent();
            this.username = username;
            BindGridView();
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from TourGuideRegi";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            /* to desable image function 
             {
              DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[5];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            }
             */

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowTemplate.Height = 80;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_dashboard f = new admin_dashboard(username);
            f.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from TourGuideRegi where Username=@username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", tripNo.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            MessageBox.Show("Guide Deleted Successfully");
            BindGridView();
            tripNo.Text = "0";
            /*
            Console.WriteLine(a);
            if (a > 0)
            {
                MessageBox.Show("Guide Deleted Successfully");
                BindGridView();
                tripNo.Text = "0";
            }
            else
            {
                MessageBox.Show("Guide Not Deleted. Don't forget to double click on cell");
            }
            */

        }

        private void btnAddGuide_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_add_guide f = new admin_add_guide(username);
            f.Show();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tripNo.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tripNo.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
