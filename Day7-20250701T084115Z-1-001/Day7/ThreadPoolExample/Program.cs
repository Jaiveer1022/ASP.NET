namespace ThreadPoolExample
{
    internal class Program
    {
        static void Main1()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Func1), "passed value");
            //ThreadPool.QueueUserWorkItem(Func1, "passed value");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Func1));

            Console.ReadLine();
        }
        static void Main()
        {
            int workerthreads, iothreads;

            //ThreadPool.GetAvailableThreads(out workerthreads, out iothreads);
            //ThreadPool.GetMaxThreads(out workerthreads, out iothreads);
            ThreadPool.GetMinThreads(out workerthreads, out iothreads);
            //ThreadPool.SetMinThreads
            //ThreadPool.SetMaxThreads
            Console.WriteLine(workerthreads);
            Console.WriteLine(iothreads);

            Console.ReadLine();
        }
        static void Func1(object o)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("First : " + i + o);
            }
        }
    }
}
