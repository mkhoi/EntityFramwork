using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        DatabaseContext db = new DatabaseContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbGender.Items.Add("Male"); //Adding values for Gender Combobox
            cmbGender.Items.Add("Female");
            Display();
        }
        
        public void Display()
        {
           
            using (DatabaseContext db = new DatabaseContext())
            {
                List<StudentInformation> _studentList = new List<StudentInformation>();
                _studentList = db.Students.ToList();
                dataGridView.DataSource = _studentList;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            StudentInformation stu = new StudentInformation();
            stu.Name = txtName.Text;
            stu.Age = Convert.ToInt32(txtAge.Text);
            stu.City = txtCity.Text;
            stu.Gender = cmbGender.SelectedItem.ToString();
            bool result = SaveStudentDetails(stu);
            ShowStatus(result, "Save");
        }

        public bool SaveStudentDetails(StudentInformation stu)
        {
            bool result = false;
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Students.Add(stu);

                db.SaveChanges();
                result = true;
                
      
            }
            return result;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    lbl_ID.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtAge.Text = row.Cells[2].Value.ToString();
                    txtCity.Text = row.Cells[3].Value.ToString();
                    cmbGender.SelectedItem = row.Cells[4].Value.ToString();
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            StudentInformation stu = SetValues(Convert.ToInt32(lbl_ID.Text), txtName.Text, Convert.ToInt32(txtAge.Text), txtCity.Text, cmbGender.SelectedItem.ToString());
            bool result = UpdateStudentDetails(stu);
            ShowStatus(result, "Update");
        }

        public bool UpdateStudentDetails(StudentInformation stu)
        {
            bool result = false;
            using (DatabaseContext db = new DatabaseContext())
            {
                StudentInformation _student = db.Students.Where(x => x.Id == stu.Id).Select(x => x).FirstOrDefault();
                _student.Name = stu.Name;
                _student.Age = stu.Age;
                _student.City = stu.City;
                _student.Gender = stu.Gender;
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            StudentInformation stu = SetValues(Convert.ToInt32(lbl_ID.Text), txtName.Text, Convert.ToInt32(txtAge.Text), txtCity.Text, cmbGender.SelectedItem.ToString());
            bool result = DeleteStudentDetails(stu);
            ShowStatus(result, "Delete");
        }

        public bool DeleteStudentDetails(StudentInformation stu)
        {
            bool result = false;
            using (DatabaseContext db = new DatabaseContext())
            {
                StudentInformation _student = db.Students.Where(x => x.Id == stu.Id).Select(x => x).FirstOrDefault();
                db.Students.Remove(_student);
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public void ShowStatus(bool result, string v)
        {
            if (result)
            {
                if (v.ToUpper() == "SAVE")
                {
                    MessageBox.Show("Saved Successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (v.ToUpper() == "UPDATE")
                {
                    MessageBox.Show("Updated Successfully!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deleted Successfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Somthing went wrong!. Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ClearFields();
            Display();
        }

        public void ClearFields()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtCity.Text = "";
            cmbGender.SelectedIndex = -1;
        }

        public StudentInformation SetValues(int Id, string Name, int Age, string City, string Gender)
        {
            StudentInformation stu = new StudentInformation();
            stu.Id = Id;
            stu.Name = Name;
            stu.Age = Age;
            stu.City = City;
            stu.Gender = Gender;
            return stu;
        }
    }
}
