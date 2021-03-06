using System;

namespace Preludio.DataAccess.Mongo.Repositories
{
    public abstract class MongoDbBase
	{
		protected readonly IMongoContext _dbContext;

		public MongoDbBase(IMongoContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("MongoDbContext");
			_dbContext = dbContext;
		}
	}
}
