namespace EPlayer.Controls
{
	//Unicode values for icons in Segoe MDL2 Assets font
	//Usage sample: char.ConvertFromUtf32((int)SegoeIcon.Smile)
	//The reason for not using a Dictionary is to make it usable as a property of WPF control
	public enum SegoeIcon
	{
		Next = 0xE101,
		Previous = 0xE100,
		Play = 0xE102,
		Pause = 0xE103,
		Search = 0xE094,
		LikeFilled = 0xE00B,
		LikeNotFilled = 0xE006,
		Smile = 0xE170,
		Maximize = 0xE739,
		Minimize = 0xE949,
		Close = 0xE106,
		Restore = 0xE923,
		ChevronLeft = 0xE00E,
		ChevronRight = 0xE00F,
		ChevronUp = 0xE010,
		ChevronDown = 0xE011,
		Home = 0xE10F,
		Settings = 0xE115,
		Video = 0xE116,
		Sync = 0xE117,
		Person = 0xE13D,
		Shuffle = 0xE14B,
		Album = 0xE93C
	}
}
