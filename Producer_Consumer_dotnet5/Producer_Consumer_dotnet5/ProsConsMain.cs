using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Producer_Consumer_dotnet5
{
    class ProsConsMain
    {
        static void Main()
        {
            // In this application, single queue ref will be shared by multiple Processing Loop.
            var mssgQueue = new BlockingCollection<MssgData>();

            // Run each Processing Loop in other thraed.
            Task<string> task_producer = Task.Run(() => { return ProducerLoop(mssgQueue); });
            Task<string> task_consumer = Task.Run(() => { return ConsumerLoop(mssgQueue); });

            Console.ReadKey();
            mssgQueue.Dispose();

            // Wait all loop completed.
            Task.WhenAll(task_producer, task_consumer);

            Console.WriteLine("Enter a Key to exit...");
            Console.ReadKey();
            
        }

        /// <summary>
        /// Generate "Message & Data" and add them into Blocking Queue.
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        static async Task<string> ProducerLoop(BlockingCollection<MssgData> queue)
        {
            ShowThreadingId();
            var _mssg = new MssgData();
            var rand = new Random();
            while(queue != null)
            {
                _mssg.Mssg = "From Producr";
                _mssg.Data = rand.NextDouble();
                queue.TryAdd(_mssg);
                await Task.Delay(2000);
               
            }
            return "";
        }

        /// <summary>
        /// Consume enqueued data.
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        static string ConsumerLoop(BlockingCollection<MssgData> queue)
        {
            ShowThreadingId();
            while (queue != null)
            {
                foreach(var _mssg in queue.GetConsumingEnumerable()) 
                {
                    Console.WriteLine("Message: {0} \t Data: {1:f2}", _mssg.Mssg, Convert.ToDouble(_mssg.Data));
                }
            }
            return "";
        }

        /// <summary>
        /// Display current thrad id.
        /// </summary>
        static void ShowThreadingId()
        {
            Console.WriteLine("Thread : {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
