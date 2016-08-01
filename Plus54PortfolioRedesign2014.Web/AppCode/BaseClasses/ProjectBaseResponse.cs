using Plus54PortfolioRedesign2014.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plus54PortfolioRedesign2014.Web.AppCode.BaseClasses
{
    public class ProjectBaseResponse<T>
    {
        public int total;
        public int page;
        public List<T> results;

        public ProjectBaseResponse()
        {
            results = new List<T>();
        }

        public ProjectBaseResponse(int page, int total)
            : this()
        {
            this.total = total;
            this.page = page;
        }

        public ProjectBaseResponse(int page, int total, List<T> results)
            : this(page, total)
        {
            this.results = results;
        }
    }
}