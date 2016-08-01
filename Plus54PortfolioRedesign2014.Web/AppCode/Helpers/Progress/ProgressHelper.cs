using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Plus54PortfolioRedesign2014.Web.AppCode.Helpers.Progress
{
    public class ProgressHelper
    {
        private Cache cache;
        private Guid idTask;
        public event ProgressFlushedEventHandler ProgressFlushed;
        public event ProgressLoggedInfoEventHandler ProgressLoggedInfo;
        public event ProgressCompletedEventHandler ProgressCompleted;
        public bool ProgressInfoFlushed = false;
        public string ImportLogFilePath;

        public ProgressHelper(Cache pCache, Guid pIdTask)
        {
            cache = pCache;
            idTask = pIdTask;
            cache.Add(idTask.ToString() + "progressHelper", this, null, Cache.NoAbsoluteExpiration, new TimeSpan(3, 0, 0), System.Web.Caching.CacheItemPriority.Normal, null);
        }

        public void Initialize(string importLogFilePath)
        {
            ImportLogFilePath = importLogFilePath;
        }

        public void SetTotalItems(long totalItems)
        {
            var currentTask = GetCurrentTask();
            currentTask.TotalItems = totalItems;
        }

        public static ProgressHelper GetProgressHelper(Cache pCache, Guid idTask)
        {
            var obj = pCache[idTask + "progressHelper"];

            if (obj == null)
                return null;
            else
                return pCache[idTask + "progressHelper"] as ProgressHelper;

        }

        private void AddTask(Task newTask)
        {
            cache.Add(idTask.ToString(), newTask, null, Cache.NoAbsoluteExpiration, new TimeSpan(3, 0, 0), System.Web.Caching.CacheItemPriority.Normal, null);
        }


        public Task GetCurrentTask()
        {
            var obj = cache[idTask.ToString()];

            Task t = null;

            if (obj != null)
                t = obj as Task;

            return t;
        }

        public void RaiseFlushedEventHandler()
        {
            ProgressInfoFlushed = true;
            if (ProgressFlushed != null)
                ProgressFlushed(this, null);
        }

        public void LogInfo(long currentItem, string info, bool errorProcessRow = false, bool endTask = false)
        {
            var currentTask = GetCurrentTask();

            if (currentTask != null)
            {
                if (currentTask.TotalItems > 0)
                {
                    currentTask.Progress = 100 * ((float)currentItem / currentTask.TotalItems) - 1;
                }
                if (errorProcessRow)
                {
                    currentTask.ErrorItems++;
                }
                currentTask.CurrentItem = currentItem;
                currentTask.Info.Append(info);
                currentTask.Completed = endTask && currentItem >= currentTask.TotalItems;

                if (ProgressLoggedInfo != null)
                    ProgressLoggedInfo(currentTask, info, ImportLogFilePath);

                //if (currentTask.Completed && ProgressCompleted != null)
                //    ProgressCompleted(currentTask, info, ImportLogFilePath);
            }
            ProgressInfoFlushed = false;
        }

        public void LogCompleted()
        {
            var currentTask = GetCurrentTask();

            if (currentTask != null)
            {
                if (currentTask.Completed && ProgressCompleted != null)
                    ProgressCompleted(currentTask, ImportLogFilePath);
            }
            ProgressInfoFlushed = false;
        }


        public void StartNewTask()
        {
            var task = new Task();
            task.Status = TaskStatus.Running;
            task.StartDate = DateTime.Now;

            AddTask(task);
        }

        public void EndTask(TaskStatus status)
        {
            var currentTask = GetCurrentTask();

            currentTask.EndDate = DateTime.Now;
            currentTask.Status = TaskStatus.Success;
        }
    }

    public delegate void ProgressFlushedEventHandler(object sender, EventArgs e);
    public delegate void ProgressLoggedInfoEventHandler(Task currentTask, string appendedInfo, string importLogFilePath);
    public delegate void ProgressCompletedEventHandler(Task currentTask, string importLogFilePath);

}