using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChainMusic
{
    class MarkovChainOutput<T>
    {
        private List<T> _outcomes;
        private List<float> _percents;

        public MarkovChainOutput(List<T> outcomes, Random rand)
        {
            this._outcomes = outcomes;
            this._percents = new List<float>();
            foreach (var outcome in this._outcomes)
                this._percents.Add((float)rand.NextDouble());
            float total = this._percents.Sum();
            for (int i = 0; i < this._percents.Count; i++)
                this._percents[i] = this._percents[i] / total;
        }

        public T Resolve(Random rand)
        {
            float fRand = (float)rand.NextDouble();
            int index = 0;
            float cursor = 0f;
            foreach (var percent in this._percents)
            {
                cursor += percent;
                if (fRand > cursor)
                {
                    index++;
                }
                else
                    break;
            }
            return this._outcomes[index];
        }
    }
}
