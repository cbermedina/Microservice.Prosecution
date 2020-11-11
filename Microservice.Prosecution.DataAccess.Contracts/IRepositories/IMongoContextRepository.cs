namespace Microservice.Prosecution.DataAccess.Contracts.IRepositories
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMongoContextRepository
    {
        /// <summary>
        /// Obtiene colección IMongoCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>(string strCollectionName);
        /// <summary>
        /// Obtiene lista de una colección
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <returns></returns>
        List<T> GetCollectionList<T>(string strCollectionName);
        /// <summary>
        /// Obtiene lista de una colección 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        /// <returns></returns>
        List<T> GetCollectionList<T>(string strCollectionName, int? intSkip, int? intLimit);
        /// <summary>
        /// Obtiene lista de una colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <returns></returns>
        Task<List<T>> GetCollectionListAsync<T>(string strCollectionName);
        /// <summary>
        /// Obtiene lista de una colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        Task<List<T>> GetCollectionListAsync<T>(string strCollectionName, int? intSkip, int? intLimit);
        /// <summary>
        /// Obtiene colección por ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        List<T> GetCollectionById<T>(string strCollectionName, string strId);
        /// <summary>
        /// Obtiene colección por ID de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        Task<List<T>> GetCollectionByIdAsync<T>(string strCollectionName, string strId);
        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<T> GetCollectionByFilter<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter);
        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<T> GetCollectionByFilter<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        /// <returns></returns>
        List<T> GetCollectionByFilter<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter, int? intSkip, int? intLimit);
        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<T>> GetCollectionByFilterAsync<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter);
        /// <summary>
        /// Obtiene la colección que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <param name="intSkip">Omitir hasta</param>
        /// <param name="intLimit">Cantidad de registros a recuperar</param>
        /// <returns></returns>
        Task<List<T>> GetCollectionByFilterAsync<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter, int? intSkip, int? intLimit);
        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetFirstDocument<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> GetFirstDocumentAsync<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Obtiene el primer documento por Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        T GetFirstDocumentById<T>(string strCollectionName, string strId);
        /// <summary>
        /// Obtiene el primer documento por Id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        Task<T> GetFirstDocumentByIdAsync<T>(string strCollectionName, string strId);
        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetFirstDocumentByFilter<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter);
        /// <summary>
        /// Obtiene el primer documento que cumpla con la condición dada de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> GetFirstDocumentByFilterAsync<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter);
        /// <summary>
        /// Obtiene el último documento que cumpla con el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetLastDocument<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Obtiene el último documento que cumpla con el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> GetLastDocumentAsync<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Obtiene el último documento por Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        T GetLastDocumentById<T>(string strCollectionName, string strId);
        /// <summary>
        /// Obtiene el último documento por Id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        Task<T> GetLastDocumentByIdAsync<T>(string strCollectionName, string strId);
        /// <summary>
        /// Obtiene el último documento que cumpla el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetLastDocumentByFilter<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter);
        /// <summary>
        /// Obtiene el último documento que cumpla el filtro dado de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> GetLastDocumentByFilterAsync<T>(string strCollectionName, System.Linq.Expressions.Expression<Func<T, bool>> filter);
        /// <summary>
        /// Inserta documento a colección
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        T InsertDocument<T>(string strCollectionName, T document);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        T InsertDocumentTransaction<T>(string strCollectionName, T document, IClientSessionHandle session);
        /// <summary>
        /// Inserta documento a colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        Task<T> InsertDocumentAsync<T>(string strCollectionName, T document);
        /// <summary>
        /// Inserta varios documentos en la colección
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="lstDocuments"></param>
        /// <returns></returns>
        List<T> InsertManyDocuments<T>(string strCollectionName, List<T> lstDocuments);
        /// <summary>
        /// Inserta varios documentos en la colección de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="lstDocuments"></param>
        /// <returns></returns>
        Task<List<T>> InsertManyDocumentsAsync<T>(string strCollectionName, List<T> lstDocuments);
        /// <summary>
        /// Reemplaza documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        Task<T> ReplaceDocumentByIdAsync<T>(string strCollectionName, T document, string strId);
        /// <summary>
        /// Reemplaza documento por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        T ReplaceDocumentById<T>(string strCollectionName, T document, string strId);
        /// <summary>
        /// Actualiza documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        Task<UpdateResult> UpdateDocumentByIdAsync<T>(string strCollectionName, string strId, UpdateDefinition<T> definition);
        /// <summary>
        /// Actualiza documento por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        UpdateResult UpdateDocumentById<T>(string strCollectionName, string strId, UpdateDefinition<T> definition);
        /// <summary>
        /// Actualiza documento que cumpla el filtro de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<UpdateResult> UpdateDocumentByFilterAsync<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter);
        /// <summary>
        /// Actualiza documento que cumpla el filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        UpdateResult UpdateDocumentByFilter<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter);
        /// <summary>
        /// Actualiza documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="document"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        Task<ReplaceOneResult> UpdateDocumentByIdAsync<T>(string strCollectionName, T document, string strId);
        /// <summary>
        /// Actualiza todos los documentos que cumplan con el filtro de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<UpdateResult> UpdateManyDocumentsAsync<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter);
        /// <summary>
        /// Actualiza todos los documentos que cumplan con el filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="definition"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        UpdateResult UpdateManyDocuments<T>(string strCollectionName, UpdateDefinition<T> definition, FilterDefinition<T> filter);
        /// <summary>
        /// Elimina documento por id de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        Task<DeleteResult> DeleteDocumentByIdAsync<T>(string strCollectionName, string strId);
        /// <summary>
        /// Elimina documento por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        DeleteResult DeleteDocumentById<T>(string strCollectionName, string strId);
        /// <summary>
        /// Elimina los documentos que cumplan con el filtro de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DeleteResult> DeleteManyDocumentsByFilterAsync<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Elimina los documentos que cumplan con el filtro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        DeleteResult DeleteManyDocumentsByFilter<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Cuenta cuantos documentos cumplen con el filro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        long CountDocuments<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Cuenta cuantos documentos cumplen con el filro dado de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> CountDocumentsAsync<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Verifica si existe un documento que cumpla con el filtro dado de manera asincrónica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> ExistDocumentAsync<T>(string strCollectionName, FilterDefinition<T> filter);
        /// <summary>
        /// Verifica si existe un documento que cumpla con el filtro dado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strCollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool ExistDocument<T>(string strCollectionName, FilterDefinition<T> filter);
    }
}
