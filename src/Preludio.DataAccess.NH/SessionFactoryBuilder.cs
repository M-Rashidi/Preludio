using System.Collections.Generic;
using System.Data;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Preludio.DataAccess.NH.DomainEventPersistence;

namespace Preludio.DataAccess.NH
{
    public class SessionFactoryBuilder
    {
        private IDomainEventCommandBuilder _commandBuilder;

        private readonly Dictionary<ListenerType, object> _listeners = new Dictionary<ListenerType, object>();

        private Assembly _mappingAssembly;
        private string _sessionFactoryName;
        public string ConnectionString { get; private set; }

        public SessionFactoryBuilder WithMappingsInAssembly(Assembly assembly)
        {
            _mappingAssembly = assembly;
            return this;
        }

        public SessionFactoryBuilder UsingConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }

        public SessionFactoryBuilder SetSessionNameAs(string name)
        {
            _sessionFactoryName = name;
            return this;
        }

        public SessionFactoryBuilder PersistDomainEvents(IDomainEventCommandBuilder commandBuilder)
        {
            _commandBuilder = commandBuilder;
            return this;
        }

        public SessionFactoryBuilder WithListener(ListenerType listenerType, object listenerInstance)
        {
            //TODO: currently we do not support multiple listeners for a listener type
            _listeners.Add(listenerType, listenerInstance);
            return this;
        }
        public SessionFactoryBuilder WithSessionInterceptor<T>() where T : IInterceptor
        {
            InterceptorContainer.InterceptorType = typeof(T);
            return this;
        }
        public ISessionFactory Build()
        {
            var configuration = new Configuration();
            configuration.SessionFactoryName(_sessionFactoryName);
            configuration.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<YekeSqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = IsolationLevel.ReadCommitted;
                db.ConnectionString = ConnectionString;
                db.Timeout = 30;
            });

            configuration.AddAssembly(_mappingAssembly);
            var modelMapper = new ModelMapper();
            modelMapper.BeforeMapClass += (mi, t, map) => map.DynamicUpdate(true);
            modelMapper.AddMappings(_mappingAssembly.GetExportedTypes());

            AddDomainEventListener(configuration);
            AddCustomListeners(configuration);

            var mappingDocument = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddDeserializedMapping(mappingDocument, _sessionFactoryName);
            SchemaMetadataUpdater.QuoteTableAndColumns(configuration, new MsSql2012Dialect());
            return configuration.BuildSessionFactory();
        }

        private void AddDomainEventListener(Configuration configuration)
        {
            if (_commandBuilder == null) return;
            var listener = new DomainEventPersistListener(_commandBuilder);
            configuration.SetListener(ListenerType.PreInsert, listener);
            configuration.SetListener(ListenerType.PreUpdate, listener);
            configuration.SetListener(ListenerType.PreDelete, listener);

        }

        private void AddCustomListeners(Configuration configuration)
        {
            foreach (var item in this._listeners)
            {
                configuration.SetListener(item.Key, (object)item.Value);
            }
        }
    }
}