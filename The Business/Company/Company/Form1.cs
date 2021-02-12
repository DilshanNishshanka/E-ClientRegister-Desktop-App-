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
using System.Globalization;

namespace Company
{
    public partial class Form1 : Form
    {
        string path = @"Data Source=DESKTOP-GT6UN2V\SQLEXPRESS;Initial Catalog=company;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            display();
        }


        private void Form1_Load(object sender, EventArgs e)   //Combobox data loading
        {
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");

            cmbStatus.Items.Add("A");
            cmbStatus.Items.Add("B");
            cmbStatus.Items.Add("C");
            cmbStatus.Items.Add("D");

            cmbTitle.Items.Add("T");
            cmbTitle.Items.Add("D");
            cmbTitle.Items.Add("Q");
            cmbTitle.Items.Add("E");
        }

        private void btnSave_Click(object sender, EventArgs e)  //Insert Values
        {

            if (txtServiceNo.Text == "" || txtNIC.Text == "" || txtInitials.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || txtAddress.Text == "" || txtTown.Text == "" || txtCity.Text == "" || txtContactNo.Text == "" || txtDivisionCode.Text == "" || txtDivisionCode.Text == "" || txtDepartmentCode.Text == "" || txtLocationCode.Text == "" || txtCATCode.Text == "")  //Check the fields empty or not
            {
                MessageBox.Show("Please Fill the blank");

            }

            else
            {
                try
                {

                    cmd = new SqlCommand("insert into Employee (ServiceNo,Title,NICNO,Initials,FirstName,LastName,StreetAddress,Town,City,Gender,DOB,ContactNo,DivisionCode,DepartmentCode,LocationCode,CATCode,RecruitmentDate,RetirementDate,Status) values('" + txtServiceNo.Text + "','" + cmbTitle.Text.ToString() + "','" + txtNIC.Text + "','" + txtInitials.Text + "','" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtAddress.Text + "','" + txtTown.Text + "','" + txtCity.Text + "','" + cmbGender.Text.ToString() + "','" + this.dtpDOB.Value.ToString("MM-dd-yyyy") + "','" + txtContactNo.Text + "','" + txtDivisionCode.Text + "','" + txtDepartmentCode.Text + "','" + txtLocationCode.Text + "','" + txtCATCode.Text + "','" + this.dtpRecruitmentDate.Value.ToString("MM-dd-yyyy") + "','" + this.dtpRetirementate.Value.ToString("MM-dd-yyyy") + "','" + cmbStatus.Text.ToString() + "')", con); //Insert SQL Query
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Your data has been inserted succesfully");
                    display();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)  //Reset Button
        {
            txtServiceNo.Text = "";
            txtNIC.Text = "";
            txtInitials.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtTown.Text = "";
            txtCity.Text = "";
            txtContactNo.Text = "";
            txtDivisionCode.Text = "";
            txtDepartmentCode.Text = "";
            txtLocationCode.Text = "";
            txtCATCode.Text = "";

        }

        public void display()         //Display Table
        {
            try
            {
                dt = new System.Data.DataTable();
                con.Open();
                adpt = new SqlDataAdapter("select * from Employee", con);
                adpt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)     //Update button
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("update Employee set Title='" + cmbTitle.Text.ToString() + "',NICNO='" + txtNIC.Text + "',Initials='" + txtInitials.Text + "',FirstName='" + txtFirstName.Text + "',LastName='" + txtLastName.Text + "',StreetAddress='" + txtAddress.Text + "',Town='" + txtTown.Text + "',City='" + txtCity.Text + "',Gender='" + cmbGender.Text.ToString() + "',DOB='" + dtpDOB.Value.ToString("MM-dd-yyyy") + "',ContactNo='"+txtContactNo.Text+ "',DivisionCode='"+txtDivisionCode.Text+ "',DepartmentCode='"+txtDepartmentCode.Text+ "',LocationCode='"+txtLocationCode.Text+ "',CATCode='"+txtCATCode.Text+ "',RecruitmentDate='"+dtpRecruitmentDate.Value.ToString("MM - dd - yyyy") + "',RetirementDate='"+dtpRetirementate.Value.ToString("MM - dd - yyyy") + "',Status='"+cmbStatus.Text.ToString()+ "'where ServiceNo='"+txtServiceNo.Text+"'",con); //update query
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Your Data has been Updated");
                display();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)  //Datagrid View to TEXTBOX
        {
            try
            {
                txtServiceNo.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var currentRow = dataGridView1.CurrentRow;
                var selectedTitle = currentRow.Cells[1].Value;                                 //Title Combobox
                var title = cmbTitle.Items.IndexOf(selectedTitle.ToString());
                cmbTitle.SelectedIndex = title;
                txtNIC.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtInitials.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtTown.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtCity.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                var selectedGender = currentRow.Cells[9].Value;                               //Gender Combobox
                var gender = cmbGender.Items.IndexOf(selectedGender.ToString());
                cmbGender.SelectedIndex = gender;
                txtContactNo.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                txtDivisionCode.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                txtDepartmentCode.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                txtLocationCode.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                txtCATCode.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                var selecteStatus = currentRow.Cells[18].Value;                                          //Status Combobox
                var status = cmbStatus.Items.IndexOf(selecteStatus.ToString());
                cmbStatus.SelectedIndex = status;


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)                   //Delete Button
        {
            con.Open();
            cmd = new SqlCommand("delete From Employee where ServiceNo='" + txtServiceNo.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Your Record has been Deleted");
            display();
        }
    }
}
