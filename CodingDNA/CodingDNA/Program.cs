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
            string observations = "GGCA";

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

            var result = ForwardViterbi(observations.ToCharArray(), states, startProbability, transitionProbability, emissionProbability);
            Console.WriteLine((float)result[0]);
            Console.WriteLine(observations);
            Console.WriteLine((String)result[1]);
            Console.WriteLine((float)result[2]);

        }

        public static Object[] ForwardViterbi(char[] obs, char[] states, 
                                              Dictionary<char, float> start_p, 
                                              Dictionary<char, Dictionary<char, float>> trans_p,
                                              Dictionary<char, Dictionary<char, float>> emit_p)
        {
            var statesData = new Dictionary<char, Object[]>();
            foreach (var state in states)
            {
                statesData.Add(state, new Object[] { start_p[state], state.ToString(), start_p[state] });
            }

            foreach (var acid in obs)
            {
                var tempData = new Dictionary<char, Object[]>();
                foreach (var nextState in states)
                {
                    float total = 0;
                    String argmax = "";
                    float valmax = 0;

                    float prob = 1;
                    String vPath = "";
                    float vProb = 1;

                    foreach (var sourceState in states)
                    {
                        Object[] objs = statesData[sourceState];
                        prob = ((float)objs[0]);
                        vPath = (String)objs[1];
                        vProb = ((float)objs[2]);

                        float p = emit_p[sourceState][acid] * trans_p[sourceState][nextState];
                        prob *= p;
                        vProb *= p;
                        total += prob;

                        if (vProb > valmax)
                        {
                            valmax = vProb;
                            argmax = vPath + nextState;
                        }
                    }

                    tempData.Add(nextState, new Object[] { total, argmax, valmax });
                }
                statesData = tempData;
            }

            float xtotal = 0;
            String xargmax = "";
            float xvalmax = 0;

            float xprob;
            String xvPath;
            float xvProb;
            foreach (var state in states)
            {
                Object[] objs = statesData[state];
                xprob = ((float)objs[0]);
                xvPath = ((String)objs[1]);
                xvProb = ((float)objs[2]);

                xtotal += xprob;
                if (xvProb > xvalmax)
                {
                    xargmax = xvPath;
                    xvalmax = xvProb;
                }
            }
            return new Object[]{ xtotal, xargmax.Substring(0, xargmax.Length - 1), xvalmax };
        }
    }
}
