using System;
using System.Collections.Generic;
using System.Diagnostics;
using Entities;
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
                // cannot use (local)\v11.0 with NHibernate
                // see http://rhnatiuk.wordpress.com/2012/11/08/localdb-and-nhibernate/
                db.ConnectionString = @"Server=np:\\.\pipe\LOCALDB#BAC2AF62\tsql\query;Integrated Security=true;Database=LearnORM";
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

            var baseType = typeof(BaseEntity);
            // BaseEntity is not an entity, but classes that inherit from it are
            mapper.IsRootEntity((type, b) => type.BaseType == baseType);
            mapper.IsEntity((type, _) => baseType.IsAssignableFrom(type) &&
                                         type != baseType);
            // use HiLo id generator
            mapper.BeforeMapClass += (inspector, type, map) => map.Id(
                id => id.Generator(Generators.HighLow,
                    generator => generator.Params(new
                    {
                        table = "NextHighVaues",
                        column = "NextHigh",
                        max_lo = 100,
                        where = string.Format("EntityName = '{0}'", type.Name.ToLowerInvariant())
                    })));

            return mapper.CompileMappingForEach(GetEntityTypes());
        }

        private IEnumerable<Type> GetEntityTypes()
        {
            return new[] {typeof (Book)};
        }
    }
}
