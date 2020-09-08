using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHIBERBNATE_CRUD.Models
{
    public class CarMap : ClassMapping<Car>
    {
        public CarMap()
        {
            Id(c => c.CarId, c =>
            {
                c.Generator(Generators.Native);
            });
            Property(c => c.Name);
            Property(c => c.Number);
            Table("Cars");
        }
    }
}
