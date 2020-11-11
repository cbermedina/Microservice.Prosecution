namespace Microservice.Prosecution.DataAccess.Repositories
{
    using Microservice.Prosecution.DataAccess.Configuration;
    using Microservice.Prosecution.DataAccess.Contracts.IRepositories;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    public class MongoContextRepository : IMongoContextRepository
    {
        #region Properties
        IMongoClient _client;
        public readonly IMongoDatabase _bd = null;
        #endregion

        #region Constructor
        public MongoContextRepository(IOptions<Settings> configuracion)
        {
            _client = new MongoClient(configuracion.Value.ConnectionString);
            if (_client != null)
            {
                _bd = _client.GetDatabase(configuracion.Value.Database);
            }
        }
        #endregion

        #region Collection methods
        /// <summary>
        /// Obtiene colección IMongoCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string strCollectionName)
        {
            return _bd.GetCollection<T>(strCollectionName);
        }

        /// <summary>
        /// Obtiene lista de una colección
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <returns></returns>
        public List<T> GetCollectionList<T>(string strCollectionName)
        {
            return _bd.GetCollection<T>(strCollectionName).AsQueryable().ToList();
        }

        /// <summary>
        /// Obtiene lista de una colección 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        public List<T> GetCollectionList<T>(string strCollectionName, int? intSkip, int? intLimit)
        {
            return _bd.GetCollection<T>(strCollectionName)
                .Find(FilterDefinition<T>.Empty)
                .Skip(intSkip)
                .Limit(intLimit)
                .ToList();
        }

        /// <summary>
        /// Obtiene lista de una colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        public async Task<List<T>> GetCollectionListAsync<T>(string strCollectionName)
        {
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(FilterDefinition<T>.Empty);
            return await response.ToListAsync();
        }

        /// <summary>
        /// Obtiene lista de una colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        public async Task<List<T>> GetCollectionListAsync<T>(string strCollectionName, int? intSkip, int? intLimit)
        {
            var response = _bd.GetCollection<T>(strCollectionName)
                .Find(FilterDefinition<T>.Empty)
                .Skip(intSkip)
                .Limit(intLimit);
            return await response.ToListAsync();
        }

        /// <summary>
        /// Obtiene colección por ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public List<T> GetCollectionById<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.ToList();
        }

        /// <summary>
        /// Obtiene colección por ID de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public async Task<List<T>> GetCollectionByIdAsync<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return await response.ToListAsync();
        }

        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> GetCollectionByFilter<T>(string strCollectionName, Expression<Func<T, bool>> filter)
        {
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.ToList();
        }
        public List<T> GetCollectionByFilter<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.ToList();
        }
        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        /// <returns></returns>
        public List<T> GetCollectionByFilter<T>(string strCollectionName, Expression<Func<T, bool>> filter, int? intSkip, int? intLimit)
        {
            var response = _bd.GetCollection<T>(strCollectionName)
                .Find(filter)
                .Skip(intSkip)
                .Limit(intLimit);
            return response.ToList();
        }

        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<T>> GetCollectionByFilterAsync<T>(string strCollectionName, Expression<Func<T, bool>> filter)
        {
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return await response.ToListAsync();
        }

        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        /// <returns></returns>
        public async Task<List<T>> GetCollectionByFilterAsync<T>(string strCollectionName, Expression<Func<T, bool>> filter, int? intSkip, int? intLimit)
        {
            var response = _bd.GetCollection<T>(strCollectionName)
                .Find(filter)
                .Skip(intSkip)
                .Limit(intLimit);
            return await response.ToListAsync();
        }
        #endregion

        #region Documents methods
        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T GetFirstDocument<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> GetFirstDocumentAsync<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return await response.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Obtiene el primer documento por Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public T GetFirstDocumentById<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el primer documento por Id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public async Task<T> GetFirstDocumentByIdAsync<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return await response.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T GetFirstDocumentByFilter<T>(string strCollectionName, Expression<Func<T, bool>> filter)
        {
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> GetFirstDocumentByFilterAsync<T>(string strCollectionName, Expression<Func<T, bool>> filter)
        {
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return await response.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Obtiene el ultimo documento que cumpla con el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T GetLastDocument<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            return _bd.GetCollection<T>(strCollectionName).Find(filter).ToList().LastOrDefault();
        }

        /// <summary>
        /// Obtiene el último documento que cumpla con el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> GetLastDocumentAsync<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return response.ToList().LastOrDefault();
        }

        /// <summary>
        /// Obtiene el último documento por Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public T GetLastDocumentById<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.ToList().LastOrDefault();
        }

        /// <summary>
        /// Obtiene el último documento por Id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public async Task<T> GetLastDocumentByIdAsync<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return response.ToList().LastOrDefault();
        }

        /// <summary>
        /// Obtiene el último documento que cumpla el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T GetLastDocumentByFilter<T>(string strCollectionName, Expression<Func<T, bool>> filter)
        {
            var response = _bd.GetCollection<T>(strCollectionName).Find(filter);
            return response.ToList().LastOrDefault();
        }

        /// <summary>
        /// Obtiene el último documento que cumpla el filtro dado de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> GetLastDocumentByFilterAsync<T>(string strCollectionName, Expression<Func<T, bool>> filter)
        {
            var response = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            return response.ToList().LastOrDefault();
        }
        #endregion

        #region Insert methods
        /// <summary>
        /// Inserta documento a colección
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public T InsertDocument<T>(string strCollectionName, T document)
        {
            _bd.GetCollection<T>(strCollectionName).InsertOne(document);
            return document;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public T InsertDocumentTransaction<T>(string strCollectionName, T document, IClientSessionHandle session)
        {
            _bd.GetCollection<T>(strCollectionName).InsertOne(session, document);
            return document;
        }

        /// <summary>
        /// Inserta documento a colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<T> InsertDocumentAsync<T>(string strCollectionName, T document)
        {
            await _bd.GetCollection<T>(strCollectionName).InsertOneAsync(document);
            return document;
        }

        /// <summary>
        /// Inserta varios documentos en la colección
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="lstDocuments"></param>
        /// <returns></returns>
        public List<T> InsertManyDocuments<T>(string strCollectionName, List<T> lstDocuments)
        {
            _bd.GetCollection<T>(strCollectionName).InsertMany(lstDocuments);
            return lstDocuments;
        }

        /// <summary>
        /// Inserta varios documentos en la colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="lstDocuments"></param>
        /// <returns></returns>
        public async Task<List<T>> InsertManyDocumentsAsync<T>(string strCollectionName, List<T> lstDocuments)
        {
            await _bd.GetCollection<T>(strCollectionName).InsertManyAsync(lstDocuments);
            return lstDocuments;
        }

        #endregion

        #region Update methods
        /// <summary>
        /// Reemplaza documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public async Task<T> ReplaceDocumentByIdAsync<T>(string strCollectionName, T document, string strId)
        {
            FilterDefinition<T> filtro = new BsonDocument("_id", strId);
            //TODO: Validar respuesta de result
            var result = await _bd.GetCollection<T>(strCollectionName).ReplaceOneAsync(filtro, document);
            return document;
        }

        /// <summary>
        /// Reemplaza documento por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public T ReplaceDocumentById<T>(string strCollectionName, T document, string strId)
        {
            FilterDefinition<T> filtro = new BsonDocument("_id", strId);
            //TODO: Validar respuesta de result
            var result = _bd.GetCollection<T>(strCollectionName).ReplaceOne(filtro, document);
            return document;
        }

        /// <summary>
        /// Actualiza documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <param name="definition"></param>
        public async Task<UpdateResult> UpdateDocumentByIdAsync<T>(string strCollectionName, string strId, UpdateDefinition<T> definition)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            //TODO: Validar respuesta de result
            var result = await _bd.GetCollection<T>(strCollectionName).UpdateOneAsync(filter, definition);
            return result;
        }

        /// <summary>
        /// Actualiza documento por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        public UpdateResult UpdateDocumentById<T>(string strCollectionName, string strId, UpdateDefinition<T> definition)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            //TODO: Validar respuesta de result
            var result = _bd.GetCollection<T>(strCollectionName).UpdateOne(filter, definition);
            return result;
        }

        /// <summary>
        /// Actualiza documento que cumpla el filtro de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateDocumentByFilterAsync<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter)
        {
            var result = await _bd.GetCollection<T>(strCollectionName).UpdateOneAsync(filter, definition);
            return result;
        }

        /// <summary>
        /// Actualiza documento que cumpla el filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public UpdateResult UpdateDocumentByFilter<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter)
        {
            var result = _bd.GetCollection<T>(strCollectionName).UpdateOne(filter, definition);
            return result;
        }

        /// <summary>
        /// Actualiza documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public async Task<ReplaceOneResult> UpdateDocumentByIdAsync<T>(string strCollectionName, T document, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var result = await _bd.GetCollection<T>(strCollectionName).ReplaceOneAsync(filter, document);
            return result;
        }

        /// <summary>
        /// Actualiza todos los documentos que cumplan con el filtro de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateManyDocumentsAsync<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter)
        {
            var result = await _bd.GetCollection<T>(strCollectionName).UpdateManyAsync(filter, definition);
            return result;
        }

        /// <summary>
        /// Actualiza todos los documentos que cumplan con el filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public UpdateResult UpdateManyDocuments<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter)
        {
            var result = _bd.GetCollection<T>(strCollectionName).UpdateMany(filter, definition);
            return result;
        }

        #endregion

        #region Delete methods
        /// <summary>
        /// Elimina documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteDocumentByIdAsync<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var result = await _bd.GetCollection<T>(strCollectionName).DeleteOneAsync(filter);
            return result;
        }

        /// <summary>
        /// Elimina documento por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public DeleteResult DeleteDocumentById<T>(string strCollectionName, string strId)
        {
            FilterDefinition<T> filter = new BsonDocument("_id", strId);
            var result = _bd.GetCollection<T>(strCollectionName).DeleteOne(filter);
            return result;
        }

        /// <summary>
        /// Elimina los documentos que cumplan con el filtro de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteManyDocumentsByFilterAsync<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var result = await _bd.GetCollection<T>(strCollectionName).DeleteManyAsync(filter);
            return result;
        }

        /// <summary>
        /// Elimina los documentos que cumplan con el filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DeleteResult DeleteManyDocumentsByFilter<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var result = _bd.GetCollection<T>(strCollectionName).DeleteMany(filter);
            return result;
        }

        #endregion

        #region Helpers
        /// <summary>
        /// Cuenta cuantos documentos cumplen con el filro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long CountDocuments<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var count = _bd.GetCollection<T>(strCollectionName).CountDocuments(filter);
            return count;
        }

        /// <summary>
        /// Cuenta cuantos documentos cumplen con el filro dado de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> CountDocumentsAsync<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var count = await _bd.GetCollection<T>(strCollectionName).CountDocumentsAsync(filter);
            return count;
        }

        /// <summary>
        /// Verifica si existe un documento que cumpla con el filtro dado de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> ExistDocumentAsync<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var exist = true;
            var document = await _bd.GetCollection<T>(strCollectionName).FindAsync(filter);
            if (document == null)
            {
                exist = false;
            }

            return exist;
        }

        /// <summary>
        /// Verifica si existe un documento que cumpla con el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool ExistDocument<T>(string strCollectionName, FilterDefinition<T> filter)
        {
            var exist = true;
            var document = _bd.GetCollection<T>(strCollectionName).Find(filter);
            if (document == null)
            {
                exist = false;
            }
            return exist;
        }
        #endregion
    }
}
