using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChainMusic
{
    class MarkovChain<T>
    {
        private Dictionary<List<T>, MarkovChainOutput<T>> _chain;

        public MarkovChain(List<T> elements, int level, Random rand)
        {
            var permutations = new List<List<T>>();
            for (int i = 0; i < elements.Count; i++)
                permutations.Add(new List<T>() { elements[i] });
            for (int depth = 1; depth < level; depth++)
                permutations = this.Permute(permutations, elements);

            this._chain = new Dictionary<List<T>, MarkovChainOutput<T>>(new ItemComparer<List<T>>());
            foreach (List<T> permutation in permutations)
                this._chain.Add(permutation, new MarkovChainOutput<T>(elements, rand));
        }

        private List<List<T>> Permute(List<List<T>> list, List<T> elements)
        {
            List<List<T>> newList = new List<List<T>>();
            foreach (List<T> item in list)
            {
                foreach (T subitem in elements)
                {
                    var subItemList = new List<T>();
                    subItemList.AddRange(item);
                    subItemList.Add(subitem);
                    newList.Add(subItemList);
                }
            }
            return newList;
        }

        public T Resolve(List<T> input, Random rand)
        {
            return this._chain[input].Resolve(rand);
        }

        private class ItemComparer<K> : IEqualityComparer<K> where K : IList<T>
        {
            public bool Equals(K x, K y)
            {
                if (x.Count != y.Count)
                    return false;

                for (int i = 0; i < x.Count(); i++)
                    if (!x[i].Equals(y[i]))
                        return false;

                return true;
            }

            public int GetHashCode(K obj)
            {
                int hash = 19;
                foreach (var item in obj)
                {
                    hash = hash * 31 + item.GetHashCode();
                }
                return hash;
            }
        }
    }
}
