using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DiffCodeLibrary.Mathematics
{
	/// <summary>
	/// Статический класс для работы с числами
	/// </summary>
	public static class Numbers
	{

		/// <summary>
		/// Верхняя граница списка простых чисел, вычисляемых заново при каждом вызове методов класса
		/// </summary>
		private const int _upperRange = 1000000;

		/// <summary>
		/// Список из первых простых чисел от 2 до _upperRange
		/// </summary>
		public static List<int> FirstPrimes = _upperRange.PrimesLessThan();





		/// <summary>
		/// Расширение для типа int.
		/// Возвращает список всех простых чисел, меньших заданного целого числа
		/// </summary>
		/// <param name="number">Целое число, для которого выполняется построение</param>
		/// <returns></returns>
		public static List<int> PrimesLessThan(this int number)
		{
			number = Math.Abs(number);
			var templist = new List<bool> { false, false };
			var ret = new List<int>();

			for (var i = 2; i <= number; i++)
				templist.Add(i % 2 != 0);

			templist[2] = true;

			for (var i = 2; i * i <= number; i++)
				if (templist[i])
					for (var j = i * i; j <= number; j += i)
						templist[j] = false;

			for (var i = 2; i <= number; i++)
				if (templist[i])
					ret.Add(i);

			return ret;
		}

		/// <summary>
		/// Расширение для типа long.
		/// Возвращает список всех простых чисел, меньших заданного целого числа
		/// </summary>
		/// <param name="number">Целое число, для которого выполняется построение</param>
		/// <returns></returns>
		public static List<long> PrimesLessThan(this long number)
		{
			number = Math.Abs(number);
			var templist = new List<bool> { false, false };
			var ret = new List<long>();

			for (var i = 2; i <= number; i++)
				templist.Add(i % 2 != 0);

			templist[2] = true;

			for (var i = 2; i * i <= number; i++)
				if (templist[i])
					for (var j = i * i; j <= number; j += i)
						templist[j] = false;

			for (var i = 2; i <= number; i++)
				if (templist[i])
					ret.Add(i);

			return ret;
		}










		/// <summary>
		/// Расширение для типа long.
		/// Возвращает список всех простых делителей для указанного числа number.
		/// </summary>
		/// <param name="number">Факторизуемое число</param>
		/// <returns></returns>
		public static List<long> ToDivisors(this long number)
		{
			/// берем абсолютное значение числа number (модуль)
			number = Math.Abs(number);

			/// вычисляем его квадратный корень, округляем в меньшую сторону
			long minsqr = (long)(Math.Floor(Math.Sqrt(number)));

			/// формируем список всех простых чисел, меньших чем minsqr - поиск возможных делителей будет производиться выборкой из этого списка
			var ret = minsqr.PrimesLessThan().Where(d => number % d == 0).ToList();

			var chk = number / ret.Multiply();

			while (chk > 1)
			{
				ret.AddRange(chk.PrimesLessThan().Where(d => chk % d == 0));
				chk = number / ret.Multiply();
			};

			return ret.OrderBy(s => s).ToList();
		}

		/// <summary>
		/// Расширение для типа int.
		/// Возвращает список всех простых делителей для указанного числа number.
		/// </summary>
		/// <param name="number">Факторизуемое число</param>
		/// <returns></returns>
		public static List<int> ToDivisors(this int number)
		{
			/// берем абсолютное значение числа number (модуль)
			number = Math.Abs(number);

			/// вычисляем его квадратный корень, округляем в меньшую сторону
			var minsqr = (int)(Math.Floor(Math.Sqrt(number)));

			/// формируем список всех простых чисел, меньших чем minsqr - поиск возможных делителей будет производиться выборкой из этого списка
			var ret = minsqr.PrimesLessThan().Where(d => number % d == 0).ToList();

			var chk = number / ret.Multiply();

			while (chk > 1)
			{
				ret.AddRange(chk.PrimesLessThan().Where(d => chk % d == 0).Cast<int>());
				chk = number / ret.Multiply();
			};

			return ret.OrderBy(s => s).ToList();
		}







		/// <summary>
		/// Расширение для типа int
		/// Возвращает словарь с ключами от 2 до number, и со значениями, являющимися списками простых делителей этих ключей
		/// </summary>
		/// <param name="number">Целое число, верхняя граница для ключей возвращаемого словаря</param>
		/// <returns></returns>
		public static Dictionary<int, List<int>> ToDivsDictionary(this int number)
		{
			// берем абсолютное значение числа number (модуль)
			number = Math.Abs(number);
			var list = new List<int>(Enumerable.Range(2, number - 1));
			var ret = new Dictionary<int, List<int>>();
			for (var i = 0; i < number - 1; i++) ret.Add(list[i], list[i].ToDivisors());
			return ret;
		}

		/// <summary>
		/// Расширение для типа int
		/// Возвращает список объектов NumProperties для каждого числа в интервале от 2 до number
		/// </summary>
		/// <param name="number">Целое число, верхняя граница интервала</param>
		/// <returns></returns>
		public static List<NumProperties> ToNumPropsList(this int number)
		{
			number = Math.Abs(number);

			var list = new List<int>(Enumerable.Range(2, number - 1));
			var ret = new List<NumProperties>();

			for (var i = 0; i < number - 1; i++)
				ret.Add(list[i].ToNumProperties());

			return ret;
		}

		public static List<NumProperties> ToNumPropsList(this IEnumerable<int> list)
		{
			var ret = new List<NumProperties>();
			foreach (var i in list)
				ret.Add((Math.Abs(i)).ToNumProperties());
			return ret;
		}









		/// <summary>
		/// Расширение для типа int
		/// Возвращает объект NumProperties для указанного целого числа
		/// </summary>
		/// <param name="number">Целое число</param>
		/// <returns></returns>
		public static NumProperties ToNumProperties(this int number)
		{
			number = Math.Abs(number);
			var ret = new NumProperties()
			{
				Id = number.ToString(),
				Divisors = ((long)number).ToDivisors()
			};
			return ret;
		}

		/// <summary>
		/// Расширение для типа long
		/// Возвращает объект NumProperties для указанного целого числа
		/// </summary>
		/// <param name="number">Целое число</param>
		/// <returns></returns>
		public static NumProperties ToNumProperties(this long number)
		{
			number = Math.Abs(number);
			var ret = new NumProperties()
			{
				Id = number.ToString(),
				Divisors = number.ToDivisors()
			};
			return ret;
		}











		/// <summary>
		/// Расширение для типа int
		/// Проверка числа на простоту
		/// </summary>
		/// <param name="number">Проверяемое число типа int</param>
		/// <returns></returns>
		public static bool IsPrime(this int number)
		{
			return (number.ToDivisors().Count == 1);
		}

		/// <summary>
		/// Расширение для типа long
		/// Проверка числа на простоту
		/// </summary>
		/// <param name="number">Проверяемое число типа long</param>
		/// <returns></returns>
		public static bool IsPrime(this long number)
		{
			return (number.ToDivisors().Count == 1);
		}


	}
}
