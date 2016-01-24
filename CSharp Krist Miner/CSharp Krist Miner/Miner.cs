using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Krist_Miner
{
    class Miner
    {
        string Prefix = "tkr";
        string addrWBlock = "";
        int cWork = 0;

        public Miner (string Prefix2)
        {
            Prefix = Prefix+Prefix2;
        }

        public void UpdateWB ()
        {
            addrWBlock = Program.Address + Program.CurrentBlock + Prefix;
        }

        public void Submit (string nonceStr)
        {
            Helper.submitBlock(Program.Address, Prefix + nonceStr);
        }

        public void Run ()
        {
            try
            {
                long nonce = 0;
                while(true)
                {
                    int i = 0;
                    for (; i<100000; i++, nonce++)
                    {
                        //Console.WriteLine("Running hash: " + i);
                        if (cWork != Program.CurrentWork)
                        {
                            cWork = Program.CurrentWork;
                            break;
                        }
                        string nonceStr = nonce.ToString();
                        for (int ii=0; i<32 - nonceStr.Length; i++)
                        {
                            nonceStr = "0" + nonceStr;
                        }
                        long hashNum = Helper.getHashAsLong(addrWBlock + nonceStr);
                        Program.Hashes++;
                        if (hashNum < Program.CurrentWork)
                        {
                            Console.WriteLine("Solved block!");
                            Submit(nonceStr);
                            Program.BlocksDone++;
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error in mining thread: " + ex.ToString());
            }
        }
    }
}
