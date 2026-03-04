namespace com.company.spancy.backend.service
{
    public interface IBaseService<Dto>
    {
        object CreateTX(Dto dto);
        IList<Dto> FindAllRO();
        Dto FindByIdRO(long id);
        object RemoveByIdTX(long id);
        object UpdateTX(Dto dto);
    }
}