using CustomerRegistration.Entities;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace CustomerRegistration.Repositories
{
    public class CustomerRepository
    {
        readonly string connection = "mongodb://localhost";
        readonly string databaseName = "registration";
        readonly string collection = "customer";

        private IMongoCollection<Customer> GetCollection()
        {
            try
            {
                var client = new MongoClient(connection);
                var database = client.GetDatabase(databaseName);
                return database.GetCollection<Customer>(collection);
            }
            catch(Exception ex)
            {
                throw new ApplicationException($"Erro ao conectar ao MongoDB. Conexão: {connection}, Base: {databaseName}, Coleção: {collection} /n {ex.Message}");
            }
        }

        public void Insert(Customer entity)
        {
            try
            {
                GetCollection().InsertOne(entity);
            }
            catch(Exception ex) 
            {
                throw new ApplicationException($"Erro ao inserir cliente. Entidade: {entity} /n {ex.Message}");
            }
        }

        public void Update(Customer entity)
        {
            try
            {
                Expression<Func<Customer, bool>> filter =
                    x => x.Id.Equals(entity.Id);

                var update =
                    Builders<Customer>.Update
                    .Set(it => it.Cpf, entity.Cpf)
                    .Set(it => it.Name, entity.Name)
                    .Set(it => it.BirthDate, entity.BirthDate);

                var updateOption = new UpdateOptions { IsUpsert = true };
                GetCollection().UpdateOne(filter, update, updateOption);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao alterar cliente. Entidade: {entity} /n {ex.Message}");
            }
        }

        public Customer Select(string cpf)
        {
            try
            {
                Expression<Func<Customer, bool>> filter =
                    x => x.Cpf.Equals(cpf);

                return GetCollection().Find(filter).First();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao buscar cliente. Cpf: {cpf} /n {ex.Message}");
            }
        }


        public void Delete(string cpf)
        {
            try
            {
                Expression<Func<Customer, bool>> filter =
                    x => x.Cpf.Equals(cpf);

                GetCollection().FindOneAndDelete(filter);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao excluir cliente. Cpf: {cpf} /n {ex.Message}");
            }
        }

    }
}
