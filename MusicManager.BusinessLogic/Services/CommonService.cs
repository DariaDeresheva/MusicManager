using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MusicManager.DataBase.Entities;


namespace MusicManager.BusinessLogic.Services
{
	public class CommonService : IService
	{
		public async Task<List<T>> GetAll<T>() where T : class
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				return await connection.Set<T>().ToListAsync();
			}
		}
		
		public async Task<List<T>> GetAllWhere<T>(Expression<Func<T, bool>> predicate) where T : class
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				return await connection.Set<T>().Where(predicate).ToListAsync();
			}
		}

		public async Task<T> GetOneWhere<T>(Expression<Func<T, bool>> predicate) where T : class
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				connection.Configuration.LazyLoadingEnabled = false;
				return await connection.Set<T>().Where(predicate).FirstAsync();
			}
		}

		public async Task<Album> GetAlbumWhere(Expression<Func<Album, bool>> predicate)
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				connection.Configuration.LazyLoadingEnabled = false;
				return await connection.Set<Album>().Include(album => album.Songs.Select(song => song.Artists.Select(list => list.Artist)))
													.Include(album => album.Songs.Select(song => song.Genre)).Where(predicate).FirstAsync();

			}
		}

		public async Task<Song> GetSongWhere(Expression<Func<Song, bool>> predicate)
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				connection.Configuration.LazyLoadingEnabled = false;
				return await connection.Set<Song>().Include(song => song.Artists.Select(list => list.Artist)).Include(song => song.Album)
												   .Include(song => song.Genre).Where(predicate).FirstAsync();
			}
		}

		public async Task<int> CreateThis<T>(T entity) where T : class
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				connection.Set<T>().Add(entity);
				return await connection.SaveChangesAsync();

			}
		}

		public async Task<int> CreateSong(Song entity)
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				connection.Set<Genre>().Attach(entity.Genre);
				connection.Set<Album>().Attach(entity.Album);
				connection.Set<Song>().Add(entity);
				await connection.SaveChangesAsync();
				return entity.SongId;

			}
		}

		public async Task<int> CreateArtistList(ArtistList entity)
		{
			using (var connection = ConnectionFactory.GetConnection())
			{

				connection.Set<Song>().Attach(entity.Song);
				connection.Set<Artist>().Attach(entity.Artist);
				connection.Set<ArtistList>().Add(entity);
				return await connection.SaveChangesAsync();

			}
		}

		public async Task<int> UpdateThis<T>(T entity) where T : class
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				connection.Entry(entity).State = EntityState.Modified;
				return await connection.SaveChangesAsync();

			}
		}

		public async Task<int> UpdateSong(Song entity)
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				var song = await connection.Set<Song>().SingleAsync(s => s.SongId == entity.SongId);
				var genre = await connection.Set<Genre>().SingleAsync(g => g.GenreId == entity.Genre.GenreId);
				song.Genre = genre;
				song.SongName = entity.SongName;
				song.SongNumber = entity.SongNumber;
				song.SongRating = entity.SongRating;
				return await connection.SaveChangesAsync();

			}
		}

		public async Task<int> DeleteThis<T>(T entity) where T : class
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				connection.Entry(entity).State = EntityState.Deleted;
				return await connection.SaveChangesAsync();

			}
		}

		public async Task<int> DeleteAlbum(Album entity)
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				foreach (var song in entity.Songs.ToArray())
				{
					await DeleteSong(song);
				}

				connection.Entry(entity).State = EntityState.Deleted;
				return await connection.SaveChangesAsync();

			}
		}

		public async Task<int> DeleteSong(Song entity)
		{
			using (var connection = ConnectionFactory.GetConnection())
			{
				foreach (var list in entity.Artists.ToArray())
				{
					await DeleteThis<ArtistList>(list);
				}

				connection.Entry(entity).State = EntityState.Deleted;
				return await connection.SaveChangesAsync();

			}
		}
	}
}
