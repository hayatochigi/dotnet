using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Producer_Consumer_dotnet5
{
    class ProsConsMain
    {
        // Forgive me... I use static as global...
        static bool continue_flag = true;

        static void Main()
        {
            ShowThreadingId("EventHandler");

            // In this application, single queue ref will be shared by multiple Processing Loop.
            var mssgQueue = new BlockingCollection<MssgData>();

            // Run each Processing Loop in other thraed.
            Task<string> task_producer = Task.Run(() => { return ProducerLoop(mssgQueue); });
            Task<string> task_consumer = Task.Run(() => { return ConsumerLoop(mssgQueue); });

            Console.ReadKey();
            // Close other Processing Loop.
            continue_flag = false;

            // Wait all loop completed.
            Task.WhenAll(task_producer, task_consumer);
            mssgQueue.Dispose();
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
            ShowThreadingId("Producer");
            var _mssg_dbl = new MssgData();
            var _mssg_str = new MssgData();
            var rand = new Random();
            while(continue_flag == true)
            {
                // Send message with Double data.
                _mssg_dbl.Mssg = "Double";
                _mssg_dbl.Data = rand.NextDouble();
                queue.TryAdd(_mssg_dbl);
            
                // Send message with 
                _mssg_str.Mssg = "String";
                _mssg_str.Data = "Hello, World";
                queue.TryAdd(_mssg_str);
                await Task.Delay(500);
               
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
            ShowThreadingId("Consumer");
            while (continue_flag == true)
            {
                foreach(var _mssg in queue.GetConsumingEnumerable()) 
                {

                    switch (_mssg.Mssg)
                    {
                        case "Double":
                            Console.WriteLine("Message: {0} \t Data: {1:f2}", _mssg.Mssg, Convert.ToDouble(_mssg.Data));
                            break;
                        case "String":
                            Console.WriteLine("Message: {0} \t Data: {1}", _mssg.Mssg, Convert.ToString(_mssg.Data));
                            break;
                        default:
                            // Do not allow not implemented message.
                            throw new NotImplementedException();
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Display current thrad id.
        /// </summary>
        static void ShowThreadingId(string place)
        {
            Console.WriteLine("Thread at {0}: {1}", place, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
