using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        // public delegate void MySimpleDelegate();
        // public event MySimpleDelegate StudentAdded;

        private int _studentId;
        private Student _student;

        private FileHelper<List<Student>> _fileHelper =
           new FileHelper<List<Student>>(Program.FilePath);

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            ComboboxHelper.InitComboboxGroups(comboAddEdit);
            
            _studentId = id;

            GetStudentData();
            tbName.Select();
        }

        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edytuj dane ucznia";

                List<Student> students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                    throw new Exception("Brak użytkownika o podanym Id");

                FillTextBoxes();
            }
        }

        private void FillTextBoxes()
        {
            tbID.Text = _student.Id.ToString();
            tbName.Text = _student.FirstName;
            tbSurname.Text = _student.Surname;
            tbMath.Text = _student.Math;
            tbTech.Text = _student.Technology;
            tbPhys.Text = _student.Physics;
            tbPolish.Text = _student.PolishLanguage;
            tbForeign.Text = _student.ForeignLanguage;
            rtbComments.Text = _student.Comments;
            chBExtraClasses.Checked = _student.ExtraClasses;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<Student> students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
            {
                students.RemoveAll(x => x.Id == _studentId);
            }
            else
                AssignIdToStudent(students);

            AddNewUserToList(students);

            _fileHelper.SerializeToFile(students);

            Close();
        }

        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbName.Text,
                Surname = tbSurname.Text,
                Math = tbMath.Text,
                Technology = tbTech.Text,
                Physics = tbPhys.Text,
                PolishLanguage = tbPolish.Text,
                ForeignLanguage = tbForeign.Text,
                Comments = rtbComments.Text,
                ExtraClasses = chBExtraClasses.Checked,
                GroupID = comboAddEdit.SelectedIndex,
                GroupName = comboAddEdit.Text
            };
            if (!chBExtraClasses.Checked)
            {
                student.GroupID = 0;
            }
            students.Add(student);
        }

        private void AssignIdToStudent(List<Student> students)
        {
            var studentWithHigherID = students
                .OrderByDescending(x => x.Id).FirstOrDefault();

            _studentId = studentWithHigherID == null ?
                1 : studentWithHigherID.Id + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
