using FluentNHibernate.Mapping;

namespace NHIBERBNATE_CRUD.Models
{
    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(c => c.CarId);
            Map(c => c.Name);
            Map(c => c.Number);
            Table("Cars");
        }
    }
}
