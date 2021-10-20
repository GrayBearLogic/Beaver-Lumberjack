using System.Linq;
using Random = UnityEngine.Random;

public class NumbersLoop
{
    private readonly int[] array;
    private int index;
    
    public NumbersLoop(int from, int length)
    {
        array = Enumerable.Range(from, length).OrderBy(x => Random.value).ToArray();
    }

    public int Next
    {
        get
        {
            if (++index >= array.Length) index = 0;
            return array[index];
        }
    }
}
