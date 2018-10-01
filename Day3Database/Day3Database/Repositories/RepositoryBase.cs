using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Day3Database.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        protected string ConnectionString { get; private set; }

        protected string InsertStatement { get; set; }

        protected string DeleteStatement { get; set; }

        protected string RetrieveStatement { get; set; }

        protected string RetrieveAllStatement { get; set; }

        protected string UpdateStatement { get; set; }
        public RepositoryBase()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["OnlineStoreDB"].ConnectionString;
        }

        protected abstract void LoadInsertParameters(SqlCommand command, TEntity newEntity);
        protected abstract void LoadDeleteParameters(SqlCommand command, Guid id);

        protected abstract void LoadUpdateParameters(SqlCommand command, TEntity newEntity);

        protected abstract void LoadRetrieveParameters(SqlCommand command, Guid id);

        protected abstract TEntity LoadEntity(SqlDataReader reader);

        public TEntity Create(TEntity newEntity)
        {
            using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                
                using(SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = InsertStatement;
                    LoadInsertParameters(command,newEntity);

                    command.ExecuteNonQuery();
                    return newEntity;
                }
            }
        }

        public void Delete(Guid id)
        {
                 using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using(SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = DeleteStatement;
                    LoadDeleteParameters(command,id);

                    command.ExecuteNonQuery();
                }
                
            }
        }

        public void Update(TEntity newEntity)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = UpdateStatement;
                    LoadUpdateParameters(command,newEntity);
                    command.ExecuteNonQuery();
                }
            }
        }

        public TEntity Retrieve(Guid id)
        {
            TEntity newEntity = null; 
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = RetrieveStatement;
                    LoadRetrieveParameters(command, id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newEntity = LoadEntity(reader);
                        }
                    }
                }
            }
            return newEntity;
        }

        public List<TEntity> Retrieve()
        {
            var entityList= new List<TEntity>();
            TEntity newEntity;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = RetrieveAllStatement;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newEntity = LoadEntity(reader);
                            entityList.Add(newEntity);
                        }
                    }
                }
            }
            return entityList;
        }

       
    }
}
