using System;
using System.Collections.Generic;
using System.Diagnostics;
using Entities;
using Entities.Quizes;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace ORM.NHibernate
{
    public class SessionFactoryBuilder
    {
        public ISessionFactory BuildForMySql()
        {
            return Build(db =>
            {
                db.Dialect<MySQL55Dialect>();
                db.ConnectionString = "Server=localhost;Database=LearnORM;Uid=vgrigoriu;Pwd=12345;";
            });
        }

        public ISessionFactory BuildForMsSql()
        {
            return Build(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.ConnectionString = @"Server=localhost\sqlexpress;Integrated Security=true;Database=LearnORM";
            });
        }

        private ISessionFactory Build(Action<IDbIntegrationConfigurationProperties> dbProperties)
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(dbProperties);

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
                        table = "NextHighValues",
                        column = "NextHigh",
                        max_lo = 99,
                        where = string.Format("EntityName = '{0}'", type.Name.ToLowerInvariant())
                    })));

            // Foreign key columns end in Id
            mapper.BeforeMapManyToOne += (inspector, member, map) => map.Column(member.LocalMember.Name + "Id");

            mapper.BeforeMapBag += (inspector, member, bag) =>
            {
                // FK column name is typeof(parent) + "Id"
                bag.Key(keyMapper => keyMapper.Column(member.GetContainerEntity(inspector).Name + "Id"));
                // cascade all from parent to children
                bag.Cascade(Cascade.All | Cascade.DeleteOrphans);
            };

            return mapper.CompileMappingForEach(GetEntityTypes());
        }

        private IEnumerable<Type> GetEntityTypes()
        {
            return new[]
            {
                typeof (Book),
                typeof (Publisher),
                typeof (Question),
                typeof (Answer)
            };
        }
    }
}
