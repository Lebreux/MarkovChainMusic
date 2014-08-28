using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChainMusic
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            List<int> keys = new List<int>(Utils.Range(-7, 8));
            Queue<int> currentKeys = new Queue<int>();
            for (int i = 0; i < length; i++)
                currentKeys.Enqueue(0);

            Console.Write("Creating Markov Chain... ");
            var mc = new MarkovChain<int>(keys, length, rand);
            Console.WriteLine("Done");

            int nbResults = 100;
            Console.Write("Playing chain results... ", nbResults);

            Midi.OutputDevice outputDevice = Midi.OutputDevice.InstalledDevices[0];
            outputDevice.Open();
            
            for (int i = 0; i < nbResults; i++)
            {
                int result = mc.Resolve(currentKeys.ToList(), rand);

                outputDevice.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.C4 + Scale.Whole.GetScaleStepsTo(result), 80);

                currentKeys.Dequeue();
                currentKeys.Enqueue(result);

                System.Threading.Thread.Sleep(250);
            }
            Console.WriteLine("Done");

            Console.ReadLine();
        }
    }
}
