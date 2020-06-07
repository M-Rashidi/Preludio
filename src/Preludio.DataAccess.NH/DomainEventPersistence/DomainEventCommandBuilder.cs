using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Preludio.Core.EventHandling;

namespace Preludio.DataAccess.NH.DomainEventPersistence
{
    public class DomainEventCommandBuilder : IDomainEventCommandBuilder
    {
        private readonly Dictionary<string, Func<IDomainEvent, object>> _columns;
        private string _tableName;

        public DomainEventCommandBuilder()
        {
            _columns = new Dictionary<string, Func<IDomainEvent, object>>();
        }

        public IDomainEventCommandBuilder WithTableName(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IDomainEventCommandBuilder WithColumn(string columnName, Func<IDomainEvent, object> valueProvider) 
        {
            _columns.Add(columnName, valueProvider);
            return this;
        }

        public SqlCommand Build(IDomainEvent parameterObject)
        {
            var commandText = GetCommandText();
            var command = new SqlCommand(commandText);
            AddParametersToCommand(command, parameterObject);
            return command;
        }

        private string GetCommandText()
        {
            var columnNames = _columns.Select(a => a.Key).ToList();
            var joinedColumnNames = string.Join(", ", columnNames);
            var valuePlaceHolders = string.Join(", ", columnNames.Select(ToParameterName));
            var commandText = $"INSERT INTO [{_tableName}]({joinedColumnNames}) VALUES({valuePlaceHolders})";
            return commandText;
        }

        private void AddParametersToCommand(SqlCommand command, IDomainEvent parameterObject)
        {
            foreach (var column in _columns)
            {
                var key = ToParameterName(column.Key);
                var value = column.Value.Invoke(parameterObject);
                if (value != null)
                    command.Parameters.AddWithValue(key, value);
                else
                    command.Parameters.AddWithValue(key, DBNull.Value);
            }
        }

        private string ToParameterName(string parameterKey)
        {
            return $"@{parameterKey.ToLower()}";
        }
      
    }
}