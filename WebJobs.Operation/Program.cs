using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebpay.WebJobs.Operation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                //Task.Run(async () => { await callWebApi(); }).GetAwaiter().GetResult();
                var currentOperationName = Convert.ToString(ConfigurationManager.AppSettings["WebJobCurrentOperationName"]);
                var webJobOperations = OperationConfiguration.GetOperations();

                if (webJobOperations != null && webJobOperations.Count > 0)
                {
                    var currentOperation = webJobOperations.Find(n => n.OperationName == currentOperationName);

                    if (currentOperation != null)
                    {
                        Console.WriteLine("Operation [" + currentOperation.OperationName + "] processing start");

                        currentOperation.Execute().Wait() ;
                        //MainAsync().Wait();
                        Console.WriteLine("Operation [" + currentOperation.OperationName + "] processed successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Exception in Main Method");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Exception Message" + ex.Message);
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
