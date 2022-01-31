using Microsoft.Extensions.Configuration;
using SalesRepServices.Services_Interfaces;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace SalesRepServices.Services_ForSalesRep
{
    public class ReportsStatic : IReportsInLog
    {
        public IConfiguration Configuration { get; }
        public void AnotherExeption(Exception ex)
        {
            var fileName = Configuration.GetConnectionString("DefaultConnection");
            var writePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
                {
                    sw.WriteLine(" ");
                    sw.WriteLine("=============================================");
                    sw.WriteLine("-------------------Another error was in" + DateTime.Now.ToString(new CultureInfo("en-US")) + "-----------------------");
                    sw.WriteLine(ex.Message + ex.InnerException);
                    sw.WriteLine("------------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
