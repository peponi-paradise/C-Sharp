using Prism.Events;
using System.Windows.Media.Imaging;

namespace Define.EventAggregator;

public class TextLoadCallEvent : PubSubEvent<string>
{ }

public class TextLoadDoneEvent : PubSubEvent<string>
{ }

public class PictureLoadCallEvent : PubSubEvent<string>
{ }

public class PictureLoadDoneEvent : PubSubEvent<BitmapImage?>
{ }