using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConeSharp
{
    interface IStep
    {
        void Initialize(State state);
        string Title { get; }
        string Description { get; }
        string MoreInfo { get;  }
        IStep OnNext();

        void OnButton();
        string ButtonText { get; }
        bool TopMost { get; }
    }

    internal enum PositionId
    {
        WestGoto,
        WestAlign,
        EastGoto,
        EastAlign,
    }
}
