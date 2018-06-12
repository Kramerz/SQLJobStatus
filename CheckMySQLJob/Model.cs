using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySQLJob
{
    public class JobStatus 
    {
       // public string job_id { get; set; }
        public string job_name { get; set; }
        public string run_status { get; set; }
        public string run_date { get; set; }
        public string run_time { get; set; }
        public string server { get; set; }
        public string retries_attempted { get; set; }

   
    }
}
