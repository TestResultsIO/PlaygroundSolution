
using System;
using Progile.TRIO.BaseModel;

namespace Playground_Model.Screens.DesktopApp
{
	public partial class RecentSign_InActivityTable
	{
		protected override void Initialize()
		{
			base.Initialize();
			// Define table rectangle as search area for column headers and rows:
			// By default we determine the rectangle with a color fill starting from the top left position of the table loaded image.
			// If required, adjust the tolerance value, so that the color fill will return the whole table.
			// In some cases, color fill is not suitable to determine the table rect, in this case define it manually with e.g. ResultRectangle.FromLTRB
			TableRect = t.SelectFromColorAtPoint(Position.Boundary.TopLeft, tolerance: 0.2).Bounds;
		}
	}
}
