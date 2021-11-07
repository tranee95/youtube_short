using System.Collections.Generic;
using System.IO;
using System.Text;
using DataContext;
using Newtonsoft.Json;

namespace DomainTests
{
	public class SeedBuilder
	{
		private readonly ApplicationDbContext _context;
		public string PATH_TO_JSON = $"{Path.GetFullPath(@"../../../")}JsonContext/";

		public SeedBuilder(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Формирования обекта context объекта из json файла
		/// </summary>
		/// <typeparam name="TEntity">Тип объекта</typeparam>
		/// <param name="fileName">имя json файла</param>
		public SeedBuilder Seed<TEntity>(string fileName) where TEntity : class
		{
			var filePath = $"{PATH_TO_JSON}{fileName}.json";
			var data = JsonDeserializeObject<TEntity>(ReadFile(filePath));

			_context.Set<TEntity>().AddRange(data);
			_context.SaveChanges();

			return this;
		}

		private string ReadFile(string path)
		{
			var file = File.OpenRead(path);
			using (var sr = new StreamReader(file, Encoding.UTF8))
			{
				return sr.ReadToEnd();
			}
		}

		private List<T> JsonDeserializeObject<T>(string json) => JsonConvert.DeserializeObject<List<T>>(json);
	}
}
