
using CryptoChain.Models;
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
         static void Main(string[] args)
        {


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ThreadPool.SetMaxThreads(0, 100);

            Block g = Block.Genesis();
            Block b1 = g.MineBlock(g, "nothing to say just testing");
            Block b2 = b1.MineBlock(b1, "nothing to say just testing");
            Block b3 = b2.MineBlock(b2, "nothing to say just testing");
            Block b4 = b3.MineBlock(b3, "nothing to say just testing");
            Block b5 = b4.MineBlock(b4, "nothing to say just testing");
            Block b6 = b5.MineBlock(b5, "nothing to say just testing");
            Block b7 = b6.MineBlock(b6, "nothing to say just testing");
            Block b8 = b7.MineBlock(b7, "nothing to say just testing");
            Block b9 = b8.MineBlock(b8, "nothing to say just testing");
            Block b10 = b9.MineBlock(b9, "nothing to say just testing");


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Total RunTime " + elapsedTime);
            

        }
        
    }
}

