﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Management_System
{
    public partial class frm_Search_Student_Details : Form
    {
        public frm_Search_Student_Details()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DB_Student_Management_System;Integrated Security=True");

        void Con_Open()
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
        }

        void Con_Close()
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
        }

        void Clear_Controls()
        {
            tb_Student_Id.Clear();
            tb_Name.Clear();
            tb_Mobile_No.Clear();
            dtp_DOB.ResetText();
            cmb_Course.SelectedIndex = -1;

            tb_Student_Id.Focus();
        }

        private void frm_Search_Student_Details_Load(object sender, EventArgs e)
        {
            Clear_Controls();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Con_Open();

            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;

            Cmd.CommandText = "Select * from Student_Details Where Student_Id = " + tb_Student_Id.Text + "";

            var Obj = Cmd.ExecuteReader();

            if (Obj.Read())
            {
                tb_Name.Text = Obj.GetString(Obj.GetOrdinal("Name"));
                dtp_DOB.Text = (Obj["DOB"].ToString());
                tb_Mobile_No.Text = (Obj["Mobile_No"].ToString());
                cmb_Course.Text = Obj.GetString(Obj.GetOrdinal("Course"));
            }
            else
            {
                MessageBox.Show("Given Information is Not Available...", "No Record Found...!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Con_Close();
        }

        private void btn_Add_New_Student_Click(object sender, EventArgs e)
        {
            frm_Add_New_Student ANS = new frm_Add_New_Student();
            ANS.Show();
            this.Hide();
        }

        private void btn_View_All_Student_Click(object sender, EventArgs e)
        {
            frm_View_All_Students VAS = new frm_View_All_Students();
            VAS.Show();
            this.Hide();
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            DialogResult Res = MessageBox.Show(" Are You Sure....!!!", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Res == DialogResult.Yes)
            {
                frm_Login LF = new frm_Login();

                LF.Show();

                this.Hide();
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Clear_Controls();
        }
    }
}