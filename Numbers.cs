using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class Numbers
{

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

	
	/// <summary>
	/// Расширение для типа IEnumerable<long>
	/// Возвращает число, равное произведению всех чисел, входящих в список list
	/// </summary>
	/// <param name="list">список перемножаемых чисел</param>
	/// <returns></returns>
	public static long Multiply(this IEnumerable<long> list)
	{
		long ret = 1;
		foreach (var l in list) ret = ret * l;
		return ret;
	}

	/// <summary>
	/// Расширение для типа IEnumerable<int>
	/// Возвращает число, равное произведению всех чисел, входящих в список list
	/// </summary>
	/// <param name="list">список перемножаемых чисел</param>
	/// <returns></returns>
	public static long Multiply(this IEnumerable<int> list)
	{
		long ret = 1;
		foreach (var l in list) ret = ret * l;
		return ret;
	}

}
