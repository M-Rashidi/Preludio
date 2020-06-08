namespace Preludio.DataAccess.Mongo
{
    public class MongoConnection : IMongoConnection
    {
        private string _connectionString { get; set; }
        public MongoConnection(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public string MongoStore()
        {
            return _connectionString;
        }
    }

}