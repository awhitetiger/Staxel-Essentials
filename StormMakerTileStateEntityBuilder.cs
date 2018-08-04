using Plukit.Base;
using Staxel.Logic;
using Staxel.Tiles;

namespace StaxelPlus
{
    class StormMakerTileStateEntityBuilder
    {
        public string Kind
        {
            get
            {
                return StormMakerTileStateEntityBuilder.KindCode;
            }
        }

        public static string KindCode
        {
            get
            {
                return "mods.StaxelPlus.tileStateEntity.StormMaker";
            }
        }

        public void Load()
        {

        }

        public static Entity Spawn(EntityUniverseFacade facade, Tile tile, Vector3I location)
        {
            Entity entity = new Entity(facade.AllocateNewEntityId(), false, StormMakerTileStateEntityBuilder.KindCode, true);
            Blob blob = BlobAllocator.Blob(true);
            blob.SetString(nameof(tile), tile.Configuration.Code);
            blob.FetchBlob(nameof(location)).SetVector3I(location);
            blob.SetLong("variant", (long)tile.Variant());
            blob.FetchBlob("position").SetVector3D(location.ToTileCenterVector3D());
            blob.FetchBlob("velocity").SetVector3D(Vector3D.Zero);
            entity.Construct(blob, facade);
            Blob.Deallocate(ref blob);
            facade.AddEntity(entity);
            return entity;
        }

        public bool IsTileStateEntityKind()
        {
            return true;
        }
    }
}
