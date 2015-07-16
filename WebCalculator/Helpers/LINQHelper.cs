using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Helpers
{
	public static class LINQHelper
	{
		public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}

			if (pageSize <= 0) {
				throw new ArgumentException("Page size must be > 0");
			}

			using (var enumerator = source.GetEnumerator()) {
				while (enumerator.MoveNext()) {
					var currentPage = new List<T>(pageSize) {
						enumerator.Current
					};

					while (currentPage.Count < pageSize && enumerator.MoveNext()) {
						currentPage.Add(enumerator.Current);
					}
					yield return new ReadOnlyCollection<T>(currentPage);
				}
			}
		}
	}
}
