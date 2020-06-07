using System;
using System.Data.SqlClient;
using Preludio.Core.EventHandling;

namespace Preludio.DataAccess.NH.DomainEventPersistence
{
    public interface IDomainEventCommandBuilder
    {
        IDomainEventCommandBuilder WithTableName(string tableName);
        IDomainEventCommandBuilder WithColumn(string columnName, Func<IDomainEvent, object> valueProvider);
        SqlCommand Build(IDomainEvent parameterObject);
    }
}