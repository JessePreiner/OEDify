using OEDify;
using System;
namespace testclient
{
    class Program
    {
        static void Main(string[] args)
        {
            OEDClient client = new OEDClient();
            var meep = client.Do();

            var morp = meep.Result;

            morp.results.ForEach((result) =>
            {
                Console.WriteLine(result.word);
            });

            Console.ReadLine();
        }
    }
}