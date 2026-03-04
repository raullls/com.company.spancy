namespace com.company.spancy.backend.mapper
{
    public interface IBaseMapper<Dto, Entity>
    {
        Dto Map(Entity entity);
        Entity Map(Dto dto);
        IList<Dto> Map(IList<Entity> entities);
        IList<Entity> Map(IList<Dto> dtos);
    }
}