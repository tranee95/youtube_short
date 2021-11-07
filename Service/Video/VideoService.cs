using Common.Models.User;
using Common.ViewModels;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Video
{
	public class VideoService : IVideoService
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<VideoService> _logger;

		public VideoService(ApplicationDbContext context, ILogger<VideoService> logger)
		{
			_context = context;
			_logger = logger;
		}

		#region  Получаем набор видео

		/// <summary>
		/// Получаем набор видео 
		/// </summary>
		public async Task<List<VideoListViewModel>> GetVidosAtFilters(List<FilterVideoModel> filters)
		{
			try
			{
				var ellapledTicks = DateTime.Now.Ticks;
				var result = new List<VideoListViewModel>();

				foreach (var filter in filters)
				{
					var tasks = new Func<Task>[]
					{
						async () =>
						{
							if (filter.BlogersId.Any())
							{
								var blogers = await GetBlogers(filter);
								if (blogers.Videos.Any()) result.Add(blogers);
							}
						},
						async () =>
						{
							if (filter.ThemesId.Any())
							{
								var themes = await GetThemes(filter);
								if (themes.Videos.Any()) result.Add(themes);
							}
						}
					};

					await Task.WhenAll(tasks.Select(s => s()).ToArray());
				}

				ellapledTicks = DateTime.Now.Ticks - ellapledTicks;
				_logger.LogWarning("Потрачено тактов на выполнение: " + ellapledTicks);

				return result.OrderByDescending(s => s.UrlAvatar.Length).ToList();
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.StackTrace);
				return null;
			}
		}

		/// <summary>
		/// Получение тем
		/// </summary>
		private async Task<VideoListViewModel> GetThemes(FilterVideoModel filter)
		{
			try
			{
				var themes =
					await _context
						.Themes
						.AsNoTracking()
						.FirstOrDefaultAsync(s => s.Id.Equals(filter.ThemesId.First()));

				var videos =
					_context
						.Videos
						.AsNoTracking()
						.Where(s => s.ThemesId.Any(i => filter.ThemesId.ToArray().Contains(i)))
						.ToList();

				return new VideoListViewModel
				{
					Id = themes?.Id ?? 0,
					ThemesId = filter.ThemesId,
					BlogersId = filter.BlogersId,
					VideoListName = themes?.Name,
					UrlAvatar = string.Empty,
					Videos = videos
				};
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.StackTrace);
				return null;
			}
		}

		/// <summary>
		/// Получение блогеров
		/// </summary>
		private async Task<VideoListViewModel> GetBlogers(FilterVideoModel filter)
		{
			try
			{
				var bloger =
					await _context
						.Blogers
						.AsNoTracking()
						.FirstOrDefaultAsync(s => filter.BlogersId.Contains(s.Id));

				var videos =
					_context
						.Videos
						.AsNoTracking()
						.Where(s => filter.BlogersId.Contains(s.BlogerId))
						.ToList();

				return new VideoListViewModel
				{
					Id = bloger?.Id ?? 0,
					ThemesId = filter.ThemesId,
					BlogersId = filter.BlogersId,
					VideoListName = bloger?.Name,
					UrlAvatar = bloger?.UrlAvatar,
					Videos = videos
				};
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.StackTrace);
				return null;
			}
		}

		#endregion

		#region Поиск списков по имени

		/// <summary>
		/// Поиск списков по имени
		/// </summary>
		public async Task<List<VideoListViewModel>> SearchVideoAtFilters(string searchStr)
		{
			try
			{
				var result = new List<VideoListViewModel>();

				await Task.Run(async () =>
				{
					var themes = await SearchTheme(searchStr);
					if (themes.Videos.Any()) result.Add(themes);
				});

				await Task.Run(async () =>
				{
					var blogers = await SearchBlogers(searchStr);
					if (blogers.Videos.Any()) result.Add(blogers);
				});

				return result;
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.StackTrace);
				return null;
			}
		}

		/// <summary>
		/// Поиск темы по имени
		/// </summary>
		private async Task<VideoListViewModel> SearchTheme(string searchStr)
		{
			try
			{
				var themes =
					_context
						.Themes
						.AsNoTracking()
						.Where(s => s.Name.ToLower().Contains(searchStr.ToLower()))
						.Select(s => s.Id)
						.ToArray();

				var video = await _context
					.Videos
					.AsNoTracking()
					.Where(s => s.ThemesId.Any(i => themes.Contains(i)))
					.ToListAsync();

				return new VideoListViewModel
				{
					ThemesId = themes.ToList(),
					BlogersId = new List<int>(),
					VideoListName = searchStr,
					UrlAvatar = string.Empty,
					Videos = video
				};
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.StackTrace);
				return null;
			}
		}

		/// <summary>
		/// Поиск блогеров по имени
		/// </summary>
		private async Task<VideoListViewModel> SearchBlogers(string searchStr)
		{
			try
			{
				searchStr = searchStr.ToLower();

				var bloger =
					await _context
						.Blogers
						.AsNoTracking()
						.FirstOrDefaultAsync(s => s.Name.ToLower().Contains(searchStr));

				return new VideoListViewModel
				{
					VideoListName = bloger?.Name,
					UrlAvatar = bloger?.UrlAvatar,
					Videos = bloger is null ? new List<Common.Models.Video.Video>() : _context
						.Videos
						.AsNoTracking()
						.Where(s => s.BlogerId.Equals(bloger.Id))
						.ToList()
				};
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.StackTrace);
				return null;
			}
		}

		#endregion
	}
}
