namespace SupermarketApi.Mapping
{
    public interface IBuilder<in TInput, out TOutput>
    {
        TOutput Build(TInput input);
    }
}
