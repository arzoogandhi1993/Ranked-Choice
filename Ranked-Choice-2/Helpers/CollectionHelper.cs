using System;
using System.Collections.Generic;

internal static class CollectionHelper
{
	public static bool RetainAll<T>(this ICollection<T> c1, ICollection<T> c2)
	{
		if (c2 is null)
			throw new NullReferenceException();

		bool changed = false;
		T[] arrayCopy = new T[c1.Count];
		c1.CopyTo(arrayCopy, 0);
		foreach (T item in arrayCopy)
		{
			if (!c2.Contains(item))
			{
				c1.Remove(item);
				changed = true;
			}
		}
		return changed;
	}
}