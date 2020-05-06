using Doormat.Pages.TiketManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls.TaskBoard;

namespace BindComboboxToEnum
{
   public class Class1
    {
        public ObservableCollection<TaskBoardCardModel> Data { get; set; }
        public Class1()
        {
            Data = GetTasks();
        }
        public ObservableCollection<TaskBoardCardModel> GetTasks()
        {
            ObservableCollection<TaskBoardCardModel> tasks = new ObservableCollection<TaskBoardCardModel>
        {
            new TaskBoardCardModel() { Assignee = "Nancy Davolio", Description = "Task Description", State = "In Progress", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Andrew Fuller", Description = "Task Description", State = "Not Done", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Janet Leverling", Description = "Task Description", State = "Not Done", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Margaret Peacock", Description = "Task Description", State = "Not Done", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Steven Buchanan", Description = "Task Description", State = "Done", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Michael Suyama", Description = "Task Description", State = "Done", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Robert King", Description = "Task Description", State = "Done", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Laura Callahan", Description = "Task Description", State = "In Progress", Title = "Task Title" },

            new TaskBoardCardModel() { Assignee = "Anne Dodsworth", Description = "Task Description", State = "In Progress", Title = "Task Title" }
        };

            return tasks;
        }
    }
}
