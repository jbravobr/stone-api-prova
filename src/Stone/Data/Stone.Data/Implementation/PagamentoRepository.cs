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
    public class PagamentoRepository : IPagamentoRepository
    {
        IMongoDatabase DbContext { get; }
        string typeName = "Pagamento";

        public PagamentoRepository()
        {
            if (DbContext == null)
                DbContext = MongoDBInstance.GetMongoDatabase;
        }

        public async Task Adicionar(Pagamento obj)
        {
            try
            {
                await DbContext.GetCollection<Pagamento>(typeName).InsertOneAsync(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Atualizar(Pagamento obj)
        {
            try
            {
                var filter = Builders<Pagamento>.Filter.Eq(x => x.Id, obj.Id);
                var result = await DbContext.GetCollection<Pagamento>(typeName)
                                            .ReplaceOneAsync(filter, obj, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Pagamento>> Buscar(Expression<Func<Pagamento, bool>> predicate)
        {
            try
            {
                var filter = Builders<Pagamento>.Filter.Where(predicate);
                var collection = await DbContext.GetCollection<Pagamento>(typeName).FindAsync(filter);
                var retList = new List<Pagamento>();

                await collection.ForEachAsync((Pagamento Entity) =>
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

        public async Task<IEnumerable<Pagamento>> BuscarTodos()
        {
            try
            {
                var collection = DbContext.GetCollection<Pagamento>(typeName).AsQueryable();
                var retList = new List<Pagamento>();

                await collection.ForEachAsync((Pagamento Entity) =>
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

        public async Task<Pagamento> ObterPorId(string id)
        {
            try
            {
                var filter = Builders<Pagamento>.Filter.Eq(x => x.Id, id);
                return await DbContext.GetCollection<Pagamento>(typeName).Find(filter).FirstOrDefaultAsync();
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
                var filter = Builders<Pagamento>.Filter.Eq(x => x.Id, id);
                var result = await DbContext.GetCollection<Pagamento>(typeName).FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagamento> ObterMaisRecente()
        {
            try
            {
                var sort = Builders<Pagamento>.Sort.Descending(x => x.Id);
                var filter = new BsonDocument();
                var result = DbContext.GetCollection<Pagamento>(typeName).Find(filter);

                return await result.Sort(sort).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}