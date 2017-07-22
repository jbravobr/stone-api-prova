using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Core.Infrastructure;
using MongoDB.Driver;
using Stone.Data.Interfaces;
using Stone.Domain.Entities;

namespace Stone.Data.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {

        IMongoDatabase DbContext { get; }
        string typeName = "Usuario";

        public UsuarioRepository()
        {

            if (DbContext == null)
                DbContext = MongoDBInstance.GetMongoDatabase;
        }

        public async Task Adicionar(Usuario obj)
        {

            try
            {
                await DbContext.GetCollection<Usuario>(typeName).InsertOneAsync(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> ObterPorId(string id)
        {

            try
            {
                var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
                return await DbContext.GetCollection<Usuario>(typeName).Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Atualizar(Usuario obj)
        {
            try
            {
                var filter = Builders<Usuario>.Filter.Eq(x => x.Id, obj.Id);
                var result = await DbContext.GetCollection<Usuario>(typeName)
                    .ReplaceOneAsync(filter, obj, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remover(string id)
        {
            try
            {
                var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
                var result = await DbContext.GetCollection<Usuario>(typeName).FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> predicate)
        {
            try
            {
                var filter = Builders<Usuario>.Filter.Where(predicate);
                var collection = await DbContext.GetCollection<Usuario>(typeName).FindAsync(filter);
                var retList = new List<Usuario>();

                await collection.ForEachAsync((Usuario Entity) =>
                {
                    retList.Add(Entity);
                });

                return await Task.FromResult(retList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Usuario>> BuscarTodos()
        {
            try
            {
                var collection = DbContext.GetCollection<Usuario>(typeName).AsQueryable();
                var retList = new List<Usuario>();

                await collection.ForEachAsync((Usuario Entity) =>
                {
                    retList.Add(Entity);
                });

                return await Task.FromResult(retList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> BuscarPorLogin(string login)
        {
            try
            {
                var filter = Builders<Usuario>.Filter.Where(x=>x.Login == login);
                var user = await DbContext.GetCollection<Usuario>(typeName).FindAsync(filter);


                return await user.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}