namespace ExpressBase.Studio.Controls
{
    [ProtoBuf.ProtoContract]
    [ProtoBuf.ProtoInclude(1, typeof(EbButton))]
    [ProtoBuf.ProtoInclude(2, typeof(EbTabControl))]
    [ProtoBuf.ProtoInclude(3, typeof(EbTabPage))]
    public interface IEbControl
    {
        string Name { get; set; }
    }
}