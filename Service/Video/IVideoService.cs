using Common.Models.User;
using Common.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Video
{
	public interface IVideoService
	{
		/// <summary>
		/// Получаем набор видео 
		/// </summary>
		Task<List<VideoListViewModel>> GetVidosAtFilters(List<FilterVideoModel> filters);

		/// <summary>
		/// Поиск набора видео 
		/// </summary>
		Task<List<VideoListViewModel>> SearchVideoAtFilters(string searchStr);
	}
}
