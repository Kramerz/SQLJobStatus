using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySQLJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Thank you for choosing the SQL Agent Job Finder! \n\nThis allows you to find if a Job that you're looking for is stopped and or broken! \nCreated by: Ryan William West (V-Rywes)");
            Console.WriteLine(string.Format("The current time is: " + DateTime.Now + "\n\n"));

            if(args.Length <= 0)
            {
                Console.WriteLine("Oops!, it appears you didn't you the appropriate switches");
                Console.WriteLine("USAGE: \n\n Type /? for more options! \n Press any key to exit..");
                Console.ReadLine();
            }

            if(args.Length == 1 && args[0] == "/?")
            {
                Console.WriteLine("How to use:");
            }

            if(args.Length > 0)
            {
                Program p = new Program();
                List<JobStatus> job = p.GetSQLJob(args[0], args[1]);

                if (job.Count == 0)
                {
                    Console.WriteLine("No Job was found under that, please check the job name again!");
                }
                else
                {
                    var t = job.ElementAt(0);
                    if(t.run_status == "1")
                    {
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine(String.Format("{0} has ran succesfully, no need to do further investigation\n\n", t.job_name));
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine("Thank you for choosing SQL Agent Finder!");
                    }
                    else if(t.run_status == "2")
                    {
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine(String.Format("{0} is currently in the retry state, please continue to monitor in a couple hours to see if it finishes\n\n", t.job_name));
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine("Thank you for choosing SQL Agent Finder!");
                    }
                    else if (t.run_status == "0")
                    {
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine(String.Format("{0} has FAILED, Please contact the owner and inform them of the issue by sending an email\n\n", t.job_name));
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine("Thank you for choosing SQL Agent Finder!");
                    }
                    else if (t.run_status == "3")
                    {
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine(String.Format("{0} was cancelled, no need to do further investigation\n\n", t.job_name));
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine("Thank you for choosing SQL Agent Finder!");
                    }
                    else if (t.run_status == "4")
                    {
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine(String.Format("{0} is currently running, please check back to see if it continues\n\n", t.job_name));
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine("Thank you for choosing SQL Agent Finder!");
                    }
                    else
                    {
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine(String.Format("you just done fucked up AARON"));
                        Console.WriteLine("##########################################################################");
                        Console.WriteLine("Thank you for choosing SQL Agent Finder!");
                    }

                }
            }
        }

        public List<JobStatus> GetSQLJob(string serverName, string jobName)
        {
            using (IDbConnection db = new SqlConnection(String.Format("Data Source={0};Integrated Security=True;Application Name='Checking Job Status (V-Rywes)'", serverName)))
            {
                try
                {
                    var output = db.Query<JobStatus>($"msdb.dbo.sp_help_jobhistory @job_name = '{jobName}'").ToList();
                    return output;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                return null;
            }
        }
    }
}
