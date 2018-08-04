using Plukit.Base;
using Staxel.Core;

namespace StaxelPlus
{
    class StormMakerComponentBuilder : IComponentBuilder
    {
        public string Kind()
        {
            return "stormmaker";
        }

        public object Instance(Blob config)
        {
            return new StormMakerComponent(config);
        }

        public class StormMakerComponent
        {
            public Vector2I Distance { get; private set; }
            public Vector3I Offset { get; private set; }

            public StormMakerComponent(Blob config)
            {
                Distance = config.Contains("distance") ? config.GetBlob("distance").GetVector2I() : new Vector2I(2, 2);
                Offset = config.Contains("offset") ? config.GetBlob("offset").GetVector3I() : new Vector3I(0, -1, 0);
            }
        }
    }
}
