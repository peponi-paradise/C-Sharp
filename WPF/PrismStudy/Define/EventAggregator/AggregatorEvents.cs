using Prism.Events;

namespace Define.EventAggregator;

public class TextLoadCallEvent : PubSubEvent<string>
{ }

public class TextLoadDoneEvent : PubSubEvent<string>
{ }