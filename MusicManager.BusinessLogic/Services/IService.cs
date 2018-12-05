using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MusicManager.DataBase.Entities;

namespace MusicManager.BusinessLogic.Services
{
	public interface IService
	{
		//Get all type T entities from DB
		Task<List<T>> GetAll<T>() where T : class;
		//Get all type T entities from DB,that correspond to predicate
		Task<List<T>> GetAllWhere<T>(Expression<Func<T, bool>> predicate) where T : class;
		//Get one entity from DB,that correspond to predicate
		Task<T> GetOneWhere<T>(Expression<Func<T, bool>> predicate) where T : class;
		//Get Album entity with chained data
		Task<Album> GetAlbumWhere(Expression<Func<Album, bool>> predicate);
		//Get Song entity with chained data
		Task<Song> GetSongWhere(Expression<Func<Song, bool>> predicate);
		//Insert type T entity into DB
		Task<int> CreateThis<T>(T entity) where T : class;
		//Insert Song entity into DB (without changes in existing elements)
		Task<int> CreateSong(Song entity);
		//Insert ArtistList entity into DB (without changes in existing elements)
		Task<int> CreateArtistList(ArtistList entity);
		//Update type T entity in DB
		Task<int> UpdateThis<T>(T entity) where T : class;
		//Update Song (including foreign key properties)
		Task<int> UpdateSong(Song entity);
		//Delete type T entity from DB
		Task<int> DeleteThis<T>(T entity) where T : class;
		//Delete Album (cascade deleting of Songs)
		Task<int> DeleteAlbum(Album entity);
		//Delete Song (cascade deleting of ArtistLists)
		Task<int> DeleteSong(Song entity);
		
	}
}
