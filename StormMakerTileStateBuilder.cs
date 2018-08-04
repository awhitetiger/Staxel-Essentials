using Plukit.Base;
using Staxel.Logic;
using Staxel.Tiles;
using Staxel.TileStates;
using System;

namespace StaxelPlus
{
    class StormMakerTileStateBuilder : ITileStateBuilder, IDisposable
    {
        public void Dispose()
        {

        }

        public void Load()
        {

        }

        public string Kind()
        {
            return "mods.StaxelPlus.tileState.StormMaker";
        }

        public Entity Instance(Vector3I location, Tile tile, Universe universe)
        {
            universe.SetWeather("staxel.weather.Rainy", false);
            return StormMakerTileStateEntityBuilder.Spawn(universe, tile, location);
        }
    }
}
