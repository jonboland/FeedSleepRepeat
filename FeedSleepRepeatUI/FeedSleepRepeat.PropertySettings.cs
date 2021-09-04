using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedSleepRepeatUI
{
    partial class FeedForm
    {
        private void ApplyPropertySettings()
        {
            dateOfBirthPicker.MaxDate = DateTime.Today;
            datePicker.MaxDate = DateTime.Today;
            activitiesListBox.KeyDown += new KeyEventHandler(activitiesListBox_KeyDown);
            // TODO: Configure activity pickers so activities are always added to the correct date
            // (EndDate.Date - StartDate.Date).Days
        }
    }
}
