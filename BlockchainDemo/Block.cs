
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BlockchainDemo.Program;

namespace CryptoChain.Models
{
    public class Block
    {
        public string LastHash { get; set; }
        public  string currentBlockHash  {get; set; }
    //public List<Transaction> Data { get; set; }
    public long Nonce { get; set; }
        public int Difficulty { get; set; }
        public DateTime Timestamp { get; private set; }
        public string Hash { get; set; }

        static ReaderWriterLock _object = new ReaderWriterLock();

         string ActualHash = "";
         long ActualNonce = 0L;




        public Block(DateTime timestamp, string lastHash, string hash, long nonce, int difficulty)
        {
            this.Timestamp = timestamp;
            this.LastHash = lastHash;
            this.Hash = hash;
            //this.Data = data;
            this.Nonce = nonce;
            this.Difficulty = difficulty;

        }

        public static Block Genesis() => new Block(timestamp: Constants.GENESIS_TIMESTAMP, lastHash: Constants.GENESIS_PREV_HASH, nonce: Constants.GENESIS_NONCE, difficulty: Constants.GENESIS_DIFFICULTY, hash: Utility.Helper.Sha256(Constants.GENESIS_TIMESTAMP.ToString(), Constants.GENESIS_PREV_HASH, Constants.GENESIS_NONCE.ToString(), Constants.GENESIS_DIFFICULTY.ToString()));


        public  Block MineBlock(Block lastBlock, String data)
        {
            
            DateTime timestamp=  DateTime.UtcNow;
            long nonce = 0L;
            
            int difficulty = lastBlock.Difficulty;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ThreadPool.SetMaxThreads(0, 100);
             currentBlockHash = Utility.Helper.Sha256(timestamp.ToString(), lastBlock.Hash, nonce.ToString(), difficulty.ToString()); ;
            

            Func<bool> whileCondFn = () => !currentBlockHash.StartsWith(string.Concat(Enumerable.Repeat("0", difficulty)));
            ParallelUtils.While(whileCondFn,new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (ParallelLoopState) =>
            {

                lock (_object)
                {

                    nonce++;
                    timestamp = DateTime.UtcNow;
                    difficulty = Block.AdjustDifficulty(lastBlock, timestamp);

                    currentBlockHash = Utility.Helper.Sha256(timestamp.ToString(), lastBlock.Hash, nonce.ToString(), difficulty.ToString());

                    //Console.WriteLine(whileCondFn());
                    if (currentBlockHash.StartsWith(string.Concat(Enumerable.Repeat("0", difficulty))))
                    {

                        //Console.WriteLine("fdasfjkla;jsdfads" + hash);
                        //Console.WriteLine(Nonce);
                        ActualHash = currentBlockHash;
                        ActualNonce = nonce;
                        ParallelLoopState.Stop();
                    }
                }


            });


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine("Hash of block " + ActualNonce +"          " + ActualHash + "          " + difficulty);
            
           

            // var difficultString = string.Concat(Enumerable.Repeat("0", difficulty));

            

            return new Block(timestamp: timestamp, lastHash: lastBlock.Hash, nonce: ActualNonce, difficulty: difficulty, hash: ActualHash);
        }


        public static int AdjustDifficulty(Block originalBlock, DateTime timestamp)
        {
            if (originalBlock.Difficulty <= 1)
                return 1;

            if ((timestamp - originalBlock.Timestamp).TotalMilliseconds > Constants.MINING_RATE_IN_MILLISEC) return originalBlock.Difficulty - 1;

            return originalBlock.Difficulty + 1;
        }
        public class ParallelUtils
        {
            public static void While(Func<bool> whileCondFn ,ParallelOptions parallelOptions, Action<ParallelLoopState> body)
            {

                Parallel.ForEach(Infinite(), parallelOptions, (ignored, loopState) => {
                    if (!loopState.IsStopped)
                    {
                        //Console.WriteLine(Task.CurrentId + "  " + currentBlockHash);
                        if (whileCondFn())
                        {

                            //Console.WriteLine(hash);
                            body(loopState);
                            //new Thread(()=> ).Start();
                        }
                        else
                        {

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
