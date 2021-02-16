using CryptoChain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BlockchainDemo
{
    public class Blockchain
    {
        public IList<Block> Chain { set;  get; }
        

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }


        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return Block.Genesis();
        }

        public void AddGenesisBlock()
        {

            Chain.Add(CreateGenesisBlock());
        }
        
        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            //Block latestBlock = GetLatestBlock();
            //block.Index = latestBlock.Index + 1;
            //block.PreviousHash = latestBlock.Hash;
            //new Thread(() => ).Start();
            //block.Mine(block.Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                //if (currentBlock.Hash != currentBlock.CalculateHash())
                
                    //return false;
               

                //if (currentBlock.PreviousHash != previousBlock.Hash)
                
                    //return false;
                
            }
            return true;
        }
    }
}
