

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainDemo
{

    class Program
    {
        static string ActualHash = "";
        static string msg = "PakistanZindabad";
        static int ActualNonce = 0;
        static string hash = "abcdefg";
        static readonly object _object = new object();
        static ManualResetEvent mre = new ManualResetEvent(false);
        static int Nonce = 0;
        static int difficulty = 3;
        static string leadingZeros = new string('0', difficulty);
        public static string CalculateHash(String msg)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(msg);
            byte[] outputBytes = sha256.ComputeHash(inputBytes);
            if(hash == Convert.ToBase64String(outputBytes))
                Console.WriteLine("duplicatefdjlsafklasdfklasdf");
            
            return Convert.ToBase64String(outputBytes);
        }

        public  void Mine()
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            hash = CalculateHash(msg);
            while (hash == null || hash.Substring(0, difficulty) != leadingZeros)
            {
                //Interlocked.Increment(ref Nonce);
                //new Thread(() => CalculateHash(msg + Nonce)).Start();
                //this.Hash = 
                Nonce++;
                CalculateHash(msg + Nonce);
                Console.WriteLine(hash);
            }
            Console.WriteLine("dfafaf              " + Nonce + "        " + hash);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            

        }
        static void Main(string[] args)
        {


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //static ManualResetEvent mre = new ManualResetEvent(false);
            //static int Nonce = 0;
            
            hash = CalculateHash(msg);

            Program p = new Program();
            //p.Mine();

            Func<bool> whileCondFn = () => (hash == null || hash.Substring(0, difficulty) != leadingZeros);
            ParallelUtils.While(new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 2 }, (ParallelLoopState) =>
            {

                lock (_object)
                {
                    
                    if (hash.Substring(0, difficulty) != leadingZeros)
                    {
                        hash = CalculateHash(msg + Nonce);
                        Nonce++;
                    }

                }
                //Console.WriteLine(whileCondFn());
                if (hash.Substring(0, difficulty) == leadingZeros)
                {

                    Console.WriteLine("fdasfjkla;jsdfads" + hash);
                    Console.WriteLine(Nonce);
                    ActualHash = hash;
                    ActualNonce = Nonce;
                    ParallelLoopState.Stop();
                }



            });


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine("dfafaf              "+ActualNonce +"        "+ ActualHash);
            
        }
        public class ParallelUtils
        {
            public static void While(ParallelOptions parallelOptions, Action<ParallelLoopState> body)
            {

                Parallel.ForEach(Infinite(),parallelOptions, (ignored, loopState) => {
                    if (!loopState.IsStopped)
                    {
                        if (!(hash.Substring(0, difficulty) == leadingZeros))
                        {
                            Console.WriteLine(hash);
                            body(loopState);
                            //new Thread(()=> ).Start();
                        }
                        else
                        {
                           
                            Console.WriteLine("khatam");
                            Console.WriteLine("h====  adsflasdf    " + hash);
                            loopState.Stop();


                        }
                    }
                });
                //Console.WriteLine("h====      " + hash);
            }

            private static IEnumerable<bool> IterateUntilFalse(Func<bool> condition)
            {
                while (condition()) yield return true;
            }
            private static IEnumerable<bool> Infinite()
            {
                while (true) yield return true;
            }
        }

    }
}
