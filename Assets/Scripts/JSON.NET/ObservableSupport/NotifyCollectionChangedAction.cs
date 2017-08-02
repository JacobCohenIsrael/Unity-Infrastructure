#if !UNITY_WINRT || UNITY_EDITOR || UNITY_WP8

namespace Newtonsoft.Json.ObservableSupport
{
	public enum NotifyCollectionChangedAction
	{
		Add,
		Remove,
		Replace,
		Move,
		Reset
	}
}

#endif
