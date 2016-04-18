import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

/*
Permutation algorithm by Tushar Roy
https://github.com/mission-peace/interview/blob/master/src/com/interview/recursion/StringPermutation.java
*/

public class PermutableList<T> extends ArrayList<T> {
	
	private static final long serialVersionUID = -8187783488999067907L;

	public List<LinkedList<T>> getPermutations()
	{
		List<LinkedList<T>> permutations = new ArrayList<LinkedList<T>>();		
		Map<T, Integer> countMap = new TreeMap<>();
		for (T object : this) {
            countMap.compute(object, (key, val) -> {
                if (val == null) {
                    return 1;
                } else {
                    return val + 1;
                }
            });
        }	
		List<T> objects = new LinkedList<T>();
		int count[] = new int[countMap.size()];
		int index = 0;
		for (Map.Entry<T, Integer> entry : countMap.entrySet()) {
           	objects.add(index, entry.getKey());
            count[index] = entry.getValue();
            index++;
        }
		HashMap<Integer, T> result = new HashMap<>();		
		permuteUtil(objects, count, result, 0, permutations);		
		return permutations;
	}
	
	public List<LinkedList<T>> getVariations(int size)
	{
		List<LinkedList<T>> variations = new ArrayList<LinkedList<T>>();
		LinkedList<T> prefix = new LinkedList<T>();
		variationsUtil(prefix, 0, size, variations);
		return variations;
	}

	private void variationsUtil(LinkedList<T> prefix, int level, int size, List<LinkedList<T>> resultList)
	{
		if(level == size)
		{
			resultList.add(prefix);
			return;
		}
		for(T object : this)
		{
			LinkedList<T> newPrefix = new LinkedList<T>(prefix);
			newPrefix.add(object);
			variationsUtil(newPrefix, level +1, size, resultList);
		}
	}
	
	private void permuteUtil(List<T> objects, int count[], Map<Integer, T> result, int level, List<LinkedList<T>> resultList)
	{
		if(level == size())
		{
			LinkedList<T> toResult = new LinkedList<T>();
			result.keySet().forEach(key -> toResult.add(result.get(key)));
			resultList.add(toResult);
			return;
		}	
		for(int i = 0; i < objects.size(); i++)
		{
			if(count[i] == 0)
				continue;
			result.put(level, objects.get(i));
			count[i]--;
			permuteUtil(objects, count, result, level +1, resultList);
			count[i]++;
		}
	}
	
	}

