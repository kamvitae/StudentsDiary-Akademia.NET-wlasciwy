using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiary
{
    class SortStudents // <T> where T : new()
    {
        private List<Student> students;
        private int sortMethod;
        public SortStudents (List<Student> listToSort, int comboMainSelectedIndex)
        {
            students = listToSort;
            sortMethod = comboMainSelectedIndex;
        }
        private int CheckSortingMethod(int sortMethod) //deFacto niepotrzebna metoda
        {
            if (sortMethod == 0)
                return 0;
            else
                return sortMethod;
        }
        private List<Student> OrderStudentByID(List<Student> list)
        {
            var orderBy = list.OrderBy(Student => Student.Id);
            return list = orderBy.ToList();
        }

        private List<Student> FilterStudentsByGroup(List<Student> listToFilter, int groupFilter)
        {
            return listToFilter.Where(x => x.GroupID == groupFilter).ToList();
        }

        public List<Student> GetSortedList()
        {
            ;
            if (CheckSortingMethod(sortMethod)==0) // if(sortMethod == 0)
                return OrderStudentByID(students);
            else
                return OrderStudentByID(FilterStudentsByGroup(students, sortMethod));
        }
    }
}
