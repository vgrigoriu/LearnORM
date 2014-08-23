using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace ORM.NHibernate
{
    public class SessionFactoryBuilder
    {
        public ISessionFactory Build()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.ConnectionString = "Server=(localdb)\v11.0;Integrated Security=true;Database=LearnORM";
            });

            foreach (var mapping in GetMappings())
            {
                Trace.WriteLine(mapping.AsString());
                configuration.AddMapping(mapping);
            }

            return configuration.BuildSessionFactory();
        }

        private IEnumerable<HbmMapping> GetMappings()
        {
            var mapper = new ConventionModelMapper();
            return mapper.CompileMappingForEach(GetEntityTypes());
        }

        private IEnumerable<Type> GetEntityTypes()
        {
            return Enumerable.Empty<Type>();
        }
    }
}
