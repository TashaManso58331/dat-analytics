using Dat.Model.Offer;

namespace Dat.Engines
{
    public interface ISplitter
    {
        Root Split(Root sourceData);
        string Name { get; }
    }
}
