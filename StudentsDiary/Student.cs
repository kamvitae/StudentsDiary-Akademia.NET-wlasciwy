using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiary
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLanguage { get; set; }
        public string ForeignLanguage { get; set; }
        public bool ExtraClasses { get; set; }
        public string GroupName { get; set; }
        public int GroupID{ get; set; }
        public string Comments { get; set; }
    }
}
