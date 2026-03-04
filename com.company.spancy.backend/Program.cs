using Nancy.Hosting.Self;

namespace com.company.spancy.backend
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.WriteLine("Nancy now listening - navigating to http://localhost:1234. Press enter to stop");
                Console.ReadLine();
            }
        }
    }
}