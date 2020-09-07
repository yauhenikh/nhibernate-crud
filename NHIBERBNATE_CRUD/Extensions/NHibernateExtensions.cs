using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Tool.hbm2ddl;

namespace NHIBERBNATE_CRUD.Extensions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var sessionFactory = Fluently.Configure()
                 .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Startup>())
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                 .BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}
