using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public static class ComboboxHelper
    {
        internal static void InitComboboxGroups(ComboBox comboBox)
        {
            var _groups = new List<Group>
            {
                new Group { Id = 0, Name = "-- brak --" },
                new Group { Id = 1, Name = "Angielski w RPG" },
                new Group { Id = 2, Name = "Karate" },
                new Group { Id = 3, Name = "Modelarstwo" },
                new Group { Id = 4, Name = "Sekcja wspinaczkowa" }
            };
            comboBox.DataSource = _groups;
            comboBox.ValueMember = "Id";
            comboBox.DisplayMember = "Name";
        }
    }
}
