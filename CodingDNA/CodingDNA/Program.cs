using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDNA
{
    class Program
    {
        //States where H means high GC content and L - low GC content
        static char H = 'H';
        static char L = 'L';
        //Observations
        static char A = 'A';
        static char C = 'C';
        static char G = 'G';
        static char T = 'T';

        static void Main(string[] args)
        {
            char[] states = { H, L };
            char[] observations = { A, C, G, T };

            var startProbability = new Dictionary<char, float>();
            startProbability.Add(H, 0.5f);
            startProbability.Add(L, 0.5f);

            //transition prob.
            var transitionProbability = new Dictionary<char, Dictionary<char, float>>();
            var HT = new Dictionary<char, float>();
            HT.Add(H, 0.5f);
            HT.Add(L, 0.5f);
            var LT = new Dictionary<char, float>();
            LT.Add(H, 0.4f);
            LT.Add(L, 0.6f);
            transitionProbability.Add(H, HT);
            transitionProbability.Add(L, LT);

            //emission prob.
            var emissionProbability = new Dictionary<char, Dictionary<char, float>>();
            var HE = new Dictionary<char, float>();
            HE.Add(A, 0.2f);
            HE.Add(C, 0.3f);
            HE.Add(G, 0.3f);
            HE.Add(T, 0.2f);
            var LE = new Dictionary<char, float>();
            LE.Add(A, 0.3f);
            LE.Add(C, 0.2f);
            LE.Add(G, 0.2f);
            LE.Add(T, 0.3f);
            emissionProbability.Add(H, HE);
            emissionProbability.Add(L, LE);
        }

        public static Object[] ForwardViterbi(string obs, char[] states, 
                                              Dictionary<char[], float> start_p, 
                                              Dictionary<char[], Dictionary<char[], float>> trans_p,
                                              Dictionary<char[], Dictionary<char[], float>> emit_p)
        {
            return null;
        }
    }
}
