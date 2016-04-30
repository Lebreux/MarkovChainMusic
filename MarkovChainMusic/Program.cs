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
            int length = 3;

            List<Note> keys = new List<Note>(Utils.GetNoteRange(Utils.Range(-7, 8), new NoteLengths[] { NoteLengths.Whole, NoteLengths.DoubleWhole }));
            Queue<Note> currentKeys = new Queue<Note>();
            for (int i = 0; i < length; i++)
                currentKeys.Enqueue(new Note(0, NoteLengths.Whole));

            Console.Write("Creating Markov Chain... ");
            var mc = new MarkovChain<Note>(keys, length, rand);
            Console.WriteLine("Done");

            int nbResults = 100;
            Console.Write("Playing chain results ({0})... ", nbResults);

            Midi.OutputDevice outputDevice = Midi.OutputDevice.InstalledDevices[0];
            outputDevice.Open();
            outputDevice.SendProgramChange(Midi.Channel.Channel1, Midi.Instrument.Celesta);

            int totalTime = 0;
            for (int i = 0; i < nbResults; i++)
            {
                Note result = mc.Resolve(currentKeys.ToList(), rand);

                Midi.Pitch currentRoot;
                switch ((int)((float)totalTime / 4000f) % 4)
                {
                    case 0:
                        currentRoot = Midi.Pitch.C4;
                        break;
                    case 1:
                        currentRoot = Midi.Pitch.G3;
                        break;
                    case 2: 
                        currentRoot = Midi.Pitch.C4;
                        break;
                    case 3: 
                        currentRoot = Midi.Pitch.A4;
                        break;
                    default:
                        currentRoot = Midi.Pitch.C4;
                        break;
                }

                outputDevice.SendNoteOn(Midi.Channel.Channel1, currentRoot + Scale.NaturalMinor.GetScaleStepsTo(result.RelValueToTonic), 80);

                currentKeys.Dequeue();
                currentKeys.Enqueue(result);

                int noteLength = (int)(1000f * result.NoteLength.Value);
                totalTime += noteLength;

                System.Threading.Thread.Sleep(noteLength);
            }
            Console.WriteLine("Done");

            Console.ReadLine();
        }
    }
}
