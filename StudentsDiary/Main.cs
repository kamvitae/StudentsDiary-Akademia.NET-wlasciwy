using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using StudentsDiary.Properties;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        public bool IsMaximize
        {
            get
            {
                return Settings.Default.IsMaximized;
            }
            set
            {
                Settings.Default.IsMaximized = value;
            }
        }

        public Main()
        {
            InitializeComponent();
            ComboboxHelper.InitComboboxGroups(comboMain);
            RefreshDiary();
            SetColumnsHeader();

            if (IsMaximize)
                WindowState = FormWindowState.Maximized;
        }
        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            SortStudents sortStudents = new SortStudents(students, comboMain.SelectedIndex);
            dgvDiary.DataSource = sortStudents.GetSortedList();
                
        }
        private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Matematyka";
            dgvDiary.Columns[4].HeaderText = "Technologia";
            dgvDiary.Columns[5].HeaderText = "Fizyka";
            dgvDiary.Columns[6].HeaderText = "Język polski";
            dgvDiary.Columns[7].HeaderText = "Język obcy";
            dgvDiary.Columns[8].HeaderText = "Zajęcia dodatkowe";
            dgvDiary.Columns[9].HeaderText = "Nazwa grupy";
            dgvDiary.Columns[10].HeaderText = "Numer grupy";
            dgvDiary.Columns[11].HeaderText = "Uwagi";
        }

       /* private List<Student> FilterStudentsByGroup(List<Student> listToFilter, int groupFilter)
        {
            return listToFilter.Where(x => x.GroupID == groupFilter).ToList();
            //poprzednia wersja bez LINQ

            var filteredList = new List<Student>();
            foreach (var item in listToFilter)
            {
                if (item.GroupID == groupFilter)
                    filteredList.Add(item);
            }
            return filteredList;
        }

        private int CheckSortingMethod()
        {
            if (comboMain.SelectedIndex == 0)
                return 0;
            else
                return comboMain.SelectedIndex;
        }

        private List<Student> OrderStudentByID(List<Student> list)
        {
            var orderBy = list.OrderBy(Student => Student.Id);
            return list = orderBy.ToList();
        }*/


        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();

            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Zaznacz ucznia do edycji");
                return;
            }
            var addEditStudent = new AddEditStudent(
                Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Zaznacz ucznia by go usunąć");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];
            var confirmDelete =
                MessageBox.Show($"Czy na pewno chcesz usunąć ucznia {(selectedStudent.Cells[1].Value.ToString() + " " + selectedStudent.Cells[2].Value.ToString()).Trim() }",
                "Usuwanie Ucznia",
                MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.SerializeToFile(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                IsMaximize = true;
            else
                IsMaximize = false;

            Settings.Default.Save();
        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }
    }
}
