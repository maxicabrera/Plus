using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;

namespace Plus54PortfolioRedesign2014.Web.AppCode.Helpers.Progress
{
    public class Task
    {
        public float Progress { get; set; }
        public StringBuilder Info { get; set; }
        public string InfoText { get { return Info.ToString(); } }
        public bool Completed { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public long TotalItems { get; set; }
        public long ErrorItems { get; set; }
        public long CurrentItem { get; set; }

        public Task()
        {
            Status = TaskStatus.NotStarted;
            Completed = false;
            Progress = 0;
            Info = new StringBuilder();
        }
    }

    public enum TaskStatus
    {
        [Description("Not Started")]
        NotStarted,
        [Description("Running")]
        Running,
        [Description("Failure")]
        Error,
        [Description("Success")]
        Success
    }
}