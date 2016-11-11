using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermutableList
{
    public class PermutableList<T> : List<T>
    {
        /// <summary>
        /// Returns the list of all possible permutations of the list (every permutation is also a list).
        /// </summary>
        /// <returns>List of permutations.</returns>
        public List<List<T>> GetPermutations()
        {
            List<List<T>> permutations = new List<List<T>>();

            Dictionary<T, int> countMap = new Dictionary<T, int>();
            foreach (T t in this)
            {
                if(!countMap.ContainsKey(t))
                {
                    countMap.Add(t, 1);
                }

                else
                {
                    int value = countMap[t]++;
                    countMap.Remove(t);
                    countMap.Add(t, value);
                }
            }

            List<T> objects = new List<T>();
            int[] count = new int[countMap.Count];
            int index = 0;
            foreach (var key in countMap.Keys)
            {
                objects.Insert(index, key);
                count[index] = countMap[key];
                index++;
            }
            Dictionary<int, T> result = new Dictionary<int, T>();
            permuteUtil(objects, count, result, 0, permutations);

            return permutations;
        }

        /// <summary>
        /// Returns the list of all variations with repetitions of size n.
        /// </summary>
        /// <param name="size">Cardinality of the variations.</param>
        /// <returns>List of list containing all variations.</returns>
        public List<List<T>> GetVariations(int size)
        {
            List<List<T>> variations = new List<List<T>>();
            List<T> prefix = new List<T>();
            variationsUtil(prefix, 0, size, variations);
            return variations;
        }

        private void permuteUtil(List<T> objects, int[] count, Dictionary<int, T> result, int level, List<List<T>> resultList)
        {
            if(level == Count)
            {
                List<T> toResult = new List<T>();
                foreach (var key in result.Keys)
                {
                    toResult.Add(result[key]);
                }
                resultList.Add(toResult);
                return;
            }

            for (int i = 0; i < objects.Count; i++)
            {
                if (count[i] == 0)
                    continue;

                if (!result.ContainsKey(level))
                {
                    result.Add(level, objects[i]);
                }
                else
                {
                    result.Remove(level);
                    result.Add(level, objects[i]);
                }
                count[i]--;
                permuteUtil(objects, count, result, level + 1, resultList);
                count[i]++;
            }
        }
        
        private void variationsUtil(List<T> prefix, int level, int size, List<List<T>> resultList)
        {
            if(level == size)
            {
                resultList.Add(prefix);
                return; 
            }
            foreach(T t in this)
            {
                List<T> newPrefix = new List<T>(prefix);
                newPrefix.Add(t);
                variationsUtil(newPrefix, level + 1, size, resultList);
            }
        }

    }
}
