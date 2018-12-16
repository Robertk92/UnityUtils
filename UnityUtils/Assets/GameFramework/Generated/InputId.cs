using System.Collections.Generic;
namespace GameFramework
{
	public enum InputId
	{
		Horizontal,
		Vertical,
		Fire,
		Fire2,
		Fire3,
		Jump,
		Mouse_X,
		Mouse_Y,
		Mouse_ScrollWheel,
		Fire1,
		Submit,
		Cancel,
	}

	public static class GeneratedInput
	{
		private static readonly Dictionary<InputId, string> _ids = new Dictionary<InputId, string>
		{
			 {InputId.Horizontal,"Horizontal"},
			 {InputId.Vertical,"Vertical"},
			 {InputId.Fire,"Fire"},
			 {InputId.Fire2,"Fire2"},
			 {InputId.Fire3,"Fire3"},
			 {InputId.Jump,"Jump"},
			 {InputId.Mouse_X,"Mouse X"},
			 {InputId.Mouse_Y,"Mouse Y"},
			 {InputId.Mouse_ScrollWheel,"Mouse ScrollWheel"},
			 {InputId.Fire1,"Fire1"},
			 {InputId.Submit,"Submit"},
			 {InputId.Cancel,"Cancel"},
		};
		public static Dictionary<InputId, string> Ids => _ids;
	}
}
