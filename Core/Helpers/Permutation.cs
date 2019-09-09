using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public static class Permutation
    {
        public static List<int> Get(int i)
        {
            return GeneratePermutationFromNumberElements(i);
        }

        private static List<int> GeneratePermutationFromNumberElements(int elements)
        {
            var intElements = new int[elements + 1];

            for (var i = 0; i <= elements; i++)
            {
                intElements[i] = i;
            }

            var t2 = new List<string>();

            for (var i = 0; i <= elements + 1; i++)
            {
                t2 = GFG.printCombination(intElements, elements + 1, i + 1);
            }

            var t3 = new List<int>();
            foreach (string s in t2)
            {
                t3.Add(int.Parse(s));
            }




            return t3;
        }

        private static class GFG
        {

            // This code is contributed by m_kit 
            private static readonly List<string> lll = new List<string>();

            private static readonly StringBuilder stringBuilder = new StringBuilder();

            /* arr[] ---> Input Array 
            data[] ---> Temporary array to 
                        store current combination 
            start & end ---> Staring and Ending 
                            indexes in arr[] 
            index ---> Current index in data[] 
            r ---> Size of a combination 
                    to be printed */
            static void CombinationUtil(int[] arr, int[] data, int start, int end, int index, int r)
            {
                // Current combination is 
                // ready to be printed, 
                // print it 
                if (index == r)
                {


                    for (int j = 0; j < r; j++)
                    {

                        stringBuilder.Append(data[j]);


                        //Debug.Write(data[j] + " ");
                    }

                    lll.Add(stringBuilder.ToString());
                    stringBuilder.Clear();

                    //Debug.WriteLine("");
                    return;
                }

                // replace index with all 
                // possible elements. The 
                // condition "end-i+1 >= 
                // r-index" makes sure that 
                // including one element 
                // at index will make a 
                // combination with remaining 
                // elements at remaining positions 
                for (int i = start; i <= end &&
                                    end - i + 1 >= r - index; i++)
                {
                    data[index] = arr[i];
                    CombinationUtil(arr, data, i + 1,
                        end, index + 1, r);
                }
            }

            // The main function that prints 
            // all combinations of size r 
            // in arr[] of size n. This 
            // function mainly uses combinationUtil() 
            public static List<string> printCombination(int[] arr, int n, int r)
            {
                // A temporary array to store 
                // all combination one by one 
                int[] data = new int[r];

                // Print all combination 
                // using temprary array 'data[]' 
                CombinationUtil(arr, data, 0, n - 1, 0, r);

                return lll;
            }

            // Driver Code 
            //static public void Main()
            //{
            //    int[] arr = { 1, 2, 3, 4, 5 };
            //    int r = 3;
            //    int n = arr.Length;
            //    printCombination(arr, n, r);
            //}
        }
    }
}