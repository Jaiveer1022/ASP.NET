namespace ThreadingExamples1
{
    internal class Program
    {
        static void Main0()
        {
            Thread t1 = new Thread(new ThreadStart(Func1));
            //Thread t2 = new Thread(new ThreadStart(Func2));
            Thread t2 = new Thread(Func2);
            t1.Start();
            t2.Start();
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Main : " + i);
            }
        }
        static void Main1()
        {
            Thread t1 = new Thread(new ThreadStart(Func1));
            Thread t2 = new Thread(new ThreadStart(Func2));
            t1.IsBackground = true;
            t2.IsBackground = true;
            t1.Start();
            t2.Start();
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine("Main : " + i);
            }
        }
        //background threads - Main thread does not wait for
        //it to finish executing and terminates the program
        //when the Main thread is over

        //foreground threads - Main thread WAITS for it to
        //finish executing and DOES NOT terminate the program
        //when the Main thread is over
        static void Main2()
        {
            Thread t1 = new Thread(new ThreadStart(Func1));
            Thread t2 = new Thread(new ThreadStart(Func2));
            t1.Start();
            t2.Start();
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine("Main : " + i);
            }
            t1.Join(); //waiting call - waits for t1 to complete
            Console.WriteLine("THIS LINE SHOULD RUN AFTER FUNC1 IS OVER");
        }

        static void Main3()
        {
            Thread t1 = new Thread(new ThreadStart(Func1));
            Thread t2 = new Thread(new ThreadStart(Func2));
            t1.Priority = ThreadPriority.Highest;
            t2.Priority = ThreadPriority.Lowest;

            t1.Start();
            t2.Start();
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Main : " + i);
            }
        }
        static void Main4()
        {
            Thread t1 = new Thread(new ThreadStart(Func1));
            Thread t2 = new Thread(new ThreadStart(Func2));
            if(t1.ThreadState == ThreadState.Unstarted)
                t1.Start();

            //t1.Abort();
            //t1.Suspend();
           // t1.Resume();

            t2.Start();
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(3000);

                Console.WriteLine("Main : " + i);
            }
        }
        static void Main5()
        {
            AutoResetEvent wh = new AutoResetEvent(false);
            Thread t1 = new Thread(delegate ()
            {
                for (int i = 0; i < 200; i++)
                {
                    Console.WriteLine("f1:" + i);
                    if (i % 50 == 0)
                    {
                        //instead of Suspend, use this
                        Console.WriteLine("waiting");
                        wh.WaitOne();
                    }
                }
            });

            t1.Start();
            //Thread.Sleep(5000);
            Console.ReadLine();
            Console.WriteLine("resuming 1....");
            wh.Set();

            //Thread.Sleep(5000);
            Console.ReadLine();
            Console.WriteLine("resuming 2....");
            wh.Set();

            //Thread.Sleep(5000);
            Console.ReadLine();
            Console.WriteLine("resuming 3....");
            wh.Set();

            //Thread.Sleep(5000);
            Console.ReadLine();
            Console.WriteLine("resuming 4....");
            wh.Set();
        }
        static void Func1()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(3000);
  //              Console.WriteLine("managed threadid" + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("First : " + i);
            }
        }
       
        static void Func2()
        {

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(3000);

                //              Console.WriteLine("managed threadid" + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Second : " + i);
            }
        }
    }
}
namespace ThreadingExamples2
{
    internal class Program
    {
        static void Main()
        {
            //Thread t1 = new Thread(new ThreadStart(Func1));
            Thread t1 = new Thread(new ParameterizedThreadStart(Func1));
            //Thread t1a = new Thread(Func1);
            //t1.Start("passed value");
            t1.Start(12345);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Main : " + i);
            }
        }
       
        static void Func1(object o)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("First : " + i + o);
            }
        }

        static void Func2()
        {

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Second : " + i);
            }
        }
    }
}
/*To DO  - 
Pass multiple values to a function (using thread)

1. array/collection
2. class/struct
3. anon method/lambda /local func - dont pass values, can be read from outer func
4. ValueTuple

Main()
{
int [] arr = new int[2];
arr[0]=  10;
arr[1]=  20;
xyz(arr);
}


void xyz(object o)
{
int[] arr = (int [])o;
....
}

*/

