using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Krist_Miner
{
    class Helper
    {

        public static WebClient wc = new WebClient();
        public static string SyncNode = "http://ceriat.net/krist/?";

        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static long getHashAsLong ( string text )
        {
            string HashResult = getHashSha256(text);
            string Partial = HashResult.Substring(HashResult.Count() - 13);
            return Convert.ToInt64(Partial, 16);
        }

        public static string webGet (string url)
        {
            Uri Dest = new Uri(url);
            string Data = System.Text.Encoding.UTF8.GetString(wc.DownloadData(Dest));
            return Data;
        }

        public static int getBalance(string Address)
        {
            return int.Parse(webGet(SyncNode + "getbalance=" + Address));
        }

        public static string getLastBlock ()
        {
            return webGet(SyncNode + "lastblock");
        }

        public static int getWork ()
        {
            return int.Parse(webGet(SyncNode + "getwork"));
        }

        public static void submitBlock (string Address, string Nonce)
        {
            webGet(SyncNode + "submitblock&address=" + Address + "&nonce=" + Nonce);
        }
    }
}
