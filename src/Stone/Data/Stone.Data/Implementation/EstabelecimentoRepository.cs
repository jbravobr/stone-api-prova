using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Core.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using Stone.Data.Interfaces;
using Stone.Domain.Entities;

namespace Stone.Data.Implementation
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        IMongoDatabase DbContext { get; }
        string typeName = "Estabelecimento";

        public EstabelecimentoRepository()
        {
            if (DbContext == null)
                DbContext = MongoDBInstance.GetMongoDatabase;
        }

        public async Task Adicionar(Estabelecimento obj)
        {
            try
            {
                await DbContext.GetCollection<Estabelecimento>(typeName).InsertOneAsync(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Atualizar(Estabelecimento obj)
        {
            try
            {
                var filter = Builders<Estabelecimento>.Filter.Eq(x => x.Id, obj.Id);
                var result = await DbContext.GetCollection<Estabelecimento>(typeName)
                                            .ReplaceOneAsync(filter, obj, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Estabelecimento>> Buscar(Expression<Func<Estabelecimento, bool>> predicate)
        {
            try
            {
                var filter = Builders<Estabelecimento>.Filter.Where(predicate);
                var collection = await DbContext.GetCollection<Estabelecimento>(typeName).FindAsync(filter);
                var retList = new List<Estabelecimento>();

                await collection.ForEachAsync((Estabelecimento Entity) =>
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

        public async Task<IEnumerable<Estabelecimento>> BuscarTodos()
        {
            try
            {
                var collection = DbContext.GetCollection<Estabelecimento>(typeName).AsQueryable();
                var retList = new List<Estabelecimento>();

                await collection.ForEachAsync((Estabelecimento Entity) =>
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

        public async Task<Estabelecimento> ObterPorId(string id)
        {
            try
            {
                var filter = Builders<Estabelecimento>.Filter.Eq(x => x.Id, id);
                return await DbContext.GetCollection<Estabelecimento>(typeName).Find(filter).FirstOrDefaultAsync();
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
                var filter = Builders<Estabelecimento>.Filter.Eq(x => x.Id, id);
                var result = await DbContext.GetCollection<Estabelecimento>(typeName).FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Estabelecimento> ObterMaisRecente()
        {
            try
            {
                var sort = Builders<Estabelecimento>.Sort.Descending(x => x.Id);
                var filter = new BsonDocument();
                var result = DbContext.GetCollection<Estabelecimento>(typeName).Find(filter);

                return await result.Sort(sort).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}